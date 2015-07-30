using Christoc.Modules.SubscriptionValidation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Christoc.Modules.SubscriptionValidation.Components
{
    public class SubscriptionModel
    {
        public SubscriptionModel(string phone = "", List<int> products = null)
        {
            this.SubscriptionLists = products ?? new List<int>();
            PhoneNumber = phone;
        }

        public string PhoneNumber { get; set; }

        public List<int> SubscriptionLists { get; set; }

        public static List<int> ParseLists(string commaSeparatedValues)
        {
            var list = new List<int>();

            foreach (var s in commaSeparatedValues.Split(','))
            {
                int value;

                if (int.TryParse(s.Trim(), out value) && value != 0)
                    list.Add(value);
            }

            return list;
        }

        public string SerializedList
        {
            get
            {
                return string.Join(",", SubscriptionLists.ToArray());
            }
        }

        public async Task<bool> IsSubscriptionValid()
        {
            if (string.IsNullOrWhiteSpace(this.PhoneNumber))
                return false;

            return await SubscriptionValidationService.ValidateUserSubscriptionAsync(this);
        }
    }
}