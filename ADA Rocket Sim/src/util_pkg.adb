with Ada.Text_IO; use Ada.Text_IO;
with Ada.Numerics.Float_Random;
with Ada.Calendar; use Ada.Calendar;

package body util_pkg is

   protected body Printer is --entire program where Ada.Put_Line should be used
   procedure Print(Outer: in  String) is
      begin
         Put_Line(Outer);
      end Print;
   end Printer;
   
   protected body Gen_Failure is --. The Boolean is determined to be true if the generated random number is less than 0.0001.
      procedure Init is 
      begin
         Ada.Numerics.Float_Random.Reset(G); --why we need long ahh package name for this to run? but atp it's worthless to fix ts (pmo icl) 
      end Init;
      
      function Failure return Boolean is --Failure without () for no identifier
         Result: Float;
         Failed: Boolean;
      begin
         Result := Ada.Numerics.Float_Random.Random(G);
         if Result < 0.0001 then
         Failed := True;
         else
         Failed := False;
         end if;
         return Failed;
      end Failure;
   end Gen_Failure;
    begin
   null;
end util_pkg;
