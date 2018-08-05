using ADSGroupSMS.Dependencies;
using ADSGroupSMS.Models;
using Rg.Plugins.Popup.Extensions;
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
	public partial class GroupSMSConfirm : ContentPage
	{
        FinalConfirmModel pagemodel;
		public GroupSMSConfirm (FinalConfirmModel model)
		{
			InitializeComponent ();
            try
            {
                pagemodel = model;
                this.BindingContext = model;
            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", ex.Message, "ok");
            }
		}

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadPopup("Sending..."), true);
            var result = await DependencyService.Get<ISMSDependency>().SendManyContactAsync(new SMSModel()
            {
                Phones = new List<string>(pagemodel.Contacts.Select(a => a.Phone).ToList()),
                SMSBody = pagemodel.SMSText
            });
            await DisplayAlert("Done", result.Message, "Ok");
            await Navigation.PopAllPopupAsync();
            await Navigation.PopModalAsync();
        }
    }
}