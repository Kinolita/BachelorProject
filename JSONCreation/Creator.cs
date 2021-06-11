using System;
using System.Collections.Generic;
using System.IO;
using BachelorProject.Models.DmfElements;
using Newtonsoft.Json;

namespace BachelorProject.JSONCreation
{
    class Creator
    {
        //method to automatically generate JSON files of any specified dimension and pixel density
        public static void SampleBoard(string boardName, int xLength, int yLength, int elecSize) {
            var info1 = new Information {
                PlatformName = boardName,
                SizeX = xLength * elecSize,
                SizeY = yLength * elecSize
            };

            var allElectrodes = new List<Electrode>();
            for (var i = 0; i < (xLength * yLength); i++) {
                var electrodes = new Electrode() {
                    name = "el" + i,
                    ID = i,
                    electrodeID = i,
                    shape = 0,
                    positionX = i % xLength * elecSize,
                    positionY = Convert.ToInt32(i / xLength) * elecSize,
                    sizeX = elecSize,
                    sizeY = elecSize,
                    status = 0,
                    corners = null
                };
                allElectrodes.Add(electrodes);
            }

            var newJson = new Board {
                Information = new List<Information> { info1 },
                Electrodes = allElectrodes
            };

            var stringJson = JsonConvert.SerializeObject((newJson));
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
            var path2 = path + @"\JSONBoards\" + boardName + ".json";
            using TextWriter tw = new StreamWriter(path2);
            tw.WriteLine(stringJson);
        }
    }
}
