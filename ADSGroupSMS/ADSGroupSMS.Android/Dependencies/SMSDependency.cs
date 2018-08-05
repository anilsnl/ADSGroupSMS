using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADSGroupSMS.Droid.Dependencies;
using ADSGroupSMS.Models;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly:Dependency(typeof(SMSDependency))]
namespace ADSGroupSMS.Droid.Dependencies
{
    public class SMSDependency : ADSGroupSMS.Dependencies.ISMSDependency
    {
        public async Task<OperationResult> SendManyContactAsync(SMSModel model)
        {
            try
            {
                await Task.Run(() => {

                    SmsManager smsManager = SmsManager.Default;
                    foreach (var item in model.Phones)
                    {
                        smsManager.SendTextMessage(item, "", model.SMSBody, null, null);

                    }
                });
                return new OperationResult(true, "Done");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message);
            }
        }

        public async Task<OperationResult> SendOneAsync(SMSModel model)
        {
            try
            {
                await Task.Run(() => {

                    SmsManager smsManager = SmsManager.Default;
                    smsManager.SendTextMessage(model.PhoneNumber, "", model.SMSBody, null, null);
                });
                return new OperationResult(true, "Done");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message);    
            }
        }
    }
}