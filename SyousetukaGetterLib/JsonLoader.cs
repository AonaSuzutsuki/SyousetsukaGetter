﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SyousetukaGetterLib
{
    public class JsonLoader
    {
        public List<Dictionary<string, string>> nodes { private set; get; } = new List<Dictionary<string, string>>();

        public JsonLoader(string url, bool localFile = false)
        {
            string json;
            if (localFile)
            {
                StreamReader sr = new StreamReader("test.txt");
                json = sr.ReadToEnd();
                sr.Dispose();
            }
            else
            {
                WebClient webClient = new WebClient();
                byte[] data = webClient.DownloadData(url);
                webClient.Dispose();
                json = Encoding.UTF8.GetString(data);
            }

            json = DecodeEncodedNonAsciiCharacters(json);

            decode(json);
        }

        private void decode(string json, string xpath = "root/item")
        {
            XmlDictionaryReader xmlReader = JsonReaderWriterFactory.CreateJsonReader(
                        Encoding.UTF8.GetBytes(json), XmlDictionaryReaderQuotas.Max);

            var element = XElement.Load(xmlReader);
            XmlDocument xmlLoader = new XmlDocument();
            xmlLoader.LoadXml(element.ToString());

            XmlNodeList itemNodes = xmlLoader.SelectNodes(xpath);
            foreach (XmlNode node in itemNodes)
            {
                var dic = new Dictionary<string, string>();
                handleNode(node, dic);
                nodes.Add(dic);
            }
        }

        private void handleNode(XmlNode node, Dictionary<string, string> dic)
        {
            if (node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    handleNode(child, dic);
                }
            }
            else
            {
                string name = node.ParentNode.LocalName;
                string value = node.InnerText;

                dic.Add(name, value);
            }
        }
        private string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(value, @"\\u(?<Value>[a-zA-Z0-9]{4})", m => {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Dictionary<string, string> dic in nodes)
            {
                foreach (KeyValuePair<string, string> pair in dic)
                {
                    sb.AppendFormat("{0} : {1}\r\n", pair.Key, pair.Value);
                }
                sb.Append("----------\r\n");
            }
            return sb.ToString();
        }
    }
}