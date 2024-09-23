using System;
using System.IO;
using TextFile;
using System.Collections.Generic;

namespace Planting
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string filename = "input.txt";
//textfile
                using (TextFileReader fileReader = new TextFileReader(filename))
                {
                    if (!fileReader.ReadLine(out string line))
                        throw new ArgumentException("The file cannot be empty.");

                    if (!int.TryParse(line, out int totalPlants) || totalPlants < 0)
                        throw new ArgumentException("Invalid number of plants specified.");//first line
                    List<Plant> plants = ReadPlants(fileReader, totalPlants);//data inbetween
                    if (!fileReader.ReadLine(out line) || !int.TryParse(line, out int totalDays) || totalDays <= 0)
                        throw new ArgumentException("Invalid number of days specified.");//last line
//textfile
                    Simulator sim = new Simulator(plants);
                    sim.Processing(totalDays);
                    sim.FinalResult();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static List<Plant> ReadPlants(TextFileReader fileReader, int totalPlants)
        {
        List<Plant> plants = new List<Plant>();
        for (int i = 0; i < totalPlants; i++)
        {
            if (!fileReader.ReadLine(out string plantData))
                throw new ArgumentException("Missing plant data, expected " + totalPlants + " entries.");
            string[] args = plantData.Trim().Split(' ');
            if (args.Length != 3 || string.IsNullOrWhiteSpace(args[0]))
                throw new ArgumentException("Incorrect plant data format or empty name is not accepted.");
            string plantName = args[0];
            string plantType = args[1];
            if (!int.TryParse(args[2], out int nutrient) || nutrient < 0 || string.IsNullOrWhiteSpace(args[2]))
                throw new ArgumentException("Nutrient level must exist or be non-negative.");
            //read arguments then pass t PlantInputSegregate
            plants.Add(PlantInputSegregate.CreatePlant(plantName, plantType, nutrient));
        }
        return plants;
        }
    }
    static class PlantInputSegregate{
       public static Plant CreatePlant(string name, string type, int nutrient)
        {
            return type switch
            {
                "wom" => new Wombleroot(name, nutrient),
                "wit" => new Wittentoot(name, nutrient),
                "wor" => new Woreroot(name, nutrient),
                _ => throw new ArgumentException("Invalid or non-exist plant type.")
            };
        }
    }
    
public class TextFileReader : IDisposable
{
    private StreamReader _reader;

    public TextFileReader(string filename)
    {
        _reader = new StreamReader(filename);
    }

    public bool ReadLine(out string line)
    {
        line = _reader.ReadLine();
        return line != null;
    }

    public void Dispose()
    {
        _reader?.Dispose();
    }
}
}


