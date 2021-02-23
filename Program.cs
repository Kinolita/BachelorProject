using Newtonsoft.Json;
using System;
using System.IO;
using BachelorProject.Models.Dtos;
using System.Drawing;

namespace BachelorProject
{
    class Program
    {
        static void Main(string[] args) {
                 //Console.WriteLine("you are here: " + Directory.GetCurrentDirectory());
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\board4x3.json");
          
            var bsObj = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);

            Console.WriteLine("Info: " + bsObj.Information[0].PlatformName);
            Console.WriteLine("Number of Electrodes: " + bsObj.Electrodes.Count);
            Console.WriteLine("Position of out0: (" + bsObj.Outputs[0].PositionX + " " + bsObj.Outputs[0].PositionY + ")");






            // experiment setting the initial board size as a matrix
            string[,] ElectrodeArray = new string[10, 6];
            string drop1 = "Droplet 1";
            Point p1 = new Point(2, 3);
            ElectrodeArray[p1.X, p1.Y] = drop1;

            // Testing move
            Point p2 = new Point(2, 2);
            Point p3 = new Point(2, 13);
            Point p4 = new Point(-2, 3);
            Point p5 = new Point(2, 6);
            //Move(p1, p2);
            //Move(p1, p5);
            //Move(p4, p2);
        }
    }
}
