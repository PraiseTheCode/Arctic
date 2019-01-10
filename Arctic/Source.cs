using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arctic
{
    class Source
    {
        private double theta, fi;

        public Source(double theta, double fi)
        {
            this.theta = theta;
            this.fi = fi;
        }

        public double Xss { get { return Math.Sin(theta) * Math.Cos(Math.PI - fi); } }
        public double Yss { get { return - Math.Sin(theta) * Math.Sin(Math.PI - fi); } }
        public double Zss { get { return Math.Cos(theta); } }
        
        public double GetTheta { get { return theta; } }
        public double GetFi { get { return fi; } }
    }
}
