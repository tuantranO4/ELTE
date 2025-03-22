with Ada.Strings.Fixed;  
with Ada.Text_IO;  
with Ada.Strings.Unbounded;
with User_Pkg;

use User_Pkg;
use Ada.Strings.Unbounded;
use Ada.Strings.Fixed;
use Ada.Text_IO;

package message_pkg is

   type Message is private;
   
   procedure Create_Message(M : out Message; Id : Integer; Txt : String; Time: String; Usent: User; Urec: User);
   function Get_id(M : Message) return Integer;
   function Get_txt(M : Message) return String;
   function Get_time(M : Message) return String;
   function Get_Usent(M : Message) return User;
   function Get_Urec(M : Message) return User;
   function Msg_String_JSON(M : Message) return String; --JSON
   
private
   
   type Message is record
      ID: Integer;
      Text: Unbounded_String;
      Timestamp: Unbounded_String;
      User_Sent: User;
      User_Received: User;
   end record;

end message_pkg;
