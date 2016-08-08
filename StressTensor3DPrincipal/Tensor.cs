using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTensor3DPrincipal
{

    class Tensor
    {
        public ulong id { get; set; }
        public double sxx { get; set; }
        public double syy { get; set; }
        public double szz { get; set; }
        public double sxy { get; set; }
        public double syz { get; set; }
        public double sxz { get; set; }
        
        public Tensor(double sxx = 0.0, double syy = 0.0, double szz = 0.0, double sxy = 0.0, double syz = 0.0, double sxz = 0.0)
        {
            this.sxx = sxx;
            this.syy = syy;
            this.szz = szz;
            this.sxy = sxy;
            this.syz = syz;
            this.sxx = sxz;
        }

        public Tensor(List<double> values)
        {
            id = 0;
            sxx = values[0];
            syy = values[1];
            szz = values[2];
            sxy = values[3];
            syz = values[4];
            sxz = values[5];
        }

        internal void read(List<double> values)
        {
            id = 0;
            sxx = values[0];
            syy = values[1];
            szz = values[2];
            sxy = values[3];
            syz = values[4];
            sxz = values[5];
        }

        internal List<double> toPrincipal()
        {
            if (principals.Count != 3)
            {
                principals = new List<double>();

                #region OtherMethod
                /*
                double I1, I2, I3;
                I1 = sxx + syy + szz;
                I2 = sxx * syy + syy * szz + szz * sxx - sxy * sxy - sxz * sxz - syz * syz;
                I3 = sxx * syy * szz - sxx * syz * syz - syy * sxz * sxz - szz * sxy * sxy + 2 * sxy * sxz * syz;

                double q, r;
                q = (3 * I2 - I1 * I1) / 9.0;
                r = (2 * Math.Pow(I1, 3) - 9 * I1 * I2 + 27 * I3) / 54.0;

                double v;
                v = r / Math.Sqrt(-Math.Pow(q, 3));
                double phi;
                phi = Math.Acos(v);

                double p1, p2, p3;
                p1 = 2 * Math.Sqrt(-q) * Math.Cos((phi) / 3.0) + I1 / 3.0;
                p2 = 2 * Math.Sqrt(-q) * Math.Cos((phi + 2 * Math.PI) / 3.0) + I1 / 3.0;
                p3 = 2 * Math.Sqrt(-q) * Math.Cos((phi + 4 * Math.PI) / 3.0) + I1 / 3.0;
                */
                #endregion
                
                // https://en.wikiversity.org/wiki/Principal_stresses
                
                double I1, I2, I3;
                I1 = sxx + syy + szz;
                I2 = sxx * syy + syy * szz + szz * sxx - sxy * sxy - sxz * sxz - syz * syz;
                I3 = sxx * syy * szz - sxx * syz * syz - syy * sxz * sxz - szz * sxy * sxy + 2 * sxy * sxz * syz;

                double q, r;
                r = 2.0 * Math.Pow(I1, 3) - 9.0 * I1 * I2 + 27.0 * I3;
                q = 2.0 * Math.Pow(I1 * I1 - 3.0 * I2, 3.0 / 2.0);

                double v;
                v = r / q;
                double phi;
                phi = Math.Acos(v) / 3.0;

                double p1, p2, p3;
                p1 = I1 / 3.0 + 2.0 / 3.0 * Math.Sqrt(I1 * I1 - 3 * I2) * Math.Cos(phi);
                p2 = I1 / 3.0 + 2.0 / 3.0 * Math.Sqrt(I1 * I1 - 3 * I2) * Math.Cos(phi - 2.0 * Math.PI / 3.0);
                p3 = I1 / 3.0 + 2.0 / 3.0 * Math.Sqrt(I1 * I1 - 3 * I2) * Math.Cos(phi - 4.0 * Math.PI / 3.0);

                principals.Add(p1);
                principals.Add(p2);
                principals.Add(p3);
            }
                
            return principals;
        }

        private List<double> principals = new List<double>();
    }
}
