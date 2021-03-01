namespace BachelorProject.Models.Dtos
{
    public class Bubble
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public static void PrintBubbles(BachelorProject.Models.Dtos.Board Specs) {
            int len = Specs.Bubbles.Count;
            System.Console.WriteLine("Number of bubbles on board: " + len);
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Droplet Name: " + Specs.Bubbles[i].Name);
                System.Console.WriteLine("Droplet ID: " + Specs.Bubbles[i].ID);
                System.Console.WriteLine("Droplet Position: (" + Specs.Bubbles[i].PositionX + "," + Specs.Bubbles[i].PositionY + ")");
                System.Console.WriteLine("Droplet Size: " + Specs.Bubbles[i].SizeX + " by " + Specs.Bubbles[i].SizeY);
            }
            System.Console.WriteLine();
        }
    }
}
