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

        // public ColorARGB(byte a, byte r, byte g, byte b)
        // {
        //     A = a;
        //     R = r;
        //     G = g;
        //     B = b;
        //     
        // }
    }
}