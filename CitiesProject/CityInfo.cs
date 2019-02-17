using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesProject
{
    [Serializable]   
    public class CityInfo
    {
        //Properties       
        public string CityName { get; set; }
        public string CityAscii { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Capital { get; set; }              
        public int Population { get; set; }
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
