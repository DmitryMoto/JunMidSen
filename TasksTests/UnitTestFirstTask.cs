using FluentAssertions;
using TestJunMidSen.FirstTask;

namespace TasksTests
{
    public class UnitTestFirstTask
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("a", "a")]
        [InlineData("ab", "ab")]
        [InlineData("aaabbcccdde", "a3b2c3d2e")]
        [InlineData("aaaaaaaaaaaaaaabbcccddeee", "a15b2c3d2e3")]
        [InlineData("aaadddwwwwvrghjnnnnb","a3d3w4vrghjn4b")]
        [InlineData("reeasdzxcgtgregeergadad","re2asdzxcgtgrege2rgadad")]
        public void Compression_Proccess_ReturnCompressionString(string inputStr, string expected)
        {
            //Arrange
            IStringProcessing compression = new Compression();

            //Act
            string result = compression.Process(inputStr);

            //Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("a", "a")]
        [InlineData("ab", "ab")]
        [InlineData("a3b2c3d2e", "aaabbcccdde")]
        [InlineData("a15b2c3d2e3", "aaaaaaaaaaaaaaabbcccddeee")]
        [InlineData("a3d3w4vrghjn4b", "aaadddwwwwvrghjnnnnb")]
        [InlineData("re2asdzxcgtgrege2rgadad", "reeasdzxcgtgregeergadad")]
        public void Decompression_Proccess_ReturnDecompressionString(string inputStr, string expected)
        {
            //Arrange
            IStringProcessing decompression = new Decompression();

            //Act
            string result = decompression.Process(inputStr);

            //Assert
            result.Should().Be(expected);
        }
    }
}