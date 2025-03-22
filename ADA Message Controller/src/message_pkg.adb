with Ada.Strings.Unbounded;
with Ada.Text_IO;
with User_pkg;
with message_pkg;

use message_pkg;
use User_pkg;
use Ada.Strings.Unbounded;
use Ada.Text_IO;

package body message_pkg is

   procedure Create_Message(M : out Message; Id : Integer; Txt : String; Time: String; Usent: User; Urec: User) is
   begin
      M.ID := Id;
      M.Text := To_Unbounded_String(Txt);
      M.Timestamp := To_Unbounded_String(Time);
      M.User_Sent := Usent; --side note:value copy (not reference copy)
      M.User_Received := Urec;
   end Create_Message;

   function Get_id(M : Message) return Integer is
   begin
      return M.ID;
   end Get_id;

   function Get_txt(M : Message) return String is
   begin
      return To_String(M.Text);
   end Get_txt;

   function Get_time(M : Message) return String is
   begin
      return To_String(M.Timestamp);
   end Get_time;

   function Get_Usent(M : Message) return User is
   begin
      return M.User_Sent;
   end Get_Usent;

   function Get_Urec(M : Message) return User is
   begin
      return M.User_Received;
   end Get_Urec;

   function Msg_String_JSON(M : Message) return String is
begin
   return "{""id"": " & Integer'Image(M.ID) & "," &
          """text"": """ & To_String(M.Text) & """," &
          """timestamp"": """ & To_String(M.Timestamp) & """," &
          """user_sent"": " & User_String_JSON(M.User_Sent) & "," &
          """user_received"": " & User_String_JSON(M.User_Received) & "}";
end Msg_String_JSON;


end message_pkg;
