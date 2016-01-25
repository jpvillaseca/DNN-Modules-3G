using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MobilePaymentProcessor.Transbank.Infrastructure.Policies
{
    public class CustomPolicyAssertion : SecurityPolicyAssertion
    {
        private string issuerCertificateName = null;
        private string transbankCertificateName = null;

        public CustomPolicyAssertion(string issuerCertificateName, string transbankCertificateName)
            : base()
        {
            this.issuerCertificateName = issuerCertificateName;
            this.transbankCertificateName = transbankCertificateName;
        }

        public override SoapFilter CreateClientInputFilter(FilterCreationContext context)
        {
            return new ClientInputFilter(this.transbankCertificateName);
        }

        public override SoapFilter CreateClientOutputFilter(FilterCreationContext context)
        {
            return new ClientOutputFilter(this.issuerCertificateName);
        }

        public override SoapFilter CreateServiceInputFilter(FilterCreationContext context)
        {
            return null;
        }

        public override SoapFilter CreateServiceOutputFilter(FilterCreationContext context)
        {
            return null;
        }

        public override IEnumerable<KeyValuePair<string, Type>> GetExtensions()
        {
            return new KeyValuePair<string, Type>[] { new KeyValuePair<string,
 Type>("CustomPolicyAssertion", this.GetType()) };
        }

        public override void ReadXml(XmlReader reader, IDictionary<string, Type>
        extensions)
        {
            reader.ReadStartElement("CustomPolicyAssertion");
        }
    }

}
