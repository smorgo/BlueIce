using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlueIce
{
	public interface ICooliceDeviceDriver
	{
		DeviceConnectionState ConnectionState { get; }

		Task<bool> Connect(CooliceDevice device);
	}
}
