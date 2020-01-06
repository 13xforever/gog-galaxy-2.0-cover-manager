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
using Newtonsoft.Json;

namespace GogGalaxy20MetaManager
{
	public partial class MainForm : Form
	{
		private bool aborted = false;

		public MainForm()
		{
			InitializeComponent();
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			UseWaitCursor = true;
			//flowLayoutPanel1.Enabled = false;
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
				foreach (var meta in db.GamePieces.AsNoTracking().Where(p => p.UserId == userId && p.GamePieceTypeId == GamePieceType.Meta).ToList())
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

					var isVisible = db.GamePieces.Where(p => p.ReleaseKey == meta.ReleaseKey && p.GamePieceTypeId == GamePieceType.IsVisibleInLibrary).Select(p => p.Value).FirstOrDefault();
					if (string.IsNullOrEmpty(isVisible)
						|| JsonConvert.DeserializeObject<GamePiecesIsVisibleInLibrary>(isVisible).IsVisibleInLibrary != true)
						continue;

					var isDlc = db.GamePieces.Where(p => p.ReleaseKey == meta.ReleaseKey && p.GamePieceTypeId == GamePieceType.IsDlc).Select(p => p.Value).FirstOrDefault();
					if (!string.IsNullOrEmpty(isDlc)
						&& JsonConvert.DeserializeObject<GamePiecesIsDlc>(isDlc).IsDlc == true)
						continue;

					metaInfo[meta.ReleaseKey] = JsonConvert.DeserializeObject<GamePiecesMeta>(meta.Value);

					// title
					var title = db.GamePieces.FirstOrDefault(p => p.ReleaseKey == meta.ReleaseKey && p.GamePieceTypeId == GamePieceType.Title && p.Value != null)?.Value
									?? db.GamePieces.FirstOrDefault(p => p.ReleaseKey == meta.ReleaseKey && p.GamePieceTypeId == GamePieceType.OriginalTitle && p.Value != null)?.Value;
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
				foreach (var cover in db.WebCacheResources.AsNoTracking().Where(r => r.WebCacheResourceTypeId == WebCacheResourceType.VerticalCover))
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

			var imgFactory = new ImageProcessor.ImageFactory();
			var rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			rootFolder = Path.Combine(rootFolder, "GOG.com", "Galaxy", "webcache", userId.ToString());
			var keySeparator = new[] { '_' };
			progressBar1.Style = ProgressBarStyle.Continuous;
			progressBar1.Maximum = titles.Count;
			foreach (var title in titles.OrderBy(kvp => kvp.Value))
			{
				if (aborted)
					break;

				if (timer.Elapsed > timerThreshold)
				{
					timer.Restart();
					Application.DoEvents();
				}

				var pictureBox = new PictureBox
				{
					SizeMode = PictureBoxSizeMode.Zoom,
					Size = new Size(171, 241), // 342x482, ~5:7
				};
				if (coverFilenames.TryGetValue(title.Key, out var filename))
				{
					var keyParts = title.Key.Split(keySeparator, 2);
					var path = Path.Combine(rootFolder, keyParts[0], keyParts[1], filename);
					if (File.Exists(path))
					{
						var img = imgFactory.Load(path);
						pictureBox.Image = img.Image;
					}
				}
				if (pictureBox.Image == null)
				{
					var text = new TextLayer
					{
						DropShadow = true,
						FontColor = ForeColor,
						FontSize = Font.Height*3,
						Text = title.Value,
					};
					var bitmap = new Bitmap(342, 482);
					var img = imgFactory.Load(bitmap)
						.BackgroundColor(BackColor)
						.Watermark(text);
					pictureBox.Image = img.Image;
				}
				flowLayoutPanel1.Controls.Add(pictureBox);

				progressBar1.Value++;
			}
			progressBar1.Visible = false;
			flowLayoutPanel1.Enabled = true;
			UseWaitCursor = false;
		}

		private void Form1_SystemColorsChanged(object sender, EventArgs e)
		{
			if (RuntimeEnvironment.OperatingSystemPlatform == Platform.Windows
				&& Version.TryParse(RuntimeEnvironment.OperatingSystemVersion, out var systemVersion)
				&& systemVersion.Major == 10)
			{
				var settings = new UISettings();
				var foreground = settings.GetColorValue(UIColorType.Foreground);
				var background = settings.GetColorValue(UIColorType.Background);
				BackColor = Color.FromArgb(background.A, background.R, background.G, background.B);
				ForeColor = Color.FromArgb(foreground.A, foreground.R, foreground.G, foreground.B);
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Form1_SystemColorsChanged(sender, EventArgs.Empty);
			Form1_Resize(sender, EventArgs.Empty);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			aborted = true;
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			progressBar1.Left = flowLayoutPanel1.Left;
			progressBar1.Width = flowLayoutPanel1.ClientRectangle.Width;
			progressBar1.Top = (ClientRectangle.Height - progressBar1.Height) / 2;
		}

		private void flowLayoutPanel1_Resize(object sender, EventArgs e)
		{
			Form1_Resize(sender, e);
		}
	}
}
