using System;

namespace BachelorProject.Models
{
    public class PolygonPoints
    {
        // Inspired from https://www.geeksforgeeks.org/check-if-two-given-line-segments-intersect/ 
        // This checks if a given point lies inside a given polygon 
        // used for pixel assignment of custom polygon electrodes

        static int INF = 10000;

        // Given 3 co-linear points p, q, r, the function checks if point q lies on line segment 'pr' 
        private static bool OnSegment(Coord p, Coord q, Coord r) {
            return q.X <= Math.Max(p.X, r.X) &&
                   q.X >= Math.Min(p.X, r.X) &&
                   q.Y <= Math.Max(p.Y, r.Y) &&
                   q.Y >= Math.Min(p.Y, r.Y);
        }

        // To find Orientation of ordered triplet (p, q, r). 
        // The function returns following values 
        // 0 --> p, q and r are co-linear 
        // 1 --> Clockwise 
        // 2 --> Counterclockwise 
        static int Orientation(Coord p, Coord q, Coord r) {
            int val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) {
                return 0;
            }
            return (val > 0) ? 1 : 2;
        }

        // The function that returns true if line segment 'p1q1' and 'p2q2' intersect. 
        static bool DoIntersect(Coord p1, Coord q1,
                                Coord p2, Coord q2) {
            // Find the four orientations needed for general and special cases 
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4) {
                return true;
            }

            // Special Cases 
            // p1, q1 and p2 are co-linear and p2 lies on segment p1q1 
            if (o1 == 0 && OnSegment(p1, p2, q1)) {
                return true;
            }

            // p1, q1 and p2 are co-linear and q2 lies on segment p1q1 
            if (o2 == 0 && OnSegment(p1, q2, q1)) {
                return true;
            }

            // p2, q2 and p1 are co-linear and p1 lies on segment p2q2 
            if (o3 == 0 && OnSegment(p2, p1, q2)) {
                return true;
            }

            // p2, q2 and q1 are co-linear and q1 lies on segment p2q2 
            if (o4 == 0 && OnSegment(p2, q1, q2)) {
                return true;
            }

            // Doesn't fall in any of the above cases 
            return false;
        }

        // Returns true if the point p lies inside the polygon[] with n vertices 
        public static bool IsInside(Coord[] polygon, int n, Coord p) {
            // There must be at least 3 vertices in polygon[] 
            if (n < 3) {
                return false;
            }

            // Create a point for line segment from p to infinite 
            Coord extreme = new Coord(INF, p.Y);

            // Count intersections of the above line with sides of polygon 
            int count = 0, i = 0;
            do {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to 'extreme' intersects with the line 
                // segment from 'polygon[i]' to 'polygon[next]' 
                if (DoIntersect(polygon[i],
                                polygon[next], p, extreme)) {
                    // If the point 'p' is co-linear with line segment 'i-next', then check if it lies 
                    // on segment. If it lies, return true, otherwise false 
                    if (Orientation(polygon[i], p, polygon[next]) == 0) {
                        return OnSegment(polygon[i], p,
                                        polygon[next]);
                    }
                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise 
            return (count % 2 == 1); // Same as (count%2 == 1) 
        }
    }
}

