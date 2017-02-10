using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Move
    {
        public int _note = 0;
        public Position _pos1 { get; set; }
        public Position _pos2 { get; set; }

        public Move(Position pos1, Position pos2)
        {
            this._pos1 = pos1;
            this._pos2 = pos2;
        }

        public void Print()
        {
            Console.Out.WriteLine(string.Format("Move from {0};{1} to {2};{3}", this._pos1.x + 1, this._pos1.y + 1, this._pos2.x + 1, this._pos2.y + 1));
         }
    }
}
