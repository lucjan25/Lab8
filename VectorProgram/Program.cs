using System;
using Vector;

namespace VectorProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vector2D v1 = new(2.5, 10.0);
            Vector2D v2 = new(4.0, 0.3);
            Vector2D v3 = new(5.0, 1.0);

            double[] compsv1 = v1.GetComponents();
            double[] compsv2 = v2.GetComponents();
            double[] compsv3 = v3.GetComponents();

            double anglev1 = new Polar2DAdapter(v1).GetAngle();
            double anglev2 = new Polar2DAdapter(v2).GetAngle();
            double anglev3 = new Polar2DAdapter(v3).GetAngle();

            Console.WriteLine(string.Format("Wektor 1: Układ kartezjański: {0}, {1}; Układ biegunowy {2}, {3}", 
                compsv1[0], compsv1[1], v1.Abs(), anglev1));
            Console.WriteLine(string.Format("Wektor 2: Układ kartezjański: {0}, {1}; Układ biegunowy {2}, {3}", 
                compsv2[0], compsv2[1], v2.Abs(), anglev2));
            Console.WriteLine(string.Format("Wektor 3: Układ kartezjański: {0}, {1}; Układ biegunowy {2}, {3}", 
                compsv3[0], compsv3[1], v3.Abs(), anglev3));

            Vector3DDecorator v1_3d = new(v1);
            Vector3DDecorator v2_3d = new(v2);
            Vector3DDecorator v3_3d = new(v3);

            double[] v1xv2 = v1_3d.Cross(v2_3d).GetComponents();
            double[] v2xv3 = v2_3d.Cross(v3_3d).GetComponents();
            double[] v1xv3 = v1_3d.Cross(v3_3d).GetComponents();

            Console.WriteLine(string.Format("Wektor 1 i 2: Iloczyn skalarny: {0}; Iloczyn wektorowy {1}, {2}, {3}",
                v1.Cdot(v2), v1xv2[0], v1xv2[1], v1xv2[2]));
            Console.WriteLine(string.Format("Wektor 2 i 3: Iloczyn skalarny: {0}; Iloczyn wektorowy {1}, {2}, {3}",
                v2.Cdot(v3), v2xv3[0], v2xv3[1], v2xv3[2]));
            Console.WriteLine(string.Format("Wektor 1 i 3: Iloczyn skalarny: {0}; Iloczyn wektorowy {1}, {2}, {3}",
                v1.Cdot(v3), v1xv3[0], v1xv3[1], v1xv3[2]));
        }
    }
}
