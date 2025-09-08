with Ada.Text_IO; use Ada.Text_IO;
with Ada.Numerics.Float_Random;
with Ada.Strings.Unbounded; use Ada.Strings.Unbounded;
with Ada.Calendar; use Ada.Calendar;
with util_pkg; use util_pkg;

package rocket_stage_pkg is

  protected type Propellant_Tank is --PROTECTED TYPE! NOT PROTECTED (only)
     procedure Init(O, RF: in Float);
     procedure D_O(T: in Float);
     procedure D_RF(T: in Float);
     function Get_P_O return Float;
     function Get_P_RF return Float; 
     private
     Max_O :Float := 0.0;
     Max_RF :Float := 0.0;
     Current_O :Float := 0.0;
     Current_RF :Float := 0.0;
  end Propellant_Tank;
  
  type Propellant_Access is access Propellant_Tank;
  --  Core_Stage_Tank : Propellant_Access; --use in main
  --  Second_Stage_Tank : Propellant_Access;

  task type Raptor_Engine is
     entry Init(stage_name : in String; engine_cnt : in Integer; propel : in Propellant_Access);
     entry Engine_Start;
     entry Engine_Shut_Off;
  end Raptor_Engine;
  
--  Engine_Arr: array(Natural range<> ) of Raptor_Engine;
  type Engine_Array is array(Natural range <>) of Raptor_Engine;

  task type Stage(Num_Engines: Natural) is
     entry Init(stg_name: in String; propel : in Propellant_Access);
     entry Start_Engines;
     entry MECO;
  end Stage;
  
  type Stage_Access is access all Stage;
  --  Core_Stage : Stage_Access; --use in main
  --  Second_Stage : Stage_Access;
  
end rocket_stage_pkg;
