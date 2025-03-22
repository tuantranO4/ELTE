with Ada.Text_IO;
use Ada.Text_IO;

package body Database_pkg is

  procedure Insert_Elem(DB : in out Database; E : in Elem; Success : out Boolean) is
  begin
      Success := False;
      if DB.Curr >= DB.Max then 
         Put_Line("ISSUE: Insert_Elem (cnt>max).");
         return;
      end if;	
      DB.Data(DB.Curr) := E;
      DB.Curr := DB.Curr + 1; 
      Success := True;
  end Insert_Elem;

   procedure Get_Elem(DB : in Database; K : in Key; E : out Elem; Success : out Boolean) is
   begin
   Success := False;
   for I in 0 .. DB.Curr - 1 loop
      if Condition(K, DB.Data(I)) then --todo Condition(...)
         E := DB.Data(I);
         Success := True;
         return;
      end if;
   end loop;
   end Get_Elem;


   procedure Update_Elem(DB : in out Database; K : in Key; E : in Elem; Success : out Boolean) is
   begin
      Success := False;
      for I in 0 .. DB.Curr - 1 loop
         if Condition(K, DB.Data(I)) then
            DB.Data(I) := E;
            Success := True;
            return;  
         end if;
      end loop;
   end Update_Elem;

   procedure Delete_Elem(DB : in out Database; K : in Key; Success : out Boolean) is
   begin
      Success := False;
   
      if DB.Curr = 0 then
         Put_Line("ISSUE: empty db delete_elem (curr == 0)");
         return;
      end if;
   
      for I in 0 .. DB.Curr - 1 loop
         if Condition(K, DB.Data(I)) then
            for J in I .. DB.Curr - 2 loop -- DB.Curr -1 doesnt work (out of bound J+1)
               DB.Data(J) := DB.Data(J + 1); --big brain! shift elem for delete!
            end loop;
            DB.Curr := DB.Curr - 1;
            Success := True;
            return;
         end if;
      end loop;
   end Delete_Elem;

end Database_pkg;
