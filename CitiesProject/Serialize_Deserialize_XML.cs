using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CitiesProject
{
    public class Serialize_Deserialize_XML
    {
        string FilePath;
        Dictionary<string,CityInfo> CityDict = new Dictionary<string, CityInfo>();
        string XML_data { get; set; }
        public Serialize_Deserialize_XML()
        {       
        }
        public Serialize_Deserialize_XML(string fname)
        {
            this.FilePath = fname;
      
        }
        public Dictionary<string, CityInfo> Deserialize()
        {
            try
            {
                StreamReader sr = new StreamReader(FilePath);
                XML_data = sr.ReadToEnd();

                ////  Have to add a root node since the source xml doesnt have one. normally not required.
                //XML_data = "<Cities>" + XML_data + "</Cities";

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(XML_data);
                
                
                 var cities = xml.GetElementsByTagName("CanadaCity");
                foreach(XmlNode el in cities)
                {                    
                    CityInfo city = new CityInfo();
                    city.CityName = el.SelectSingleNode("./city").InnerText;

                    //  ignore blank entries
                    if (city.CityName == "")
                    {
                        continue;
                    }

                    //  if city name already exists ignore adding a duplicate..for now
                    if (CityDict.Count > 0)
                    {
                        if (CityDict.Any(c => c.Key == city.CityName) == true)
                            continue;
                    }
                    city.CityAscii = el.SelectSingleNode("./city_ascii").InnerText;
                    city.Latitude =Convert.ToDouble(el.SelectSingleNode("./lat").InnerText);
                    city.Longitude = Convert.ToDouble(el.SelectSingleNode("./lng").InnerText);
                    city.Country = el.SelectSingleNode("./country").InnerText;
                    city.Province = el.SelectSingleNode("./admin_name").InnerText;
                    city.Capital = el.SelectSingleNode("./capital").InnerText;
                    city.Population = Convert.ToInt32(el.SelectSingleNode("./population").InnerText);
                    city.CityID = Convert.ToInt32(el.SelectSingleNode("./id").InnerText);
                    //  Add to catalog
                    CityDict.Add(city.CityName, city);
                }
                
                return CityDict;
               
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }

        //public string Serialize(Dictionary<string, CityInfo> dict)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    if (dict != null && dict.Count > 0)
        //    {
        //        foreach(var item in dict)
        //        {
        //            var city = item.Value;
        //            System.Xml.Serialization.XmlSerializer writer =
        //                       new System.Xml.Serialization.XmlSerializer(typeof(List<CityInfo>));

        //            System.IO.FileStream file = new FileStream(file)

        //            writer.Serialize(
        //        }
                              
        //    }
        //    return sb.ToString();
        //}


    }
}

       