using Xamarin.Forms;

namespace BlueIce
{
	public partial class BlueIcePage : ContentPage
	{
		public BlueIceViewModel ViewModel { get; private set; }

		public BlueIcePage()
		{
			ViewModel = new BlueIceViewModel();

			BindingContext = ViewModel;

			InitializeComponent();
		}
	}
}
