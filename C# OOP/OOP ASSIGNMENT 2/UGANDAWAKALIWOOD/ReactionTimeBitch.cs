using System;
using System.Collections.Generic;
using System.Text;

namespace Planting{ 
     public interface iRadioReact
    {
        void Reaction(Plant plant, RadiationType rad);
    }

 public class WomblerootReaction : iRadioReact
 {
     public void Reaction(Plant plant, RadiationType rad)
     {
         switch (rad)//switch: use for each radiation type that change the Nutrition level of whatever tf the plant is 
         {
             case RadiationType.Alpha: 
                 plant.NutritionChange(2);
                 break;
             case RadiationType.Delta:
                 plant.NutritionChange(-2);
                 break;
             case RadiationType.None:
                 plant.NutritionChange(-1);
                 break;
         }
     }
 }

 public class WittentootReaction : iRadioReact
 {
     public void Reaction(Plant plant, RadiationType rad)
     {
         switch (rad)
         {
             case RadiationType.Alpha:
                 plant.NutritionChange(-3);
                 break;
             case RadiationType.Delta:
                 plant.NutritionChange(4);
                 break;
             case RadiationType.None:
                 plant.NutritionChange(-1);
                 break;
         }
     }
 }

 public class WorerootReaction : iRadioReact
 {
     public void Reaction(Plant plant, RadiationType rad)
     {
         switch (rad)
         {
             case RadiationType.Alpha:
                plant.NutritionChange(1);
                break;
             case RadiationType.Delta:
                plant.NutritionChange(1);
                break;
             case RadiationType.None:
                plant.NutritionChange(-1);
                break;
         }
     }
 }

 public class Simulator{
    protected readonly List<Plant> plants;
    public Simulator(List<Plant> plants){
        this.plants= plants;
    }
    public void Processing(int days) {
    List<RadiationType> radios = new List<RadiationType> { RadiationType.None }; // set none as default
    int day = 0;
    while (day < days) {
        DayStat now = new DayStat();
        for (int i = 0; i < plants.Count; i++) {
            if (plants[i].Alive) {
                RadiationType curr = radios.Last(); //Last(), using on the list to access recent radiation
                plants[i].reactToRadType(curr);
                plants[i].Demand(now);
            }
        }
        RadiationType nextDay = now.RadioOfNextDay(); //this to change based on the trees and the gaps, append to the list
        radios.Add(nextDay); 
        Statistics(day, now, nextDay);
        day++; 
    }
}

 //the daily line
    private void Statistics(int day, DayStat now, RadiationType rad){
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Day {day + 1} stats \nAlpha amount: {now.alphaReq}, Delta amount: {now.deltaReq}");
        foreach (var plant in plants){
            sb.AppendLine($"{plant.Name} ({plant.GetType().Name}): Nutrient Level - {plant.Nutrient}, Alive - {plant.Alive}");
        }   
        sb.AppendLine($"Current radiation: {rad}\n");
        Console.WriteLine(sb.ToString());
    }


    //the final line
    public  void FinalResult()
    {
    Plant strongest = null;
    foreach (var plant in plants){
        if (plant.Alive){ //checking for alive status (nutrition>0)

            if (strongest == null || plant.Nutrient > strongest.Nutrient)
            {
                strongest = plant; 
            }
        }
    }

    if (strongest != null){
        Console.WriteLine($"The strongest plant: {strongest.Name}, Nutrient level: {strongest.Nutrient}.");
    }else{
        Console.WriteLine("All died.");
    }
    }

 }
}

