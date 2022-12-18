using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DesktopInterface.Control
{
    public class Led
    {
        public int Id { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public int x { get; set; }
        
        public int y { get; set; }

        public Led()
        {
            R = 255;
            G = 255;
            B = 255;
            x = 0; y = 0;
        }
        public Led(int x, int y)
        {
            R = 255;
            G = 255;
            B = 255;
            this.x = x;
            this.y = y;
        }

        public SolidColorBrush ColorToBrush()
        {
            byte A = 255;
            return new SolidColorBrush(Color.FromArgb(A, (byte)R, (byte)G, (byte)B));
        }

        public void SetViewColor(int r, int g, int b) 
        {
            R = r;
            G = g;
            B = b;
        }

        public bool ColorNotNull()
        {
            return (R >= 0) & (G >= 0) & (B >= 0);
        }

        public void Clear()
        {
            R = 0;
            G = 0;
            B = 0;
        }
    }
}
