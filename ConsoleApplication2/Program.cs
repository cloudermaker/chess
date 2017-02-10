using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        public static int size = 8;

        static void Main(string[] args)
        {
            Game g = Game.getInstance;

            int i = 0;
            char col = 'B';
            char res = ' ';
            while (res == ' ')
            {
                // Print MAP
                Console.Clear();
                g.PrintMapGame();
                Console.Out.WriteLine();

                // Quelle couleur
                col = (i % 2 == 0) ? 'B' : 'N';

                // Calcul du mouvement
                Move mo = g.GetNextMove(col, 3);
                //Move mo = g.GetRandomMove(col);
                if (mo != null)
                    mo.Print();
                else
                {
                    Console.Out.WriteLine(string.Format("{0} give up!!", col));
                    res = col == 'B' ? 'N' : 'B';
                    break;
                }

                Console.Out.WriteLine();

                bool isCorrectMove = g.Move(mo);

                if (!isCorrectMove)
                {
                    g.Move(mo);
                    res = col;
                    Console.Out.WriteLine("You cannot do this move => Elimination.");
                    break;
                }

                res = g.IsGameOver();

                System.Threading.Thread.Sleep(200);
                i++;
            }
            Console.Out.WriteLine(string.Format("Winner is: {0}.", res));

            Console.Out.WriteLine();
            Console.In.ReadLine();
        }
    }
}
