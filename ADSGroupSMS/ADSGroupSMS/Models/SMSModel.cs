using System;
using System.Collections.Generic;
using System.Text;

namespace ADSGroupSMS.Models
{
    public class SMSModel
    {
        public string SMSBody { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Phones { get; set; } = new List<string>();
    }
}
