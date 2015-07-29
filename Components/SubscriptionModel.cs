using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Christoc.Modules.SubscriptionValidation.Components
{
    public class SubscriptionModel
    {
        public SubscriptionModel()
        {
            this.SubscriptionLists = new List<int>();
        }

        public string RedirectAddress { get; set; }

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
    }
}