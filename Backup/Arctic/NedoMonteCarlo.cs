using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using DotNumerics.Optimization;
using System.Threading.Tasks;

namespace Arctic
{
    class NedoMonteCarlo
    {
        private Arctic.FuncDel func;
        private int n_pars, n_carlo;
        private double[] pars_LM, chis;
        private double[][] lims, sols;
        private bool max;

        public NedoMonteCarlo(double[][] lims, int n_pars, Arctic.FuncDel func, int n_carlo, bool max, Arctic.FuncDel2 prgr)
        {
            this.func = func;
            this.n_pars = n_pars;
            this.lims = lims;
            this.max = max;
            this.n_carlo = n_carlo;

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            chis = new double[n_carlo];
            sols = new double[n_carlo][];
            for (int i = 0; i < n_carlo; i++)
                sols[i] = new double[n_pars];

            /*Thread thr1 = new Thread(new ThreadStart(DoThr1));
            Thread thr2 = new Thread(new ThreadStart(DoThr2));
            Thread thr3 = new Thread(new ThreadStart(DoThr3));
            Thread thr4 = new Thread(new ThreadStart(DoThr4));

            thr1.Start();
            thr2.Start();
            thr3.Start();
            thr4.Start();

            thr1.Join();
            thr2.Join();
            thr3.Join();
            thr4.Join();

            Sort();*/

            /*for (int j = 0; j < n_carlo; j++)
            {
                for (int i = 0; i < this.n_pars; i++)
                    sols[j][i] = rand.NextDouble() * lims[i][1];

                //sols[j] = Nelder_Mead.NM(5, sols[j], false, lims, func);


                chis[j] = func(sols[j]);
                Debug.WriteLine(j);
            }

            Sort();*/
            /*Array.Resize(ref sols, n_carlo / 100);
            for (int i = 0; i < sols.Length; i++)
            {
                Debug.WriteLine(i);
                sols[i] = Nelder_Mead.NM(5, sols[i], false, lims, func);
            }
            Sort();*/

            int kk = 0;
            Parallel.For(0, n_carlo, j =>
            {
                Simplex simplex = new Simplex();
                simplex.Tolerance = 1e-20;

                for (int i = 0; i < this.n_pars; i++)
                    sols[j][i] = rand.NextDouble() * lims[i][1];

                //sols[j] = Nelder_Mead.NM(5, sols[j], false, lims, func);

                sols[j] = simplex.ComputeMin(fuunc, sols[j]);

                chis[j] = func(sols[j]);
                Debug.WriteLine(j);
                kk++;
                prgr(kk);
            });
            Sort();
        }

        public double fuunc(double[] pars)
        {
            return func(pars);
        }

        /*private void DoThr1()
        {
            Simplex simplex = new Simplex();
            simplex.Tolerance = 1e-20;

            Random rand1 = new Random(Guid.NewGuid().GetHashCode());
            for (int j = 0; j < n_carlo/4; j++)
            {
                for (int i = 0; i < this.n_pars; i++)
                    sols[j][i] = rand1.NextDouble() * lims[i][1];

                //sols[j] = Nelder_Mead.NM(5, sols[j], false, lims, func);

                sols = simplex.ComputeMin(func, sols);

                chis[j] = func(sols[j]);
                Debug.WriteLine(j);
            }
        }

        private void DoThr2()
        {
            Random rand2 = new Random(Guid.NewGuid().GetHashCode());
            for (int j = n_carlo / 4; j < n_carlo / 2; j++)
            {
                for (int i = 0; i < this.n_pars; i++)
                    sols[j][i] = rand2.NextDouble() * lims[i][1];

                //sols[j] = Nelder_Mead.NM(5, sols[j], false, lims, func);

                chis[j] = func(sols[j]);
                Debug.WriteLine(j);
            }
        }

        private void DoThr3()
        {
            Random rand3 = new Random(Guid.NewGuid().GetHashCode());
            for (int j = n_carlo / 2; j < 3 * n_carlo / 4; j++)
            {
                for (int i = 0; i < this.n_pars; i++)
                    sols[j][i] = rand3.NextDouble() * lims[i][1];

                //sols[j] = Nelder_Mead.NM(5, sols[j], false, lims, func);

                chis[j] = func(sols[j]);
                Debug.WriteLine(j);
            }
        }

        private void DoThr4()
        {
            Random rand4 = new Random(Guid.NewGuid().GetHashCode());
            for (int j = 3* n_carlo / 4; j < n_carlo; j++)
            {
                for (int i = 0; i < this.n_pars; i++)
                    sols[j][i] = rand4.NextDouble() * lims[i][1];

                //sols[j] = Nelder_Mead.NM(5, sols[j], false, lims, func);

                chis[j] = func(sols[j]);
                Debug.WriteLine(j);
            }
        }*/

        public double[] GetBest()
        {
            return sols[0];
        }

        private void Sort()
        {
            double temp;
            double[] temp1;

            if (max)
            {
                for (int i = 0; i < chis.Length; i++)
                {
                    for (int j = i + 1; j < chis.Length; j++)
                    {
                        if (chis[i] < chis[j])
                        {
                            temp = chis[i];
                            chis[i] = chis[j];
                            chis[j] = temp;

                            temp1 = sols[i];
                            sols[i] = sols[j];
                            sols[j] = temp1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < chis.Length; i++)
                {
                    for (int j = i + 1; j < chis.Length; j++)
                    {
                        if (chis[i] > chis[j])
                        {
                            temp = chis[i];
                            chis[i] = chis[j];
                            chis[j] = temp;

                            temp1 = sols[i];
                            sols[i] = sols[j];
                            sols[j] = temp1;
                        }
                    }
                }
            }
        }
    }
}
