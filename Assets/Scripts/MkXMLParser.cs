using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public abstract class MkXmlParser : MonoBehaviour
{
    public List<string> xmlParentHierarchy = new List<string>();
    public string currentElementName = string.Empty;
    public XmlReader reader;
    public Dictionary<string, string> attributes;

    public MkXmlParser(string xmlString)
    {
        this.reader = XmlReader.Create(xmlString);
    }

    public void parse()
    {
        while (this.reader.Read())
        {
            XmlNodeType nodeType = this.reader.NodeType;
            switch (nodeType)
            {
                case XmlNodeType.Element:
                    this.attributes = new Dictionary<string, string>();
                    for (int i = 0; i < this.reader.AttributeCount; ++i)
                    {
                        this.reader.MoveToAttribute(i);
                        this.attributes.Add(this.reader.Name, this.reader.Value);
                    }
                    this.reader.MoveToElement();
                    this.currentElementName = this.reader.Name;
                    this.openElement(this.reader.Name, this.getOpenParentElement(), this.attributes);
                    if (!this.reader.IsEmptyElement)
                    {
                        this.xmlParentHierarchy.Add(this.reader.Name);
                        continue;
                    }
                    else
                        continue;
                case XmlNodeType.Text:
                    this.dataElement(this.currentElementName, this.getDataParentElement(), this.reader.Value, this.attributes);
                    continue;
                default:
                    if (nodeType == XmlNodeType.EndElement)
                    {
                        this.xmlParentHierarchy.RemoveAt(this.xmlParentHierarchy.Count - 1);
                        this.attributes = (Dictionary<string, string>)null;
                        this.closeElement(this.reader.Name);
                        continue;
                    }
                    else
                        continue;
            }
        }
    }

    private string getOpenParentElement()
    {
        if (this.xmlParentHierarchy.Count > 0)
            return this.xmlParentHierarchy[this.xmlParentHierarchy.Count - 1];
        else
            return string.Empty;
    }

    private string getDataParentElement()
    {
        if (this.xmlParentHierarchy.Count > 1)
            return this.xmlParentHierarchy[this.xmlParentHierarchy.Count - 2];
        else
            return string.Empty;
    }

    public abstract void openElement(string elementName, string parentElementName, Dictionary<string, string> attributes);

    public abstract void dataElement(string elementName, string parentElementName, string data, Dictionary<string, string> attributes);

    public abstract void closeElement(string elementName);
}

