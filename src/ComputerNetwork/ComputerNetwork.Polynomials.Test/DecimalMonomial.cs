using System;
using FluentAssertions;
using NUnit.Framework;

namespace ComputerNetwork.Polynomials.Test
{
    public abstract class DecimalMonomial
    {
        [TestFixture]
        public class OperatorPlus
        {
            [Test]
            public void DivideByZeroMonomialThrowsException()
            {
                var nonZeroMonomial = new Polynomials.DecimalMonomial(4, 7);
                var zeroMonomial = new Polynomials.DecimalMonomial(0, 7);

                nonZeroMonomial.Invoking(monomial =>
                {
                    var result = monomial / zeroMonomial;
                }).ShouldThrow<Exception>();
            }
            [Test]
            public void ZeroMonomialDivisionThrowsException()
            {
                var zeroMonomial = new Polynomials.DecimalMonomial(0, 7);
                var nonZeroMonomial = new Polynomials.DecimalMonomial(4, 7);
                zeroMonomial.Invoking(monomial =>
                {
                    var result = monomial / nonZeroMonomial;
                }).ShouldThrow<Exception>();
            }
        }
        [TestFixture]
        public class EqualsMethod
        {
            [Test]
            public void TestMethod()
            {
                var a = new Polynomials.DecimalMonomial(4, 7);
                var b = new Polynomials.DecimalMonomial(4, 7);
                Assert.AreEqual(a, b);
            }
        }

        [TestFixture]
        public class ToStringMethod
        {
            [Test]
            public void NullMonomial()
            {
                var firstDegreeMonomial = new Polynomials.DecimalMonomial();
                var expecting = "+ 0";
                expecting.ShouldBeEquivalentTo(firstDegreeMonomial.ToString());
            }

            [Test]
            public void FirstDegreeMonomial()
            {
                var firstDegreeMonomial = new Polynomials.DecimalMonomial(4, 1);
                var expecting = "+ 4";
                expecting.ShouldBeEquivalentTo(firstDegreeMonomial.ToString());
            }

            [Test]
            public void NonFirstDegreeMonomial()
            {
                var firstDegreeMonomial = new Polynomials.DecimalMonomial(87, 5);
                var expecting = "+ 87x4";
                expecting.ShouldBeEquivalentTo(firstDegreeMonomial.ToString());
            }

            [Test]
            public void NegativeMonomial()
            {
                var firstDegreeMonomial = new Polynomials.DecimalMonomial(-11, 3);
                var expecting = "- 11x2";
                expecting.ShouldBeEquivalentTo(firstDegreeMonomial.ToString());
            }
        }
    }
}
