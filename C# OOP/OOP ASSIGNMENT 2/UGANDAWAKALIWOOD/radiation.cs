using System;

namespace Planting{
    public enum RadiationType{None, Alpha, Delta}

    public class DayStat{
        public int alphaReq{get;  set;}
        public int deltaReq{get; set;}

        public void AddAlpha(int amount)
        {
            alphaReq += amount;
        }   
        public void AddDelta(int amount) {deltaReq += amount;}
        
        //change of radiation based on difference between alpha and delta
        public RadiationType RadioOfNextDay(){
        if (alphaReq - deltaReq >= 3)
            return RadiationType.Alpha;
        else if (deltaReq - alphaReq >= 3)
            return RadiationType.Delta;
        else
        return RadiationType.None;
        }
    }
}

