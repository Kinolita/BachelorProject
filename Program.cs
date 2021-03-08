using Newtonsoft.Json;
using System;
using System.IO;
using BachelorProject.Models.Dtos;
using BachelorProject.Models;
using BachelorProject.Movement;

namespace BachelorProject
{
    class Program
    {
        static void Main(string[] args) {
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\board4x3.json");
            var BoardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);
            Pixels[,] PixelBoard = Pixels.Create(BoardSpecs);

            //have not yet set initial vacancy for start position
            Coord starting = new Coord(74, 52);
            Coord ending = new Coord(3, 7);

            //finding something on the board
            //should i search through pixels to find one with the right label?
            //should i expand the pixelboard to basically carry all the information from the json file?
                    //(Droplet presence, Sensor presence, Input/Output presence))

            PixelMapping.FindPath(PixelBoard, starting, ending);

            ////Print functions for each element of the loaded board
            Information.PrintInformation(BoardSpecs);
            //Electrode.PrintElectrodes(BoardSpecs);
            //Actuator.PrintActuators(BoardSpecs);
            //Sensor.PrintSensors(BoardSpecs);
            //Input.PrintInputs(BoardSpecs);
            //Output.PrintOutputs(BoardSpecs);
            //Droplet.PrintDroplets(BoardSpecs);
            //Bubble.PrintBubbles(BoardSpecs);


        }
    }
}
