namespace BachelorProject.Models.DmfElements
{
    public class Bubble
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public static void PrintBubbles(Board specs) {
            int len = specs.Bubbles.Count;
            System.Console.WriteLine("Number of bubbles on board: " + len);
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Droplet Name: " + specs.Bubbles[i].Name);
                System.Console.WriteLine("Droplet Id: " + specs.Bubbles[i].Id);
                System.Console.WriteLine("Droplet Position: (" + specs.Bubbles[i].PositionX + "," + specs.Bubbles[i].PositionY + ")");
                System.Console.WriteLine("Droplet Size: " + specs.Bubbles[i].SizeX + " by " + specs.Bubbles[i].SizeY);
            }
            System.Console.WriteLine();
        }
    }
}
