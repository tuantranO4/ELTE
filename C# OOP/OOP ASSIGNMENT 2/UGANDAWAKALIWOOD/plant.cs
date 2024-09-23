using System;

namespace Planting{
    //abstract apply
    public abstract class Plant{
        public string Name { get; set; }
        public int Nutrient { get; set; }

        public bool Alive{
        get{return Nutrient > 0;}
        }
       protected iRadioReact TreeReact;

        protected Plant (string name, int nutrient, iRadioReact react){ 
            Name=name;
            Nutrient=nutrient;
            TreeReact=react; //interface for each different radio reaction here. 
        }

        protected virtual void NutrientReset(){ //override this shit boyo for Wombleroot (>10 = dead)
            if (Nutrient <= 0){Nutrient=0;}
        }
        public virtual void Demand(DayStat daystat){ } //override this shit also
 
        public void NutritionChange(int amount){ //nutrition plusing for environment (todo: switch case for interface react)
            Nutrient+=amount;
        }
        
        public void reactToRadType (RadiationType rad){
            TreeReact.Reaction(this,rad);
            NutrientReset();
        }

    }

    public class Wombleroot : Plant{
        public Wombleroot(string name, int nutrient) : base(name, nutrient, new WomblerootReaction()){}

        public override void Demand(DayStat daystat)
        {
        if (Alive)
        {
            daystat.AddAlpha(10);
        }
        
        }

        protected override void NutrientReset() {
        base.NutrientReset();
        if (Nutrient > 10) {Nutrient = 0;}
        }
        
    }

    public class Wittentoot : Plant{
        public Wittentoot(string name, int nutrient) : base(name, nutrient, new WittentootReaction()){}

        public override void Demand(DayStat daystat)
        {
        if (Alive && Nutrient <= 10){
            if (Nutrient < 5){
                daystat.AddDelta(4);
            }else{
                daystat.AddDelta(1);
            }
        }
        }
    }

    public class Woreroot: Plant{
        public Woreroot(string name, int nutrient) : base(name, nutrient, new WorerootReaction()){}
        public override void Demand(DayStat daystat){ }//doesnt influence radio the next day ->empty
    
    }
}