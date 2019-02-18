using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
namespace CitiesProject
{
    [Serializable]
    [XmlRoot(ElementName = "CanadaCity",    
     IsNullable = true)]
    public class CityInfo
    {
        //Properties       
        [XmlElement(ElementName = "city")]
        public string CityName { get; set; }
        [XmlElement(ElementName = "city_ascii")]
        public string CityAscii { get; set; }
        [XmlElement(ElementName = "lat")]
        public double Latitude { get; set; }
        [XmlElement(ElementName = "lng")]
        public double Longitude { get; set; }
        [XmlElement(ElementName = "country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "admin_name")]
        public string Province { get; set; }
        [XmlElement(ElementName = "capital")]
        public string Capital { get; set; }
        [XmlElement(ElementName = "population")]
        public int Population { get; set; }
        [XmlElement(ElementName = "id")]
        // [JsonProperty("id")]
        public int CityID { get; set; }



        //  Methods
        public string GetProvince()
        {
            return Province;
        }

        public int GetPopulation()
        {
            return Population;
        }

        public Tuple<double, double> GetLocation()
        {
            var LocationList = new Tuple<double, double>(Latitude, Longitude);            
            return LocationList;
        }

    }
}
