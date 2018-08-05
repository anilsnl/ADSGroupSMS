using ADSGroupSMS.Dependencies;
using ADSGroupSMS.Views;
using Plugin.ContactService.Shared;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ADSGroupSMS
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            Title = "Send Group SMS";
		}

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushPopupAsync(new LoadPopup("Loading..."),true);
                await Navigation.PushAsync(new NewSMSGroup());
                await Navigation.PopPopupAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", ex.Message, "Tamam");
            }

        }
        
    }
}
