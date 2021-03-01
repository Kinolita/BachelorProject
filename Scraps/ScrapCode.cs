using System.Drawing;
using System;

namespace BachelorProject
{
    public class ScrapCode
    {
        public static void Move(Point current, Point newPos) {
            if (!Validate(current) || !Validate(newPos)) {
                Console.WriteLine("One of the points was outside the board: " + current + " + " + newPos);
            } else {
                Point temp = new Point();
                temp.X = current.X + newPos.X;
                temp.Y = current.Y + newPos.Y;
                if (!Validate(temp)) {
                    Console.WriteLine("The new position is not valid: " + temp);
                } else {
                    current = temp;
                    Console.WriteLine("New position is: " + current);
                }
            }
        }

        public static bool Validate(Point check) {
            if (check.X <= 10 && check.Y <= 6 && check.X >= 0 && check.Y >= 0) {
                return true;
            }
            return false;
        }
    }
}
