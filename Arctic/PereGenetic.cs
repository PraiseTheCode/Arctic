using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;


namespace Arctic
{
    class PereGenetic
    {
        private int paramN, PLen;
        private double CrossProb, MutationProb;
        private int[] precs;
        private double[][] lims;
        private bool max;
        private Arctic.FuncDel func;
        private double[] chi2;
        private BetterPerson[] popNew;


        //private Random GlobalRandom = new Random();

        private BetterPerson[] population;

        public PereGenetic(int paramN, double[][] lims, int[] precs, bool max, Arctic.FuncDel func, int PLen, int NGener, double CrossProb, double MutationProb, Arctic.FuncDel2 prgr)
        {
            this.paramN = paramN;
            this.lims = lims;
            this.precs = precs;
            this.max = max;
            this.func = func;
            this.PLen = PLen;
            this.CrossProb = CrossProb;
            this.MutationProb = MutationProb;

            population = new BetterPerson[PLen];
            for (int i = 0; i < PLen; i++)
                population[i] = new BetterPerson(paramN, precs, lims);

            chi2 = new double[3 * PLen];

            for (int i = 0; i < NGener; i++)
            {
                Debug.WriteLine("Generation " + Convert.ToString(i));
                popNew = Cross(population);
                Mutate(ref popNew);
                popNew = Con(population, popNew);

                Thread thr1 = new Thread(new ThreadStart(Thr1_chi2));
                Thread thr2 = new Thread(new ThreadStart(Thr2_chi2));
                Thread thr3 = new Thread(new ThreadStart(Thr3_chi2));
                Thread thr4 = new Thread(new ThreadStart(Thr4_chi2));

                thr1.Start();
                thr2.Start();
                thr3.Start();
                thr4.Start();

                thr1.Join();
                thr2.Join();
                thr3.Join();
                thr4.Join();

                population = Sorted(ref popNew);
                Array.Resize(ref population, PLen);
                //population = ThisIsSparta(Sorted(ref popNew));
                //Debug.Write("generation: {0}, chi2: {1} \n", i, func(population[0].GetPheno()));
                //Debug.WriteLine("generation: " + Convert.ToString(i) + ", chi2: " + Convert.ToString(func(population[0].GetPheno())));
                /*using (StreamWriter sw = new StreamWriter("D:/50000x30.txt", true, System.Text.Encoding.Default))
                {
                    Str = Convert.ToString(i) + "\t" + Convert.ToString(func(population[0].GetPheno())) + "\n";
                    sw.WriteLine(Str);
                }*/

                prgr(i);
            }
        }

        public double[] GetBest()
        {
            return population[0].GetPheno();
        }

        private void Thr1_chi2()
        {
            bool flag;
            int count1;
            int npop = 0;
            chi2[0] = func(popNew[0].GetPheno());
            for (int i = 1; i < 3*PLen/4; i++)
            {
                flag = false;
                count1 = 0;
                for (int j = 0; j < paramN; j++)
                {
                    if (popNew[i].GetPheno()[j] == popNew[i-1].GetPheno()[j])
                        count1 += 1;
                }
                if (count1 == paramN)
                {
                    flag = true;
                    //Debug.WriteLine("flag on! i: " + Convert.ToString(i) + "\t k: " + Convert.ToString(k));
                    //count_flags += 1;
                    npop = i-1;
                    break;
                }
                if (flag) chi2[i] = chi2[npop];
                else chi2[i] = func(popNew[i].GetPheno());
                //Debug.WriteLine("1 thread " + Convert.ToString(i));
                //chi2[i] = func(popNew[i].GetPheno());
            }
        }
        private void Thr2_chi2()
        {
            bool flag;
            int count1;
            int[] equ;
            int npop = 0;
            chi2[3 * PLen / 4] = func(popNew[3 * PLen / 4].GetPheno());
            for (int i = 3 * PLen / 4 + 1; i < 3 * PLen / 2; i++)
            {
                if (i <= PLen)
                {
                    equ = new int[1];
                    equ[0] = i - 1;
                }
                else
                {
                    equ = new int[3];
                    equ[0] = i - 1;
                    equ[1] = i - PLen;
                    equ[2] = i - PLen - 1;
                }

                flag = false;
                for (int k = 0; k < equ.Length; k++)
                {
                    count1 = 0;
                    for (int j = 0; j < paramN; j++)
                    {
                        if (popNew[i].GetPheno()[j] == popNew[equ[k]].GetPheno()[j])
                            count1 += 1;
                    }
                    if (count1 == paramN)
                    {
                        flag = true;
                        //Debug.WriteLine("flag on! i: " + Convert.ToString(i) + "\t k: " + Convert.ToString(k));
                        //count_flags += 1;
                        npop = equ[k];
                        break;
                    }
                }
                if (flag) chi2[i] = chi2[npop];
                else chi2[i] = func(popNew[i].GetPheno());
                //Debug.WriteLine("2 thread " + Convert.ToString(i));
                //chi2[i] = func(popNew[i].GetPheno());
            }
        }
        private void Thr3_chi2()
        {
            bool flag;
            int count1;
            int[] equ;
            int npop = 0;
            chi2[3 * PLen / 2] = func(popNew[3 * PLen / 2].GetPheno());
            for (int i = 3 * PLen / 2 + 1; i < 3 * 3 * PLen / 4; i++)
            {
                if (i <= 2 * PLen)
                {
                    equ = new int[3];
                    equ[0] = i - 1;
                    equ[1] = i - PLen;
                    equ[2] = i - PLen - 1;
                }
                else
                {
                    equ = new int[5];
                    equ[0] = i - 1;
                    equ[1] = i - PLen;
                    equ[2] = i - PLen - 1;
                    equ[3] = i - 2 * PLen;
                    equ[4] = i - 2 * PLen - 1;
                }
                flag = false;
                for (int k = 0; k < equ.Length; k++)
                {
                    count1 = 0;
                    for (int j = 0; j < paramN; j++)
                    {
                        if (popNew[i].GetPheno()[j] == popNew[equ[k]].GetPheno()[j])
                            count1 += 1;
                    }
                    if (count1 == paramN)
                    {
                        flag = true;
                        //Debug.WriteLine("flag on! i: " + Convert.ToString(i) + "\t k: " + Convert.ToString(k));
                        //count_flags += 1;
                        npop = equ[k];
                        break;
                    }
                }
                if (flag) chi2[i] = chi2[npop];
                else chi2[i] = func(popNew[i].GetPheno());
                //Debug.WriteLine("3 thread " + Convert.ToString(i));
                //chi2[i] = func(popNew[i].GetPheno());
            }
        }
        private void Thr4_chi2()
        {
            bool flag;
            int count1;
            int[] equ = new int[5];
            int npop = 0;
            chi2[3 * 3 * PLen / 4] = func(popNew[3 * 3 * PLen / 4].GetPheno());
            for (int i = 3 * 3 * PLen / 4 + 1; i < 3 * PLen; i++)
            {
                equ[0] = i - 1;
                equ[1] = i - PLen;
                equ[2] = i - PLen - 1;
                equ[3] = i - 2 * PLen;
                equ[4] = i - 2 * PLen - 1;

                flag = false;
                for (int k = 0; k < equ.Length; k++)
                {
                    count1 = 0;
                    for (int j = 0; j < paramN; j++)
                    {
                        if (popNew[i].GetPheno()[j] == popNew[equ[k]].GetPheno()[j])
                            count1 += 1;
                    }
                    if (count1 == paramN)
                    {
                        flag = true;
                        //Debug.WriteLine("flag on! i: " + Convert.ToString(i) + "\t k: " + Convert.ToString(k));
                        //count_flags += 1;
                        npop = equ[k];
                        break;
                    }
                }
                if (flag) chi2[i] = chi2[npop];
                else chi2[i] = func(popNew[i].GetPheno());


                //Debug.WriteLine("4 thread " + Convert.ToString(i));
                //chi2[i] = func(popNew[i].GetPheno());
            }
        }

        private BetterPerson[] Cross(BetterPerson[] popNotRef)
        {
            Random rand4 = new Random(Guid.NewGuid().GetHashCode());
            BetterPerson[] popNew = new BetterPerson[2*popNotRef.Length];
            int[][] genes1, genes2, genesB1, genesB2;
            BetterPerson par1, par2;
            int chosen, sep;

            for (int i = 0; i < popNotRef.Length; i++)
            {
                if (rand4.NextDouble() < CrossProb)
                {
                    par1 = popNotRef[i];
                    chosen = rand4.Next(0, popNotRef.Length);
                    while (chosen == i)
                        chosen = rand4.Next(0, popNotRef.Length);
                    par2 = popNotRef[chosen];

                    genes1 = par1.GetGene();
                    genes2 = par2.GetGene();

                    genesB1 = new int[genes1.Length][];
                    genesB2 = new int[genes2.Length][];

                    for (int j = 0; j < genes1.Length; j++)
                    {
                        genesB1[j] = new int[genes1[j].Length];
                        genesB2[j] = new int[genes2[j].Length];

                        sep = rand4.Next(0, genes1[j].Length);

                        try
                        {
                            for (int k = 0; k < genes1[j].Length; k++)
                            {
                                if (k < sep)
                                {
                                    genesB1[j][k] = genes1[j][k];
                                    genesB2[j][k] = genes2[j][k];
                                }
                                else
                                {
                                    genesB1[j][k] = genes2[j][k];
                                    genesB2[j][k] = genes1[j][k];
                                }
                            }
                        }
                        catch
                        {
                            Debug.WriteLine("excep");
                            break;
                        }
                    }
                    popNew[i] = new BetterPerson(genesB1, lims, precs, paramN);
                    popNew[i + popNotRef.Length] = new BetterPerson(genesB1, lims, precs, paramN);
                }

                else
                {
                    popNew[i] = popNotRef[i];
                    popNew[i + popNotRef.Length] = popNotRef[i];
                }
            }

            return popNew;
        }

        private BetterPerson[] Mutate(ref BetterPerson[] population)
        {
            Random rand3 = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < population.Length; i++)
            {
                if (rand3.NextDouble() < MutationProb)
                {
                    population[i].Mutate();
                    //Debug.WriteLine("!!!Mutation!!!");
                }
            }

            return population;
        }

        private BetterPerson[] Con(BetterPerson[] pop1, BetterPerson[] pop2)
        {
            BetterPerson[] pop = new BetterPerson[pop1.Length + pop2.Length];
            for (int i = 0; i < pop1.Length; i++)
                pop[i] = pop1[i];
            for (int i = pop1.Length; i < pop.Length; i++)
                pop[i] = pop2[i - pop1.Length];

            return pop;
        }

        private BetterPerson[] Sorted2(ref BetterPerson[] pop)
        {
            double temp;
            BetterPerson temp1;
            
            for (int i = 9; i < pop.Length; i++)
            {
                chi2[i] = func(pop[i].GetPheno());
            }

            //Debug.WriteLine(count_flags);

            if (max)
            {
                for (int i = 0; i < chi2.Length; i++)
                {
                    for (int j = i + 1; j < chi2.Length; j++)
                    {
                        if (chi2[i] < chi2[j])
                        {
                            temp = chi2[i];
                            chi2[i] = chi2[j];
                            chi2[j] = temp;

                            temp1 = pop[i];
                            pop[i] = pop[j];
                            pop[j] = temp1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < chi2.Length; i++)
                {
                    for (int j = i + 1; j < chi2.Length; j++)
                    {
                        if (chi2[i] > chi2[j])
                        {
                            temp = chi2[i];
                            chi2[i] = chi2[j];
                            chi2[j] = temp;

                            temp1 = pop[i];
                            pop[i] = pop[j];
                            pop[j] = temp1;
                        }
                    }
                }
            }

            return pop;
        }


        private BetterPerson[] Sorted(ref BetterPerson[] pop)
        {
            //chi2 = new double[pop.Length];
            double temp;
            BetterPerson temp1;
            /*bool flag;
            int count1;
            int[] equ;
            int npop = 0;
            chi2[0] = func(pop[0].GetPheno());

            //int count_flags = 0;
            for (int i = 1; i < pop.Length; i++)
            {
                if (i <= PLen)
                {
                    equ = new int[1];
                    equ[0] = i - 1;
                }
                else if (i <= 2 * PLen)
                {
                    equ = new int[3];
                    equ[0] = i - 1;
                    equ[1] = i - PLen;
                    equ[2] = i - PLen - 1;
                }
                else
                {
                    equ = new int[5];
                    equ[0] = i - 1;
                    equ[1] = i - PLen;
                    equ[2] = i - PLen - 1;
                    equ[3] = i - 2 * PLen;
                    equ[4] = i - 2 * PLen - 1;
                }
                flag = false;
                for (int k = 0; k < equ.Length; k++)
                {
                    count1 = 0;
                    for (int j = 0; j < paramN; j++)
                    {
                        if (pop[i].GetPheno()[j] == pop[equ[k]].GetPheno()[j])
                            count1 += 1;
                    }
                    if (count1 == paramN)
                    {
                        flag = true;
                        //Debug.WriteLine("flag on! i: " + Convert.ToString(i) + "\t k: " + Convert.ToString(k));
                        //count_flags += 1;
                        npop = equ[k];
                        break;
                    }
                }
                if (flag) chi2[i] = chi2[npop];
                else chi2[i] = func(pop[i].GetPheno());
                //vals[i] = func(pop[i].GetPheno());
            }
            //for (int i = 0; i < pop.Length; i++)
                //chi2[i] = func(pop[i].GetPheno());*/

            //Debug.WriteLine(count_flags);

            if (max)
            {
                for (int i = 0; i < chi2.Length; i++)
                {
                    for (int j = i + 1; j < chi2.Length; j++)
                    {
                        if (chi2[i] < chi2[j])
                        {
                            temp = chi2[i];
                            chi2[i] = chi2[j];
                            chi2[j] = temp;

                            temp1 = pop[i];
                            pop[i] = pop[j];
                            pop[j] = temp1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < chi2.Length; i++)
                {
                    for (int j = i + 1; j < chi2.Length; j++)
                    {
                        if (chi2[i] > chi2[j])
                        {
                            temp = chi2[i];
                            chi2[i] = chi2[j];
                            chi2[j] = temp;

                            temp1 = pop[i];
                            pop[i] = pop[j];
                            pop[j] = temp1;
                        }
                    }
                }
            }

            return pop;
        }


        private BetterPerson[] ThisIsSparta(BetterPerson[] pop)
        {
            BetterPerson[] chosen = new BetterPerson[PLen];
            for (int i = 0; i < PLen; i++)
                chosen[i] = pop[i];
            return chosen;
        }
    }

    class BetterPerson
    {
        private int[][] gene;
        private double[] pheno;
        private int paramN;
        private int[] precs;
        private double[][] lims;

        public BetterPerson(int paramN, int[] precs, double[][] lims)
        {
            this.paramN = paramN;
            this.precs = precs;
            this.lims = lims;
            Random rand1 = new Random(Guid.NewGuid().GetHashCode());
            gene = new int[paramN][];
            pheno = new double[paramN];
            for (int i = 0; i < paramN; i++)
            {
                gene[i] = new int[precs[i] + Convert.ToString(Math.Round(lims[i][1])).Length];

                for (int j = 0; j < gene[i].Length; j++)
                {
                    gene[i][j] = rand1.Next(0, 10);
                }
                string cash = String.Join("", gene[i].Select(p=>p.ToString()).ToArray()) ;
                pheno[i] = Convert.ToDouble(cash) / Math.Pow(10, precs[i]+1);

                while ( pheno[i] < lims[i][0] || pheno[i] > lims[i][1])
                {
                    for (int j = 0; j < gene[i].Length; j++)
                    {
                        gene[i][j] = rand1.Next(0, 10);
                    }
                    cash = String.Join("", gene[i].Select(p => p.ToString()).ToArray());
                    pheno[i] = Convert.ToDouble(cash) / Math.Pow(10, precs[i] + 1);
                }
            }

            //Debug.WriteLine(pheno[0]);
        }

        public BetterPerson(int[][] gene, double[][] lims, int[] precs, int paramN)
        {
            this.gene = gene;

            this.lims = lims;
            this.precs = precs;
            this.paramN = paramN;

            string cash;
            double[] pheno = new double[paramN];
            for (int i = 0; i < paramN; i++)
            {
                cash = String.Join("", gene[i].Select(p => p.ToString()).ToArray());
                pheno[i] = Convert.ToDouble(cash) / Math.Pow(10, precs[i] + 1);
            }
        }

        public int[][] GetGene() { return gene; }

        public double[] GetPheno() 
        {
            string cash;
            double[] pheno = new double[paramN];
            for (int i = 0; i < paramN; i++)
            {
                cash = String.Join("", gene[i].Select(p => p.ToString()).ToArray());
                pheno[i] = Convert.ToDouble(cash) / Math.Pow(10, precs[i] + 1);
            }
            return pheno; 
        }

        public void Mutate()
        {
            Random rand2 = new Random(Guid.NewGuid().GetHashCode());

            int mut = rand2.Next(0, gene.Length);
            int mut2 = rand2.Next(0, gene[mut].Length);

            int mgCash = gene[mut][mut2]; 

            int newGene = rand2.Next(0, 10);
            while (newGene == mgCash)
                newGene = rand2.Next(0, 10);

            gene[mut][mut2] = newGene;
        }
    }
}
