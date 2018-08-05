using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ADSGroupSMS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadPopup : PopupPage
	{
		public LoadPopup (string message)
		{
			InitializeComponent ();
            this.WidthRequest = stcMain.WidthRequest = 120;
            this.HeightRequest = stcMain.HeightRequest = 120;
            txtMessage.Text = message;
            
		}
	}
}