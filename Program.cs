using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace BachelorProject
{
    class Program
    {
        static void Main(string[] args) {
            /*
             * move - current location, new location
             * input - size, source
             * output - target 
             * merge - # of droplets (locations?)
             * split - # of droplets (locations? vert or horiz)
             * temp - temp, duration
             * sense - position of sensor
             */                 

            // read JSON directly from a file
            // gonna use Json.NET library for serialization/deserialization
            string theStringToEndAllStrings = File.ReadAllText(@"C:\Users\adrie\source\repos\BachelorProject\Moves.json");
            SDS bsObj = JsonConvert.DeserializeObject<SDS>(theStringToEndAllStrings);
            Console.WriteLine(theStringToEndAllStrings);
            Console.WriteLine(bsObj.input);
            Console.WriteLine(bsObj.move[0] + " " + bsObj.move[1]);


            // setting the initial board size as a matrix
            string[,] electrodeArray = new string[10, 6];
            string drop1 = "Droplet 1";
            Point p1 = new Point(2, 3);
            electrodeArray[p1.X, p1.Y] = drop1;


            // Input/Output list
            List<Inputs> inputs = new List<Inputs>();
            inputs.Add(new Inputs() { InputName = "A", InputLocation = electrodeArray[0, 0] });
            inputs.Add(new Inputs() { InputName = "B", InputLocation = electrodeArray[6, 0] });
            List<Outputs> outputs = new List<Outputs>();
            outputs.Add(new Outputs() { OutputName = "Z", OutputLocation = electrodeArray[9, 5] });

            // Testing move
            Point p2 = new Point(2, 2);
            Point p3 = new Point(2, 13);
            Point p4 = new Point(-2, 3);
            Point p5 = new Point(2, 6);
            Move(p1, p2);
            Move(p1, p5);
            Move(p4, p2);
        }

        public class Inputs
        {
            public string InputName { get; set; }
            public string InputLocation { get; set; }
        }
        public class Outputs
        {
            public string OutputName { get; set; }
            public string OutputLocation { get; set; }
        }

        public class SDS
        {
            public string input { get; set; }
            public int[] move { get; set; }
            public string operation { get; set; }
            public string[] name { get; set; }
            public string[] steps { get; set; }
        }

        public static void Move(Point current, Point newPos) {
            if (!Validate(current) || !Validate(newPos)) {
                Console.WriteLine("One of the points was outside the board: " + current + " + " + newPos);
            } else {
                Point temp = new Point();
                temp.X = current.X + newPos.X;
                temp.Y = current.Y + newPos.Y;
                if (!Validate(temp)) {
                    Console.WriteLine("The new position is not valid: " + temp);
                } else {
                    current = temp;
                    Console.WriteLine("New position is: " + current);
                }
            }
        }

        public static bool Validate(Point check) {
            if (check.X <= 10 && check.Y <= 6 && check.X >= 0 && check.Y >= 0) {
                return true;
            }
            return false;
        }
    }
}
