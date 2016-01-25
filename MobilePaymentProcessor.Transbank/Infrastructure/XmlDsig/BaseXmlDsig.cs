using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace MobilePaymentProcessor.Transbank.Infrastructure.Policies.XmlDsig
{
    public abstract class BaseXmlDsig : IVerifyable
    {
        public abstract string Name { get; }

        public abstract string SignXml(System.Xml.XmlDocument Document);

        public bool VerifyXml(string SignedXmlDocumentString)
        {
            byte[] stringData = Encoding.UTF8.GetBytes(SignedXmlDocumentString);
            using (MemoryStream ms = new MemoryStream(stringData))
                return VerifyXmlFromStream(ms);
        }

        public bool VerifyXmlFromStream(System.IO.Stream SignedXmlDocumentStream)
        {
            // load the document to be verified
            XmlDocument xd = new XmlDocument();
            xd.PreserveWhitespace = true;
            SignedXmlDocumentStream.Position = 0; //Bugfix
            xd.Load(SignedXmlDocumentStream);

            SignedXmlWithId signedXml = new SignedXmlWithId(xd);

            // load the first <signature> node and load the signature  
            XmlNode MessageSignatureNode =
               xd.GetElementsByTagName("Signature")[0];

            signedXml.LoadXml((XmlElement)MessageSignatureNode);

            // get the cert from the signature
            X509Certificate2 certificate = null;
            foreach (KeyInfoClause clause in signedXml.KeyInfo)
            {
                if (clause is KeyInfoX509Data)
                {
                    if (((KeyInfoX509Data)clause).Certificates.Count > 0)
                    {
                        certificate =
                        (X509Certificate2)((KeyInfoX509Data)clause).Certificates[0];
                    }
                }
            }

            // check the signature and return the result.
            return signedXml.CheckSignature(certificate, true);
        }
    }
}
