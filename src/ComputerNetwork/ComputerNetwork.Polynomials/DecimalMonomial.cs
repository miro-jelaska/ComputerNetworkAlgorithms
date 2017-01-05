using System;
using FluentAssertions;

namespace ComputerNetwork.Polynomials
{
    /// <summary>
    /// Represents one part of the polynomial.
    /// </summary>
    /// <example> 
    /// In the polynomial
    /// 4x^3 + 2x -1
    /// monomials are [4x^3, 2x, -1]
    /// </example>
    public class DecimalMonomial
    {
        /// <summary>
        /// Constructs a monomial.
        /// </summary>
        /// <example>
        /// For the monomial
        /// 4x^3
        /// value is 4 and degree is 3
        /// </example>
        public DecimalMonomial(int value, int degree)
        {
            this.Value = value;
            this.Degree = degree;

            degree.Should().BeGreaterOrEqualTo(1);
        }

        public DecimalMonomial() : this(0, 1)
        {
        }

        public int Value { get; }
        public int Degree { get; }

        public static DecimalMonomial operator +(DecimalMonomial left, DecimalMonomial right)
        {
            (left.Degree == right.Degree).Should().BeTrue();
            return new DecimalMonomial(left.Value + right.Value, left.Degree);
        }
        public static DecimalMonomial operator -(DecimalMonomial left, DecimalMonomial right)
        {
            (left.Degree == right.Degree).Should().BeTrue();
            return new DecimalMonomial(left.Value - right.Value, left.Degree);
        }
        public static DecimalMonomial operator *(DecimalMonomial left, DecimalMonomial right)
        {
            return new DecimalMonomial(left.Value * right.Value, left.Degree + right.Degree);
        }
        public static DecimalMonomial operator /(DecimalMonomial left, DecimalMonomial right)
        {
            left.Degree.Should().BeGreaterThan(0);
            right.Degree.Should().BeGreaterThan(0);
            return new DecimalMonomial(left.Value / right.Value, left.Degree - right.Degree);
        }
        public static DecimalMonomial operator %(DecimalMonomial left, DecimalMonomial right)
        {
            return new DecimalMonomial(left.Value % right.Value, left.Degree - right.Degree);
        }

        public override string ToString()
        {
            var sign = this.Value < 0 ? "-" : "+";
            var xPart = this.Degree > 1 ? $"x{this.Degree - 1}" : string.Empty;

            return $"{sign} {Math.Abs(this.Value)}{xPart}";
        }

        public override bool Equals(object obj)
        {
            var objAsMonomial = obj as DecimalMonomial;
            if (objAsMonomial == null)
                return false;

            return this.Equals(objAsMonomial);
        }

        protected bool Equals(DecimalMonomial other)
        {
            if (other == null)
                return false;

            return this.Value == other.Value && this.Degree == other.Degree;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value * 397) ^ Degree;
            }
        }
    }
}
