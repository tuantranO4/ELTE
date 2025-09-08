package body rocket_stage_pkg is
  
  protected body Propellant_Tank is
     procedure Init(O, RF: in Float) is
        begin
        Max_O := O;
        Current_O := O;
        Max_RF := RF;
        Current_RF := RF;
     end Init;
     
     procedure D_O(T: in Float) is
     begin
        Current_O := Current_O - 4.85*T;
     end D_O;
     
     procedure D_RF(T: in Float) is
     begin
        Current_RF := Current_RF - T;
     end D_RF;
     
     function Get_P_O return Float is
     begin
        return (Current_O/Max_O);
     end Get_P_O;
     
     function Get_P_RF return Float is
     begin
        return (Current_RF/Max_RF);
     end Get_P_RF;
  end Propellant_Tank;

  task body Raptor_Engine is
     raptor_stage_name : String(1..25) := (others => ' '); --unconstrained sybtype arent allowed -> need constraints 
     raptor_eng_cnt : Integer;
     raptor_propel: Propellant_Access;
     
     --sect for time elapse
      Last_Time : Time;
      Current_Time : Time;
      Elapsed_Time : Duration;
      
  begin
     accept Init(stage_name : in String; engine_cnt : in Integer; propel : in Propellant_Access) do
        raptor_stage_name(1..stage_name'Length) := stage_name;
        raptor_eng_cnt := engine_cnt;
        raptor_propel := propel;
     end Init;
     
     Last_Time := Clock; --time elapsed
     
     loop --NOTE: Loop keeps select alive and responsive until Engine_Shut_Off is called
        select
           accept Engine_Start do
              Printer.Print(raptor_stage_name & "engine" &Integer'Image(raptor_eng_cnt) &" is starting...");
              Last_Time := Clock;
           end Engine_Start;
        or
           delay 0.1;
           --Putain souviens-toi du RAPTOR_PROPEL et pas de PROPELLANT_ACCESS
            Current_Time := Clock;
            Elapsed_Time := Current_Time - Last_Time;
            
            raptor_propel.D_O(Float(Elapsed_Time));
            raptor_propel.D_RF(Float(Elapsed_Time)); 
            
            Last_Time := Current_Time; --reserve for next operation
        or
           accept Engine_Shut_Off do
              Printer.Print(raptor_stage_name & " engine " &Integer'Image(raptor_eng_cnt) &" is shut off.");
           end Engine_Shut_Off;
           exit;
        end select;
     end loop;
  end Raptor_Engine;

  task body Stage is --wrong: Stage(Num_Engines: Natural) in task declare -> syntax
     Stage_Name : String(1..25) := (others =>' ');
     stage_propel : Propellant_Access;
     Engine_Arr : array(1..Num_Engines) of Raptor_Engine;
  begin --exec section, DONT DECLARE SHIT HERE UNDER ANY CIRCUMSTANCE
     --  Engine_Arr : array(1..Num_Engines) of Raptor_Engine; ---> false
     accept Init (stg_name : in String; propel: in Propellant_Access) do
        Stage_Name(1..stg_name'Length) := stg_name;
        stage_propel := propel;
        
        for I in Engine_Arr'Range loop
           Engine_Arr(I).Init(stg_name, I, propel);
        end loop;
     end Init;
     loop
        select
           accept Start_Engines  do
              Printer.Print("Main engines on " & Stage_Name & " starting...");
              for I in Engine_Arr'Range loop
                 Engine_Arr(I).Engine_Start;
              end loop;
           end Start_Engines;
        or
           accept MECO do
              for I in Engine_Arr'Range loop
                 Engine_Arr(I).Engine_Shut_Off;
              end loop;
              Printer.Print("Main engines on " & Stage_Name & " shut down.");
            end MECO;
            exit;-- Then exit from the loop
        or
           terminate; --"Finally, the Stage continually runs and either waits for MECO to be called or it terminates when no other tasks are running anymore."
        end select;
     end loop;
  end Stage;
  
end rocket_stage_pkg;
