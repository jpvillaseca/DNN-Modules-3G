using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobilePaymentProcessor.Transbank.Services.Wrapper
{
    public abstract class ServiceWrapperBase
    {
        public string IssuerCertificateName { get; private set; }

        public string TransbankCertificateName { get; private set; }

        public ServiceWrapperBase(string issuerCertificateName, string transbankCertificateName)
        {
            this.IssuerCertificateName = issuerCertificateName;
            this.TransbankCertificateName = transbankCertificateName;
        }

        public Exception ParseException(Exception ex)
        {
            ex = ex.InnerException ?? ex;

            var message = ex.Message;
            if (ex.Message.StartsWith("soap:Server"))
            {
                message = ex.Message.Substring(ex.Message.IndexOf("<!--") + 5, ex.Message.IndexOf("-->") - 5 - ex.Message.IndexOf("<!--")).Trim();
            }

            return new ApplicationException(message, ex);
        }
    }
}
