using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DesktopBackgroundChanger.Extensions;

namespace DesktopBackgroundChanger.Sources.Wallhaven
{
	// source heavily inspired by https://github.com/ivkos/Wallhaven/blob/master/Wallhaven.php
	public class WallhavenSource : WebSourceBase, IWallhavenSource
	{
		const string URL_HOME = "http://alpha.wallhaven.cc/";
		const string URL_WALLPAPER = "http://alpha.wallhaven.cc/wallpaper";
		const string URL_LOGIN = "http://alpha.wallhaven.cc/auth/login";
		const string URL_SEARCH = "http://alpha.wallhaven.cc/search";
		const string URL_IMG_PREFIX = "http://alpha.wallhaven.cc/wallpapers/full/wallhaven-";
		const string URL_THUMB_PREFIX = "http://alpha.wallhaven.cc/wallpapers/thumb/small/th-";

		private static readonly Regex ImageTypeRegex = new Regex(@"<img id=""wallpaper""\s*src="".*\.(png|jpg)""");
		private static readonly Regex PurityRegex = new Regex(@"<input id=""(sfw|sketchy|nsfw)"" checked=""checked"" name=""purity"" type=""radio"" value=""(sfw|sketchy|nsfw)"">");
		private static readonly Regex ResolutionRegex = new Regex(@"<dt>Resolution<\/dt>\s*<dd>\s*(\d+)\s*x\s*(\d+)\s*<\/dd>");
		private static readonly Regex SiteRegex = new Regex(@"<dt>Size<\/dt>\s*<dd>\s*(\d+\.?\d* (MiB|KiB))\s*<\/dd>");
		private static readonly Regex CategoryRegex = new Regex(@"<dt>Category<\/dt>\s*<dd>\s*(General|Anime|People)\s*<\/dd>");
		private static readonly Regex ViewsCountRegex = new Regex(@"<dt>Views<\/dt>\s*<dd>\s*([\d\,]+)\s*<\/dd>");
		private static readonly Regex FavoritesCountRegex = new Regex(@"<dt>Favorites<\/dt>\s*<dd>\s*(<a.*>)?([\d\,]+)\s*(<\/a>)?\s*<\/dd>", RegexOptions.Singleline);
		private static readonly Regex AddedTimeRegex = new Regex(@"<dt>Added<\/dt>\s*<dd>\s*<time.+datetime=""(.+)"">.*<\/time>\s*<\/dd>", RegexOptions.Singleline);

		public WallhavenSource()
		{
		}

		public async Task<WallhavenImageInformation> GetWallpaperInformation(int wallpaperId)
		{
			var response = await HttpGet(URL_WALLPAPER + "/" + wallpaperId);

			// Image type (JPG or PNG)
			var imageType = ImageTypeRegex.Match(response).GetGroupValueOrThrow(1, "No image type found.");
			// Purity
			var purity = PurityRegex.Match(response).GetGroupValueAsEnumOrThrow<PurityType>(1, "No purity found.");
			// Resolution
			var resolutionMatch = ResolutionRegex.Match(response);
			var resolution = resolutionMatch.GetGroupValueOrThrow(1, "No X-Resolution found.") + "x" + resolutionMatch.GetGroupValueOrThrow(2, "No Y-Resolution found.");
			// Size
			var size = SiteRegex.Match(response).GetGroupValueOrThrow(1, "No size found.");
			// Category
			var category = CategoryRegex.Match(response).GetGroupValueAsEnumOrThrow<CategoryType>(1, "No category found.");
			// Views count
			var viewsCount = ViewsCountRegex.Match(response).GetGroupValueOrThrow(1, "No views count found.").Replace(",", string.Empty);
			// Favorites count
			var favoritesCount = FavoritesCountRegex.Match(response).GetGroupValueOrThrow(2, "No favorites count found.").Replace(",", string.Empty);
			// Added time
			var addedTime = AddedTimeRegex.Match(response).GetGroupValueOrThrow(1, "No 'added time' found.");

			return new WallhavenImageInformation
			{
				InformationUrl = URL_WALLPAPER + "/" + wallpaperId,
				ImageUrl = URL_IMG_PREFIX + wallpaperId + "." + imageType,
				ThumbnailUrl = URL_THUMB_PREFIX  + wallpaperId + "." + imageType,
				ImageType = imageType,
				Purity = purity,
				Resolution = resolution,
				Size = size,
				Category = category,
				ViewsCount = viewsCount,
				FavoritesCount = favoritesCount,
				AddedTime = DateTime.Parse(addedTime)
			};
		}
	}

	public interface IWallhavenSource
	{
		Task<WallhavenImageInformation> GetWallpaperInformation(int wallpaperId);
	}

	public class WallhavenImageInformation
	{
		public string ImageType { get; set; }
		public string InformationUrl { get; set; }
		public string ImageUrl { get; set; }
		public string ThumbnailUrl { get; set; }
		public PurityType Purity { get; set; }
		public string Resolution { get; set; }
		public string Size { get; set; }
		public CategoryType Category { get; set; }
		public string ViewsCount { get; set; }
		public string FavoritesCount { get; set; }
		public DateTime AddedTime { get; set; }
	}
}