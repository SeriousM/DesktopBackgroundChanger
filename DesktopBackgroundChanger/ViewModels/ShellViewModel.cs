using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using DesktopBackgroundChanger.Infrastructure;
using DesktopBackgroundChanger.Sources.Wallhaven;

namespace DesktopBackgroundChanger.ViewModels
{
	public class ShellViewModel : PropertyChangedBase, IShell
	{
		private readonly IWindowManager _windowManager;
		private readonly IWallhavenSource _wallhavenSource;

		public ShellViewModel(IWindowManager windowManager, IWallhavenSource wallhavenSource)
		{
			_windowManager = windowManager;
			_wallhavenSource = wallhavenSource;
			BackgroundItems = new ObservableCollection<BackgroundImageViewModel>();
		}

		public void ShowConfiguration()
		{
			_windowManager.ShowDialog(new ConfigurationViewModel());
		}

		public ObservableCollection<BackgroundImageViewModel> BackgroundItems { get; set; }

		public async void LoadBackgrounds()
		{
			BackgroundItems.Clear();
			var imageInformation = await _wallhavenSource.GetWallpaperInformation(113377);
			BackgroundItems.Add(new BackgroundImageViewModel(imageInformation));
		}
	}
}