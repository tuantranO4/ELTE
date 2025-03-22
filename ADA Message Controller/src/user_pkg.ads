with Ada.Strings.Fixed;  
with Ada.Text_IO;  
with Ada.Strings.Unbounded;

use Ada.Strings.Unbounded;
use Ada.Strings.Fixed;
use Ada.Text_IO;

package User_Pkg is

   type User is private;
   
   procedure Create_User(U : in out User; E : in String; N : in String; S : in String; A : in Natural);
   function Get_Mail(U : User) return String;
   function Get_Name(U : User) return String;
   function Get_Sur(U : User) return String;
   function Get_Age(U : User) return Natural;
   function User_String_JSON(U : User) return String; --json file maker

private

   type User is record --record: copied by default. assign one record to another, a full copy of value is made is made (instead of address
      Email: Unbounded_String;
      Name: Unbounded_String;
      Surname: Unbounded_String;
      Age: Natural;
   end record;

end User_Pkg;
