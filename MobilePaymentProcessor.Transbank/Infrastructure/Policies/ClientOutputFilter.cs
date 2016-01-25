using MobilePaymentProcessor.Transbank.Infrastructure.Helpers;
using Intergrup.Core4.Soap.Security;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Security;
using Microsoft.Web.Services3.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace MobilePaymentProcessor.Transbank.Infrastructure.Policies
{
    public class ClientOutputFilter : SoapFilter
    {
        private string issuerCertificateName = null;

        public ClientOutputFilter(string issuerCertificateName)
            : base()
        {
            this.issuerCertificateName = issuerCertificateName;
        }
        public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
        {
            //Se crea una instancia de la componente “Intergrup.Core4.Soap.dll”
            WSSecuritySignature<SoapEnvelope, X509Certificate2> signed = new
            WSSecuritySignature<SoapEnvelope, X509Certificate2>();
            X509Certificate2 certificate = new CertManager(issuerCertificateName).Certificate;

            //Generar la firma digital
            signed.Signature(envelope, certificate);
            return SoapFilterResult.Continue;

        }
    }
}
