using System;
using System.Collections.Generic;
using System.IO;
using BachelorProject.Models.DmfElements;
using Newtonsoft.Json;

namespace BachelorProject.JSONCreation
{
    class Class1
    {
        public static void SampleBoard(string boardName, int xLength, int yLength, int elecSize) {
            var info1 = new Information {
                PlatformName = boardName,
                SizeX = xLength * elecSize,
                SizeY = yLength * elecSize
            };

            var allElectrodes = new List<Electrode>();
            for (int i = 0; i < (xLength * yLength); i++) {
                var electrodes = new Electrode() {
                    name = "el" + i,
                    ID = i,
                    electrodeID = i,
                    shape = 0,
                    positionX = i%xLength * elecSize,
                    positionY = Convert.ToInt32(i/yLength) * elecSize,
                    sizeX = elecSize,
                    sizeY = elecSize,
                    status = 0,
                    corners = null
                };
                allElectrodes.Add(electrodes);
            }

            Board newJson = new Board {
                Information = new List<Information> { info1 },
                Electrodes = allElectrodes
            };

            string stringJson = JsonConvert.SerializeObject((newJson));
            string path = Directory.GetCurrentDirectory() + @"\JSONBoards\" + boardName + ".json";
            using (TextWriter tw = new StreamWriter(path)) {
                tw.WriteLine(stringJson);
            }
        }
    }
}
