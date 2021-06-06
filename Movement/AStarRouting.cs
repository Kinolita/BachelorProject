using BachelorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using BachelorProject.Models.DmfElements;

namespace BachelorProject.Movement
{
    // inspired from https://dotnetcoretutorials.com/2020/07/25/a-search-pathfinding-algorithm-in-c/
    public class AStarRouting
    {
        public class Tile
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Cost { get; set; }           //G
            public int Distance { get; set; }       //H
            public int CostDistance => Cost + Distance; // G + H
            public Tile Parent { get; set; }
            public void SetDistance(int targetX, int targetY) {
                this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
            }

            public static List<Coord> AStar(Pixels[,] pixelBoard, Coord starting, Coord ending, Droplet drop) {
                List<Coord> pixelList = new List<Coord>();
                var start = new Tile {
                    X = starting.X,
                    Y = starting.Y
                };

                var finish = new Tile {
                    X = ending.X,
                    Y = ending.Y
                };
                start.SetDistance(finish.X, finish.Y);

                var activeTiles = new List<Tile> { start };
                var visitedTiles = new List<Tile>();

                static List<Tile> GetPossibleTiles(Pixels[,] pixelBoard, Tile currentTile, Tile targetTile, Droplet drop) {
                    var possibleTiles = new List<Tile>() {
                        new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                        new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                        new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                        new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 }, };

                    possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

                    var maxX = pixelBoard.GetLength(0);
                    var maxY = pixelBoard.GetLength(1);

                    return possibleTiles
                              .Where(tile => tile.X >= 0 && tile.X < maxX)
                              .Where(tile => tile.Y >= 0 && tile.Y < maxY)
                              .Where(tile => Blockages.DropletBubbleCheck(pixelBoard, new Coord(tile.X, tile.Y), drop))
                              .Where(tile => (pixelBoard[tile.X, tile.Y].Empty || pixelBoard[tile.X, tile.Y].BlockageType == drop.Id.ToString())
                                             || (tile.X, tile.Y) == (targetTile.X, targetTile.Y))
                              .ToList();
                }

                while (activeTiles.Any()) {
                    var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();
                    if (checkTile.X == finish.X && checkTile.Y == finish.Y) {
                        Console.Write("Destination found for ");
                        var tile = checkTile;
                        while (true) {
                            pixelList.Insert(0, new Coord(tile.X, tile.Y));
                            tile = tile.Parent;
                            if (tile != null) continue;
                            return pixelList;
                        }
                    }

                    visitedTiles.Add(checkTile);
                    activeTiles.Remove(checkTile);

                    var walkableTiles = GetPossibleTiles(pixelBoard, checkTile, finish, drop);

                    //When the tile has already been visited
                    foreach (var walkableTile in walkableTiles.Where(walkableTile => !visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))) {
                        //When the tile is already active 
                        if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y)) {
                            var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                            if (existingTile.CostDistance <= checkTile.CostDistance) continue;
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        } else {
                            //When the tile is completely new 
                            activeTiles.Add(walkableTile);
                        }
                    }
                }
                Console.Write("No Path Found for ");
                return pixelList;
            }
        }
    }
}