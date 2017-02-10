using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class SolutionTree
    {
        public Move mo {get;set;}
        public List<SolutionTree> allSolution = new List<SolutionTree>();
        public int note = 0;
        public Map currentMap { get; set; }
        
        public SolutionTree(Move mo, Map m)
        {
            this.mo = mo;
            this.currentMap = new Map(m);
        }

        public SolutionTree(Move mo, Map m, int note)
        {
            this.mo = mo;
            this.currentMap = new Map(m);
            this.note += note;
        }

        public int CountSolution()
        {
            return this.allSolution.Count;
        }

        public Move GetBestMove()
        {
            int maxNote = 0;
            Move m = null;
            Random rnd = new Random();

            Tools.Shuffle<SolutionTree>(this.allSolution);
            foreach (SolutionTree st in this.allSolution)
            {
                if (maxNote < st.note)
                {
                    maxNote = st.note;
                    m = st.mo;
                }
            }

            return m;
        }

        public SolutionTree AddSolution(Move mo, Map m, int note)
        {
            SolutionTree st = new SolutionTree(mo, m, note);
            //this.note += note;

            this.allSolution.Add(st);

            return st;
        }
    }
}
