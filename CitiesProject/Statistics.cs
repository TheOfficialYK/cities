using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Net;
using System.Xml.Linq;

namespace CitiesProject
{
    public class Statistics
    {
        //  Properties
        //  Holds info extracted from data files, Key: city name, value: object of CityInfo class
        public Dictionary<string, CityInfo> CityCatalogue { get; private set; }
        string FileName { get; set; }
        string FileType { get; set; }   //  JSON, CSV OR XMl
      
        public Statistics(string fname, string ftype)
        {
            FileName = fname;
            FileType = ftype;
            
        }

        //  Takes a CityName param and returns that city info from dictionary as ...? CityInfo?
        public CityInfo DisplayCityInformation(string CityName)
        {
            //   find city in cata by name
            CityInfo city = CityCatalogue.Where(c => c.Key == CityName) as CityInfo;
            return city;
        }

        //  Returns the sum of all cities populations from Dictionary 
        public int CalculateProvincePopulation(string ProvinceName)
        {
            int TotalPop = 0;
            //  for each city in given province
           foreach(var c in CityCatalogue.Where(city => city.Value.Province == ProvinceName).ToList())
            {
                TotalPop += c.Value.Population;
            }
            return TotalPop;
        }

        //  Takes a ProvinceName param and retuns a list of all cities located in that province from the dictionary
        public List<CityInfo> DisplayProvinceCities(string province)
        {
            return CityCatalogue.Where(c => c.Value.Province == province).Select(c => c.Value).ToList();
        }

        //  Returns the city with bigger population of both 
        public CityInfo MatchCitiesPopluation(CityInfo CityOne, CityInfo CityTwo)
        {
            
            if (CityOne.Population >= CityTwo.Population) return CityOne;
            else return CityTwo;           
        }

        //  Returns the distance between two cities using lat/long  from dictionary
        //  Use google GeoCodingAPI to calculate the distance
        public void DistanceBetweenCities(string CityOne, string CityTwo)
        {
            //string address = "123 something st, somewhere";
            //string requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

            //WebRequest request = WebRequest.Create(requestUri);
            //WebResponse response = request.GetResponse();
            //XDocument xdoc = XDocument.Load(response.GetResponseStream());

            //XElement result = xdoc.Element("GeocodeResponse").Element("result");
            //XElement locationElement = result.Element("geometry").Element("location");
            //XElement lat = locationElement.Element("lat");
            //XElement lng = locationElement.Element("lng");
        }
        public void DeserializeFileToDictionary()
        {
            //  Call appropriate class method by ftype
            if (FileType.Equals("csv"))
            {
                Serialize_Deserialize_CSV _CSV = new Serialize_Deserialize_CSV(FileName);
                CityCatalogue = _CSV.Deserialize();

            }
            else if (FileType.Equals("json"))
            {
                Serialize_Deserialize_JSON deserialize_JSON = new Serialize_Deserialize_JSON(FileName);
                CityCatalogue = deserialize_JSON.Deserialize();
            }
            else if (FileType.Equals("xml"))
            {
                Serialize_Deserialize_XML deserialize_XML = new Serialize_Deserialize_XML(FileName);
                CityCatalogue = deserialize_XML.Deserialize();
            }

        }

        public string Serialize(string filetype)
        {
            string data = "";
            //  Call appropriate class method by ftype
            if (filetype.ToLower().Equals("csv"))
            {
                Serialize_Deserialize_CSV _CSV = new Serialize_Deserialize_CSV();
                data = _CSV.Serialize(CityCatalogue);

            }
            else if (filetype.ToLower().Equals("json"))
            {
                Serialize_Deserialize_JSON _JSON = new Serialize_Deserialize_JSON();
                data = _JSON.Serialize(CityCatalogue);
            }
            //else if (filetype.ToLower().Equals("xml"))
            //{
            //    Serialize_Deserialize_XML _XML = new Serialize_Deserialize_XML();
            //    data = _XML.Serialize(CityCatalogue);
            //}
            return data;

        }


    }
}
