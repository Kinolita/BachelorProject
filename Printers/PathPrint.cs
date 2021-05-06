using System;
using System.Collections.Generic;

namespace BachelorProject.Printers
{
    class PathPrint
    {
        public static void FinalPrint(List<SortedSet<int>> expandedElecList) {
            //printing the expended list
            foreach (var stepList in expandedElecList) {
                foreach (var step in stepList) {
                    Console.Write(" " + step);
                }
                Console.Write(",");
                Console.WriteLine();
            }
        }
    }
}
