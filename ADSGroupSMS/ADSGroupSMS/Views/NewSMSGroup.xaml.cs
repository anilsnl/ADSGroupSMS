using ADSGroupSMS.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
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
	public partial class NewSMSGroup : ContentPage
	{
        public static GroupSMSPageModel pageModel;
		public NewSMSGroup ()
		{
			InitializeComponent ();
            pageModel = new GroupSMSPageModel();
            this.BindingContext = pageModel;
		}

        private async Task btnSelectContact_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadPopup("Opening..."), true);
            await Navigation.PushModalAsync(new MultiSelectContactPopup(pageModel.Contacts.ToList()));
            await Navigation.PopAllPopupAsync();
        }

        private async void btnNext_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (pageModel.SelectedCount==0)
                {
                    await DisplayAlert("Error", "You've to select number to be able continiu.", "ok");
                    return;
                }
                var model = new FinalConfirmModel()
                {
                    SMSText = pageModel.SMSText
                };
                model.Contacts.AddRange(pageModel.Contacts.Where(a => a.isSelected).ToList());
                model.SelectedCount = pageModel.SelectedCount;
                await Navigation.PushModalAsync(new GroupSMSConfirm(model));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}