using System;
using Xamarin.Forms;

namespace BlueIce
{
	public class CooliceDevice
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public Command ConnectCommand { get; private set; }
		public EventHandler<BluetoothConnectionReceivedEventArgs> BluetoothConnectionReceived;
		public ICooliceDeviceDriver Driver { get; private set; }

		public CooliceDevice()
		{
			Driver = DependencyService.Get<ICooliceDeviceDriver>();

			ConnectCommand = new Command(
				(arg) =>
				{
					Driver?.Connect(this);

				}, (arg) => true);
		}
	}
}
