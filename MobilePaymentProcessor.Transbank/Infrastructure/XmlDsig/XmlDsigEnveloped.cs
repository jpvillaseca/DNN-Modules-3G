using MobilePaymentProcessor.Transbank.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace MobilePaymentProcessor.Transbank.Infrastructure.Policies.XmlDsig
{
    public class XmlDsigEnveloped : BaseXmlDsig, IVerifyable
    {
        private CertManager manager { get; set; }

        private bool c14 { get; set; }

        public XmlDsigEnveloped(bool c14, CertManager manager)
        {
            this.manager = manager;
            this.c14 = c14;
        }

        public override string Name
        {
            get
            {
                return string.Format("XmlDsigEnveloped {0} c14", c14 ? "with" : "without");
            }
        }

        public override string SignXml(XmlDocument Document)
        {
            SignedXmlWithId signedXml = new SignedXmlWithId(Document);
            signedXml.SigningKey = manager.Certificate.PrivateKey;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.            
            XmlDsigEnvelopedSignatureTransform env =
               new XmlDsigEnvelopedSignatureTransform(true);
            reference.AddTransform(env);

            if (c14)
            {
                XmlDsigC14NTransform c14t = new XmlDsigC14NTransform();
                reference.AddTransform(c14t);
            }

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(manager.Certificate);
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;
            
            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save 
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            Document.DocumentElement.AppendChild(
                Document.ImportNode(xmlDigitalSignature, true));

            return Document.OuterXml;
        }

    }
}
