using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripAssistantSearchEngineApi
{
   public class DistanceCalculator:IDistanceCalculator
    {

        public double distance(string geoCode, string currentgeoCode,char unit)
        {
            double lat1=0,lon1 = 0,lat2=0,lon2=0;
            //double currentLongitude =0;
            string[] geoCodesvalues = currentgeoCode.Split(',');
            string[] currentgeoCodesvalues = geoCode.Split(',');

            lat1 = Convert.ToDouble(geoCodesvalues[0]);
            lon1 = Convert.ToDouble(geoCodesvalues[1]);

            lat2 = Convert.ToDouble(currentgeoCodesvalues[0]);
            lon2 = Convert.ToDouble(currentgeoCodesvalues[1]);

            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
