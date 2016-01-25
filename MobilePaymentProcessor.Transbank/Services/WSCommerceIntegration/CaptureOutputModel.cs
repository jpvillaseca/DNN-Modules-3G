using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobilePaymentProcessor.Transbank.Services.WSCommerceIntegration
{
    public class CaptureOutputModel
    {
        public string AuthorizationCode { get; set; }

        public DateTime? AuthorizationDate { get; set; }

        public decimal? CapturedAmount { get; set;}

        public string Token { get; set; }
    }
}
