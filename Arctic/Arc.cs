using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arctic
{
    class Arc
    {
        private double fi1, fi2, theta;
        private int N_sour;
        private double mag_str, kT, size_par;
        public Source[] sources;

        public Arc(double fi1, double fi2, double theta, int N_sour, double mag_str, double kT, double size_par)
        {
            this.fi1 = fi1;
            this.fi2 = fi2;
            this.theta = theta;
            this.N_sour = N_sour;
            this.mag_str = mag_str;
            this.kT = kT;
            this.size_par = size_par;

            sources = new Source[N_sour];
            for (int i = 0; i < N_sour; i++)
            {
                sources[i] = new Source(theta, (fi1+i*(fi2-fi1)/(N_sour-1))); 
            }
        }

        public void SetPars(double fi1, double fi2, double theta, int N_sour, double mag_str, double kT, double size_par)
        {
            this.fi1 = fi1;
            this.fi2 = fi2;
            this.theta = theta;

            this.N_sour = N_sour;
            for (int i = 0; i < N_sour; i++)
            {
                sources[i] = new Source(theta, (fi1 + i * (fi2 - fi1) / (N_sour - 1)));
            }

            this.mag_str = mag_str;
            this.kT = kT;
            this.size_par = size_par;
        }

        public double XCoord(int n_source) { return sources[n_source].Xss; }
        public double YCoord(int n_source) { return sources[n_source].Yss; }
        public double ZCoord(int n_source) { return sources[n_source].Zss; }

        public double getB { get { return mag_str; } }
        public double getkT { get { return kT; } }
        public double getSP { get { return size_par; } }
    }
}
