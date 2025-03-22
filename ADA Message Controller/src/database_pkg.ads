with Ada.Text_IO;
use Ada.Text_IO;

generic
   type Elem is private;      --definite type
   type Key(<>) is private;       --generic nonlimited (any) type
   with function Condition(K : Key; E : Elem) return Boolean;  -- searching (get_elem) only btw

package Database_pkg is

   type Database(Max: Natural) is limited private;

   --NOTE: all must have output param called SUCCESS (Boolean, True: succeed, False: When db is full AND/OR elem not found when search)
   procedure Insert_Elem(DB : in out Database; E : in Elem; Success : out Boolean);
   procedure Get_Elem(DB : in Database; K : in Key; E : out Elem; Success : out Boolean);
   procedure Update_Elem(DB : in out Database; K : in Key; E : in Elem; Success : out Boolean);
   procedure Delete_Elem(DB : in out Database; K : in Key; Success : out Boolean);

private

   type Elem_Array is array (Natural range <>) of Elem; --private arr implement
                                                        
   type Database(Max: Natural) is record
      Data : Elem_Array(0 .. Max);  
      Curr : Natural := 0;--curr elem cnter (add = cnt++)
   end record;
 
end Database_pkg;
