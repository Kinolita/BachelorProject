using BachelorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BachelorProject.Movement
{
    public class AStarRouting
    {
        public class Tile
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Cost { get; set; }
            public int Distance { get; set; }
            public int CostDistance => Cost + Distance;
            public Tile Parent { get; set; }

            //The distance is essentially the estimated distance, ignoring walls to our target. 
            //So how many tiles left and right, up and down, ignoring walls, to get there. 
            public void SetDistance(int targetX, int targetY) {
                this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
            }


            //C# code found at:
            //https://dotnetcoretutorials.com/2020/07/25/a-search-pathfinding-algorithm-in-c/

            public static void AStar(Pixels[,] PixelBoard, Tile start, Tile finish) {
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
            }

        }
    }
}
