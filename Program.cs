using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace osztalystatisztika
{
    struct REK
    {
        //tanulók nevének beolvasásához
        public string vnev;
        public string unev;
    }
    class Program
    {
        static void Main(string[] args)
        {
            string egysor;
            REK[] tomb = new REK[50];
            string[] darab;
            int n = 0;
            int M = 0;
            int i = 0;
            int j = 0;
            int[,] tanuloiEredmenyek = new int[50, 50];
            bool tovabb = true;

            Console.WriteLine("Főglein Simon");
            Console.WriteLine();
            //beolvasás
            StreamReader beolv = File.OpenText("osztalyadat.txt");
            M = Convert.ToInt32(beolv.ReadLine()); //első sorban található szám tárolása
            string[] ttargy = new string[M];

            while (tovabb == true)
	        {
                //tantárgyak beolvasása soronként 
                for (i = 0; i < M; i++)
                {
                    egysor = beolv.ReadLine();
                    ttargy[i] = egysor;
                    tovabb = false;
                }
                //ok ^ 

                n = Convert.ToInt32(beolv.ReadLine());

                for (i = 0; i < n; i++)
                {
                    egysor = beolv.ReadLine();
                    darab = egysor.Split(' ');
                    for (j = 0; j < M; j++)
                    {
                        tanuloiEredmenyek[i, j] = int.Parse(darab[j]);
                    }
                    tomb[i].vnev = darab[14];
                    tomb[i].unev = darab[15];
                }
            }
            //-----átlagszámítás-----\\
            StreamWriter kiir = File.CreateText("FogleinSimon_statisztika.txt");
            double[] tanuloAtlag = new double[M];   //tanló átlaga
            double[] ttargyAtlag = new double[M];   //tantárgy átlaga

            kiir.WriteLine("{0} {1}", M, n);
            Console.WriteLine("{0} {1}", M, n);
            kiir.WriteLine();
            Console.WriteLine();

            //tanulók átlaga
            //egy sorban lévő osztályzatok összeadása + osztás 14-gyel
            for (i = 0; i < n; i++)
            {
                tanuloAtlag[i] = 0;
                for (j = 0; j < M; j++)
                {
                    tanuloAtlag[i] += tanuloiEredmenyek[i, j];

                    if (j == M - 1)
                    {
                        tanuloAtlag[i] /= M;
                        kiir.WriteLine(tomb[i].vnev + " " + tomb[i].unev + " " + Math.Round(tanuloAtlag[i], 2));
                        Console.WriteLine(tomb[i].vnev + " " + tomb[i].unev + " " + Math.Round(tanuloAtlag[i], 2));
                    }
                }
            }
            kiir.WriteLine();
            Console.WriteLine();

            //tantárgyi átlagok
            //egy oszlopban lévő osztályzatok összeadása + osztás 13-mal
            for (j = 0; j < M; j++)
            {
                ttargyAtlag[j] = 0;
                for (i = 0; i < M; i++)
                {
                    ttargyAtlag[j] += tanuloiEredmenyek[i, j];

                    if (i == n)
                    {
                        ttargyAtlag[j] /= n;
                        kiir.WriteLine(ttargy[j] + " " + Math.Round(ttargyAtlag[j], 2));
                        Console.WriteLine(ttargy[j] + " " + Math.Round(ttargyAtlag[j], 2));
                    }
                }
            }
            kiir.Close();
            Console.ReadLine();

            //2. feladat
            Console.WriteLine("2. feladat: ");
            bool bukott = false;
            Console.Write("Adja meg egy tanuló sorszámát: ");
            int valasz = Convert.ToInt32(Console.ReadLine());

            for (j = 0; j < n; j++)
            {
                if (Convert.ToString(tanuloiEredmenyek[valasz, j]).Contains('1'))
                {
                    bukott = true;
                }
                if (j == n - 1)
                {
                    if (bukott == true)
                    {
                        Console.WriteLine(tomb[valasz].vnev + " " + tomb[valasz].unev + " megbukott.");
                    }
                    else
                    {
                        Console.WriteLine(tomb[valasz].vnev + " " + tomb[valasz].unev + " nem bukott meg.");
                    }
                }
            }
            Console.ReadLine();

            //3. feladat
            Console.WriteLine("3. feladat: ");
            bool kozepesnelRosszabb = false;
            Console.Write("Adja meg egy tantárgy sorszámát: ");
            int ttargyValasz = Convert.ToInt32(Console.ReadLine());

            for (i = 0; i < M; i++)
            {
                //if (Convert.ToString(tanuloiEredmenyek[i, ttargyValasz]).Contains('1') || Convert.ToString(tanuloiEredmenyek[i, ttargyValasz]).Contains('2'))
                if (tanuloiEredmenyek[i, ttargyValasz] < 3 && tanuloiEredmenyek[i, ttargyValasz] != 0)
                {
                    kozepesnelRosszabb = true;
                }
                if (i == M - 1)
                {
                    if (kozepesnelRosszabb == true)
                    {
                        Console.WriteLine(ttargy[ttargyValasz] + " tantárgyból volt közepesnél rosszabb osztályzat.");
                    }
                    else
                    {
                        Console.WriteLine(ttargy[ttargyValasz] + " tantárgyból nem volt közepesnél rosszabb osztályzat.");
                    }
                }
            }
            Console.ReadLine();

            //4. feladat
            double max = tanuloAtlag[0];
            for (i = 0; i < n; i++)
            {
                if (tanuloAtlag[i] > max)
                {
                    max = tanuloAtlag[i];
                    //maxhol = i;
                }
            }
            for (i = 0; i < n; i++)
            {
                if (tanuloAtlag[i] == max)
                {
                    Console.WriteLine("A lemagasabb átlag: {0}, ezt {1} {2} érte el.", max, tomb[i].vnev, tomb[i].unev);
                }
            }
        }
    }
}


//kiíratásnál utolsó tantárgyat nem írja ki [kész]
//maxhol2 => ne csak az első maxátlagot elért nevét írja ki [kész]
