using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MobilePaymentProcessor.Transbank.Infrastructure.Policies.XmlDsig
{
    public interface IVerifyable
    {
        string Name { get; }

        string SignXml(XmlDocument Document);

        bool VerifyXml(string SignedXmlDocumentString);

        bool VerifyXmlFromStream(Stream SignedXmlDocumentStream);
    }
}
