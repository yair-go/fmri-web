using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CliqueAlgo
{
    class CL_Graph
    {
        private double m_threshold;
        private StreamReader m_reader;
        private List<Set<int>> v;

        public CL_Graph(double threshold, string filename)
        {
            m_threshold = threshold;
            m_reader = new StreamReader(filename);

            // initial size optimization, based on file size
            // divide by 6, as a heuristic average of cell length 
            int numLinesGuess = (int)Math.Sqrt(m_reader.BaseStream.Length / 6);
            v = new List<Set<int>>(numLinesGuess);

            init();
        }

        private void init()
        {
            int lineCounter = 0;
            while (m_reader.Peek() > 0)
            {
                string line = m_reader.ReadLine();
                Set<int> s = new Set<int>();
                int colCounter = 0;

                foreach (string t in line.Split(",".ToCharArray()))
                {
                    double val = Convert.ToDouble(t);
                    if (Math.Abs(val) > m_threshold)
                    {
                        s.Add(colCounter);
                    }
                    colCounter++;
                }
                v.Add(s);

                lineCounter++;
            }
        }

        public List<Set<int>> All_Cliques(int min_Q_size, int max_Q_size)
        {
            List<Set<int>> ans = new List<Set<int>>();

            List<Set<int>> C0 = allEdges();
            
            foreach (Set<int> i in C0)
            {
                List<Set<int>> C1 = All_Cliques_of_edge(i, min_Q_size, max_Q_size);
                ans.AddRange(C1);
            }

            return ans;
        }

        private List<Set<int>> All_Cliques_of_edge(Set<int> e, int min_Q_size, int max_Q_size)
        {
            List<Set<int>> ans = new List<Set<int>>();
            ans.Add(e);
            int last_size = e.Count;

            for (int i = 0; i < ans.Count && last_size <= max_Q_size; i++)
            {
                Set<int> curr = ans[i];
                Set<int> inter = intersection(curr);
                addbiggerCliQ(ans, curr, inter);
                last_size = ans[ans.Count - 1].Count;
            }

            
            for (int i = 0; i < ans.Count; i++)
            {
                if (ans[i].Count < min_Q_size)
                {
                    ans.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
            
            return ans;
        }

        private Set<int> intersection(Set<int> C)
        {
            Set<int> ans = v[C[0]];
            for (int i = 0; ans.Count > 0 && i < C.Count; i++)
            {
                ans = ans.Intersect(v[C[i]]);
            }
            return ans;
        }

        private void addbiggerCliQ(List<Set<int>> ans, Set<int> curr, Set<int> inter)
        {
            int last = curr[curr.Count - 1];
            for (int i = 0; i < inter.Count; i++)
            {
                int ind_inter = inter[i];
                if (last < ind_inter)
                {
                    Set<int> c = new Set<int>(curr.ToArray());
                    c.Add(ind_inter);
                    ans.Add(c);
                }
            }
        }

        private List<Set<int>> allEdges()
        {
            List<Set<int>> ans = new List<Set<int>>();
            for (int i = 0; i < v.Count; i++)
            {
                Set<int> curr = v[i];
                for (int a = 0; a < curr.Count; a++)
                {
                    if (i < curr[a])
                    {
                        Set<int> tmp = new Set<int>();
                        tmp.Add(i);
                        tmp.Add(curr[a]);
                        ans.Add(tmp);
                    }
                }

            }
            return ans;
        }
	
	
    }
}
