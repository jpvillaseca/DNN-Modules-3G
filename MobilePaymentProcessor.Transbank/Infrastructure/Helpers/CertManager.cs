using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MobilePaymentProcessor.Transbank.Infrastructure.Helpers
{
    public class CertManager
    {
        public CertManager(string issuerName)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
            X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindByIssuerName, issuerName, false);
            if (certificates.Count == 0)
            {
                throw new IOException("Certificate for CN " + issuerName + " not installed on local machine");
            }
            else
            {
                this.Certificate = certificates[0];
            }
            store.Close();
        }

        public X509Certificate2 Certificate { get; private set; }

        public string IssuerName { get; set; }
    }
}
