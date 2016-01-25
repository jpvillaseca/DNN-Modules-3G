using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MobilePaymentProcessor.Transbank.Infrastructure.Policies.XmlDsig
{
    public class CustomXmlNodeList : XmlNodeList
    {
        XmlNode[] _elements;

        public CustomXmlNodeList(XmlNode[] elements)
        {
            if (elements == null)
                throw new ArgumentException();

            this._elements = elements;
        }

        public override int Count
        {
            get { return _elements.Count(); }
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        public override XmlNode Item(int index)
        {
            return _elements[index];
        }
    }
}
