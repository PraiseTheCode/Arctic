using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPlot;
using DotNumerics.Optimization;
using System.IO;
using System.Diagnostics;

namespace Arctic
{
    public partial class Arctic : Form
    {
        static double[] phases_obs_B, flux_obs_B, phases_obs_V, flux_obs_V, phases_obs_R, flux_obs_R, phases_obs_I, flux_obs_I;
        static double[] phases_obs_filt1, flux_obs_filt1, phases_obs_filt2, flux_obs_filt2, phases_obs_filt3, flux_obs_filt3, phases_obs_filt4, flux_obs_filt4, phases_obs_filt5, flux_obs_filt5, phases_obs_filt6, flux_obs_filt6;
        static double[] flux_th_B, flux_th_V, flux_th_R, flux_th_I;
        static double[] flux_th_filt1, flux_th_filt2, flux_th_filt3, flux_th_filt4, flux_th_filt5, flux_th_filt6;
        static double inc, mag_str, kT, sp;
        static double[] solution;
        public delegate double FuncDel(double[] pars);
        public delegate void FuncDel2(int value);
        static Star star;
        static StokesParsProvider IstokesB, IstokesV, IstokesR, stokesV;
        static StokesParsProvider IstokesF1, IstokesF2, IstokesF3, IstokesF4, IstokesF5, IstokesF6;

        static double[] phases_th;
        static double[] IStokesB, IStokesV, IStokesR, VStokesV, thetass;
        static double[] IStokesF1, IStokesF2, IStokesF3, IStokesF4, IStokesF5, IStokesF6;
        static LinInterpolator ISB, ISV, ISR, VSV;
        static LinInterpolator ISF1, ISF2, ISF3, ISF4, ISF5, ISF6;

        static double[][] lims;

        static String separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0].ToString();

        public Arctic()
        {
            InitializeComponent();

            plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));

            plotBVRB.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            plotBVRB.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            plotBVRB.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));

            plotBVRV.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            plotBVRV.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            plotBVRV.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));

            plotBVRR.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            plotBVRR.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            plotBVRR.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));

            plotIVI.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            plotIVI.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            plotIVI.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));

            plotIVV.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            plotIVV.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            plotIVV.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
        }

        #region V

        private void btnLoadV_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;

            StreamReader sr = new StreamReader(path);

            string[] s = sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] mmm;
            phases_obs_V = new double[s.Length];
            flux_obs_V = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_V[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_V[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            sr.Close();

            btnShowV.Enabled = true;
            btnClearV.Enabled = true;
            btnSavePlotV.Enabled = true;
            btnLC.Enabled = true;
        }

        private void btnShowV_Click(object sender, EventArgs e)
        {
            PointPlot pp = new PointPlot();
            pp.AbscissaData = phases_obs_V;
            pp.OrdinateData = flux_obs_V;
            pp.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plot.Add(pp);

            plot.Refresh();
            plot.Show();
        }

        private void btnClearV_Click(object sender, EventArgs e)
        {
            plot.Clear();
            plot.Refresh();
        }

        private void btnSavePlotV_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string path = saveFileDialog1.FileName;

            Bitmap bitmap = new Bitmap(plot.Width, plot.Height);

            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.CopyFromScreen(plot.PointToScreen(Point.Empty), Point.Empty, plot.Size);
            }

            bitmap.Save(path, ImageFormat.Png);
        }

        private double chi2_V(double[] pars)
        {
            double psi = pars[0];
            double fi1 = pars[1];
            double dfi = pars[2];
            double beta = pars[3];
            double ksi = pars[4];

            if (psi < 0 || fi1 < 0 || dfi < 0 || beta < 0 || ksi < 0)
                return 1e20;

            else
            {
                double fi2 = fi1 + dfi;
                if (fi2 > 2 * Math.PI) fi2 = Math.PI * 2;

                double theta, cos_gamma;
                int Ns = 100;

                Star star1 = new Star(inc, beta, psi);
                star1.AddArc(fi1, fi2, ksi, Ns, mag_str, kT, sp);

                double[] flux_th_V1 = new double[phases_th.Length];

                for (int r = 0; r < phases_th.Length; r++)
                {
                    star1.SetPhase(phases_th[r]);

                    flux_th_V1[r] = 0;
                    for (int i = 0; i < star1.get_arcs_count; i++)
                    {
                        for (int j = 0; j < Ns; j++)
                        {
                            theta = star1.GetTheta(i, j);
                            cos_gamma = star1.GetCosGamma(i, j);

                            if (cos_gamma > 0)
                            {
                                if (theta < Math.PI/2)
                                    flux_th_V1[r] += ISV.Interp(theta) * cos_gamma;
                                else
                                    flux_th_V1[r] += ISV.Interp(Math.PI - theta) * cos_gamma;
                            }
                        }
                    }
                }
                LinInterpolator lipV1 = new LinInterpolator(phases_th, flux_th_V1);
                double[] flux_th_V_obs = new double[phases_obs_V.Length];
                for (int i = 0; i < phases_obs_V.Length; i++)
                    flux_th_V_obs[i] = lipV1.Interp(phases_obs_V[i]);

                double ccV = flux_obs_V.Sum() / flux_th_V_obs.Sum();

                double ff = 0;
                for (int i = 0; i < phases_obs_V.Length; i++)
                    ff += Math.Pow((flux_obs_V[i] - flux_th_V_obs[i] * ccV), 2);

                return ff * 1e30;
            }
        }

        #endregion

        #region I + V Stokes

        private void btnLoadVV_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string pathV = openFileDialog1.FileName;

            StreamReader srV = new StreamReader(pathV);

            string[] s = srV.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] mmm;
            phases_obs_V = new double[s.Length];
            flux_obs_V = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_V[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_V[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            srV.Close();

            btnShowIV.Enabled = true;
            btnClearIV.Enabled = true;
            btnLC.Enabled = true;
            btnSaveIV.Enabled = true;
        }

        private void btnLoadIV_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            string pathI = openFileDialog1.FileName;

            StreamReader srI = new StreamReader(pathI);

            string[] s = srI.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] mmm;
            phases_obs_I = new double[s.Length];
            flux_obs_I = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_I[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_I[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            srI.Close();
        }

        private void btnShowIV_Click(object sender, EventArgs e)
        {
            PointPlot ppI = new PointPlot();
            ppI.AbscissaData = phases_obs_I;
            ppI.OrdinateData = flux_obs_I;
            ppI.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotIVI.Add(ppI);

            plotIVI.Refresh();
            plotIVI.Show();

            PointPlot ppV = new PointPlot();
            ppV.AbscissaData = phases_obs_V;
            ppV.OrdinateData = flux_obs_V;
            ppV.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotIVV.Add(ppV);

            plotIVV.Refresh();
            plotIVV.Show();
        }

        private void btnClearIV_Click(object sender, EventArgs e)
        {
            plotIVI.Clear();
            plotIVV.Clear();
            plotIVI.Refresh();
            plotIVV.Refresh();
        }

        private void btnSaveIV_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string pathI = saveFileDialog1.FileName;

            Bitmap bitmap = new Bitmap(plotIVI.Width, plotIVI.Height);
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.CopyFromScreen(plotIVI.PointToScreen(Point.Empty), Point.Empty, plotIVI.Size);
            }
            bitmap.Save(pathI, ImageFormat.Png);

            saveFileDialog1.ShowDialog();
            string pathV = saveFileDialog1.FileName;

            bitmap = new Bitmap(plotIVV.Width, plotIVV.Height);
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.CopyFromScreen(plotIVV.PointToScreen(Point.Empty), Point.Empty, plotIVV.Size);
            }
            bitmap.Save(pathV, ImageFormat.Png);
        }

        private double chi2_IVStokes(double[] pars)
        {
            double psi = pars[0];
            double fi1 = pars[1];
            double dfi = pars[2];
            double beta = pars[3];
            double ksi = pars[4];

            if (psi < 0 || fi1 < 0 || dfi < 0 || beta < 0 || ksi < 0)
                return 1e120;
            else if (psi < lims[0][0] || psi > lims[0][1] || fi1 < lims[1][0] || fi1 > lims[1][1] || dfi < lims[2][0] || dfi > lims[2][1] || beta < lims[3][0] || beta > lims[3][1] || ksi < lims[4][0] || ksi > lims[4][1])
                return 1e120;

            else
            {
                double fi2 = fi1 + dfi;
                if (fi2 > 2 * Math.PI) fi2 = Math.PI * 2;

                double theta, cos_gamma;
                int Ns = 100;

                Star star1 = new Star(inc, beta, psi);
                star1.AddArc(fi1, fi2, ksi, Ns, mag_str, kT, sp);

                double[] flux_th_I1 = new double[phases_th.Length];
                double[] flux_th_V1 = new double[phases_th.Length];

                for (int r = 0; r < phases_th.Length; r++)
                {
                    star1.SetPhase(phases_th[r]);

                    flux_th_I1[r] = 0;
                    flux_th_V1[r] = 0;
                    for (int i = 0; i < star1.get_arcs_count; i++)
                    {
                        for (int j = 0; j < Ns; j++)
                        {
                            theta = star1.GetTheta(i, j);
                            cos_gamma = star1.GetCosGamma(i, j);

                            if (cos_gamma > 0)
                            {
                                if (theta < Math.PI / 2)
                                {
                                    flux_th_I1[r] += ISV.Interp(theta) * cos_gamma;
                                    flux_th_V1[r] += VSV.Interp(theta) * cos_gamma;
                                }
                                else
                                {
                                    flux_th_I1[r] += ISV.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_V1[r] -= VSV.Interp(Math.PI - theta) * cos_gamma;
                                }
                            }
                        }
                    }
                }
                LinInterpolator lipI1 = new LinInterpolator(phases_th, flux_th_I1);
                LinInterpolator lipV1 = new LinInterpolator(phases_th, flux_th_V1);
                double[] flux_th_I_obs = new double[phases_obs_I.Length];
                double[] flux_th_V_obs = new double[phases_obs_V.Length];
                for (int i = 0; i < phases_obs_I.Length; i++)
                    flux_th_I_obs[i] = lipI1.Interp(phases_obs_I[i]);
                for (int i = 0; i < phases_obs_V.Length; i++)
                    flux_th_V_obs[i] = lipV1.Interp(phases_obs_V[i]);

                double ccI = (flux_obs_I.Sum() + flux_obs_V.Sum()) / (flux_th_I_obs.Sum()+flux_th_V_obs.Sum());
                //double ccV = flux_obs_V.Sum() / flux_th_V_obs.Sum();

                double ccp = flux_obs_I.Sum() / flux_obs_V.Sum();

                double ff = 0;
                for (int i = 0; i < phases_obs_I.Length; i++)
                    ff += Math.Pow((flux_obs_I[i] - flux_th_I_obs[i] * ccI), 2);
                for (int i = 0; i < phases_obs_V.Length; i++)
                    ff += ccp*Math.Pow((flux_obs_V[i] - flux_th_V_obs[i] * ccI), 2);

                return ff * 1e30;
            }
        }

        #endregion

        #region BVR

        private void btnLoadB_BVR_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string pathB = openFileDialog1.FileName;

            StreamReader srB = new StreamReader(pathB);

            string[] s = srB.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] mmm;
            phases_obs_B = new double[s.Length];
            flux_obs_B = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_B[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_B[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            srB.Close();
        }

        private void btnLoadV_BVR_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string pathV = openFileDialog1.FileName;

            StreamReader srV = new StreamReader(pathV);

            string[] s = srV.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] mmm;
            phases_obs_V = new double[s.Length];
            flux_obs_V = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_V[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_V[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            srV.Close();
        }

        private void btnLoadR_BVR_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string pathR = openFileDialog1.FileName;

            StreamReader srR = new StreamReader(pathR);

            string[] s = srR.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] mmm;
            phases_obs_R = new double[s.Length];
            flux_obs_R = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_R[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_R[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            srR.Close();

            btnShowBVR.Enabled = true;
            btnClearBVR.Enabled = true;
            btnLC.Enabled = true;
            btnSavePlotBVR.Enabled = true;
        }

        private void btnShowBVR_Click(object sender, EventArgs e)
        {
            PointPlot pp = new PointPlot();
            pp.AbscissaData = phases_obs_B;
            pp.OrdinateData = flux_obs_B;
            pp.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotBVRB.Add(pp);

            plotBVRB.Refresh();
            plotBVRB.Show();

            PointPlot pp2 = new PointPlot();
            pp2.AbscissaData = phases_obs_V;
            pp2.OrdinateData = flux_obs_V;
            pp2.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotBVRV.Add(pp2);

            plotBVRV.Refresh();
            plotBVRV.Show();

            PointPlot pp3 = new PointPlot();
            pp3.AbscissaData = phases_obs_R;
            pp3.OrdinateData = flux_obs_R;
            pp3.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotBVRR.Add(pp3);

            plotBVRR.Refresh();
            plotBVRR.Show();
        }

        private void btnClearBVR_Click(object sender, EventArgs e)
        {
            plotBVRB.Clear();
            plotBVRB.Refresh();
            plotBVRV.Clear();
            plotBVRV.Refresh();
            plotBVRR.Clear();
            plotBVRR.Refresh();
        }

        private void btnSavePlotBVR_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string pathB = saveFileDialog1.FileName;

            Bitmap bitmap = new Bitmap(plotBVRB.Width, plotBVRB.Height);
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.CopyFromScreen(plotBVRB.PointToScreen(Point.Empty), Point.Empty, plotBVRB.Size);
            }
            bitmap.Save(pathB, ImageFormat.Png);

            saveFileDialog1.ShowDialog();
            string pathV = saveFileDialog1.FileName;

            bitmap = new Bitmap(plotBVRV.Width, plotBVRV.Height);
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.CopyFromScreen(plotBVRV.PointToScreen(Point.Empty), Point.Empty, plotBVRV.Size);
            }
            bitmap.Save(pathV, ImageFormat.Png);

            saveFileDialog1.ShowDialog();
            string pathR = saveFileDialog1.FileName;

            bitmap = new Bitmap(plotBVRR.Width, plotBVRR.Height);
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.CopyFromScreen(plotBVRR.PointToScreen(Point.Empty), Point.Empty, plotBVRR.Size);
            }
            bitmap.Save(pathR, ImageFormat.Png);
        }

        private double chi2_BVR(double[] pars)
        {
            double psi = pars[0];
            double fi1 = pars[1];
            double dfi = pars[2];
            double beta = pars[3];
            double ksi = pars[4];

            if (psi < 0 || fi1 < 0 || dfi < 0 || beta < 0 || ksi < 0)
                return 1e20;
            //else if (psi < lims[0][0] || psi > lims[0][1] || fi1 < lims[1][0] || fi1 > lims[1][1] || dfi < lims[2][0] || dfi > lims[2][1] || beta < lims[3][0] || beta > lims[3][1] || ksi < lims[4][0] || ksi > lims[4][1])
            //    return 1e20;

            else
            {
                double fi2 = fi1 + dfi;
                if (fi2 > 2 * Math.PI) fi2 = Math.PI * 2;

                double theta, cos_gamma;
                int Ns = 100;

                Star star1 = new Star(inc, beta, psi);
                star1.AddArc(fi1, fi2, ksi, Ns, mag_str, kT, sp);

                double[] flux_th_B1 = new double[phases_th.Length];
                double[] flux_th_V1 = new double[phases_th.Length];
                double[] flux_th_R1 = new double[phases_th.Length];

                for (int r = 0; r < phases_th.Length; r++)
                {
                    star1.SetPhase(phases_th[r]);

                    flux_th_B1[r] = 0;
                    flux_th_V1[r] = 0;
                    flux_th_R1[r] = 0;
                    for (int i = 0; i < star1.get_arcs_count; i++)
                    {
                        for (int j = 0; j < Ns; j++)
                        {
                            theta = star1.GetTheta(i, j);
                            cos_gamma = star1.GetCosGamma(i, j);

                            if (cos_gamma > 0)
                            {
                                if (theta < Math.PI / 2)
                                {
                                    flux_th_B1[r] += ISB.Interp(theta) * cos_gamma;
                                    flux_th_V1[r] += ISV.Interp(theta) * cos_gamma;
                                    flux_th_R1[r] += ISR.Interp(theta) * cos_gamma;
                                }
                                else
                                {
                                    flux_th_B1[r] += ISB.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_V1[r] += ISV.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_R1[r] += ISR.Interp(Math.PI - theta) * cos_gamma;
                                }
                            }
                        }
                    }
                }
                LinInterpolator lipB1 = new LinInterpolator(phases_th, flux_th_B1);
                LinInterpolator lipV1 = new LinInterpolator(phases_th, flux_th_V1);
                LinInterpolator lipR1 = new LinInterpolator(phases_th, flux_th_R1);
                double[] flux_th_B_obs = new double[phases_obs_B.Length];
                double[] flux_th_V_obs = new double[phases_obs_V.Length];
                double[] flux_th_R_obs = new double[phases_obs_R.Length];
                for (int i = 0; i < phases_obs_B.Length; i++)
                    flux_th_B_obs[i] = lipB1.Interp(phases_obs_B[i]);
                for (int i = 0; i < phases_obs_V.Length; i++)
                    flux_th_V_obs[i] = lipV1.Interp(phases_obs_V[i]);
                for (int i = 0; i < phases_obs_R.Length; i++)
                    flux_th_R_obs[i] = lipR1.Interp(phases_obs_R[i]);

                /*double ccB = flux_obs_B.Sum() / flux_th_B_obs.Sum();
                double ccV = flux_obs_V.Sum() / flux_th_V_obs.Sum();
                double ccR = flux_obs_R.Sum() / flux_th_R_obs.Sum();*/

                double cc = (flux_obs_B.Sum() + flux_obs_V.Sum() + flux_obs_R.Sum()) / (flux_th_B_obs.Sum() + flux_th_V_obs.Sum() + flux_th_R_obs.Sum());

                double ff = 0;
                for (int i = 0; i < phases_obs_B.Length; i++)
                    ff += Math.Pow((flux_obs_B[i] - flux_th_B_obs[i] * cc), 2);
                for (int i = 0; i < phases_obs_V.Length; i++)
                    ff += Math.Pow((flux_obs_V[i] - flux_th_V_obs[i] * cc), 2);
                for (int i = 0; i < phases_obs_R.Length; i++)
                    ff += Math.Pow((flux_obs_R[i] - flux_th_R_obs[i] * cc), 2);

                return ff * 1e30;
            }
        }

        #endregion

        #region warning


        private void btnClearWarning_Click(object sender, EventArgs e)
        {
            plotFilt1.Clear();
            plotFilt1.Refresh();
            plotFilt2.Clear();
            plotFilt2.Refresh();
            plotFilt3.Clear();
            plotFilt3.Refresh();
            plotFilt4.Clear();
            plotFilt4.Refresh();
            plotFilt5.Clear();
            plotFilt5.Refresh();
            plotFilt6.Clear();
            plotFilt6.Refresh();
        }

        private void btnShowWarning_Click(object sender, EventArgs e)
        {

            PointPlot pp = new PointPlot();
            pp.AbscissaData = phases_obs_filt1;
            pp.OrdinateData = flux_obs_filt1;
            pp.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt1.Add(pp);
            plotFilt1.Refresh();
            plotFilt1.Show();

            PointPlot pp2 = new PointPlot();
            pp2.AbscissaData = phases_obs_filt2;
            pp2.OrdinateData = flux_obs_filt2;
            pp2.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt2.Add(pp2);
            plotFilt2.Refresh();
            plotFilt2.Show();

            PointPlot pp3 = new PointPlot();
            pp3.AbscissaData = phases_obs_filt3;
            pp3.OrdinateData = flux_obs_filt3;
            pp3.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt3.Add(pp3);
            plotFilt3.Refresh();
            plotFilt3.Show();

            PointPlot pp4 = new PointPlot();
            pp4.AbscissaData = phases_obs_filt4;
            pp4.OrdinateData = flux_obs_filt4;
            pp4.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt4.Add(pp4);
            plotFilt4.Refresh();
            plotFilt4.Show();

            PointPlot pp5 = new PointPlot();
            pp5.AbscissaData = phases_obs_filt5;
            pp5.OrdinateData = flux_obs_filt5;
            pp5.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt5.Add(pp5);
            plotFilt5.Refresh();
            plotFilt5.Show();

            PointPlot pp6 = new PointPlot();
            pp6.AbscissaData = phases_obs_filt6;
            pp6.OrdinateData = flux_obs_filt6;
            pp6.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt6.Add(pp6);
            plotFilt6.Refresh();
            plotFilt6.Show();

        }

        private void btnLoadWarning_Click()
        {

            StreamReader srB = new StreamReader("D:/AstroDpt/Pract3kurs/BS Tri/myfiltconv/curve_filt1.dat");

            string[] s = srB.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] mmm;
            phases_obs_filt1 = new double[s.Length];
            flux_obs_filt1 = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_filt1[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_filt1[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            srB.Close();


            srB = new StreamReader("D:/AstroDpt/Pract3kurs/BS Tri/myfiltconv/curve_filt2.dat");
            s = srB.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            phases_obs_filt2 = new double[s.Length];
            flux_obs_filt2 = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_filt2[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_filt2[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            srB.Close();


            srB = new StreamReader("D:/AstroDpt/Pract3kurs/BS Tri/myfiltconv/curve_filt3.dat");
            s = srB.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            phases_obs_filt3 = new double[s.Length];
            flux_obs_filt3 = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_filt3[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_filt3[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            srB.Close();


            srB = new StreamReader("D:/AstroDpt/Pract3kurs/BS Tri/myfiltconv/curve_filt4.dat");
            s = srB.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            phases_obs_filt4 = new double[s.Length];
            flux_obs_filt4 = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_filt4[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_filt4[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            srB.Close();


            srB = new StreamReader("D:/AstroDpt/Pract3kurs/BS Tri/myfiltconv/curve_filt5.dat");
            s = srB.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            phases_obs_filt5 = new double[s.Length];
            flux_obs_filt5 = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_filt5[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_filt5[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            srB.Close();


            srB = new StreamReader("D:/AstroDpt/Pract3kurs/BS Tri/myfiltconv/curve_filt6.dat");
            s = srB.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            phases_obs_filt6 = new double[s.Length];
            flux_obs_filt6 = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                mmm = s[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                phases_obs_filt6[i] = Convert.ToDouble(mmm[0].Replace(".", separator).Replace(",", separator));
                flux_obs_filt6[i] = Convert.ToDouble(mmm[1].Replace(".", separator).Replace(",", separator));
            }
            srB.Close();

            PointPlot pp = new PointPlot();
            pp.AbscissaData = phases_obs_filt1;
            pp.OrdinateData = flux_obs_filt1;
            pp.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt1.Add(pp);
            plotFilt1.Refresh();
            plotFilt1.Show();

            PointPlot pp2 = new PointPlot();
            pp2.AbscissaData = phases_obs_filt2;
            pp2.OrdinateData = flux_obs_filt2;
            pp2.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt2.Add(pp2);
            plotFilt2.Refresh();
            plotFilt2.Show();

            PointPlot pp3 = new PointPlot();
            pp3.AbscissaData = phases_obs_filt3;
            pp3.OrdinateData = flux_obs_filt3;
            pp3.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt3.Add(pp3);
            plotFilt3.Refresh();
            plotFilt3.Show();

            PointPlot pp4 = new PointPlot();
            pp4.AbscissaData = phases_obs_filt4;
            pp4.OrdinateData = flux_obs_filt4;
            pp4.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt4.Add(pp4);
            plotFilt4.Refresh();
            plotFilt4.Show();

            PointPlot pp5 = new PointPlot();
            pp5.AbscissaData = phases_obs_filt5;
            pp5.OrdinateData = flux_obs_filt5;
            pp5.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt5.Add(pp5);
            plotFilt5.Refresh();
            plotFilt5.Show();

            PointPlot pp6 = new PointPlot();
            pp6.AbscissaData = phases_obs_filt6;
            pp6.OrdinateData = flux_obs_filt6;
            pp6.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
            plotFilt6.Add(pp6);
            plotFilt6.Refresh();
            plotFilt6.Show();

        }

        private double chi2_warning(double[] pars)
        {
            double psi = pars[0];
            double fi1 = pars[1];
            double dfi = pars[2];
            double beta = pars[3];
            double ksi = pars[4];

            if (psi < 0 || fi1 < 0 || dfi < 0 || beta < 0 || ksi < 0)
                return 1e20;
            //else if (psi < lims[0][0] || psi > lims[0][1] || fi1 < lims[1][0] || fi1 > lims[1][1] || dfi < lims[2][0] || dfi > lims[2][1] || beta < lims[3][0] || beta > lims[3][1] || ksi < lims[4][0] || ksi > lims[4][1])
            //    return 1e20;

            else
            {
                double fi2 = fi1 + dfi;
                if (fi2 > 2 * Math.PI) fi2 = Math.PI * 2;

                double theta, cos_gamma;
                int Ns = 100;

                Star star1 = new Star(inc, beta, psi);
                star1.AddArc(fi1, fi2, ksi, Ns, mag_str, kT, sp);

                double[] flux_th_F11 = new double[phases_th.Length];
                double[] flux_th_F22 = new double[phases_th.Length];
                double[] flux_th_F33 = new double[phases_th.Length];
                double[] flux_th_F44 = new double[phases_th.Length];
                double[] flux_th_F55 = new double[phases_th.Length];
                double[] flux_th_F66 = new double[phases_th.Length];

                for (int r = 0; r < phases_th.Length; r++)
                {
                    star1.SetPhase(phases_th[r]);

                    flux_th_F11[r] = 0;
                    flux_th_F22[r] = 0;
                    flux_th_F33[r] = 0;
                    flux_th_F44[r] = 0;
                    flux_th_F55[r] = 0;
                    flux_th_F66[r] = 0;
                    
                    for (int i = 0; i < star1.get_arcs_count; i++)
                    {
                        for (int j = 0; j < Ns; j++)
                        {
                            theta = star1.GetTheta(i, j);
                            cos_gamma = star1.GetCosGamma(i, j);

                            if (cos_gamma > 0)
                            {
                                if (theta < Math.PI / 2)
                                {
                                    flux_th_F11[r] += ISF1.Interp(theta) * cos_gamma;
                                    flux_th_F22[r] += ISF2.Interp(theta) * cos_gamma;
                                    flux_th_F33[r] += ISF3.Interp(theta) * cos_gamma;
                                    flux_th_F44[r] += ISF4.Interp(theta) * cos_gamma;
                                    flux_th_F55[r] += ISF5.Interp(theta) * cos_gamma;
                                    flux_th_F66[r] += ISF6.Interp(theta) * cos_gamma;
                                }
                                else
                                {
                                    flux_th_F11[r] += ISF1.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_F22[r] += ISF2.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_F33[r] += ISF3.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_F44[r] += ISF4.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_F55[r] += ISF5.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_F66[r] += ISF6.Interp(Math.PI - theta) * cos_gamma;
                                }
                            }
                        }
                    }
                }
                LinInterpolator lipF1 = new LinInterpolator(phases_th, flux_th_F11);
                LinInterpolator lipF2 = new LinInterpolator(phases_th, flux_th_F22);
                LinInterpolator lipF3 = new LinInterpolator(phases_th, flux_th_F33);
                LinInterpolator lipF4 = new LinInterpolator(phases_th, flux_th_F44);
                LinInterpolator lipF5 = new LinInterpolator(phases_th, flux_th_F55);
                LinInterpolator lipF6 = new LinInterpolator(phases_th, flux_th_F66);
                double[] flux_th_F1_obs = new double[phases_obs_filt1.Length];
                double[] flux_th_F2_obs = new double[phases_obs_filt2.Length];
                double[] flux_th_F3_obs = new double[phases_obs_filt3.Length];
                double[] flux_th_F4_obs = new double[phases_obs_filt4.Length];
                double[] flux_th_F5_obs = new double[phases_obs_filt5.Length];
                double[] flux_th_F6_obs = new double[phases_obs_filt6.Length];
                for (int i = 0; i < phases_obs_filt1.Length; i++)
                    flux_th_F1_obs[i] = lipF1.Interp(phases_obs_filt1[i]);
                for (int i = 0; i < phases_obs_filt2.Length; i++)
                    flux_th_F2_obs[i] = lipF2.Interp(phases_obs_filt2[i]);
                for (int i = 0; i < phases_obs_filt3.Length; i++)
                    flux_th_F3_obs[i] = lipF3.Interp(phases_obs_filt3[i]);
                for (int i = 0; i < phases_obs_filt4.Length; i++)
                    flux_th_F4_obs[i] = lipF4.Interp(phases_obs_filt4[i]);
                for (int i = 0; i < phases_obs_filt5.Length; i++)
                    flux_th_F5_obs[i] = lipF5.Interp(phases_obs_filt5[i]);
                for (int i = 0; i < phases_obs_filt6.Length; i++)
                    flux_th_F6_obs[i] = lipF6.Interp(phases_obs_filt6[i]);

                /*double ccB = flux_obs_B.Sum() / flux_th_B_obs.Sum();
                double ccV = flux_obs_V.Sum() / flux_th_V_obs.Sum();
                double ccR = flux_obs_R.Sum() / flux_th_R_obs.Sum();*/

                //double cc = (flux_obs_B.Sum() + flux_obs_V.Sum() + flux_obs_R.Sum()) / (flux_th_B_obs.Sum() + flux_th_V_obs.Sum() + flux_th_R_obs.Sum());
                double cc = (flux_obs_filt1.Sum() + flux_obs_filt2.Sum() + flux_obs_filt3.Sum() + flux_obs_filt4.Sum() + flux_obs_filt5.Sum() + flux_obs_filt6.Sum()) / (flux_th_F1_obs.Sum()+flux_th_F2_obs.Sum()+flux_th_F3_obs.Sum()+flux_th_F4_obs.Sum()+flux_th_F5_obs.Sum()+flux_th_F6_obs.Sum());

                double ff = 0;
                for (int i = 0; i < phases_obs_filt1.Length; i++)
                    ff += Math.Pow((flux_obs_filt1[i] - flux_th_F1_obs[i] * cc), 2);
                for (int i = 0; i < phases_obs_filt2.Length; i++)
                    ff += Math.Pow((flux_obs_filt2[i] - flux_th_F2_obs[i] * cc), 2);
                for (int i = 0; i < phases_obs_filt3.Length; i++)
                    ff += Math.Pow((flux_obs_filt3[i] - flux_th_F3_obs[i] * cc), 2);
                for (int i = 0; i < phases_obs_filt4.Length; i++)
                    ff += Math.Pow((flux_obs_filt4[i] - flux_th_F4_obs[i] * cc), 2);
                for (int i = 0; i < phases_obs_filt5.Length; i++)
                    ff += Math.Pow((flux_obs_filt5[i] - flux_th_F5_obs[i] * cc), 2);
                for (int i = 0; i < phases_obs_filt6.Length; i++)
                    ff += Math.Pow((flux_obs_filt6[i] - flux_th_F6_obs[i] * cc), 2);


                return ff * 1e30;
            }
        }

        #endregion

        private void btnLC_Click(object sender, EventArgs e)
        {
            inc = Convert.ToDouble(txtInc.Text.Replace(".", separator).Replace(",", separator)) * Math.PI / 180;
            mag_str = Convert.ToDouble(txtB.Text.Replace(".", separator).Replace(",", separator));
            kT = Convert.ToDouble(txtkT.Text.Replace(".", separator).Replace(",", separator));
            sp = Math.Pow(10, Convert.ToDouble(txtSP.Text.Replace(".", separator).Replace(",", separator)));

            int pop_size = Convert.ToInt32(txtPop.Text.Replace(".", separator).Replace(",", separator));
            int mode;
            if (radioButton1.Checked) mode = 1;
            else if (radioButton2.Checked) mode = 2;
            else mode = 0;


            int n_gener;
            if (mode == 1)
            {
                n_gener = Convert.ToInt32(txtGen.Text.Replace(".", separator).Replace(",", separator));
            }
            else if (mode == 2)
            {
                n_gener = Convert.ToInt32(txtMC.Text.Replace(".", separator).Replace(",", separator));
            }
            else n_gener = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = n_gener;

            star = new Star(inc, 0, 0);
            star.AddArc(0, 0.1, 0.1, 100, mag_str, kT, sp);

            lims = new double[5][];
            for (int i = 0; i < 5; i++)
                lims[i] = new double[2]; 
            
            lims[0][0] = 0;
            lims[0][1] = 2 * Math.PI; // psi
            lims[1][0] = 0;
            lims[1][1] = 2 * Math.PI; // fi1
            lims[2][0] = 0;
            lims[2][1] = 2 * Math.PI; // dfi
            lims[3][0] = 0;
            lims[3][1] = Math.PI / 2; // beta
            lims[4][0] = 0;
            lims[4][1] = Math.PI / 6; // ksi 

            /*lims[0][0] = 100 *Math.PI/180;
            lims[0][1] = 130 * Math.PI / 180;
            lims[1][0] = 0 * Math.PI / 180;
            lims[1][1] = 10 * Math.PI / 180;
            lims[2][0] = 50 * Math.PI / 180; 
            lims[2][1] = 70 * Math.PI / 180; 
            lims[3][0] = 25 * Math.PI / 180;
            lims[3][1] = 35 * Math.PI / 180;
            lims[4][0] = 60 * Math.PI / 180;
            lims[4][1] = 80 * Math.PI / 180;*/

            int[] precs = new int[5];
            precs[0] = 4;
            precs[1] = 4;
            precs[2] = 4;
            precs[3] = 4;
            precs[4] = 4;

            bool max = false;

            phases_th = new double[50];
            for (int l = 0; l < 50; l++)
            {
                phases_th[l] = l * 0.02;
            }

            int ll = Convert.ToInt32(Math.Round(Math.PI / 2 / 0.01));
            FuncDel Func = new FuncDel(chi2_BVR);
            if (tabControl1.SelectedIndex == 0)
            {
                IstokesV = new StokesParsProvider("grid_stockesI_V_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_V_Te" + Convert.ToString(kT)
                + ".dat", "grid_stockesV_V_Te" + Convert.ToString(kT) + ".dat");

                flux_th_V = new double[phases_th.Length];

                IStokesV = new double[ll];
                thetass = new double[ll];
                for (int i = 0; i < ll; i++)
                {
                    thetass[i] = i * 0.01;
                    IStokesV[i] = IstokesV.StokesI(mag_str, thetass[i], sp);
                }
                ISV = new LinInterpolator(thetass, IStokesV);

                Func = new FuncDel(chi2_V);
            }

            else if (tabControl1.SelectedIndex == 1)
            {
                IstokesB = new StokesParsProvider("grid_stockesI_B_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_B_Te" + Convert.ToString(kT)
                + ".dat", "grid_stockesV_B_Te" + Convert.ToString(kT) + ".dat");
                IstokesV = new StokesParsProvider("grid_stockesI_V_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_V_Te" + Convert.ToString(kT)
                    + ".dat", "grid_stockesV_V_Te" + Convert.ToString(kT) + ".dat");
                IstokesR = new StokesParsProvider("grid_stockesI_R_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_R_Te" + Convert.ToString(kT)
                    + ".dat", "grid_stockesV_R_Te" + Convert.ToString(kT) + ".dat");

                flux_th_B = new double[phases_th.Length];
                flux_th_V = new double[phases_th.Length];
                flux_th_R = new double[phases_th.Length];

                IStokesB = new double[ll];
                IStokesV = new double[ll];
                IStokesR = new double[ll];
                thetass = new double[ll];
                for (int i = 0; i < ll; i++)
                {
                    thetass[i] = i * 0.01;
                    IStokesB[i] = IstokesB.StokesI(mag_str, thetass[i], sp);
                    IStokesV[i] = IstokesV.StokesI(mag_str, thetass[i], sp);
                    IStokesR[i] = IstokesR.StokesI(mag_str, thetass[i], sp);
                }

                ISB = new LinInterpolator(thetass, IStokesB);
                ISV = new LinInterpolator(thetass, IStokesV);
                ISR = new LinInterpolator(thetass, IStokesR);

                Func = new FuncDel(chi2_BVR);
            }

            else if (tabControl1.SelectedIndex == 2)
            {
                stokesV = new StokesParsProvider("grid_stockesI_V_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_V_Te" + Convert.ToString(kT)
                    + ".dat", "grid_stockesV_V_Te" + Convert.ToString(kT) + ".dat");

                flux_th_I = new double[phases_th.Length];
                flux_th_V = new double[phases_th.Length];

                IStokesV = new double[ll];
                VStokesV = new double[ll];
                thetass = new double[ll];
                for (int i = 0; i < ll; i++)
                {
                    thetass[i] = i * 0.01;
                    IStokesV[i] = stokesV.StokesI(mag_str, thetass[i], sp);
                    VStokesV[i] = stokesV.StokesV(mag_str, thetass[i], sp);
                }
                ISV = new LinInterpolator(thetass, IStokesV);
                VSV = new LinInterpolator(thetass, VStokesV);

                Func = new FuncDel(chi2_IVStokes);
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                btnLoadWarning_Click();

                /*IstokesB = new StokesParsProvider("grid_stockesI_B_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_B_Te" + Convert.ToString(kT)
                + ".dat", "grid_stockesV_B_Te" + Convert.ToString(kT) + ".dat");
                IstokesV = new StokesParsProvider("grid_stockesI_V_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_V_Te" + Convert.ToString(kT)
                    + ".dat", "grid_stockesV_V_Te" + Convert.ToString(kT) + ".dat");
                IstokesR = new StokesParsProvider("grid_stockesI_R_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_R_Te" + Convert.ToString(kT)
                    + ".dat", "grid_stockesV_R_Te" + Convert.ToString(kT) + ".dat");*/
                IstokesF1 = new StokesParsProvider("grid_stockesI_filt1_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_filt1_Te" + Convert.ToString(kT)
                + ".dat", "grid_stockesV_filt1_Te" + Convert.ToString(kT) + ".dat");
                IstokesF2 = new StokesParsProvider("grid_stockesI_filt2_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_filt2_Te" + Convert.ToString(kT)
                + ".dat", "grid_stockesV_filt2_Te" + Convert.ToString(kT) + ".dat");
                IstokesF3 = new StokesParsProvider("grid_stockesI_filt3_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_filt3_Te" + Convert.ToString(kT)
                + ".dat", "grid_stockesV_filt3_Te" + Convert.ToString(kT) + ".dat");
                IstokesF4 = new StokesParsProvider("grid_stockesI_filt4_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_filt4_Te" + Convert.ToString(kT)
                + ".dat", "grid_stockesV_filt4_Te" + Convert.ToString(kT) + ".dat");
                IstokesF5 = new StokesParsProvider("grid_stockesI_filt5_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_filt5_Te" + Convert.ToString(kT)
                + ".dat", "grid_stockesV_filt5_Te" + Convert.ToString(kT) + ".dat");
                IstokesF6 = new StokesParsProvider("grid_stockesI_filt6_Te" + Convert.ToString(kT) + ".dat", "grid_stockesQ_filt6_Te" + Convert.ToString(kT)
                + ".dat", "grid_stockesV_filt6_Te" + Convert.ToString(kT) + ".dat");


                flux_th_filt1 = new double[phases_th.Length];
                flux_th_filt2 = new double[phases_th.Length];
                flux_th_filt3 = new double[phases_th.Length];
                flux_th_filt4 = new double[phases_th.Length];
                flux_th_filt5 = new double[phases_th.Length];
                flux_th_filt6 = new double[phases_th.Length];

                IStokesF1 = new double[ll];
                IStokesF2 = new double[ll];
                IStokesF3 = new double[ll];
                IStokesF4 = new double[ll];
                IStokesF5 = new double[ll];
                IStokesF6 = new double[ll];

                thetass = new double[ll];
                for (int i = 0; i < ll; i++)
                {
                    thetass[i] = i * 0.01;

                    IStokesF1[i] = IstokesF1.StokesI(mag_str, thetass[i], sp);
                    IStokesF2[i] = IstokesF2.StokesI(mag_str, thetass[i], sp);
                    IStokesF3[i] = IstokesF3.StokesI(mag_str, thetass[i], sp);
                    IStokesF4[i] = IstokesF4.StokesI(mag_str, thetass[i], sp);
                    IStokesF5[i] = IstokesF5.StokesI(mag_str, thetass[i], sp);
                    IStokesF6[i] = IstokesF6.StokesI(mag_str, thetass[i], sp);
                }

                ISF1 = new LinInterpolator(thetass, IStokesF1);
                ISF2 = new LinInterpolator(thetass, IStokesF2);
                ISF3 = new LinInterpolator(thetass, IStokesF3);
                ISF4 = new LinInterpolator(thetass, IStokesF4);
                ISF5 = new LinInterpolator(thetass, IStokesF5);
                ISF6 = new LinInterpolator(thetass, IStokesF6);

                Func = new FuncDel(chi2_warning);
            }
            else
            {
                MessageBox.Show("None of modes not seems to be chosen.", "Oops... Something wrong");
            }

            FuncDel2 prgr = new FuncDel2(SetProgress);
            if (mode == 1)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                PereGenetic ga = new PereGenetic(5, lims, precs, max, Func, pop_size, n_gener, 0.8, 0.05, prgr);
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Debug.WriteLine("GA RunTime " + elapsedTime);

                solution = ga.GetBest();
                Debug.WriteLine(solution[0] * 180 / Math.PI);
                Debug.WriteLine(solution[1] * 180 / Math.PI);
                Debug.WriteLine(solution[2] * 180 / Math.PI);
                Debug.WriteLine(solution[3] * 180 / Math.PI);
                Debug.WriteLine(solution[4] * 180 / Math.PI);

                stopWatch = new Stopwatch();
                stopWatch.Start();
                Simplex simplex = new Simplex();
                simplex.Tolerance = 1e-20;
                if (tabControl1.SelectedIndex == 0)
                    solution = simplex.ComputeMin(chi2_V, solution);
                if (tabControl1.SelectedIndex == 1)
                    solution = simplex.ComputeMin(chi2_BVR, solution);
                if (tabControl1.SelectedIndex == 2)
                    solution = simplex.ComputeMin(chi2_IVStokes, solution);
                stopWatch.Stop();
                ts = stopWatch.Elapsed;

                SetProgress(n_gener);

                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Debug.WriteLine("NM RunTime " + elapsedTime);

                Debug.WriteLine(solution[0] * 180 / Math.PI);
                Debug.WriteLine(solution[1] * 180 / Math.PI);
                Debug.WriteLine(solution[2] * 180 / Math.PI);
                Debug.WriteLine(solution[3] * 180 / Math.PI);
                Debug.WriteLine(solution[4] * 180 / Math.PI);
            }
            else if (mode == 2)
            {
                NedoMonteCarlo mc = new NedoMonteCarlo(lims, 5, Func, n_gener, false, prgr);
                solution = mc.GetBest();

                Debug.WriteLine(solution[0] * 180 / Math.PI);
                Debug.WriteLine(solution[1] * 180 / Math.PI);
                Debug.WriteLine(solution[2] * 180 / Math.PI);
                Debug.WriteLine(solution[3] * 180 / Math.PI);
                Debug.WriteLine(solution[4] * 180 / Math.PI);
            }

            txtPsi.Text = Convert.ToString(solution[0] * 180 / Math.PI);
            txtFi1.Text = Convert.ToString(solution[1] * 180 / Math.PI);
            txtdFi.Text = Convert.ToString(solution[2] * 180 / Math.PI);
            txtBeta.Text = Convert.ToString(solution[3] * 180 / Math.PI);
            txtKsi.Text = Convert.ToString(solution[4] * 180 / Math.PI);

            double psi = solution[0];
            double fi1 = solution[1];
            double dfi = solution[2];
            double beta = solution[3];
            double ksi = solution[4];

            double fi2 = fi1 + dfi;
            if (fi2 > 2 * Math.PI) fi2 = Math.PI * 2;

            int Ns = 100;
            double theta, cos_gamma;

            star = new Star(inc, beta, psi);
            star.AddArc(fi1, fi2, ksi, Ns, mag_str, kT, sp);

            phases_th = new double[500];
            for (int l = 0; l < 500; l++)
            {
                phases_th[l] = l * 0.002;
            }

            if (tabControl1.SelectedIndex == 0)
            {
                flux_th_V = new double[phases_th.Length];
                for (int r = 0; r < phases_th.Length; r++)
                {
                    star.SetPhase(phases_th[r]);
                    flux_th_V[r] = 0;
                    for (int i = 0; i < star.get_arcs_count; i++)
                    {
                        for (int j = 0; j < Ns; j++)
                        {
                            theta = star.GetTheta(i, j);
                            cos_gamma = star.GetCosGamma(i, j);

                            if (cos_gamma > 0)
                            {
                                if (theta < Math.PI / 2)
                                    flux_th_V[r] += ISV.Interp(theta) * cos_gamma;
                                else
                                    flux_th_V[r] += ISV.Interp(Math.PI - theta) * cos_gamma;
                            }
                        }
                    }
                }

                LinInterpolator lipV = new LinInterpolator(phases_th, flux_th_V);
                double[] flux_th_V_obs = new double[phases_obs_V.Length];
                for (int i = 0; i < phases_obs_V.Length; i++)
                    flux_th_V_obs[i] = lipV.Interp(phases_obs_V[i]);

                double ccV = flux_obs_V.Sum() / flux_th_V_obs.Sum();

                for (int i = 0; i < phases_th.Length; i++)
                    flux_th_V[i] *= ccV;

                LinePlot lp = new LinePlot();
                lp.AbscissaData = phases_th;
                lp.OrdinateData = flux_th_V;
                lp.Pen = new Pen(Color.SeaGreen, 3);

                plot.Add(lp);
                plot.Refresh();
                plot.Show();

                PointPlot pp = new PointPlot();
                pp.AbscissaData = phases_obs_V;
                pp.OrdinateData = flux_obs_V;
                pp.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
                plot.Add(pp);

                plot.Refresh();
                plot.Show();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                flux_th_B = new double[phases_th.Length];
                flux_th_V = new double[phases_th.Length];
                flux_th_R = new double[phases_th.Length];
                for (int r = 0; r < phases_th.Length; r++)
                {
                    star.SetPhase(phases_th[r]);
                    flux_th_B[r] = 0;
                    flux_th_V[r] = 0;
                    flux_th_R[r] = 0;
                    for (int i = 0; i < star.get_arcs_count; i++)
                    {
                        for (int j = 0; j < Ns; j++)
                        {
                            theta = star.GetTheta(i, j);
                            cos_gamma = star.GetCosGamma(i, j);

                            if (cos_gamma > 0)
                            {
                                if (theta < Math.PI / 2)
                                {
                                    flux_th_B[r] += ISB.Interp(theta) * cos_gamma;
                                    flux_th_V[r] += ISV.Interp(theta) * cos_gamma;
                                    flux_th_R[r] += ISR.Interp(theta) * cos_gamma;
                                }
                                else
                                {
                                    flux_th_B[r] += ISB.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_V[r] += ISV.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_R[r] += ISR.Interp(Math.PI - theta) * cos_gamma;
                                }
                            }
                        }
                    }
                }

                LinInterpolator lipB = new LinInterpolator(phases_th, flux_th_B);
                LinInterpolator lipV = new LinInterpolator(phases_th, flux_th_V);
                LinInterpolator lipR = new LinInterpolator(phases_th, flux_th_R);
                double[] flux_th_B_obs = new double[phases_obs_B.Length];
                double[] flux_th_V_obs = new double[phases_obs_V.Length];
                double[] flux_th_R_obs = new double[phases_obs_R.Length];
                for (int i = 0; i < phases_obs_B.Length; i++)
                    flux_th_B_obs[i] = lipB.Interp(phases_obs_B[i]);
                for (int i = 0; i < phases_obs_V.Length; i++)
                    flux_th_V_obs[i] = lipV.Interp(phases_obs_V[i]);
                for (int i = 0; i < phases_obs_R.Length; i++)
                    flux_th_R_obs[i] = lipR.Interp(phases_obs_R[i]);

                /*double ccB = flux_obs_B.Sum() / flux_th_B_obs.Sum();
                double ccV = flux_obs_V.Sum() / flux_th_V_obs.Sum();
                double ccR = flux_obs_R.Sum() / flux_th_R_obs.Sum();*/

                double cc = (flux_obs_B.Sum() + flux_obs_V.Sum() + flux_obs_R.Sum()) / (flux_th_B_obs.Sum() + flux_th_V_obs.Sum() + flux_th_R_obs.Sum());

                for (int i = 0; i < phases_th.Length; i++)
                {
                    flux_th_B[i] *= cc;
                    flux_th_V[i] *= cc;
                    flux_th_R[i] *= cc;
                }

                LinePlot lp = new LinePlot();
                lp.AbscissaData = phases_th;
                lp.OrdinateData = flux_th_B;
                lp.Pen = new Pen(Color.DarkCyan, 3);

                plotBVRB.Add(lp);
                plotBVRB.Refresh();
                plotBVRB.Show();

                LinePlot lp2 = new LinePlot();
                lp2.AbscissaData = phases_th;
                lp2.OrdinateData = flux_th_V;
                lp2.Pen = new Pen(Color.SeaGreen, 3);

                plotBVRV.Add(lp2);
                plotBVRV.Refresh();
                plotBVRV.Show();

                LinePlot lp3 = new LinePlot();
                lp3.AbscissaData = phases_th;
                lp3.OrdinateData = flux_th_R;
                lp3.Pen = new Pen(Color.Crimson, 3);

                plotBVRR.Add(lp3);
                plotBVRR.Refresh();
                plotBVRR.Show();

                PointPlot pp = new PointPlot();
                pp.AbscissaData = phases_obs_B;
                pp.OrdinateData = flux_obs_B;
                pp.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
                plotBVRB.Add(pp);

                plotBVRB.Refresh();
                plotBVRB.Show();

                PointPlot pp2 = new PointPlot();
                pp2.AbscissaData = phases_obs_V;
                pp2.OrdinateData = flux_obs_V;
                pp2.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
                plotBVRV.Add(pp2);

                plotBVRV.Refresh();
                plotBVRV.Show();

                PointPlot pp3 = new PointPlot();
                pp3.AbscissaData = phases_obs_R;
                pp3.OrdinateData = flux_obs_R;
                pp3.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
                plotBVRR.Add(pp3);

                plotBVRR.Refresh();
                plotBVRR.Show();
            }

            else if (tabControl1.SelectedIndex == 2)
            {
                flux_th_I = new double[phases_th.Length];
                flux_th_V = new double[phases_th.Length];
                for (int r = 0; r < phases_th.Length; r++)
                {
                    star.SetPhase(phases_th[r]);
                    flux_th_I[r] = 0;
                    flux_th_V[r] = 0;
                    for (int i = 0; i < star.get_arcs_count; i++)
                    {
                        for (int j = 0; j < Ns; j++)
                        {
                            theta = star.GetTheta(i, j);
                            cos_gamma = star.GetCosGamma(i, j);

                            if (cos_gamma > 0)
                            {
                                if (theta < Math.PI / 2)
                                {
                                    flux_th_I[r] += ISV.Interp(theta) * cos_gamma;
                                    flux_th_V[r] += VSV.Interp(theta) * cos_gamma;
                                }
                                else
                                {
                                    flux_th_I[r] += ISV.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_V[r] -= VSV.Interp(Math.PI - theta) * cos_gamma;
                                }
                            }
                        }
                    }
                }

                LinInterpolator lipI = new LinInterpolator(phases_th, flux_th_I);
                LinInterpolator lipV = new LinInterpolator(phases_th, flux_th_V);
                double[] flux_th_I_obs = new double[phases_obs_I.Length];
                double[] flux_th_V_obs = new double[phases_obs_V.Length];
                for (int i = 0; i < phases_obs_I.Length; i++)
                    flux_th_I_obs[i] = lipI.Interp(phases_obs_I[i]);
                for (int i = 0; i < phases_obs_V.Length; i++)
                    flux_th_V_obs[i] = lipV.Interp(phases_obs_V[i]);

                double ccI = flux_obs_I.Sum() / flux_th_I_obs.Sum();
                //double ccV = flux_obs_V.Sum() / flux_th_V_obs.Sum();

                for (int i = 0; i < phases_th.Length; i++)
                {
                    flux_th_I[i] *= ccI;
                    flux_th_V[i] *= ccI;
                }

                LinePlot lp = new LinePlot();
                lp.AbscissaData = phases_th;
                lp.OrdinateData = flux_th_I;
                lp.Pen = new Pen(Color.SeaGreen, 3);

                plotIVI.Add(lp);
                plotIVI.Refresh();
                plotIVI.Show();

                LinePlot lp2 = new LinePlot();
                lp2.AbscissaData = phases_th;
                lp2.OrdinateData = flux_th_V;
                lp2.Pen = new Pen(Color.DarkCyan, 3);

                plotIVV.Add(lp2);
                plotIVV.Refresh();
                plotIVV.Show();

                PointPlot ppI = new PointPlot();
                ppI.AbscissaData = phases_obs_I;
                ppI.OrdinateData = flux_obs_I;
                ppI.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
                plotIVI.Add(ppI);

                plotIVI.Refresh();
                plotIVI.Show();

                PointPlot ppV = new PointPlot();
                ppV.AbscissaData = phases_obs_V;
                ppV.OrdinateData = flux_obs_V;
                ppV.Marker = new Marker(Marker.MarkerType.FilledCircle, 5);
                plotIVV.Add(ppV);

                plotIVV.Refresh();
                plotIVV.Show();
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                flux_th_filt1 = new double[phases_th.Length];
                flux_th_filt2 = new double[phases_th.Length];
                flux_th_filt3 = new double[phases_th.Length];
                flux_th_filt4 = new double[phases_th.Length];
                flux_th_filt5 = new double[phases_th.Length];
                flux_th_filt6 = new double[phases_th.Length];

                for (int r = 0; r < phases_th.Length; r++)
                {
                    star.SetPhase(phases_th[r]);
                    flux_th_filt1[r] = 0;
                    flux_th_filt2[r] = 0;
                    flux_th_filt3[r] = 0;
                    flux_th_filt4[r] = 0;
                    flux_th_filt5[r] = 0;
                    flux_th_filt6[r] = 0;
                    for (int i = 0; i < star.get_arcs_count; i++)
                    {
                        for (int j = 0; j < Ns; j++)
                        {
                            theta = star.GetTheta(i, j);
                            cos_gamma = star.GetCosGamma(i, j);

                            if (cos_gamma > 0)
                            {
                                if (theta < Math.PI / 2)
                                {
                                    flux_th_filt1[r] += ISF1.Interp(theta) * cos_gamma;
                                    flux_th_filt2[r] += ISF2.Interp(theta) * cos_gamma;
                                    flux_th_filt3[r] += ISF3.Interp(theta) * cos_gamma;
                                    flux_th_filt4[r] += ISF4.Interp(theta) * cos_gamma;
                                    flux_th_filt5[r] += ISF5.Interp(theta) * cos_gamma;
                                    flux_th_filt6[r] += ISF6.Interp(theta) * cos_gamma;
                                }
                                else
                                {
                                    flux_th_filt1[r] += ISF1.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_filt2[r] += ISF2.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_filt3[r] += ISF3.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_filt4[r] += ISF4.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_filt5[r] += ISF5.Interp(Math.PI - theta) * cos_gamma;
                                    flux_th_filt6[r] += ISF6.Interp(Math.PI - theta) * cos_gamma;
                                }
                            }
                        }
                    }
                }

                LinInterpolator lipF1 = new LinInterpolator(phases_th, flux_th_filt1);
                LinInterpolator lipF2 = new LinInterpolator(phases_th, flux_th_filt2);
                LinInterpolator lipF3 = new LinInterpolator(phases_th, flux_th_filt3);
                LinInterpolator lipF4 = new LinInterpolator(phases_th, flux_th_filt4);
                LinInterpolator lipF5 = new LinInterpolator(phases_th, flux_th_filt5);
                LinInterpolator lipF6 = new LinInterpolator(phases_th, flux_th_filt6);
                double[] flux_th_F1_obs = new double[phases_obs_filt1.Length];
                double[] flux_th_F2_obs = new double[phases_obs_filt2.Length];
                double[] flux_th_F3_obs = new double[phases_obs_filt3.Length];
                double[] flux_th_F4_obs = new double[phases_obs_filt4.Length];
                double[] flux_th_F5_obs = new double[phases_obs_filt5.Length];
                double[] flux_th_F6_obs = new double[phases_obs_filt6.Length];
                for (int i = 0; i < phases_obs_filt1.Length; i++)
                    flux_th_F1_obs[i] = lipF1.Interp(phases_obs_filt1[i]);
                for (int i = 0; i < phases_obs_filt2.Length; i++)
                    flux_th_F2_obs[i] = lipF2.Interp(phases_obs_filt2[i]);
                for (int i = 0; i < phases_obs_filt3.Length; i++)
                    flux_th_F3_obs[i] = lipF3.Interp(phases_obs_filt3[i]);
                for (int i = 0; i < phases_obs_filt4.Length; i++)
                    flux_th_F4_obs[i] = lipF4.Interp(phases_obs_filt4[i]);
                for (int i = 0; i < phases_obs_filt5.Length; i++)
                    flux_th_F5_obs[i] = lipF5.Interp(phases_obs_filt5[i]);
                for (int i = 0; i < phases_obs_filt6.Length; i++)
                    flux_th_F6_obs[i] = lipF6.Interp(phases_obs_filt6[i]);

                /*double ccB = flux_obs_B.Sum() / flux_th_B_obs.Sum();
                double ccV = flux_obs_V.Sum() / flux_th_V_obs.Sum();
                double ccR = flux_obs_R.Sum() / flux_th_R_obs.Sum();*/

                //double cc = (flux_obs_B.Sum() + flux_obs_V.Sum() + flux_obs_R.Sum()) / (flux_th_B_obs.Sum() + flux_th_V_obs.Sum() + flux_th_R_obs.Sum());
                double cc = (flux_obs_filt1.Sum() + flux_obs_filt2.Sum() + flux_obs_filt3.Sum() + flux_obs_filt4.Sum() + flux_obs_filt5.Sum() + flux_obs_filt6.Sum()) / (flux_th_F1_obs.Sum() + flux_th_F2_obs.Sum() + flux_th_F3_obs.Sum() + flux_th_F4_obs.Sum() + flux_th_F5_obs.Sum() + flux_th_F6_obs.Sum());


                for (int i = 0; i < phases_th.Length; i++)
                {
                    flux_th_filt1[i] *= cc;
                    flux_th_filt2[i] *= cc;
                    flux_th_filt3[i] *= cc;
                    flux_th_filt4[i] *= cc;
                    flux_th_filt5[i] *= cc;
                    flux_th_filt6[i] *= cc;
                }

                LinePlot lp = new LinePlot();
                lp.AbscissaData = phases_th;
                lp.OrdinateData = flux_th_filt1;
                lp.Pen = new Pen(Color.DarkCyan, 3);
                plotFilt1.Add(lp);
                plotFilt1.Refresh();
                plotFilt1.Show();

                LinePlot lp2 = new LinePlot();
                lp2.AbscissaData = phases_th;
                lp2.OrdinateData = flux_th_filt2;
                lp2.Pen = new Pen(Color.DarkCyan, 3);
                plotFilt2.Add(lp2);
                plotFilt2.Refresh();
                plotFilt2.Show();

                LinePlot lp3 = new LinePlot();
                lp3.AbscissaData = phases_th;
                lp3.OrdinateData = flux_th_filt3;
                lp3.Pen = new Pen(Color.DarkCyan, 3);
                plotFilt3.Add(lp3);
                plotFilt3.Refresh();
                plotFilt3.Show();

                LinePlot lp4 = new LinePlot();
                lp4.AbscissaData = phases_th;
                lp4.OrdinateData = flux_th_filt4;
                lp4.Pen = new Pen(Color.DarkCyan, 3);
                plotFilt4.Add(lp4);
                plotFilt4.Refresh();
                plotFilt4.Show();

                LinePlot lp5 = new LinePlot();
                lp5.AbscissaData = phases_th;
                lp5.OrdinateData = flux_th_filt5;
                lp5.Pen = new Pen(Color.DarkCyan, 3);
                plotFilt5.Add(lp5);
                plotFilt5.Refresh();
                plotFilt5.Show();

                LinePlot lp6 = new LinePlot();
                lp6.AbscissaData = phases_th;
                lp6.OrdinateData = flux_th_filt6;
                lp6.Pen = new Pen(Color.DarkCyan, 3);
                plotFilt6.Add(lp6);
                plotFilt6.Refresh();
                plotFilt6.Show();
            }

            btnSaveLC.Enabled = true;
            btn_SavePars.Enabled = true;
        }

        private void SetProgress(int progress)
        {
            progressBar1.Value = progress;
        }

        private void btnSaveLC_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string path = saveFileDialog1.FileName;
            StreamWriter sw = new StreamWriter(path);
            if (tabControl1.SelectedIndex == 0)
            {
                for (int i = 0; i < phases_th.Length; i++)
                    sw.WriteLine("{0:0.000}\t{1:0.000e0}", phases_th[i], flux_th_V[i]);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                for (int i = 0; i < phases_th.Length; i++)
                    sw.WriteLine("{0:0.000}\t{1:0.000e0}\t{2:0.000e0}\t{3:0.000e0}", phases_th[i], flux_th_B[i], flux_th_V[i], flux_th_R[i]);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                for (int i = 0; i < phases_th.Length; i++)
                    sw.WriteLine("{0:0.000}\t{1:0.000e0}\t{2:0.000e0}", phases_th[i], flux_th_I[i], flux_th_V[i]);
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                for (int i = 0; i < phases_th.Length; i++)
                    sw.WriteLine("{0:0.000}\t{1:0.000e0}\t{2:0.000e0}\t{3:0.000e0}\t{4:0.000e0}\t{5:0.000e0}\t{6:0.000e0}", phases_th[i], flux_th_filt1[i], flux_th_filt2[i], flux_th_filt3[i], flux_th_filt4[i], flux_th_filt5[i], flux_th_filt6[i]);
            }
            sw.Close();
        }

        private void btn_SavePars_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string path = saveFileDialog1.FileName;
            StreamWriter sw = new StreamWriter(path);
            double psi = solution[0];
            double fi1 = solution[1];
            double dfi = solution[2];
            double beta = solution[3];
            double ksi = solution[4];

            if (tabControl1.SelectedIndex == 0)
                sw.WriteLine("With V lightcurve" + "\n");
            if (tabControl1.SelectedIndex == 1)
                sw.WriteLine("With B V R lightcurve" + "\n");
            if (tabControl1.SelectedIndex == 2)
                sw.WriteLine("With V lightcurve and V circular polarization curve" + "\n");
            if (tabControl1.SelectedIndex == 3)
                sw.WriteLine("With our filters" + "\n");

            sw.WriteLine("psi = {0:0.000}, beta = {1:0.000}, ksi = {2:0.000}, fi1 = {3:0.000}, dfi = {4:0.000}", psi, beta, ksi, fi1, dfi);

            sw.Close();
        }
    }
}
