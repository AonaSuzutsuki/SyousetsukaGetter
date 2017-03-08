﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace KimamaLib.XMLWrapper
{
    /// <summary>
    /// Read from a file as XML Document.
    /// </summary>
    public class Reader
    {
        public string XmlPath { get; } = string.Empty;

        private readonly XmlDocument _document;

        public Reader(string xmlPath)
        {
            XmlPath = xmlPath;
            _document = new XmlDocument();
            _document.Load(xmlPath);
        }
        public Reader(Stream stream)
        {
            _document = new XmlDocument();
            _document.Load(stream);
        }

        public List<string> GetAttributes(string attribute, string xpath)
        {
            List<string> Values = new List<string>();

            ///items/item/property/property[@name='DegradationMax']
            XmlNodeList personList = _document.SelectNodes(xpath);

            for (int i = 0; i < personList.Count; i++)
            {
                Values.Add((personList[i] as XmlElement).GetAttribute(attribute));
            }

            return Values;
        }
        public string GetAttribute(string attribute, string xpath)
        {
            ///items/item/property/property[@name='DegradationMax']
            XmlNodeList personList = _document.SelectNodes(xpath);

            string value = string.Empty;
            if (personList.Count > 0)
            {
                value = (personList[0] as XmlElement).GetAttribute(attribute);
            }
            return value;
        }

        public List<string> GetValues(string xpath)
        {
            List<string> Values = new List<string>();

            XmlNodeList personList = _document.SelectNodes(xpath);
            for (int i = 0; i < personList.Count; i++)
            {
                string value = (personList[i] as XmlElement).InnerText;
                value = this.RemoveSpace(value, true);
                Values.Add(value);
            }

            return Values;
        }
        public string GetValue(string xpath)
        {
            XmlNodeList personList = _document.SelectNodes(xpath);
            
            string value = string.Empty;
            if (personList.Count > 0)
            {
                value = (personList[0] as XmlElement).InnerText;
                value = this.RemoveSpace(value, true);
            }
            return value;
        }

        private string RemoveSpace(string text, bool isAddLine = false)
        {
            var sb = new StringBuilder();

            const string expression = "^ *(?<text>.*?)$";
            var reg = new Regex(expression);
            var sr = new StringReader(text);
            while (sr.Peek() > -1)
            {
                var match = reg.Match(sr.ReadLine());
                if (match.Success)
                {
                    if (isAddLine)
                    {
                        sb.AppendLine(match.Groups["text"].Value);
                    }
                    else
                    {
                        sb.Append(match.Groups["text"].Value);
                    }
                }
                else
                {
                    sb.Append(sr.ReadLine());
                }
            }

            return sb.ToString();
        }
    }
}
