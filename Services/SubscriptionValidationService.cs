using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Digevo.Infrastructure.Extensions;
using System.Net;
using System.IO;
using Christoc.Modules.SubscriptionValidation.Components;

namespace Christoc.Modules.SubscriptionValidation.Services
{
    public class SubscriptionValidationService
    {
        private static readonly string ValidationServiceEndpoint = "http://146.82.89.83/le/dnn/dnn_suscstatus.asp?ani={PhoneNumber}&productos={Products}";

        public static async Task<bool> ValidateUserSubscriptionAsync(SubscriptionModel model)
        {
            if (string.IsNullOrWhiteSpace(model.PhoneNumber) || string.IsNullOrWhiteSpace(model.SerializedList))
                return false;

            var serviceCall = ValidationServiceEndpoint.FormatWith(
                new { PhoneNumber = model.PhoneNumber, Products = model.SerializedList });

            var request = (HttpWebRequest)WebRequest.Create(serviceCall);
            var response = (HttpWebResponse)await request.GetResponseAsync();

            bool isValidSubscription = false;

            return bool.TryParse(new StreamReader(response.GetResponseStream()).ReadToEnd(), out isValidSubscription) ? isValidSubscription : false;
        }
    }
}