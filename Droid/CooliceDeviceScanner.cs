using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;

namespace BlueIce.Droid
{
	public class CooliceDeviceScanner : ICooliceDeviceScanner
	{
		private const int REQUEST_ENABLE_BT = 2;
		private const string COOLICE_DEVICE_NAME = "IM-V1-161";

		public async Task EnumerateDevices(System.Collections.ObjectModel.ObservableCollection<CooliceDevice> devices)
		{
			var activity = (Activity)Xamarin.Forms.Forms.Context;

			var adapter = BluetoothAdapter.DefaultAdapter;

			if (adapter != null)
			{
				// If bluetooth is not enabled, ask to enable it
				if (!adapter.IsEnabled)
				{
					var enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
					activity.StartActivityForResult(enableBtIntent, REQUEST_ENABLE_BT);
				}

				// Get connected devices
				var listOfDevices = adapter.BondedDevices;

				if (listOfDevices != null)
				{
					foreach (var bluetoothDevice in listOfDevices)
					{
						var uuids = bluetoothDevice.GetUuids();
						if (uuids != null)
						{
							if (uuids.Any(x => x.ToString() == BluetoothService.BT_SERIAL_DEVICE_UUID))
							{
								devices.Add(new CooliceDevice
								{
									Name = bluetoothDevice.Name,
									Address = bluetoothDevice.Address
								});
							}
						}
					}
				}
			}
		}
	}
}
