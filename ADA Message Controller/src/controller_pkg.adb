with Ada.Text_IO;
use Ada.Text_IO;

package body Controller_pkg is

   procedure Create(Con : in out Controller; E : in Elem) is
      Success : Boolean; 
   begin
      cntrl_db.Insert_Elem(Con.DB, E, Success);
      if Success = False then
         Put_Line("Create controller.adb ERROR");
      else
         Put_Line("Create controller.adb OK");
      end if;
   end Create;
   
   procedure Read(Con : in Controller; K : in Key) is
      reade : Elem;
      readsucc : Boolean;
   begin
      cntrl_db.Get_Elem(Con.DB, K, reade, readsucc);
      
      if readsucc = False then
         Put_Line("Read controller.adb ERROR");
      else
         Put_Line("[Read controller.adb OK] --> elem: " & JSON(reade)); --todo json
      end if;
   end Read;
   
   procedure Update(Con : in out Controller; K : in Key; E : in Elem) is
      updatesucc : Boolean;
   begin
      cntrl_db.Update_Elem(Con.DB, K, E, updatesucc);
      
      if updatesucc = False then
         Put_Line("Update controller.adb ERROR");
      else
         Put_Line("Update controller.adb OK");
      end if;
   end Update;
   
   procedure Delete(Con : in out Controller; K : in Key) is
      delsucc : Boolean;
   begin
      cntrl_db.Delete_Elem(Con.DB, K, delsucc);

      
      if delsucc = False then
         Put_Line("Delete controller.db ERROR");
      else
         Put_Line("Delete controller.adb OK");
      end if;
   end Delete;
   
end Controller_pkg;
