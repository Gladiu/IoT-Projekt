using DesktopInterface.Control;

namespace DesktopInterface.Dtos
{
    public class LedDto
    {
        public int x { get; set; }

        public int y { get; set; }

        public int R { get; set; }

        public int G { get; set; }

        public int B { get; set; }

        public LedDto() { }

        public LedDto(int x, int y) 
        {
            this.x = x; this.y = y;
            R = 0;
            G = 0;
            B = 0;
        }

        public LedDto(int x, int y, int r, int g, int b)
        {
            this.x = x; this.y = y;
            R = r;
            G = g;
            B = b;
        }

        public LedDto(Led led)
        {
            x = led.x;
            y = led.y;
            R = led.R!.Value;
            G = led.G!.Value;
            B = led.B!.Value;
        }
    }
}
