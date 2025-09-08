with Ada.Text_IO; use Ada.Text_IO;
with Ada.Numerics.Discrete_Random;
with Ada.Strings.Unbounded; use Ada.Strings.Unbounded;
with Ada.Calendar; use Ada.Calendar;
with util_pkg; use util_pkg;
with rocket_stage_pkg; use rocket_stage_pkg;

procedure Main is
   Core_Stage : Stage_Access; --use in main
   Second_Stage : Stage_Access;

   Core_Stage_Tank : Propellant_Access; --use in main
   Second_Stage_Tank : Propellant_Access;

   Catastrophic_Failure : exception;

   task Flight_Computer;

   task body Flight_Computer is
      Failure_holder : Boolean;
   begin
      Gen_Failure.Init;

      --  Core_Stage_Tank := new Propellant_Access; --FALSE, IT'S NOT OBJECT TYPE BUT POINTER TYPE
      Core_Stage_Tank := new Propellant_Tank;
      Second_Stage_Tank := new Propellant_Tank;
      Core_Stage_Tank.all.Init(3400.0, 700.0);
      Second_Stage_Tank.all.Init(1600.0, 330.0);

      --  Core_Stage(33) := new Stage;--goofy ahh answer
      Core_Stage := new Stage(33);
      Core_Stage.Init("Super Heavy Booster", Core_Stage_Tank);

      --  for I in (10..1) loop --are you dumb?
      for I in reverse 1..10 loop
         delay 1.0;
         Printer.Print(Integer'Image(I));
      end loop;

      Core_Stage.Start_Engines;
      Printer.Print("Nous avons décollé!");

      while Core_Stage_Tank.all.Get_P_O >= 0.1 and Core_Stage_Tank.all.Get_P_RF>=0.1 and Gen_Failure.Failure = False loop
         delay 0.2;
         Failure_holder := Gen_Failure.Failure;
         Printer.Print("Core Engine remaining O2:" & Float'Image(Core_Stage_Tank.all.Get_P_O));
         Printer.Print("Core Engine remaining CH4:" & Float'Image(Core_Stage_Tank.all.Get_P_RF));
         if Failure_holder = True then
            Core_Stage.all.MECO;
            raise Catastrophic_Failure;
         end if;
      end loop;
      Core_Stage.all.MECO;
      delay 2.0;
      Printer.Print("Successful Stage Separation");

      Second_Stage := new Stage(6);
      Second_Stage.Init("Starship", Second_Stage_Tank);
      Second_Stage.Start_Engines;
      while Second_Stage_Tank.all.Get_P_O >= 0.1 and Second_Stage_Tank.all.Get_P_RF>=0.1 and Gen_Failure.Failure = False loop
         delay 0.2;
         Failure_holder := Gen_Failure.Failure;
         Printer.Print("Second Engine remaining O2:" & Float'Image(Second_Stage_Tank.all.Get_P_O));
         Printer.Print("Second Engine remaining CH4:" & Float'Image(Second_Stage_Tank.all.Get_P_RF));
         if Failure_holder = True then
            Second_Stage.all.MECO;
            raise Catastrophic_Failure;
         end if;
      end loop;
      Second_Stage.all.MECO;
      Printer.Print("Successful Orbit Separation");

   exception
      when Catastrophic_Failure => Printer.Print("BOOOMMMMM!!!!!! AAAAAAAAAAAAAAAAAAAAAAAAAAARRRRRRRRRRRRRGGGGGGGGGGGHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
   end Flight_Computer;

begin
   null;
end Main;
