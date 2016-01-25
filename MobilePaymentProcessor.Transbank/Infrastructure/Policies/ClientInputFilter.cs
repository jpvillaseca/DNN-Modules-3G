using MobilePaymentProcessor.Transbank.Infrastructure.Helpers;
using Intergrup.Core4.Soap.Security;
using Microsoft.Web.Services3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MobilePaymentProcessor.Transbank.Infrastructure.Policies
{
    public class ClientInputFilter : SoapFilter
    {
        private string issuerCertificateName = null;

        public ClientInputFilter(string issuerCertificateName)
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

            //Generar la validación de la firma digital
            if (signed.CheckSignature(envelope, certificate))
            {
                return SoapFilterResult.Continue;
            }

            return SoapFilterResult.Terminate;
        }
    }
}
