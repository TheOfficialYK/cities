﻿using System;
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
        
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string CityAscii { get; set; }
        public int Population { get; set; }
        public string Province { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

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