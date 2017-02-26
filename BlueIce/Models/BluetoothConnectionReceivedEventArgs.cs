using System;
namespace BlueIce
{
	public class BluetoothConnectionReceivedEventArgs
	{
		public bool Connected { get; private set; }
		public Exception Exception { get; private set; }

		public BluetoothConnectionReceivedEventArgs(bool connected, Exception exception = null)
		{
			Connected = connected;
			Exception = exception;
		}
	}
}
