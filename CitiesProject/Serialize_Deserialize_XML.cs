using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesProject
{
    public class Serialize_Deserialize_XML
    {
        string FilePath;
        Dictionary<string,CityInfo> CityDict { get; set; }
        public Serialize_Deserialize_XML()
        {       
        }
        public Serialize_Deserialize_XML(string fname)
        {
            this.FilePath = fname;
            Deserialize();
        }
        public void Deserialize()
        {
            try
            {               
                System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(CityInfo));
                System.IO.StreamReader file = new System.IO.StreamReader(
                    FilePath);
                CityInfo person = (CityInfo)reader.Deserialize(file);
                
                file.Close();
               
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }
        
    }
}
