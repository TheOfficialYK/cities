using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
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

        //public Dictionary<string, CityInfo> Deserialize()
        //{
        //    //  Read the file
        //    StreamReader sr = new StreamReader(FilePath);
        //    JSON_data = sr.ReadToEnd().Split('\n');
        //}

    }
}
