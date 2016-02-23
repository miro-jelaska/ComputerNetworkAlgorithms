using System;
using FluentAssertions;
using NUnit.Framework;

namespace ComputerNetwork.Polynomials.Test
{
    public abstract class BinaryPolynomial
    {
        [TestFixture]
        public class Constructor
        {
            [Test]
            public void WorksWith_ExtraSpaceAtBeggining_InStringFormatedPolynomial()
            {
                Assert.DoesNotThrow(() => new Polynomials.BinaryPolynomial("  101011"));
            }

            [Test]
            public void WorksWith_ExtraSpaceAtEnd_InStringFormatedPolynomial()
            {
                Assert.DoesNotThrow(() => new Polynomials.BinaryPolynomial("101011  "));
            }

            [Test]
            public void StringContainingZeroesShouldResultInNulPolynomial()
            {
                new Polynomials.BinaryPolynomial("0").IsNullPolynomial.Should().BeTrue();
            }

            [Test]
            public void SuccessfullyConstructsFromString()
            {
                var a = new Polynomials.BinaryPolynomial("1001");
                a.Polynomial.Should().Be(9);
            }

            [Test]
            public void SuccessfullyConstructsFromStringWithExtra0AtBegining()
            {
                var a = new Polynomials.BinaryPolynomial("001001");
                a.Polynomial.Should().Be(9);
            }

            [Test]
            public void SuccessfullyConstructsFromStringThatEndsWithZero()
            {
                var a = new Polynomials.BinaryPolynomial("10");
                a.Polynomial.Should().Be(2);
            }

            [Test]
            public void WhenLargerThanMaximumDegreeThrowException()
            {
                Action act = () =>
                {
                    var newPolynomial = new Polynomials.BinaryPolynomial(
                        "1000000000000000000000000000000000000000000000");
                };
                act.ShouldThrow<Exception>();
            }
        }

        [TestFixture]
        public class Indexer
        {
            [Test]
            public void GetValue()
            {
                var a = new Polynomials.BinaryPolynomial("1011");
                var expecting = true;

                a[2].Should().Be(expecting);
            }

            [Test]
            public void GetValue_indexGreaterThanDegree()
            {
                var a = new Polynomials.BinaryPolynomial("10");
                var expecting = false;

                a[5].Should().Be(expecting);
            }

            [Test]
            public void SetValue()
            {
                var a = new Polynomials.BinaryPolynomial("1000");

                a[2] = true;
                a[2].Should().BeTrue();
            }
        }

        [TestFixture]
        public class Degree
        {
            [Test]
            public void ForNullPolynomialShouldReturn1()
            {
                var polynomial = new Polynomials.BinaryPolynomial();
                var expecting = 0;
                polynomial.Degree.Should().Be(expecting);
            }

            [Test]
            public void For1ShouldReturn1()
            {
                var polynomial = new Polynomials.BinaryPolynomial("1");
                var expecting = 1;
                polynomial.Degree.Should().Be(expecting);
            }

            [Test]
            public void For2ShouldReturn2()
            {
                var polynomial = new Polynomials.BinaryPolynomial("10");
                var expecting = 2;
                polynomial.Degree.Should().Be(expecting);
            }

            [Test]
            public void For3ShouldReturn2()
            {
                var polynomial = new Polynomials.BinaryPolynomial("11");
                var expecting = 2;
                polynomial.Degree.Should().Be(expecting);
            }

            [Test]
            public void For4ShouldReturn3()
            {
                var polynomial = new Polynomials.BinaryPolynomial("100");
                var expecting = 3;
                polynomial.Degree.Should().Be(expecting);
            }
        }

        [TestFixture]
        public class RiseDegree
        {
            [Test]
            public void ShouldThrowExceptionOnNegativeRiseDifference()
            {
                var polynomial= new Polynomials.BinaryPolynomial("10100");
                var riseDifference = -5;
                Action act = () =>
                {
                    var newPolynomial = polynomial.RiseDegree(riseDifference);
                };
                act.ShouldThrow<Exception>();
            }

            [Test]
            public void RiseBy0()
            {
                var polynomial = new Polynomials.BinaryPolynomial("1010");
                var riseDifference = 0;
                var expecting = 10;

                polynomial.RiseDegree(riseDifference).Polynomial.Should().Be((uint)expecting);
            }

            [Test]
            public void RiseBy1()
            {
                var polynomial = new Polynomials.BinaryPolynomial("1010");
                var riseDifference = 1;
                var expecting = 20;

                polynomial.RiseDegree(riseDifference).Polynomial.Should().Be((uint)expecting);
            }

            [Test]
            public void RiseBy2()
            {
                var polynomial = new Polynomials.BinaryPolynomial("11001");
                var riseDifference = 2;
                var expecting = 100;

                polynomial.RiseDegree(riseDifference).Polynomial.Should().Be((uint)expecting);
            }
        }

        [TestFixture]
        public class operator_XOR
        {
            [Test]
            public void SimpleXorTest()
            {
                var a               = new Polynomials.BinaryPolynomial("101011");
                var b               = new Polynomials.BinaryPolynomial("   110");
                var expectingResult = new Polynomials.BinaryPolynomial("101101");
                var xorResult = a ^ b;

                xorResult.Should().Be(expectingResult);
            }

            [Test]
            public void RightPolynomialHasGreaterDegree()
            {
                var a               = new Polynomials.BinaryPolynomial("   100");
                var b               = new Polynomials.BinaryPolynomial("110101");
                var expectingResult = new Polynomials.BinaryPolynomial("110001");
                var xorResult = a ^ b;

                xorResult.Should().Be(expectingResult);
            }
        }

        [TestFixture]
        public class operator_Sum
        {
            [Test]
            public void SimpleSumTest()
            {
                var a = new Polynomials.BinaryPolynomial("100");
                var b = new Polynomials.BinaryPolynomial("  1");
                var expectingResult = new Polynomials.BinaryPolynomial("101");
                var sumResult = a ^ b;

                sumResult.Should().Be(expectingResult);
            }

            [Test]
            public void SumTest2()
            {
                var a = new Polynomials.BinaryPolynomial("011");
                var b = new Polynomials.BinaryPolynomial("110");
                var expectingResult = new Polynomials.BinaryPolynomial("1001");
                var sumResult = a + b;

                sumResult.Should().Be(expectingResult);
            }

            [Test]
            public void SumTest3()
            {
                var a = new Polynomials.BinaryPolynomial("11");
                var b = new Polynomials.BinaryPolynomial("11");
                var expectingResult = new Polynomials.BinaryPolynomial("110");
                var sumResult = a + b;

                sumResult.Should().Be(expectingResult);
            }
        }

        [TestFixture]
        public class ToStringMethod
        {
            [Test]
            public void ForNullPolynomialEmpty()
            {
                var a = new Polynomials.BinaryPolynomial();
                var expecting = "0";

                a.ToString().Should().Be(expecting);
            }

            [Test]
            public void For3ShouldBe11()
            {
                var a = new Polynomials.BinaryPolynomial(3);
                var expecting = "11";

                a.ToString().Should().Be(expecting);
            }
        }

        [TestFixture]
        public class Divide
        {
            [Test]
            public void DivisionTest1()
            {
                var a = new Polynomials.BinaryPolynomial("10011010000");
                var b = new Polynomials.BinaryPolynomial("1101");
                var result = Polynomials.BinaryPolynomial.Division(a, b);

                result.Item1.Should().Be(new Polynomials.BinaryPolynomial("11111001"));
                result.Item2.Should().Be(new Polynomials.BinaryPolynomial("101"));
            }

            [Test]
            public void DivisionTest2()
            {
                var a = new Polynomials.BinaryPolynomial("111001010000");
                var b = new Polynomials.BinaryPolynomial("11011");
                var result = Polynomials.BinaryPolynomial.Division(a, b);

                result.Item1.Should().Be(new Polynomials.BinaryPolynomial("10101100"));
                result.Item2.Should().Be(new Polynomials.BinaryPolynomial("100"));
            }
        }

        [TestFixture]
        public class Copy
        {
            [Test]
            public void CopyEmpty()
            {
                var a = new Polynomials.BinaryPolynomial();
                var copy = a.Copy();
                var expecting = new Polynomials.BinaryPolynomial();

                expecting.Equals(copy).Should().BeTrue();
                object.ReferenceEquals(a, copy).Should().BeFalse();
            }

            [Test]
            public void CopyNonEmpty()
            {
                var a = new Polynomials.BinaryPolynomial("101011");
                var copy = a.Copy();
                var expecting = new Polynomials.BinaryPolynomial("101011");

                expecting.Equals(copy).Should().BeTrue();
                object.ReferenceEquals(a, copy).Should().BeFalse();
            }
        }

        [TestFixture]
        public class EqualsMethod
        {
            [Test]
            public void BasicEquals()
            {
                var a = new Polynomials.BinaryPolynomial("101011");
                var b = new Polynomials.BinaryPolynomial("101011");
                a.Equals(b).Should().BeTrue();
            }
        }
    }
}