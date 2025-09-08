with Ada.Text_IO;
with Ada.Numerics.Float_Random;

package util_pkg is
   
  protected Printer is
     procedure Print(Outer: in String); --only in, no out
  end Printer;
  
  protected Gen_Failure is
     procedure Init;
     function Failure return Boolean; --without () for no params
  private
     G: Ada.Numerics.Float_Random.Generator;
  end Gen_Failure;
end util_pkg;
