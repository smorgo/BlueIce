using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Util;

namespace BlueIce.Droid
{
	public class CooliceDeviceDriver : ICooliceDeviceDriver
	{
		private const string TAG = "CooliceDeviceDriver";
		private const int REQUEST_CONNECT_DEVICE = 1;
		public bool Connected { get; private set; }
		private BluetoothAdapter _adapter;
		private BluetoothDevice _device;
		private BluetoothSocket _socket;

		public CooliceDeviceDriver()
		{
			_adapter = BluetoothAdapter.DefaultAdapter;
		}

		public async Task<bool> Connect(CooliceDevice device)
		{

			// Get the remote device, the bluetooth address has already been verified
			_device = _adapter.GetRemoteDevice(device.Address);

			try
			{
				_socket = _device.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString(BluetoothService.BT_SERIAL_DEVICE_UUID));
			}
			catch (Java.IO.IOException e)
			{
				Log.Error(TAG, "Create Socket Failed", e);
			}

			Connected = false;
			if (_socket != null)
			{
				SynchronizationContext currentContext = SynchronizationContext.Current;
				try
				{
					await Task.Run(async () =>
					{
						await _socket.ConnectAsync();
						if (_socket.IsConnected)
						{
							Connected = true;
							currentContext.Post((e) =>
							{
								if (device.BluetoothConnectionReceived != null)
									device.BluetoothConnectionReceived(this, new BluetoothConnectionReceivedEventArgs(Connected));

							}, null);
						}
					});


				}
				catch (Java.IO.IOException ex)
				{
					Connected = false;
					currentContext.Post((e) =>
					{
						if (device.BluetoothConnectionReceived != null)
							device.BluetoothConnectionReceived(this, new BluetoothConnectionReceivedEventArgs(Connected, ex));

					}, null);
					Log.Error(TAG, "Bluetooth Socket Connection Failed", ex);
				}
			}

			return true;
		}		
	}
}
