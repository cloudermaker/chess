using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Game
    {
        private Map _m { get; set; }
        private static Game _g = null;
        public static Game getInstance
        {
            get
            {
                if (_g == null)
                    _g = new Game();
                return _g;
            }
        }
        private SolutionTree _tree { get; set; }

        private Game()
        {
            this.InitGame();
        }

        private void InitGame()
        {
            this._m = new Map(Program.size);
            this._m.InitMap();
            this._m.InitGame();
            this._tree = new SolutionTree(null, this._m);
        }

        public void PrintMapGame()
        {
            this._m.PrintMap();
        }

        public Move GetRandomMove(char color)
        {
            return this._m.GetRandomMove(color);
        }

        public char IsGameOver()
        {
            return this._m.IsGameOver();
        }

        public bool Move(Move mo)
        {
            return this._m.Move(mo);
        }

        public Move GetNextMove(char col, int lvl)
        {
            this._tree = new SolutionTree(null, this._m);
            this._tree = Tools.BuildSolutionTree(this._tree, this._m, col, lvl - 1);

            return this._tree.GetBestMove();
        }
    }
}
