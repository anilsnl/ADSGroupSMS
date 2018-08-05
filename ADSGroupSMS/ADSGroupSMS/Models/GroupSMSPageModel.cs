using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ADSGroupSMS.Models
{
    public class GroupSMSPageModel : INotifyPropertyChanged
    {
        public GroupSMSPageModel(bool isDefault=true)
        {
            this.SelectedCount = 0;
            if (isDefault==false)
            {
                this.Contacts = new ObservableCollection<PageContactModel>();
                return;
            }
            this.Contacts = new System.Collections.ObjectModel.ObservableCollection<PageContactModel>(
                Plugin.ContactService.CrossContactService.Current.GetContactList().Where(a => !string.IsNullOrEmpty(a.Number)).OrderBy(a => a.Name)
                .Select(a => new PageContactModel()
                {
                    Name = a.Name,
                    Phone = a.Number.Trim(new Char[] { ' ','-','(',')'}),
                    isSelected = false
                }));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        ObservableCollection<PageContactModel> _contacts;
        public ObservableCollection<PageContactModel> Contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                _contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }

        string _smstext;
        public string SMSText
        {
            get
            {
                return _smstext;
            }
            set
            {
                _smstext = value;
                OnPropertyChanged(nameof(SMSText));
            }
        }

        int _selectedCount;
        public int SelectedCount
        {
            get
            {
                return _selectedCount;
            }
            set
            {
                _selectedCount = value;
                OnPropertyChanged(nameof(SelectedCount));
            }
        }
    }
    public class FinalConfirmModel
    {
        public List<PageContactModel> Contacts { get; set; } = new List<PageContactModel>();
        public string SMSText { get; set; } = "";
        public int SelectedCount { get; set; } = 0;
    }
}
