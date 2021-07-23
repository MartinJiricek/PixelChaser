using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PixelChaser
{
    public static class xMath
    {
        public static bool LineIntersectsRect( PointF p1, PointF p2, RectangleF r)
        {
            return LineIntersectsLine(p1, p2, new PointF(r.X, r.Y), new PointF(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new PointF(r.X + r.Width, r.Y), new PointF(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new PointF(r.X + r.Width, r.Y + r.Height), new PointF(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new PointF(r.X, r.Y + r.Height), new PointF(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }
        public static bool LineIntersectsLine(PointF l1p1, PointF l1p2, PointF l2p1, PointF l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }
        private static void FindIntersection( PointF p1, PointF p2, PointF p3, PointF p4, out bool lines_intersect, out bool segments_intersect,  out PointF intersection,  out PointF close_p1, out PointF close_p2)
        {
            // Get the segments' parameters.
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
                    / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new PointF(float.NaN, float.NaN);
                close_p1 = new PointF(float.NaN, float.NaN);
                close_p2 = new PointF(float.NaN, float.NaN);
                return;
            }
            lines_intersect = true;

            float t2 =
                ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
                    / -denominator;

            // Find the point of intersection.
            intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new PointF(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }

        //public static PointF GetRotatedPoint(PointF pt, double radians)
        //{
        //    PointF result = new PointF(pt.X, pt.Y);

        //    result.X = (float)(pt.X * Math.Cos((radians)) - pt.Y * Math.Sin((radians)));
        //    result.Y = (float)(pt.Y * Math.Cos((radians)) - pt.X * Math.Sin((radians)));

        //    return result;
        //}

        public static void GetRotatedRectangle(RectangleF rect, double radians, out PointF TL, out PointF TR, out PointF BL, out PointF BR)
        {
            PointF C = new PointF(rect.X, rect.Y);
            double rads = radians;

            TL = new PointF(rect.X - rect.Width / 2, rect.Y - rect.Height / 2);
            double r_TL = Math.Sqrt(Math.Pow(TL.X - C.X, 2) + Math.Pow(TL.Y - C.Y, 2));
            radians = rads + GetDefaultAngle(TL, C);
            TL.X = (float)(r_TL * Math.Cos(-radians) + C.X);
            TL.Y = (float)(r_TL * Math.Sin(-radians) + C.Y);


            TR = new PointF(rect.X + rect.Width / 2, rect.Y - rect.Height / 2);
            double r_TR = Math.Sqrt(Math.Pow(TR.X - C.X, 2) + Math.Pow(TR.Y - C.Y, 2));
            radians = rads + GetDefaultAngle(TR, C);
            TR.X = (float)(r_TR * Math.Cos(-radians) + C.X);
            TR.Y = (float)(r_TR * Math.Sin(-radians) + C.Y);


            BL = new PointF(rect.X - rect.Width / 2, rect.Y + rect.Height / 2);
            double r_BL = Math.Sqrt(Math.Pow(BL.X - C.X, 2) + Math.Pow(BL.Y - C.Y, 2));
            radians = rads + GetDefaultAngle(BL, C);
            BL.X = (float)(r_BL * Math.Cos(-radians) + C.X);
            BL.Y = (float)(r_BL * Math.Sin(-radians) + C.Y);


            BR = new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            double r_BR = Math.Sqrt(Math.Pow(BR.X - C.X, 2) + Math.Pow(BR.Y - C.Y, 2));
            radians = rads + GetDefaultAngle(BR, C);
            BR.X = (float)(r_BR * Math.Cos(-radians) + C.X);
            BR.Y = (float)(r_BR * Math.Sin(-radians) + C.Y);

        }
        public static double GetRadians(this double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static double GetDefaultAngle(PointF center, PointF pt)
        {
            double dx = center.X - pt.X;
            double dy = center.Y - pt.Y;

            return Math.Atan2(dy, dx);
        }
        public static int GetPointsOrientation(PointF p, PointF q, PointF r)
        {
            // See https://www.geeksforgeeks.org/orientation-3-ordered-points/
            // for details of below formula.
            int val = (int)((q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y));

            if (val == 0) return 0; // colinear

            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }
        
        // Given three colinear points p, q, r, the function checks if
         // point q lies on line segment 'pr'
        static Boolean CheckPointIsOnSegment(PointF p, PointF q, PointF r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }

        public static Boolean CheckLinesIntersects(PointF p1, PointF q1, PointF p2, PointF q2)
        {
            // Find the four orientations needed for general and
            // special cases
            int o1 = GetPointsOrientation(p1, q1, p2);
            int o2 = GetPointsOrientation(p1, q1, q2);
            int o3 = GetPointsOrientation(p2, q2, p1);
            int o4 = GetPointsOrientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && CheckPointIsOnSegment(p1, p2, q1)) return true;

            // p1, q1 and q2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && CheckPointIsOnSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && CheckPointIsOnSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && CheckPointIsOnSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases
        }

        public static PointF GetRotatedPoint(PointF pt, PointF center, double rotation)
        {
            PointF ptx = new PointF(pt.X + center.X, pt.Y + center.Y);

            double c_dist = Math.Sqrt(Math.Pow(ptx.X - center.X, 2) + Math.Pow(ptx.Y - center.Y, 2));
            double radians = rotation + xMath.GetDefaultAngle(ptx, center);

            float x = (float)(c_dist * Math.Cos(-radians) + center.X);
            float y = (float)(c_dist * Math.Sin(-radians) + center.Y);

            return new PointF(x, y);
        }
    }
}
