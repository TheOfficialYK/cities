using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace CitiesProject
{
    //  Return CityInfo object list
     class Serialize_Deserialize_CSV
    {
        public Dictionary<string, CityInfo> CityDict = new Dictionary<string, CityInfo>();
        string FilePath { get; set; }
        string[] CSV_Data { get; set; }

        public Serialize_Deserialize_CSV(string fname)
        {
            this.FilePath = fname;
        }
        public Dictionary<string, CityInfo> Deserialize()
        {
            
            //  Read the file
            StreamReader sr = new StreamReader(FilePath);
            CSV_Data = sr.ReadToEnd().Split('\n');

            //  Map the data to CityInfo class and add to dictionary
            for (int i = 1; i < CSV_Data.Length-1; i++)
            {
               
                //  map to csv string
                var CurrentCity = CSV_Data[i];
                CityInfo city = new CityInfo();
                string[] CityDetails = CurrentCity.Split(',');
                city.CityName = CityDetails[0];

                //  if city name already exists ignore adding a duplicate..for now
                if(CityDict.Count > 0)
                {
                    if (CityDict.Any(c => c.Key == city.CityName) == true)
                        continue;
                }
                
                city.CityAscii = CityDetails[1];
                city.Latitude = Convert.ToDouble(CityDetails[2]);
                city.Longitude = Convert.ToDouble(CityDetails[3]);
                city.Province = CityDetails[5];
                city.Population = Convert.ToInt32(CityDetails[7]);
                city.CityID = Convert.ToInt32(CityDetails[8]);

                //  Add to dict
               
                CityDict.Add(city.CityName, city);                
            }
            return CityDict;
        }

    }
}
