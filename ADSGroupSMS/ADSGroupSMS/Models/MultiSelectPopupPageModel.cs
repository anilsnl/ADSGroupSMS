using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ADSGroupSMS.Models
{
    public class MultiSelectPopupPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _btnSelectAllEnabled;
        private bool _btnUnselectAllEnabled;
        public bool BtnSelectAllEnabled
        {
            get => _btnSelectAllEnabled; set
            {
                _btnSelectAllEnabled = value;
                OnPropertyChanged(nameof(BtnSelectAllEnabled));
            }
        }
        public bool BtnUnselectAllEnabled
        {
            get => _btnUnselectAllEnabled; set
            {
                _btnUnselectAllEnabled = value;
                OnPropertyChanged(nameof(BtnUnselectAllEnabled));
            }
        }
        ObservableCollection<PageContactModel> _contacts;
        ObservableCollection<PageContactModel> _filteredContacts;
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
        public ObservableCollection<PageContactModel> FilteredContacts
        {
            get
            {
                return _filteredContacts;
            }
            set
            {
                _filteredContacts = value;
                OnPropertyChanged(nameof(FilteredContacts));
            }
        }

        public bool isInProgress { get; internal set; }

        public MultiSelectPopupPageModel(List<PageContactModel> list)
        {
            Contacts  = new ObservableCollection<PageContactModel>(list);
            FilteredContacts  = new ObservableCollection<PageContactModel>(list);
            BtnSelectAllEnabled = list.Where(a => a.isSelected).Count() != list.Count();
            BtnUnselectAllEnabled = list.Where(a => !a.isSelected).Count() != list.Count();
        }
    }
}
