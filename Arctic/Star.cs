using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arctic
{
    class Star
    {
        public Arc[] arcs = null;
        private double inc, beta, psi;
        private double cos_90inc, cos_beta, sin_90inc, sin_beta;
        private double phase;
        private double cos_phase_psi, sin_phase_psi;
        private int arcs_count = 0;

        //double[][] rotate_i = null, rotate_beta = null, rotate_phi = null; 

        public Star(double inc, double beta, double psi) 
        { 
            this.inc = inc;
            this.beta = beta;
            this.psi = psi;
            this.SetPhase(0);

            cos_90inc = Math.Cos(Math.PI / 2 - inc);
            sin_90inc = Math.Sin(Math.PI / 2 - inc);
            cos_beta = Math.Cos(beta);
            sin_beta = Math.Sin(beta);

            /*rotate_i = new double[3][];
            for (int w = 0; w < 3; w++)
                rotate_i[w] = new double[3];
            rotate_i[0][0] = Math.Cos(Math.PI / 2 - inc);
            rotate_i[0][1] = 0;
            rotate_i[0][2] = Math.Sin(Math.PI / 2 - inc);
            rotate_i[1][0] = 0;
            rotate_i[1][1] = 1;
            rotate_i[1][2] = 0;
            rotate_i[2][0] = -1 * Math.Sin(Math.PI / 2 - inc);
            rotate_i[2][1] = 0;
            rotate_i[2][2] = Math.Cos(Math.PI / 2 - inc);

            rotate_beta = new double[3][];
            for (int i = 0; i < 3; i++)
                rotate_beta[i] = new double[3];
            rotate_beta[0][0] = Math.Cos(beta);
            rotate_beta[0][1] = 0;
            rotate_beta[0][2] = Math.Sin(beta);
            rotate_beta[1][0] = 0;
            rotate_beta[1][1] = 1;
            rotate_beta[1][2] = 0;
            rotate_beta[2][0] = -1 * Math.Sin(beta);
            rotate_beta[2][1] = 0;
            rotate_beta[2][2] = Math.Cos(beta);*/

            this.arcs = null;  
            this.arcs_count = 0; 
        }

        public void SetPhase(double phase) 
        {
            this.phase = 2 * Math.PI * phase;

            cos_phase_psi = Math.Cos(this.phase + psi);
            sin_phase_psi = Math.Sin(this.phase + psi);

            /*if (rotate_phi == null)
            {
                rotate_phi = new double[3][];
                for (int i = 0; i < 3; i++)
                    rotate_phi[i] = new double[3];
            }

            rotate_phi[0][0] = Math.Cos(this.phase + psi);
            rotate_phi[0][1] = -1 * Math.Sin(this.phase + psi);
            rotate_phi[0][2] = 0;
            rotate_phi[1][0] = Math.Sin(this.phase + psi);
            rotate_phi[1][1] = Math.Cos(this.phase + psi);
            rotate_phi[1][2] = 0;
            rotate_phi[2][0] = 0;
            rotate_phi[2][1] = 0;
            rotate_phi[2][2] = 1;*/
        }

        public void AddArc(double fi1, double fi2, double theta, int N_sour, double mag_str, double kT, double size_par)
        {
            if (arcs == null)
            {
                arcs = new Arc[1];
                arcs[0] = new Arc(fi1, fi2, theta, N_sour, mag_str, kT, size_par) ;
                arcs_count += 1;
            }
            else
            {
                Array.Resize(ref arcs, arcs.Length + 1);
                arcs[arcs.Length - 1] = new Arc(fi1, fi2, theta, N_sour, mag_str, kT, size_par);
                arcs_count += 1;
            }
        }
        public void DelArc(int selected)
        {
            if (arcs.Length == 1)
            {
                arcs = null;
            }
            else
            {
                Arc[] buff = new Arc[arcs.Length - 1];
                for (int i = 0; i < selected; i++)
                    buff[i] = arcs[i];
                for (int i = selected; i < arcs.Length - 1; i++)
                    buff[i] = arcs[i + 1];
                arcs = buff;
            }

            arcs_count -= 1;
        }

        public double get_arcs_count { get { return arcs_count; } }

        public double GetCosGamma(int n_arc, int n_source)
        {
            double[] xsyszs = new double[3];
            xsyszs[0] = arcs[n_arc].XCoord(n_source);
            xsyszs[1] = arcs[n_arc].YCoord(n_source);
            xsyszs[2] = arcs[n_arc].ZCoord(n_source);

            /*double[] mmm = Matrix_Mult(rotate_beta, xsyszs);
            mmm = Matrix_Mult(rotate_phi, mmm);
            mmm = Matrix_Mult(rotate_i, mmm);

            return mmm[0];*/

            //return (Math.Cos(Math.PI/2 - inc)*(Math.Cos(phase+psi)*(Math.Cos(beta)*xsyszs[0]+Math.Sin(beta)*xsyszs[2])+Math.Sin(phase+psi)*(-xsyszs[1]))+
            //    Math.Sin(Math.PI/2 - inc)*(-Math.Sin(beta)*xsyszs[0]+Math.Cos(beta)*xsyszs[2]));

            double res = cos_90inc * (cos_phase_psi * (cos_beta * xsyszs[0] + sin_beta * xsyszs[2]) + sin_phase_psi * (-xsyszs[1])) + sin_90inc * (-sin_beta * xsyszs[0] + cos_beta * xsyszs[2]);

            return res;
        }

        public double GetTheta(int n_arc, int n_source)
        {
            double theta = arcs[n_arc].sources[n_source].GetTheta;
            double fi = arcs[n_arc].sources[n_source].GetFi;
            double tgE = (2 - 3 * Math.Pow(Math.Sin(theta), 2)) / (3 * Math.Cos(theta) * Math.Sin(theta));
            double alf;
            if (theta < Math.PI / 2)
                alf = Math.PI / 2 - Math.Atan(tgE);
            else
                alf = 3 * Math.PI / 2 - Math.Atan(tgE);

            double[] coordsss = new double[3];
            coordsss[0] = Math.Sin(alf) * Math.Cos(Math.PI - fi);
            coordsss[1] = - Math.Sin(alf) * Math.Sin(Math.PI - fi);
            coordsss[2] = Math.Cos(alf);

            /*double[][] mmm = Matrix_Mult(rotate_i, rotate_phi);
            mmm = Matrix_Mult(mmm, rotate_beta);
            double[] xyz = Matrix_Mult(mmm, coordsss);

            return Math.Acos(xyz[0]);*/

            //double mm = Math.Cos(Math.PI / 2 - inc) * (Math.Cos(phase + psi) * (Math.Cos(beta) * coordsss[0] + Math.Sin(beta) * coordsss[2])
            //    + Math.Sin(phase + psi) * (-coordsss[1])) + Math.Sin(Math.PI / 2 - inc) * (-Math.Sin(beta) * coordsss[0] + Math.Cos(beta) * coordsss[2]);

            double mm = cos_90inc*(cos_phase_psi*(cos_beta*coordsss[0] + sin_beta*coordsss[2])+sin_phase_psi*(-coordsss[1]))+sin_90inc*(-sin_beta*coordsss[0]+cos_beta*coordsss[2]);

            return Math.Acos(mm);
        }

        static double[] Matrix_Mult(double[][] m1, double[] m2)
        {
            double[] m3 = new double[3];
            for (int i = 0; i < 3; i++)
            {
                m3[i] = 0;
                for (int k = 0; k < 3; k++)
                    m3[i] += m1[i][k] * m2[k];
            }
            return m3;
        }

        static double[][] Matrix_Mult(double[][] m1, double[][] m2)
        {
            double[][] m3 = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                m3[i] = new double[3];
                for (int j = 0; j < 3; j++)
                {
                    m3[i][j] = 0;
                    for (int k = 0; k < 3; k++)
                        m3[i][j] += m1[i][k] * m2[k][j];
                }
            }
            return m3;
        }
    }
}