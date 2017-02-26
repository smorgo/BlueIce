using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BlueIce
{
	public interface ICooliceDeviceScanner
	{
		Task EnumerateDevices(ObservableCollection<CooliceDevice> devices);
	}
}
