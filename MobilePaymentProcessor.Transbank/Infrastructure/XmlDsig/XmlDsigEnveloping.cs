using MobilePaymentProcessor.Transbank.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace MobilePaymentProcessor.Transbank.Infrastructure.Policies.XmlDsig
{
    public class XmlDsigEnveloping : BaseXmlDsig, IVerifyable
    {
        private CertManager manager { get; set; }
        private bool c14 { get; set; }

        public XmlDsigEnveloping(bool c14, CertManager manager)
        {
            this.manager = manager;
            this.c14 = c14;
        }

        public override string Name
        {
            get
            {
                return string.Format("XmlDsigEnveloping {0} c14", c14 ? "with" : "without");
            }
        }

        public override string SignXml(XmlDocument Document)
        {
            SignedXmlWithId signedXml = new SignedXmlWithId(Document);
            signedXml.SigningKey = manager.Certificate.PrivateKey;

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(manager.Certificate);
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;

            // the DataObject has to point to a XmlNodeList
            DataObject dataObject = new DataObject();
            dataObject.Id = "MyObjectID1";
            dataObject.Data =
               new CustomXmlNodeList(new[] { Document.DocumentElement });
            signedXml.AddObject(dataObject);

            // Add the reference to the SignedXml object.
            Reference reference = new Reference();
            reference.Uri = "#MyObjectID1";
            signedXml.AddReference(reference);

            // Create a reference to be signed.
            if (c14)
            {
                XmlDsigC14NTransform env = new XmlDsigC14NTransform();
                reference.AddTransform(env);
            }

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save 
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // create detached envelope 
            XmlDocument envelope = new XmlDocument();
            envelope.AppendChild(envelope.CreateElement("Envelope"));

            envelope.DocumentElement.AppendChild(
               envelope.ImportNode(xmlDigitalSignature, true));

            return envelope.OuterXml;
        }
    }
}
