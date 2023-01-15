using DesktopInterface.Control;
using DesktopInterface.Dtos;

namespace Test.Conversions
{
    public class TestLedDtoConversion_ShouldBeEqual
    {
        [Fact]
        public void AssertLedDtoShouldBeEqualToLed()
        {
            LedDto ledDto = new(1, 1, 1, 1, 1);
            Led led = new(ledDto);

            Assert.Equal(ledDto.x, led.x);
            Assert.Equal(ledDto.y, led.y);
            Assert.Equal(ledDto.R, led.R);
            Assert.Equal(ledDto.G, led.G);
            Assert.Equal(ledDto.B, led.B);
        }

        [Fact]
        public void AssertLedShouldBeEqualToLedDto()
        {
            Led led = new(1, 1, 1, 1, 1);
            LedDto ledDto = new(led);

            Assert.Equal(led.x, ledDto.x);
            Assert.Equal(led.y, ledDto.y);
            Assert.Equal(led.R, ledDto.R);
            Assert.Equal(led.G, ledDto.G);
            Assert.Equal(led.B, ledDto.B);
        }
    }
}