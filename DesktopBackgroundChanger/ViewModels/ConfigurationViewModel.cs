using System;
using Caliburn.Micro;

namespace DesktopBackgroundChanger.ViewModels
{
	public class ConfigurationViewModel : IGuardClose
	{
		public void TryClose(bool? dialogResult = null)
		{
			
		}

		public void CanClose(Action<bool> callback)
		{
			callback(true);
		}
	}
}