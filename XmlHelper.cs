using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net.Mime;
using System.Xml;
using System.Xml.Linq;
using  System.Reflection;
using System.Windows.Forms;

namespace Reporting.Forms.Helpers
{
   
    public class XmlHelper
    {
        private const string XmlPath = @"\\prodwin0107\userdatashare0001\andy.flynn\visual studio 2015\Projects\ReportingCode\Reporting.Forms\XML\Reports.xml";

      //  private readonly string XmlPath = System.IO.Directory.GetCurrentDirectory();
        public bool AddRecord<T>(T item)
        {
            try
            {
                Type t = typeof(T);
                var values = new List<string>();
                string tblName = t.Name.ToString() + "s", propVal = string.Empty;
                string itemName = t.Name.ToString();
            
                XDocument doc = XDocument.Load(XmlPath);
                XElement root = new XElement(itemName);
                foreach (var prop in typeof(T).GetProperties())
                {
                    try
                    {
                        propVal = prop.GetValue(item, null).ToString();
                    }
                    catch
                    {
                        propVal = string.Empty;
                    }
                    try
                    {
                       values = new List<string>();
                       values = (List<string>) prop.GetValue(item, null);
                        if (values.Count > 0)
                        {
                            XElement listRoot = new XElement(prop.Name);
                            foreach (var listItem in values)
                            {
                                listRoot.Add(new XElement(prop.Name, listItem));
                            }
                            root.Add(listRoot);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    if (values.Count <1)
                        root.Add(new XElement(prop.Name, propVal));
                }
                XElement xElement = doc.Element(tblName);
                if (xElement != null) xElement.Add(root);//"Reports"
                doc.Save(XmlPath);
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
