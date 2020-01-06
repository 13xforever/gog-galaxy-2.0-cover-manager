using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using ImageProcessor.Imaging;
using ImageProcessor.Plugins.Cair;
using ImageProcessor.Plugins.Cair.Imaging;
using Size = System.Drawing.Size;

namespace GogGalaxy20MetaManager
{
	public partial class CoverEditForm : Form
	{
		private MainForm.OnPaintDelegate OnPaint;

		private readonly MainForm parent;
		private readonly PictureBox pictureBox;
		private readonly string releaseKey;
		private readonly TypedEventHandler<UISettings, object> onColorChanged;
		private readonly ulong userId;

		public CoverEditForm(MainForm parent, PictureBox pictureBox, string releaseKey, ulong userId)
		{
			this.parent = parent;
			this.pictureBox = pictureBox;
			this.releaseKey = releaseKey;
			this.userId = userId;

			InitializeComponent();

			OnPaint = UpdateColors;
			onColorChanged = (_uiSettings, _obj) => Invoke(OnPaint);
			if (parent.uiSettings != null)
				parent.uiSettings.ColorValuesChanged += onColorChanged;
		}

		private void UpdateColors()
		{
			if (parent.uiSettings != null)
			{
				var foreground = parent.uiSettings.GetColorValue(UIColorType.Foreground);
				var background = parent.uiSettings.GetColorValue(UIColorType.Background);
				BackColor = Color.FromArgb(background.A, background.R, background.G, background.B);
				ForeColor = Color.FromArgb(foreground.A, foreground.R, foreground.G, foreground.B);

				var frontWave = Controls.Cast<Control>().ToList();
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

		private void galaxyCoverPicture_Click(object sender, System.EventArgs e) => galaxyCoverSelect.Select();
		private void steamCoverPicture_Click(object sender, System.EventArgs e) => steamCoverSelect.Select();

		private void galaxyBackgroundPicture_Click(object sender, System.EventArgs e) => galaxyBackgroundSelect.Select();
		private void steamBackgroundPicture_Click(object sender, System.EventArgs e) => steamBackgroundSelect.Select();

		private void galaxyIconPicture_Click(object sender, System.EventArgs e) => galaxyIconSelect.Select();
		private void steamIconPicture_Click(object sender, System.EventArgs e) => steamIconSelect.Select();

		private void CoverEditForm_StyleChanged(object sender, System.EventArgs e) => UpdateColors();
		private void CoverEditForm_SystemColorsChanged(object sender, System.EventArgs e) => UpdateColors();
		private void CoverEditForm_Load(object sender, System.EventArgs e) => UpdateColors();

		private void CoverEditForm_Shown(object sender, System.EventArgs e)
		{
			UseWaitCursor = true;

			var keyParts = releaseKey.Split(MainForm.Separator);
			var galaxyCacheRoot = Path.Combine(parent.galaxyRootPath, keyParts[0], keyParts[1]);
			string bannerFilename;
			string iconFilename;
			using (var db = new GalaxyDb())
			{
				bannerFilename = db.WebCacheResources
					.Where(r => r.ReleaseKey == releaseKey
								&& r.UserId == userId
								&& r.WebCacheResourceTypeId == WebCacheResourceType.Background
					)
					.Select(r => r.Filename)
					.FirstOrDefault();
				iconFilename = db.WebCacheResources
					.Where(r => r.ReleaseKey == releaseKey
								&& r.UserId == userId
								&& r.WebCacheResourceTypeId == WebCacheResourceType.SquareIcon
					)
					.Select(r => r.Filename)
					.FirstOrDefault();
			}
			var imgFactory = new ImageProcessor.ImageFactory();

			// cover 342x482, ~7:5
			galaxyCoverPicture.Image = pictureBox.Image;
			var coverPath = Path.Combine(parent.steamRootPath, $"{keyParts[1]}_library_600x900.jpg");
			if (File.Exists(coverPath))
				using (var img = imgFactory.Load(coverPath).Resize(new ResizeLayer(new Size(342, 482), ResizeMode.Min))) // 600x900
					steamCoverPicture.Image = (Image)img.Image.Clone();

			// banner 1600x650
			if (!string.IsNullOrEmpty(bannerFilename))
			{
				bannerFilename = Path.Combine(galaxyCacheRoot, bannerFilename);
				if (File.Exists(bannerFilename))
					using (var img = imgFactory.Load(bannerFilename))
						galaxyBackgroundPicture.Image = (Image)img.Image.Clone();
			}
			var steamBannerPath = Path.Combine(parent.steamRootPath, $"{keyParts[1]}_library_hero.jpg");
			if (File.Exists(steamBannerPath))
				using (var img = imgFactory.Load(steamBannerPath).Resize(new ResizeLayer(new Size(1600, 650), ResizeMode.Crop))) // 1920x620
					steamBackgroundPicture.Image = (Image)img.Image.Clone();

			// icon 112x112
			if (!string.IsNullOrEmpty(iconFilename))
			{
				iconFilename = Path.Combine(galaxyCacheRoot, iconFilename);
				if (File.Exists(iconFilename))
					using (var img = imgFactory.Load(iconFilename))
						galaxyIconPicture.Image = (Image)img.Image.Clone();
			}
			var steamIconPath = Path.Combine(parent.steamRootPath, $"{keyParts[1]}_library_600x900.jpg");
			if (File.Exists(steamIconPath))
				using (var img = imgFactory.Load(steamIconPath).EntropyCrop().Resize(new ResizeLayer(new Size(112, 112), ResizeMode.Crop))) // 1920x620
					steamIconPicture.Image = (Image)img.Image.Clone();

			UseWaitCursor = false;
		}

		private void CoverEditForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (parent.uiSettings != null)
				parent.uiSettings.ColorValuesChanged -= onColorChanged;
		}
	}
}
