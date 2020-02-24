using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Windows.UI.ViewManagement;
using ImageProcessor.Imaging;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace GogGalaxy20MetaManager
{
	public partial class MainForm : Form
	{
		private bool aborted;
		internal readonly UISettings uiSettings;
		internal delegate void OnPaintDelegate();
		private OnPaintDelegate OnPaint;
		internal readonly string steamRootPath;
		internal string galaxyRootPath;
		internal static readonly char[] Separator = { '_' };

		public MainForm()
		{
			InitializeComponent();

			OnPaint = () => UpdateColors(this);
			if (RuntimeEnvironment.OperatingSystemPlatform == Platform.Windows
				&& Version.TryParse(RuntimeEnvironment.OperatingSystemVersion, out var systemVersion)
				&& systemVersion.Major == 10)
			{
				uiSettings = new UISettings();
				uiSettings.ColorValuesChanged += (_uiSettings, _obj) => Invoke(OnPaint);
			}
			if (RuntimeEnvironment.OperatingSystemPlatform == Platform.Windows)
			{
				var steamInstallPath = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam", "InstallPath", null)
										?? Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam", "InstallPath", null);
				if (steamInstallPath != null)
				{
					var steamLibraryCachePath = Path.Combine(steamInstallPath as string, "appcache", "librarycache");
					if (Directory.Exists(steamLibraryCachePath))
						steamRootPath = steamLibraryCachePath;
				}
			}
			if (string.IsNullOrEmpty(steamRootPath))
				throw new InvalidOperationException("Couldn't find Steam install");
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			UseWaitCursor = true;
			progressBar1.Visible = true;
			var metaInfo = new Dictionary<string, GamePiecesMeta>();
			var titles = new Dictionary<string, string>();
			var userId = 0ul;
			var timer = Stopwatch.StartNew();
			var timerThreshold = TimeSpan.FromMilliseconds(16);
			using (var db = new GalaxyDb())
			{
				if (!db.Users.Any())
					throw new InvalidOperationException("No users");

				if (db.Users.Count() > 1)
					throw new InvalidOperationException("Multi-user setups are not supported");

				userId = db.Users.First().Id;
				foreach (var meta in db.GamePieces.AsNoTracking().Where(p => p.UserId == userId && p.GamePieceTypeId == GamePieceType.Meta).ToListAsync().GetAwaiter().GetResult())
				{
					if (aborted)
						break;

					if (timer.Elapsed > timerThreshold)
					{
						timer.Restart();
						Application.DoEvents();
					}

					if (!meta.ReleaseKey.StartsWith("steam_"))
						continue;

					var isVisible = db.GamePieces.Where(p => p.ReleaseKey == meta.ReleaseKey && p.GamePieceTypeId == GamePieceType.IsVisibleInLibrary).Select(p => p.Value).FirstOrDefaultAsync().GetAwaiter().GetResult();
					if (!string.IsNullOrEmpty(isVisible)
						&& JsonConvert.DeserializeObject<GamePiecesIsVisibleInLibrary>(isVisible).IsVisibleInLibrary == false)
						continue;

					var isDlc = db.GamePieces.Where(p => p.ReleaseKey == meta.ReleaseKey && p.GamePieceTypeId == GamePieceType.IsDlc).Select(p => p.Value).FirstOrDefaultAsync().GetAwaiter().GetResult();
					if (!string.IsNullOrEmpty(isDlc)
						&& JsonConvert.DeserializeObject<GamePiecesIsDlc>(isDlc).IsDlc == true)
						continue;

					metaInfo[meta.ReleaseKey] = JsonConvert.DeserializeObject<GamePiecesMeta>(meta.Value);

					// title
					var title = db.GamePieces.FirstOrDefaultAsync(p => p.ReleaseKey == meta.ReleaseKey && p.GamePieceTypeId == GamePieceType.Title && p.Value != null).GetAwaiter().GetResult()?.Value
									?? db.GamePieces.FirstOrDefaultAsync(p => p.ReleaseKey == meta.ReleaseKey && p.GamePieceTypeId == GamePieceType.OriginalTitle && p.Value != null).GetAwaiter().GetResult()?.Value;
					if (!string.IsNullOrEmpty(title))
						title = JsonConvert.DeserializeObject<GamePiecesTitle>(title).Title;
					if (string.IsNullOrEmpty(title))
						title = meta.ReleaseKey;
					titles[meta.ReleaseKey] = title;
				}
			}

			var coverFilenames = new Dictionary<string, string>(metaInfo.Count);
			using (var db = new GalaxyDb())
			{
				foreach (var cover in db.WebCacheResources.AsNoTracking().Where(r => r.WebCacheResourceTypeId == WebCacheResourceType.VerticalCover).ToListAsync().GetAwaiter().GetResult())
				{
					if (aborted)
						break;

					if (timer.Elapsed > timerThreshold)
					{
						timer.Restart();
						Application.DoEvents();
					}

					if (!metaInfo.ContainsKey(cover.ReleaseKey))
						continue;

					coverFilenames[cover.ReleaseKey] = cover.Filename;
				}
			}
			Application.DoEvents();

			var pictureBoxList = new List<Control>(titles.Count);
			using (var imgFactory = new ImageProcessor.ImageFactory())
			{
				var rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
				galaxyRootPath = Path.Combine(rootFolder, "GOG.com", "Galaxy", "webcache", userId.ToString());
				progressBar1.Style = ProgressBarStyle.Continuous;
				progressBar1.Maximum = titles.Count*2;
				foreach (var title in titles.OrderBy(kvp => kvp.Value))
				{
					if (aborted)
						break;

					if (timer.Elapsed > timerThreshold)
					{
						timer.Restart();
						Application.DoEvents();
					}

					var keyParts = title.Key.Split(Separator, 2);
					var pictureBox = new PictureBox
					{
						SizeMode = PictureBoxSizeMode.Zoom,
						Size = new Size(171, 241), // 342x482, ~5:7
					};
					pictureBox.Click += (s, args) =>
										{
											var editForm = new CoverEditForm(this, pictureBox, title.Key, userId)
											{
												Text = $"Editing \"{title.Value}\" ({keyParts[1]})",
											};
											var result = editForm.ShowDialog(this);
										};
					if (coverFilenames.TryGetValue(title.Key, out var filename))
					{
						var path = Path.Combine(galaxyRootPath, keyParts[0], keyParts[1], filename);
						if (File.Exists(path))
							try
							{
								using (var img = imgFactory.Load(path))
									pictureBox.Image = (Image)img.Image.Clone();
							}
							catch
							{
							}
					}
					if (pictureBox.Image == null)
					{
						var text = new TextLayer
						{
							DropShadow = true,
							FontColor = ForeColor,
							FontSize = Font.Height * 3,
							Text = title.Value,
						};
						var bitmap = new Bitmap(342, 482);
						using (var img = imgFactory.Load(bitmap)
							.BackgroundColor(BackColor)
							.Watermark(text))
							pictureBox.Image = (Image)img.Image.Clone();
					}
					pictureBoxList.Add(pictureBox);
					toolTip1.SetToolTip(pictureBox, title.Value);
					progressBar1.Value++;
				}
			}
			var range = pictureBoxList.ToArray().AsSpan();
			while (!range.IsEmpty)
			{
				Application.DoEvents();
				var len = Math.Min(range.Length, 100);
				flowLayoutPanel1.Controls.AddRange(range.Slice(0, len).ToArray());
				range = range.Slice(len);
				progressBar1.Value += len;
			}
			progressBar1.Visible = false;
			UseWaitCursor = false;
		}

		internal void UpdateColors(Form form)
		{
			if (uiSettings != null)
			{
				var foreground = uiSettings.GetColorValue(UIColorType.Foreground);
				var background = uiSettings.GetColorValue(UIColorType.Background);
				form.BackColor = Color.FromArgb(background.A, background.R, background.G, background.B);
				form.ForeColor = Color.FromArgb(foreground.A, foreground.R, foreground.G, foreground.B);

				var frontWave = form.Controls.Cast<Control>().ToList();
				var nextWave = new List<Control>();
				while (frontWave.Count > 0)
				{
					nextWave.Clear();
					foreach (var c in frontWave)
					{
						c.BackColor = BackColor;
						c.ForeColor = ForeColor;
						nextWave.AddRange(c.Controls.Cast<Control>());
					}
					var tmp = frontWave;
					frontWave = nextWave;
					nextWave = tmp;
				}
			}
		}

		private void Form1_SystemColorsChanged(object sender, EventArgs e) => UpdateColors(this);
		private void MainForm_StyleChanged(object sender, EventArgs e) => UpdateColors(this);

		private void Form1_Load(object sender, EventArgs e)
		{
			UpdateColors(this);
			Form1_Resize(sender, EventArgs.Empty);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e) => aborted = true;

		private void Form1_Resize(object sender, EventArgs e)
		{
			progressBar1.Left = flowLayoutPanel1.Left;
			progressBar1.Width = flowLayoutPanel1.ClientRectangle.Width;
			progressBar1.Top = (ClientRectangle.Height - progressBar1.Height) / 2;
			//Text = $"{Width}x{Height}";
		}

		private void flowLayoutPanel1_Resize(object sender, EventArgs e) => Form1_Resize(sender, e);
	}
}
