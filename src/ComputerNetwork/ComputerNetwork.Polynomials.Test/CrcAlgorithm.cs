using FluentAssertions;
using NUnit.Framework;

namespace ComputerNetwork.Polynomials.Test
{
    public abstract class CrcAlgorithm
    {
        [TestFixture]
        public class GenerateCrc
        {
            [Test]
            public void Generate()
            {
                var a = new Polynomials.BinaryPolynomial("10110011");
                var crc = Polynomials.CrcAlgorithm.GenerateCrc(a);

                crc.Should().Be(new Polynomials.BinaryPolynomial("101100111000"));
            }
        }

        [TestFixture]
        public class IsValid
        {
            [Test]
            public void ShouldBeValid()
            {
                var crcPolynomial = new Polynomials.BinaryPolynomial("101100111000");
                Polynomials.CrcAlgorithm.IsCrcValid(crcPolynomial).Should().BeTrue();
            }

            [Test]
            public void ShouldBeInvalid()
            {
                var crcPolynomial = new Polynomials.BinaryPolynomial("101100111001");
                Polynomials.CrcAlgorithm.IsCrcValid(crcPolynomial).Should().BeFalse();
            }
        }
    }
}