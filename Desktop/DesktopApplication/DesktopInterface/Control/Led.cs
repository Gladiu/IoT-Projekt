using DesktopInterface.Dtos;
using System.Windows.Media;

namespace DesktopInterface.Control
{
    public class Led
    {
        public int? R { get; set; }
        public int? G { get; set; }
        public int? B { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        private SolidColorBrush _nullColor = new SolidColorBrush(Color.FromRgb(0, 0, 0)); // Not light up LED

        public Led()
        {
            R = 0;
            G = 0;
            B = 0;
            x = 0;
            y = 0;
        }
        public Led(int x, int y)
        {
            R = 0;
            G = 0;
            B = 0;
            this.x = x;
            this.y = y;
        }

        public Led(LedDto dto)
        {
            R = dto.R;
            G = dto.G;
            B = dto.B;
            this.x = dto.x;
            this.y = dto.y;
        }


        public SolidColorBrush ColorToBrush()
        {
            if (ColorNotNull())
            {
                return new SolidColorBrush(Color.FromRgb((byte)R!, (byte)G!, (byte)B!));
            }
            else
            {
                return _nullColor;
            }
        }
        public void SetViewColor(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public bool ColorNotNull()
        {
            return R != null & G != null & B != null;
        }

        public void Clear()
        {
            R = null;
            G = null;
            B = null;
        }
    }
}
