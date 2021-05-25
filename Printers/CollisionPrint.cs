using System;
using System.Collections.Generic;
using System.Linq;

namespace BachelorProject.Printers
{
    class CollisionPrint
    {
        public static void PrintCollisions(Dictionary<(string, string), List<int>> collisionsDictionary) {
            if (!collisionsDictionary.Any()) {
                Console.WriteLine("There are no collisions for this routing.");
            } else {
                Console.WriteLine("Path collisions are: ");
                foreach (var (keyTuple, value) in collisionsDictionary) {
                    Console.Write(keyTuple + " collide on electrodes: ");
                    foreach (var electrodeInt in value) {
                        Console.Write(electrodeInt + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
