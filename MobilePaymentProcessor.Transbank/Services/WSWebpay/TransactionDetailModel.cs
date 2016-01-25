using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobilePaymentProcessor.Transbank.Services.WSWebpay
{
    public class TransactionDetailModel
    {
        public string AuthorizationCodeField { get; set; }

        public string PaymentTypeCodeField { get; set; }

        public int ResponseCodeField { get; set; }
    }
}
