using QuickTest.Classes;
using Xunit;

namespace QuickTest.Tests
{
    public class ColorTests
    {
        [Theory]
        [InlineData(0, 255, 0, 0)]
        [InlineData(60, 255, 255, 0)]
        [InlineData(120, 0, 255, 0)]
        [InlineData(180, 0, 255, 255)]
        [InlineData(240, 0, 0, 255)]
        [InlineData(300, 255, 0, 255)]
        [InlineData(360, 255, 0, 0)]
        [InlineData(-120, 0, 0, 255)]
        public void ConvertsScoreHuesToExpectedColors(float hue, int red, int green, int blue)
        {
            var color = new HSV(hue, 1, 0.5f).ColorFromHSL();

            Assert.InRange((int)color.R, System.Math.Max(0, red - 1), System.Math.Min(255, red + 1));
            Assert.InRange((int)color.G, System.Math.Max(0, green - 1), System.Math.Min(255, green + 1));
            Assert.InRange((int)color.B, System.Math.Max(0, blue - 1), System.Math.Min(255, blue + 1));
            Assert.Equal(255, color.A);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0.5f, 127)]
        [InlineData(1, 255)]
        public void ZeroSaturationProducesTheRequestedGray(float lightness, int expected)
        {
            var color = new HSV(45, 0, lightness).ColorFromHSL();

            Assert.Equal(expected, color.R);
            Assert.Equal(expected, color.G);
            Assert.Equal(expected, color.B);
        }

        [Theory]
        [InlineData(0.25f, 127)]
        [InlineData(0.75f, 255)]
        public void HandlesLightnessBelowAndAboveTheMidpoint(float lightness, int red)
        {
            var color = new HSV(0, 1, lightness).ColorFromHSL();

            Assert.Equal(red, color.R);
            Assert.Equal(color.G, color.B);
        }
    }
}
