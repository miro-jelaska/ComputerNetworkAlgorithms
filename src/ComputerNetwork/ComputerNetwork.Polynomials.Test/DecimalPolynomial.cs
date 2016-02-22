using System;
using FluentAssertions;
using NUnit.Framework;

namespace ComputerNetwork.Polynomials.Test
{
    public abstract class DecimalPolynomial
    {
        [TestFixture]
        public class Constructor
        {
            [Test]
            public void WorksWith_ExtraSpaceAtBeggining_InStringFormatedPolynomial()
            {
                Assert.DoesNotThrow(() => new Polynomials.DecimalPolynomial("  20 7 0 4"));
            }

            [Test]
            public void WorksWith_ExtraSpaceAtEnd_InStringFormatedPolynomial()
            {
                Assert.DoesNotThrow(() => new Polynomials.DecimalPolynomial("20 7 0 4  "));
            }

            [Test]
            public void TrimsLeadingZeroMonomials()
            {
                var a = new Polynomials.DecimalPolynomial("  20 7 0 4");
                var b = new Polynomials.DecimalPolynomial("0 20 7 0 4");
                a.Equals(b).Should().BeTrue();
                a.Degree.Should().Be(4);
                b.Degree.Should().Be(4);
            }

            [Test]
            public void CopyShouldThrowErrorIfPolynomialIsNull()
            {
                Polynomials.DecimalPolynomial nullPolynomial = null;
                Action act = () => new Polynomials.DecimalPolynomial(nullPolynomial);

                act.ShouldThrow<Exception>();
            }
        }

        [TestFixture]
        public class Copy
        {
            [Test]
            public void CopyEmpty()
            {
                var a = new Polynomials.DecimalPolynomial();
                var copy = a.Copy();
                var expecting = new Polynomials.DecimalPolynomial();

                expecting.Equals(copy).Should().BeTrue();
                object.ReferenceEquals(a, copy).Should().BeFalse();
            }

            [Test]
            public void CopyNonEmpty()
            {
                var a = new Polynomials.DecimalPolynomial("20 7 0 4");
                var copy = a.Copy();
                var expecting = new Polynomials.DecimalPolynomial("20 7 0 4");

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
                var a = new Polynomials.DecimalPolynomial("20 7 0 4");
                var b = new Polynomials.DecimalPolynomial("20 7 0 4");
                a.Equals(b).Should().BeTrue();
            }
        }

        [TestFixture]
        public class Normalize
        {
            [Test]
            public void NormalizeForSingleMonomial()
            {
                var a = new Polynomials.DecimalPolynomial();
                a.Add(new Polynomials.DecimalMonomial(10, 3));

                var expecting = new Polynomials.DecimalPolynomial("10 0 0");
                expecting.Equals(a).Should().BeTrue();
            }

            [Test]
            public void AddsMissingMonomials_InsertedInDescendingOrder()
            {
                var a = new Polynomials.DecimalPolynomial();
                a.Add(new Polynomials.DecimalMonomial(10, 3));
                a.Add(new Polynomials.DecimalMonomial(7, 1));

                var expecting = new Polynomials.DecimalPolynomial("10 0 7");
                expecting.Equals(a).Should().BeTrue();
            }

            [Test]
            public void AddsMissingMonomials_InsertedInAscendingOrder()
            {
                var a = new Polynomials.DecimalPolynomial();
                a.Add(new Polynomials.DecimalMonomial(7, 1));
                a.Add(new Polynomials.DecimalMonomial(10, 3));

                var expecting = new Polynomials.DecimalPolynomial("10 0 7");
                expecting.Equals(a).Should().BeTrue();
            }
        }

        [TestFixture]
        public class operator_Division
        {
            [Test]
            public void SimpleDivisionTest()
            {
                var a = new Polynomials.DecimalPolynomial("10 0 7");
                var b = new Polynomials.DecimalPolynomial("2 1");
                var divisionResult = Polynomials.DecimalPolynomial.Divide(a, b);
                var result = divisionResult.Item1;
                var rest = divisionResult.Item2;

                var expectingResult = new Polynomials.DecimalPolynomial("5 -2");
                var expectingRest = new Polynomials.DecimalPolynomial("-1 9");
                expectingResult.Should().Be(result);
                expectingRest.Should().Be(rest);
            }

            [Test]
            public void Undivisible()
            {
                var a = new Polynomials.DecimalPolynomial("18 7");
                var b = new Polynomials.DecimalPolynomial("4 2 1");
                var divisionResult = Polynomials.DecimalPolynomial.Divide(a, b);
                var result = divisionResult.Item1;
                var rest = divisionResult.Item2;

                var expectingResult = new Polynomials.DecimalPolynomial();
                var expectingRest = new Polynomials.DecimalPolynomial("18 7");
                expectingResult.Should().Be(result);
                expectingRest.Should().Be(rest);
            }

            [Test]
            public void ComplexDivisionTest()
            {
                var a = new Polynomials.DecimalPolynomial("92 17 0 18 0 7");
                var b = new Polynomials.DecimalPolynomial("4 1 0 3");
                var divisionResult = Polynomials.DecimalPolynomial.Divide(a, b);
                var result = divisionResult.Item1;
                var rest = divisionResult.Item2;

                var expectingResult = new Polynomials.DecimalPolynomial("23 -1 0");
                var expectingRest = new Polynomials.DecimalPolynomial("-2 1 -51 3 7");
                expectingResult.Should().Be(result);
                expectingRest.Should().Be(rest);
            }

            [Test]
            public void DivisionWithNullPolynomialShouldThrowException()
            {
                var a = new Polynomials.DecimalPolynomial("18 7");
                var b = new Polynomials.DecimalPolynomial();
                Action act = () => Polynomials.DecimalPolynomial.Divide(a, b);

                act.ShouldThrow<Exception>();
            }
        }
    }
}