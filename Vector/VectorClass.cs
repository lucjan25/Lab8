using System;

namespace Vector
{
    public interface IVector
    {
        double Abs();
        double Cdot(IVector vector);
        double[] GetComponents();
    }

    public class Vector2D : IVector
    {
        protected double x, y;
        public Vector2D()
        {
            x = 0;
            y = 0;
        }
        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double Abs()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        public double Cdot(IVector vector)
        {
            double[] vector2comp = vector.GetComponents();
            return this.x * vector2comp[0] + this.y * vector2comp[1];
        }

        public double[] GetComponents()
        {
            return new double[] { this.x, this.y };
        }
    }
    public class Polar2DInheritance : Vector2D
    {
        public double GetAngle()
        {
            double[] comps = this.GetComponents();
            return Math.Atan(comps[1] / comps[0]);
        }
    }
    public interface IPolar2D
    {
        double GetAngle();
        double Abs();
    }
    public class Polar2DAdapter : IPolar2D, IVector
    {
        private readonly Vector2D srcVector;
        public Polar2DAdapter(Vector2D srcVector)
        {
            this.srcVector = srcVector;
        }

        public double Abs()
        {
            return ((IVector)srcVector).Abs();
        }

        public double Cdot(IVector vector)
        {
            return ((IVector)srcVector).Cdot(vector);
        }
        public double[] GetComponents()
        {
            return ((IVector)srcVector).GetComponents();
        }
        public double GetAngle()
        {
            double[] comps = this.GetComponents();
            return Math.Atan(comps[1] / comps[0]);
        }
    }
    public class Vector3DInheritance : Vector2D
    {
        double z;

        public Vector3DInheritance() : base()
        {
            z = 0;
        }

        public Vector3DInheritance(double x, double y) : base(x, y)
        {
            z = 0;
        }

        public Vector3DInheritance(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public new double Abs()
        {
            return Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2) + Math.Pow(this.z, 2));
        }
        public new double Cdot(IVector vector)
        {
            double[] vector3comp = vector.GetComponents();
            return this.x * vector3comp[0] + this.y * vector3comp[1] + this.z * vector3comp[2];
        }
        public new double[] GetComponents()
        {
            return new double[] { this.x, this.y, this.z };
        }
        public Vector3DInheritance Cross(IVector vector)
        {
            double[] compsA = this.GetComponents();
            double[] compsB = vector.GetComponents();
            return new Vector3DInheritance(
                compsA[1] * compsB[2] - compsB[1] * compsA[2], 
                -(compsA[0] * compsB[2] - compsA[2] * compsB[0]), 
                compsA[0] * compsB[1] - compsA[1] * compsB[0]
                );
        }
        public IVector GetSrcV()
        {
            return new Vector2D(this.x, this.y);
        }
    }
    public class Vector3DDecorator : IVector
    {
        private readonly IVector srcVector;
        double z;
        public Vector3DDecorator(IVector srcVector)
        {
            this.srcVector = srcVector;
            this.z = 0;
        }
        public Vector3DDecorator(IVector srcVector, double z)
        {
            this.srcVector = srcVector;
            this.z = z;
        }

        public double Abs()
        {
            return srcVector.Abs();
        }

        public double Cdot(IVector vector)
        {
            return srcVector.Cdot(vector);
        }

        public double[] GetComponents()
        {
            double[] comps = srcVector.GetComponents();
            Array.Resize(ref comps, comps.Length + 1);
            comps[2] = this.z;
            return comps;
        }
        public Vector3DDecorator Cross(IVector vector)
        {
            double[] compsA = this.GetComponents();
            double[] compsB = new Vector3DDecorator(vector).GetComponents();
            double nx, ny, nz;
            nx = compsA[1] * compsB[2] - compsB[1] * compsA[2];
            ny = -(compsA[0] * compsB[2] - compsA[2] * compsB[0]);
            nz = compsA[0] * compsB[1] - compsA[1] * compsB[0];

            return new Vector3DDecorator(new Vector2D(nx, ny), nz);
        }
        public IVector GetSrcV()
        {
            return srcVector;
        }
    }
}
