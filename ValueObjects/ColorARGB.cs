using Microsoft.EntityFrameworkCore;

namespace ValueObjects
{
    [Owned]
    public class ColorARGB
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public ColorARGB(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public ColorARGB()
        {
        }

        public override bool Equals(object? obj)
        {
            if (obj is ColorARGB c)
            {
                return c.A == A && c.B == B && c.G == G && c.R == R;
            }

            return false;
        }
        public static bool operator ==(ColorARGB r1, ColorARGB r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(ColorARGB r1, ColorARGB r2)
        {
            return !(r1 == r2);
        }
    }
}