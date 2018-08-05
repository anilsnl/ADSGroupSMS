using ADSGroupSMS.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ADSGroupSMS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MultiSelectContactPopup : ContentPage
	{

        MultiSelectPopupPageModel pageModel;
        public MultiSelectContactPopup(List<PageContactModel> list)
        {
            InitializeComponent();
            pageModel = new MultiSelectPopupPageModel(list);
            this.BindingContext = pageModel;
        }
        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadPopup("Loading..."), true);
            pageModel.FilteredContacts.Clear();

            if (String.IsNullOrEmpty(txtSearch.Text))
            {
                var list = pageModel.Contacts.ToList();
                foreach (var item in list)
                {
                    pageModel.FilteredContacts.Add(item);
                }
            }
            else
            {
                var list =  pageModel.Contacts.Where(a => a.Name.ToLower().Contains(e.NewTextValue.ToLower()));
                foreach (var item in list)
                {
                    pageModel.FilteredContacts.Add(item);
                }
            }
            await Navigation.PopAllPopupAsync();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadPopup("Canceling..."), true);
            await Navigation.PopModalAsync();
            await Navigation.PopAllPopupAsync();
        }

        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (pageModel.isInProgress)
                return;
            try
            {
                await Task.Run(() =>
                {
                    var swc = (Switch)sender;
                    var contact = (PageContactModel)swc.BindingContext;
                    pageModel.Contacts.Remove(pageModel.Contacts.First(a => a.Name.Equals(contact.Name) && a.Phone.Equals(contact.Phone)));
                    pageModel.Contacts.Add(contact);
                    pageModel.Contacts.OrderBy(a => a.Name);
                    pageModel.BtnSelectAllEnabled = pageModel.Contacts.Where(a => a.isSelected).Count() != pageModel.Contacts.Count();
                    pageModel.BtnUnselectAllEnabled = pageModel.Contacts.Where(a => !a.isSelected).Count() != pageModel.Contacts.Count();
                });
            }
            catch (Exception ex)
            {
              //  await DisplayAlert("Error", "An error appied!", "Ok");
            }
        }

        private async void btnUnSelectAll_Clicked(object sender, EventArgs e)
        {
            try
            {
                pageModel.isInProgress = true;
                await Navigation.PushPopupAsync(new LoadPopup("Loading..."), true);
                foreach (var item in pageModel.Contacts)
                {
                    item.isSelected = false;
                }
                // pageModel.FilteredContacts = pageModel.Contacts;
                txtSearch.Text = "";
                pageModel.BtnSelectAllEnabled = true;
                pageModel.BtnUnselectAllEnabled = false;
                
            }
            catch (Exception)
            {
                
            }
            finally
            {
                await Navigation.PopAllPopupAsync();
                pageModel.isInProgress = false;
            }
        }

        private async void btnSelectAll_Clicked(object sender, EventArgs e)
        {
            try
            {
                pageModel.isInProgress = true;
                await Navigation.PushPopupAsync(new LoadPopup("Loading..."), true);
                foreach (var item in pageModel.Contacts)
                {
                    item.isSelected = true;
                }
                pageModel.BtnSelectAllEnabled = false;
                pageModel.BtnUnselectAllEnabled = true;
                pageModel.FilteredContacts = pageModel.Contacts;

            }
            catch (Exception)
            {
                
            }
            finally
            {
                await Navigation.PopAllPopupAsync();
                pageModel.isInProgress = false;
            }
        }

        private async void btnNext_Clicked(object sender, EventArgs e)
        {
            await Task.Run(()=>
            {
                Navigation.PushPopupAsync(new LoadPopup("Loading..."), true);
                NewSMSGroup.pageModel.Contacts.Clear();
                foreach (var item in pageModel.Contacts)
                {
                    NewSMSGroup.pageModel.Contacts.Add(item);
                }
                NewSMSGroup.pageModel.SelectedCount = pageModel.Contacts.Where(A => A.isSelected).Count();
                Navigation.PopModalAsync();
                Navigation.PopAllPopupAsync();
            });
        }
    }
}