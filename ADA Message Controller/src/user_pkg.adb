with Ada.Strings.Unbounded;
with Ada.Text_IO;
with Ada.Strings.Fixed;  

use Ada.Strings.Unbounded;
use Ada.Text_IO;
use Ada.Strings.Fixed;


package body User_Pkg is

   procedure Create_User(U : in out User; E : in String; N : in String; S : in String; A : in Natural) is
   begin
      U.Email := To_Unbounded_String(E);
      U.Name := To_Unbounded_String(N);
      U.Surname := To_Unbounded_String(S);
      U.Age := A;
   end Create_User;

   function Get_Mail(U : User) return String is
   begin
      return To_String(U.Email);
   end Get_Mail;

   function Get_Name(U : User) return String is
   begin
      return To_String(U.Name);
   end Get_Name;

   function Get_Sur(U : User) return String is
   begin
      return To_String(U.Surname);
   end Get_Sur;

   function Get_Age(U : User) return Natural is
   begin
      return U.Age;
   end Get_Age;

   function User_String_JSON(U : User) return String is
   begin
      return "{""email"":""" & To_String(U.Email) & 
             """,""name"":""" & To_String(U.Name) &
             """,""surname"":""" & To_String(U.Surname) &
             """,""age"":" & Natural'Image(U.Age) & "}";
   end User_String_JSON;

end User_Pkg;
