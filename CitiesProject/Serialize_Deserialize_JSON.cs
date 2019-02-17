using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
namespace CitiesProject
{
    
    public class Serialize_Deserialize_JSON
    {
        string FilePath;
        Dictionary<string, CityInfo> CityDict { get; set; }
        public Serialize_Deserialize_JSON(string fname)
        {
            FilePath = fname;
        }

    }
}
