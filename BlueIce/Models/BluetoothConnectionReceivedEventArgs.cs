using System;
namespace BlueIce
{
	public class BluetoothConnectionReceivedEventArgs
	{
		public DeviceConnectionState ConnectionState { get; private set; }
		public Exception Exception { get; private set; }

		public BluetoothConnectionReceivedEventArgs(DeviceConnectionState connectionState, Exception exception = null)
		{
			ConnectionState = connectionState;
			Exception = exception;
		}
	}
}
