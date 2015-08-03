using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Digevo.Infrastructure.Extensions;
using System.Net;

namespace Christoc.Modules.LandingSubscription.Services
{
    public class LandingService
    {
        public LandingService(string username = "", string referal = "", string phoneNumber = "", string viralToken = "")
        {
            this.Username = username;
            this.Referal = referal;
            this.PhoneNumber = phoneNumber;
            this.ViralToken = viralToken;
        }

        public string Username { get; set; }

        public string PhoneNumber { get; set; }

        public string Referal { get; set; }

        public string ViralToken { get; set; }

        public bool ExecuteLandingService(string service)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.PhoneNumber))
                    return false;

                var serviceCall = service.FormatWith(
                new { PhoneNumber = this.PhoneNumber, Username = this.Username, Referal = this.Referal, ViralToken = this.ViralToken });

                var request = (HttpWebRequest)WebRequest.Create(serviceCall);
                var response = (HttpWebResponse)request.GetResponse();

                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}