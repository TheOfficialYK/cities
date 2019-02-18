using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CitiesProject
{
    
    public class Serialize_Deserialize_JSON
    {
        string FilePath;
        public Dictionary<string, CityInfo> CityDict = new Dictionary<string, CityInfo>();
        string JSON_data { get; set; }

        public Serialize_Deserialize_JSON()
        {        

        }
        public Serialize_Deserialize_JSON(string fname)
        {
            FilePath = fname;
        }

        public Dictionary<string, CityInfo> Deserialize()
        {
            //  Read the file
            StreamReader sr = new StreamReader(FilePath);
            JSON_data = sr.ReadToEnd();

           var datalist = JsonConvert.DeserializeObject<dynamic>(JSON_data);

            //JObject jObj = datalist; //initialized somewhere, perhaps in your foreach
            //foreach(var item in jObj)
            //{
            //    JObject city = item.Value.;
            //    //check if property exists
            //    if (msgProperty == null)
            //    {
            //        //  Check if the other property exists
            //      //  var mag = msgProperty.Value;
            //    }
            //    else
            //    {
            //        //there is no "msg" property, compensate somehow.
            //    }
            //}
           

            

            foreach (var item in datalist)
            {
                CityInfo city = new CityInfo();               
                city.CityName = item.city;

                //  ignore blank entries
                if(city.CityName == "")
                {
                    continue;
                }

                //  if city name already exists ignore adding a duplicate..for now
                if (CityDict.Count > 0)
                {
                    if (CityDict.Any(c => c.Key == city.CityName) == true)
                        continue;
                }
                city.CityAscii = item.city_ascii;
                city.Latitude = Convert.ToDouble(item.lat);
                city.Longitude = Convert.ToDouble(item.lng);
                city.Country = item.country;
                city.Province = item.admin_name;
                city.Capital = item.capital;
                city.Population = Convert.ToInt32(item.population);
                city.CityID = Convert.ToInt32(item.id);
                CityDict.Add(city.CityName, city);
            }

            return CityDict;

        }

        public string Serialize(Dictionary<string, CityInfo> dict)
        {
            StringBuilder sb = new StringBuilder();
            if (dict != null && dict.Count > 0)
            {                
                
                return JsonConvert.SerializeObject(dict, Formatting.Indented);               
            }
            return sb.ToString();
        }

    }
}
