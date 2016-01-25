using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobilePaymentProcessor.Transbank.Services.WSWebpay
{
    public class TransactionResultModel
    {
        public string AccountingDate { get; set; }

        public string BuyOrder { get; set; }

        public string CardNumber { get; set; }

        public string CardExpirationDate { get; set; }

        public List<TransactionDetailModel> DetailOutput { get; set; }

        public string SessionId { get; set; }

        public DateTime? TransactionDate { get; set; }

        public string UrlRedirection { get; set; }

        public string VCI { get; set; }
    }
}
