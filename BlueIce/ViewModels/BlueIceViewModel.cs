using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Diagnostics.Tracing;
using System.Linq;

namespace BlueIce
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null)
			{
				Device.BeginInvokeOnMainThread(() => PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));
			}
		}
	}

	public class BlueIceViewModel : ViewModelBase
	{
		public Command ScanCommand { get; private set; }

		public ObservableCollection<CooliceDevice> Devices { get; private set; }

		public BlueIceViewModel()
		{
			Devices = new ObservableCollection<CooliceDevice>();

			ScanCommand = new Command(
				(arg) =>
				{
					var deviceWrapper = DependencyService.Get<ICooliceDeviceScanner>();

					deviceWrapper?.EnumerateDevices(Devices);
				}, (arg) => true);
		}
	}
}
