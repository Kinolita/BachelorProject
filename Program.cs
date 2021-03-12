using Newtonsoft.Json;
using System;
using System.IO;
using BachelorProject.Models.Dtos;
using BachelorProject.Models;
using BachelorProject.Scraps;
using BachelorProject.Movement;
using static BachelorProject.Scraps.PolygonPoints;
using System.Collections.Generic;
using static BachelorProject.Movement.AStarRouting;
using System.Linq;

namespace BachelorProject
{
    class Program
    {
        static void Main(string[] args) {
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\board4x3.json");
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\boardWithEverything.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\mazeBoard.json");

            var BoardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);
            Pixels[,] PixelBoard = Pixels.Create(BoardSpecs);

            ////Print functions for each element of the loaded board
            Information.PrintInformation(BoardSpecs);
            //Electrode.PrintElectrodes(BoardSpecs);
            //Actuator.PrintActuators(BoardSpecs);
            //Sensor.PrintSensors(BoardSpecs);
            //Input.PrintInputs(BoardSpecs);
            //Output.PrintOutputs(BoardSpecs);
            //Droplet.PrintDroplets(BoardSpecs);
            //Bubble.PrintBubbles(BoardSpecs);


            //Dummy navigation
            //Models.Coord starting = new Models.Coord(10, 15);
            //Models.Coord ending = new Models.Coord(70, 55);

            //testing custom polygon electrodes
            //Models.Coord starting = new Models.Coord(70, 58);
            //Models.Coord ending = new Models.Coord(10, 15);

            //testing navigation with droplet/bubble detection
            //Models.Coord starting = new Models.Coord(23, 10);
            //Models.Coord ending = new Models.Coord(70, 55);
            Models.Coord starting = new Models.Coord(50, 33);
            Models.Coord ending = new Models.Coord(23, 10);

            //A* demo (work in progress)
            //Models.Coord starting = new Models.Coord(70, 5);
            //Models.Coord ending = new Models.Coord(30, 15);


            PixelBoard[starting.x, starting.y].Vacancy = false;
            Console.WriteLine("Starting point: (" + starting.x + "," + starting.y + ")");
            Console.WriteLine("Ending point: (" + ending.x + "," + ending.y + ")");
            BasicRouting.FindPath(PixelBoard, starting, ending);


            /////////////////A* Routing////////////////////
            //C# code found at:
            //https://dotnetcoretutorials.com/2020/07/25/a-search-pathfinding-algorithm-in-c/

            var start = new Tile();
            start.X = starting.x;
            start.Y = starting.y;

            var finish = new Tile();
            finish.X = ending.x;
            finish.Y = ending.y;

            start.SetDistance(finish.X, finish.Y);

            var activeTiles = new List<Tile>();
            activeTiles.Add(start);
            var visitedTiles = new List<Tile>();


            static List<Tile> GetWalkableTiles(Pixels[,] PixelBoard, Tile currentTile, Tile targetTile) {
                var possibleTiles = new List<Tile>()
                {
                new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 }, };

                possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

                var maxX = PixelBoard.GetLength(0);
                var maxY = PixelBoard.GetLength(1);

                return possibleTiles
                        .Where(tile => tile.X >= 0 && tile.X < maxX)
                        .Where(tile => tile.Y >= 0 && tile.Y < maxY)
                        .Where(tile => PixelBoard[tile.X, tile.Y].Vacancy == true || (tile.X, tile.Y) == (targetTile.X, targetTile.Y))
                        .ToList();
            }

            //This is where we created the map from our previous step etc. 
            while (activeTiles.Any()) {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y) {
                    Console.WriteLine("Destination found using A*");
                    //We can actually loop through the parents of each tile to find our exact path which we will show shortly. 

                    //We found the destination and we can be sure (Because the the OrderBy above)
                    //That it's the most low cost option. 
                    var tile = checkTile;
                    Console.WriteLine("Retracing steps backwards...");
                    while (true) {
                        Console.Write($"({tile.X},{tile.Y})  ");
                        tile = tile.Parent;
                        if (tile == null) {
                            Console.WriteLine("Done!");
                            return;
                        }
                    }
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = GetWalkableTiles(PixelBoard, checkTile, finish);

                foreach (var walkableTile in walkableTiles) {
                    //We have already visited this tile so we don't need to do so again!
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;

                    //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                    if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y)) {
                        var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > checkTile.CostDistance) {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }
                    } else {
                        //We've never seen this tile before so add it to the list. 
                        activeTiles.Add(walkableTile);
                    }
                }
            }
            Console.WriteLine("No Path Found!");


            /////////////////Polygon Point finder////////////////////
            //Coord[] polygon1 = { new Coord(0, 0), new Coord(10, 0), new Coord(10, 10), new Coord(0, 10) };
            //int n = polygon1.Length;
            //Coord p = new Coord(20, 20);
            //if (PolygonPoints.IsInside(polygon1, n, p)) {
            //    Console.WriteLine("Yes");
            //} else {
            //    Console.WriteLine("No");
            //}
            //p = new Coord(5, 5);
            //if (PolygonPoints.IsInside(polygon1, n, p)) {
            //    Console.WriteLine("Yes");
            //} else {
            //    Console.WriteLine("No");
            //}
            //Coord[] polygon2 = { new Coord(0, 0), new Coord(5, 5), new Coord(5, 0) };
            //p = new Coord(3, 3);
            //n = polygon2.Length;
            //if (PolygonPoints.IsInside(polygon2, n, p)) {
            //    Console.WriteLine("Yes");
            //} else {
            //    Console.WriteLine("No");
            //}
            //p = new Coord(5, 1);
            //if (PolygonPoints.IsInside(polygon2, n, p)) {
            //    Console.WriteLine("Yes");
            //} else {
            //    Console.WriteLine("No");
            //}
            //p = new Coord(8, 1);
            //if (PolygonPoints.IsInside(polygon2, n, p)) {
            //    Console.WriteLine("Yes");
            //} else {
            //    Console.WriteLine("No");
            //}
            //Coord[] polygon3 = { new Coord(0, 0), new Coord(10, 0), new Coord(10, 10), new Coord(0, 10) };
            //p = new Coord(-1, 10);
            //n = polygon3.Length;
            //if (PolygonPoints.IsInside(polygon3, n, p)) {
            //    Console.WriteLine("Yes");
            //} else {
            //    Console.WriteLine("No");
            //}
        }
    }
}
