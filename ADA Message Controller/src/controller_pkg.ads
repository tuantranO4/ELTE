with Ada.Text_IO;
use Ada.Text_IO;
with Database_pkg;

generic
   type Elem is private;      -- definite type
   type Key(<>) is private;   -- generic nonlimited (any) type
   with function Condition(K : Key; E : Elem) return Boolean;
   with function JSON(E : Elem) return String;

package Controller_pkg is
   
   type Controller(Max: Natural) is limited private;
     
   procedure Create(con : in out Controller; e : in Elem);
   procedure Read(con : in Controller; k : in Key);
   procedure Update(con : in out Controller; k : in Key; e: in Elem);
   procedure Delete(con : in out Controller; k : in Key);
   
private
   
   package cntrl_db is new Database_pkg(Elem, Key, Condition);
   
   --  type Elem_Array is array (Natural range <>) of Elem; --private arr implement
   
   type Controller(Max: Natural) is record
      --  Data : Elem_Array(0 .. Max);
      --  Curr : Natural := 0;--curr elem cnter (add = cnt++)
      DB : cntrl_db.Database(Max); --?
   end record;
   
end controller_pkg;
