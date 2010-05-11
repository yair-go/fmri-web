using System;

namespace CliqueAlgo
{
    class TesterApp
    {
        static string SetToString(Set<int> s)
        {
            string ret = "Set: |" + s.Count + "| ";
            foreach (int i in s)
            {
                ret += "" + i + ", ";
            }
            return ret;
        }

        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: <input csv> <threshold> <min clique size> <max clique size>");
                return;
            }

            string csv_filename = args[0];
            double threshold = Convert.ToDouble(args[1]);
            int min = Convert.ToInt32(args[2]);
            int max = Convert.ToInt32(args[3]);

            DateTime before = DateTime.Now;

            CL_Graph g = new CL_Graph(threshold, csv_filename);
            foreach (Set<int> i in g.All_Cliques(min, max))
            {
                if (i.Count >= min)
                {
                    Console.WriteLine(SetToString(i));
                }
            }

            Console.WriteLine();
            Console.WriteLine("Time consumed: " + (DateTime.Now - before));
        }
    }
}
