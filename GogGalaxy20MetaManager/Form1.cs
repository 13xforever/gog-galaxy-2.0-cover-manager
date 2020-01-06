using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Windows.UI.ViewManagement;
using Microsoft.DotNet.PlatformAbstractions;
using Newtonsoft.Json;

namespace GogGalaxy20MetaManager
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Shown(object sender, System.EventArgs e)
		{
			var metaInfo = new Dictionary<string, GamePiecesMeta>();
			var jsonSettings = new JsonSerializerSettings {};
			var userId = 0ul;
			using (var db = new GalaxyDb())
			{
				if (!db.Users.Any())
					throw new InvalidOperationException("No users");

				if (db.Users.Count() > 1)
					throw new InvalidOperationException("Multi-user setups are not supported");

				userId = db.Users.First().Id;
				foreach (var meta in db.GamePieces.Where(p => p.UserId == userId && p.GamePieceTypeId == GamePieceType.Meta))
					metaInfo[meta.ReleaseKey] = JsonConvert.DeserializeObject<GamePiecesMeta>(meta.Value);
			}

			var imgFactory = new ImageProcessor.ImageFactory();
			var rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			rootFolder = Path.Combine(rootFolder, "GOG.com", "Galaxy", "webcache", userId.ToString());
			var keySeparator = new[] {'_'};
			using (var db = new GalaxyDb())
			{
				foreach (var cover in db.WebCacheResources.Where(r => r.WebCacheResourceTypeId == WebCacheResourceType.VerticalCover))
				{
					if (!metaInfo.ContainsKey(cover.ReleaseKey))
						continue;

					var keyParts = cover.ReleaseKey.Split(keySeparator, 2);
					var path = Path.Combine(rootFolder, keyParts[0], keyParts[1], cover.Filename);
					if (File.Exists(path))
					{
						var img = imgFactory.Load(path);
						var pictureBox = new PictureBox
						{
							SizeMode = PictureBoxSizeMode.Zoom,
							Size = new Size(171, 241), // 342x482, ~5:7
							Image = img.Image,
						};
						flowLayoutPanel1.Controls.Add(pictureBox);
					}
					Application.DoEvents();
				}
			}
		}

		private void Form1_SystemColorsChanged(object sender, EventArgs e)
		{
			if (RuntimeEnvironment.OperatingSystemPlatform == Platform.Windows)
			{
				if (Version.TryParse(RuntimeEnvironment.OperatingSystemVersion, out var systemVersion)
					&& systemVersion.Major == 10)
				{
					var settings = new UISettings();
					var foreground = settings.GetColorValue(UIColorType.Foreground);
					var background = settings.GetColorValue(UIColorType.Background);
					BackColor = Color.FromArgb(background.A, background.R, background.G, background.B);
					ForeColor = Color.FromArgb(foreground.A, foreground.R, foreground.G, foreground.B);
				}
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Form1_SystemColorsChanged(sender, EventArgs.Empty);
		}
	}
}
