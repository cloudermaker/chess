using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Position
    {
        private int _x { get; set; }
        private int _y { get; set; }

        public int x { get {return this._x;} }
        public int y { get { return this._y; } }

        public Position(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public void Print()
        {
            Console.Out.Write(string.Concat(" (", this._x + 1, ',', this._y + 1, ") "));
        }

        public static bool operator ==(Position p1, Position p2)
        {
            return p1._x == p2._x && p1._y == p2._y;
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return p1._x != p2._x || p1._y != p2._y;
        }
    }
}
