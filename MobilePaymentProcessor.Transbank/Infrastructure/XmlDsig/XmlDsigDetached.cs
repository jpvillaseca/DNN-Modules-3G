using MobilePaymentProcessor.Transbank.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace MobilePaymentProcessor.Transbank.Infrastructure.Policies.XmlDsig
{
    public class XmlDsigDetached : BaseXmlDsig, IVerifyable
    {
        private CertManager manager { get; set; }

        private bool c14 { get; set; }

        public XmlDsigDetached(bool c14, CertManager manager)
        {
            this.manager = manager;
            this.c14 = c14;
        }

        public override string Name
        {
            get
            {
                return string.Format("XmlDsigDetached {0} c14", c14 ? "with" : "without");
            }
        }

        public override string SignXml(XmlDocument Document)
        {
            // create detached envelope 
            XmlDocument envelope = new XmlDocument();
            envelope.PreserveWhitespace = true;
            envelope.AppendChild(envelope.CreateElement("Envelope"));

            XmlElement message = envelope.CreateElement("Message");
            message.InnerXml = Document.DocumentElement.OuterXml;
            message.SetAttribute("Id", "MyObjectID");
            envelope.DocumentElement.AppendChild(message);

            SignedXmlWithId signedXml = new SignedXmlWithId(envelope);
            signedXml.SigningKey = manager.Certificate.PrivateKey;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "#MyObjectID";

            if (c14)
            {
                XmlDsigC14NTransform env = new XmlDsigC14NTransform();
                reference.AddTransform(env);
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

            envelope.DocumentElement.AppendChild(
               envelope.ImportNode(xmlDigitalSignature, true));

            return envelope.OuterXml;
        }
    }
}
