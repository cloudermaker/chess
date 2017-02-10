using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public static class Tools
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static SolutionTree BuildSolutionTree(SolutionTree tree, Map map, char col, int lvl)
        {
            List<Piece> allColPiece = map.GetAllPiece(col);

            foreach (Piece p in allColPiece)
            {
                foreach (Position pos in p.GetAllPos(map))
                {
                    Move m = new Move(p.GetPos(), pos);
                    if (pos.x > 7 || pos.y > 7)
                        p.GetAllPos(map);
                    int moveNote = map.GetNote(pos.x, pos.y);

                    // on inverse les notes un tour sur deux
                    moveNote = tree.note >= 0 ? moveNote : -moveNote;
                    
                    tree.AddSolution(m, map, moveNote);
                }
            }

            if (lvl > 0)
            {
                foreach (SolutionTree t in tree.allSolution)
                {
                    Map nextMap = new Map(map);
                    nextMap.Move(t.mo);

                    BuildSolutionTree(t, nextMap, col == 'B' ? 'N' : 'B', lvl - 1);
                }
            }

            return tree;
        }
    }
}
