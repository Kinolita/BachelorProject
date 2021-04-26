using System;
using System.Collections.Generic;

namespace BachelorProject.Printers
{
    class PathPrint
    {
        public static void FinalPrint(List<SortedSet<int>> expandedElecList) {
            //printing the expended list
            foreach (var that in expandedElecList) {
                foreach (var smallThat in that) {
                    Console.Write(" " + smallThat);
                }
                Console.Write(",");
                Console.WriteLine();
            }
        }
    }
}
