using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using ImageProcessor.Imaging;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
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

			OnPaint = () => parent.UpdateColors(this);
			onColorChanged = (_uiSettings, _obj) => Invoke(OnPaint);
			if (parent.uiSettings != null)
				parent.uiSettings.ColorValuesChanged += onColorChanged;
		}

		private void galaxyCoverPicture_Click(object sender, System.EventArgs e) => galaxyCoverSelect.Select();
		private void steamCoverPicture_Click(object sender, System.EventArgs e) => steamCoverSelect.Select();

		private void galaxyBackgroundPicture_Click(object sender, System.EventArgs e) => galaxyBackgroundSelect.Select();
		private void steamBackgroundPicture_Click(object sender, System.EventArgs e) => steamBackgroundSelect.Select();

		private void galaxyIconPicture_Click(object sender, System.EventArgs e) => galaxyIconSelect.Select();
		private void steamIconPicture_Click(object sender, System.EventArgs e) => steamIconSelect.Select();

		private void CoverEditForm_StyleChanged(object sender, System.EventArgs e) => parent.UpdateColors(this);
		private void CoverEditForm_SystemColorsChanged(object sender, System.EventArgs e) => parent.UpdateColors(this);
		private void CoverEditForm_Load(object sender, System.EventArgs e) => parent.UpdateColors(this);

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
			using (var imgFactory = new ImageProcessor.ImageFactory())
			{
				// cover 342x482, ~7:5
				galaxyCoverPicture.Image = pictureBox.Image;
				var coverPath = Path.Combine(parent.steamRootPath, $"{keyParts[1]}_library_600x900.jpg");
				if (File.Exists(coverPath))
					using (var img = imgFactory.Load(coverPath).Resize(new ResizeLayer(new Size(342, 482), ResizeMode.Crop))) // 600x900
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
			}
			UseWaitCursor = false;
		}

		private void CoverEditForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (parent.uiSettings != null)
				parent.uiSettings.ColorValuesChanged -= onColorChanged;
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			var keyParts = releaseKey.Split(MainForm.Separator);
			var galaxyCacheRoot = Path.Combine(parent.galaxyRootPath, keyParts[0], keyParts[1]);
			if (!Directory.Exists(galaxyCacheRoot))
				Directory.CreateDirectory(galaxyCacheRoot);

			using (var imgFactory = new ImageProcessor.ImageFactory())
			{
				if (steamCoverSelect.Checked)
				{
					pictureBox.Image = steamCoverPicture.Image;
					string filename;
					using (var stream = new MemoryStream())
					{
						using (var img = imgFactory.Load(steamCoverPicture.Image).Format(new WebPFormat{Quality = 100}))
							img.Save(stream);
						stream.Seek(0, SeekOrigin.Begin);
						using (var sha = SHA256.Create())
							filename = string.Concat(sha.ComputeHash(stream).Select(b => b.ToString("x2")));
						filename += "_glx_vertical_cover.webp";
						stream.Seek(0, SeekOrigin.Begin);
						var outputFilename = Path.Combine(galaxyCacheRoot, filename);
						File.WriteAllBytes(outputFilename, stream.ToArray());
					}
					using (var db = new GalaxyDb())
					{
						var existingCover = db.WebCacheResources.FirstOrDefault(r => r.ReleaseKey == releaseKey && r.UserId == userId && r.WebCacheResourceTypeId == WebCacheResourceType.VerticalCover);
						if (existingCover == null)
						{
							db.WebCacheResources.Add(new WebCacheResources
							{
								ReleaseKey = releaseKey,
								UserId = userId,
								WebCacheResourceTypeId = WebCacheResourceType.VerticalCover,
								Filename = filename,
							});
						}
						else
							existingCover.Filename = filename;
						db.SaveChanges();
					}
				}
				if (steamBackgroundSelect.Checked)
				{
					galaxyBackgroundPicture.Image = steamBackgroundPicture.Image;
					string filename;
					using (var stream = new MemoryStream())
					{
						using (var img = imgFactory.Load(steamBackgroundPicture.Image).Format(new WebPFormat{Quality = 100}))
							img.Save(stream);
						stream.Seek(0, SeekOrigin.Begin);
						using (var sha = SHA256.Create())
							filename = string.Concat(sha.ComputeHash(stream).Select(b => b.ToString("x2")));
						filename += "_glx_bg_top_padding_7.webp";
						stream.Seek(0, SeekOrigin.Begin);
						var outputFilename = Path.Combine(galaxyCacheRoot, filename);
						File.WriteAllBytes(outputFilename, stream.ToArray());
					}
					using (var db = new GalaxyDb())
					{
						var existingCover = db.WebCacheResources.FirstOrDefault(r => r.ReleaseKey == releaseKey && r.UserId == userId && r.WebCacheResourceTypeId == WebCacheResourceType.Background);
						if (existingCover == null)
						{
							db.WebCacheResources.Add(new WebCacheResources
							{
								ReleaseKey = releaseKey,
								UserId = userId,
								WebCacheResourceTypeId = WebCacheResourceType.Background,
								Filename = filename,
							});
						}
						else
							existingCover.Filename = filename;
						db.SaveChanges();
					}
				}
				if (steamIconSelect.Checked)
				{
					galaxyIconPicture.Image = steamIconPicture.Image;
					string filename;
					using (var stream = new MemoryStream())
					{
						using (var img = imgFactory.Load(steamIconPicture.Image).Format(new WebPFormat{Quality = 100}))
							img.Save(stream);
						stream.Seek(0, SeekOrigin.Begin);
						using (var sha = SHA256.Create())
							filename = string.Concat(sha.ComputeHash(stream).Select(b => b.ToString("x2")));
						filename += "_glx_square_icon_v2.webp";
						stream.Seek(0, SeekOrigin.Begin);
						var outputFilename = Path.Combine(galaxyCacheRoot, filename);
						File.WriteAllBytes(outputFilename, stream.ToArray());
					}
					using (var db = new GalaxyDb())
					{
						var existingCover = db.WebCacheResources.FirstOrDefault(r => r.ReleaseKey == releaseKey && r.UserId == userId && r.WebCacheResourceTypeId == WebCacheResourceType.SquareIcon);
						if (existingCover == null)
						{
							db.WebCacheResources.Add(new WebCacheResources
							{
								ReleaseKey = releaseKey,
								UserId = userId,
								WebCacheResourceTypeId = WebCacheResourceType.SquareIcon,
								Filename = filename,
							});
						}
						else
							existingCover.Filename = filename;
						db.SaveChanges();
					}
				}
			}

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
