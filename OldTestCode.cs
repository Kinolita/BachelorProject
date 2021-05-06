
namespace BachelorProject
{
    class OldTestCode
    {
        //These are lines used to test different aspects of the project as it developed

        ////Print functions for each element of the loaded board
        //Information.PrintInformation(boardSpecs);
        //Electrode.PrintElectrodes(boardSpecs);
        //Actuator.PrintActuators(boardSpecs);
        //Sensor.PrintSensors(boardSpecs);
        //Input.PrintInputs(boardSpecs);
        //Output.PrintOutputs(boardSpecs);
        //Droplet.PrintDroplets(boardSpecs);
        //Bubble.PrintBubbles(boardSpecs);
        //Board.PrintBoard(pixelBoard1);


        //Dummy navigation on board4x3
        //Models.Coord starting = new Models.Coord(10, 15);
        //Models.Coord ending = new Models.Coord(70, 55);

        //testing custom polygon electrodes on boardWithEverything
        //Models.Coord starting = new Models.Coord(70, 58);
        //Models.Coord ending = new Models.Coord(10, 15);

        //testing navigation with droplet/bubble detection on boardWithEverything
        //one is successful and the other is not as the routing is very primitive at this point
        //Models.Coord starting = new Models.Coord(23, 10);
        //Models.Coord ending = new Models.Coord(70, 55);
        //Models.Coord starting = new Models.Coord(50, 33);
        //Models.Coord ending = new Models.Coord(23, 10);

        //A* routing on mazeBoard
        //Models.Coord starting = new Models.Coord(70, 5);
        //Models.Coord ending = new Models.Coord(30, 15);

        //A* routing on board10x10Maze
        //Models.Coord starting = new Models.Coord(5, 25);
        //Models.Coord ending = new Models.Coord(95, 65);

        //Console.WriteLine("Starting point: (" + starting.X + "," + starting.Y + ")");
        //Console.WriteLine("Ending point: (" + ending.X + "," + ending.Y + ")");
        //PixelElectrodeConversion.FindPath(pixelBoard1, starting, ending);
        //Tile.AStar(pixelBoard1, starting, ending);

        //testing input with electrodes vs coordinates or a mix
        //int startElec = 20;
        //int endElec = 69;
        //InputHandler.CheckInputType(pixelBoard1, startElec, endElec);
        //InputHandler.CheckInputType(pixelBoard1, starting, ending);
        //InputHandler.CheckInputType(pixelBoard1, starting, endElec);

        //combining buffer with routing droplets
        //int startElec = 0;
        //int endElec = 99;
        //int dropletSize = 15;
        //Buffers.AddBuffer(pixelBoard1, dropletSize);
        //InputHandler.CheckInputType(pixelBoard1, startElec, endElec);
        //Buffers.RemoveBuffer(pixelBoard1);
        //dropletSize = 5;
        //Buffers.AddBuffer(pixelBoard1, dropletSize);
        //InputHandler.CheckInputType(pixelBoard1, startElec, endElec);

    }
}
