using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlideShowProblem_HillClimbing
{
    class Program
    {
        public static string filePath = @"C:\f\inputfile.txt";
        public static string filePaths = @"C:\f\writefile.txt";
        public static string[] lines = File.ReadAllLines(filePath);
        public static string[] liness = File.ReadAllLines(filePaths);
        public static int mm = 0;
        public static string[] str = new string[lines.Count()];

    static void Main(string[] args)
    {
        int w = 0;
        foreach (string line in lines)
        {
            mm = 0;
            for (int i = w; i < line.Count(); i++)
            {
                if (mm == 0)
                {
                    str[i] = line + " " + i.ToString();
                }
                mm++;
            }
            w++;
        }
        int[] Fitness = new int[500];

        //Fitnness of intial solution
        Fitness[0] = Convert.ToInt32((GenerateSolution.func(str)));


        for (int j = 0; j < 500; j++)
        {
            for (int i = 1; i < str.Count() - 1; i++)
            {
                var first = str[i + 1];
                str[i + 1] = str[i];
                str[i] = first;

            }
                //Fitnness of solution after mutation
                Fitness[1] = Convert.ToInt32((GenerateSolution.func(str)));
            //Console.WriteLine(Fitness[0]);

           //Compare all fitnesses 
            if (Fitness[j] < Fitness[0])
            {
                Fitness[0] = Fitness[j];
                for (int i = 1; i < GenerateSolution.photoToAdd.Count() - 1; i++)
                {
                    string first = GenerateSolution.photoToAdd[i + 1];
                    GenerateSolution.photoToAdd[i + 1] = GenerateSolution.photoToAdd[i];
                    GenerateSolution.photoToAdd[i] = first;
                }
            }

            for (int i = 1; i < GenerateSolution.photoToAdd.Count(); i++)
            {
                GenerateSolution.photoToAdd[1] = (GenerateSolution.photoToAdd.Count() - 2).ToString();
                Console.WriteLine(GenerateSolution.photoToAdd[i]);
            }
            int h = 1;
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\f\writefile.txt", true))
            {
                do
                {
                    file.WriteLine(GenerateSolution.photoToAdd[h]);
                    h++;
                }
                while (h < GenerateSolution.photoToAdd.Count());
            }
            Console.ReadKey();
        }
    }
}
  }
