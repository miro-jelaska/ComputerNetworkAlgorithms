using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FluentAssertions;

namespace ComputerNetwork.Polynomials
{
    public class BinaryPolynomial
    {
        public static int MaxDegree => (int)Math.Floor(Math.Log(uint.MaxValue, 2)) + 1;

        /// <summary>
        /// Constructs polynomial from coefficients.
        /// <example> 
        /// E.g. for polynomial
        /// x^3 + x + 1
        /// string is
        /// "1011"
        /// </example>
        /// </summary>
        public BinaryPolynomial(string polynomialAsString)
        {
            polynomialAsString.Should().NotBeNullOrEmpty();

            var cleanPolynomialString = polynomialAsString.Trim().TrimStart('0');
            Regex.IsMatch(cleanPolynomialString, @"^[01]*$").Should().BeTrue();
            cleanPolynomialString.Length.Should().BeLessOrEqualTo(MaxDegree);

            cleanPolynomialString
            .ToCharArray()
            .ToList()
            .ForEach(x =>
            {
                this.Polynomial = this.Polynomial << 1;
                if (x == '1')
                    this.Polynomial = this.Polynomial + 1;
            });
        }

        /// <summary>
        /// Constructs polynomial from existing polynomial.
        /// </summary>
        public BinaryPolynomial(BinaryPolynomial copySource)
        {
            this.Polynomial = copySource.Polynomial;
        }

        /// <summary>
        /// Constructs polynomial from uint representing polynomial.
        /// </summary>
        public BinaryPolynomial(uint polynomial)
        {
            this.Polynomial = polynomial;
        }

        /// <summary>
        /// Constructs null polynomial.
        /// </summary>
        public BinaryPolynomial()
        {
            this.Polynomial = 0;
        }

        /// <summary>
        /// Gets/sets value for monomial with selected degree.
        /// </summary>
        public bool this[int degree]
        {
            get
            {
                var polynomialAsArray = this.ToString().ToCharArray().Reverse();
                return
                    polynomialAsArray.Count() >= degree 
                        ? polynomialAsArray.Select(x => x == '1').ToList()[degree - 1]
                        : false;
            }
            set
            {
                var valueToNumber = Convert.ToUInt32(value) << (degree - 1);
                var isCurrentValue1 = this[degree];
                if (!isCurrentValue1)
                    this.Polynomial = this.Polynomial + valueToNumber;
            }
        }

        public uint Polynomial { get; private set; }
        public bool IsNullPolynomial => this.Polynomial == 0;

        /// <summary>
        /// Degree is defined as max degree of leading monomial. For nullmonomial degree is 0.
        /// </summary>
        public int Degree => !this.IsNullPolynomial ? (int) Math.Floor(Math.Log(this.Polynomial, 2)) + 1 : 0;

        /// <summary>
        /// Rises degree of all monomials for degreeDifference by bit shifing polynomial to the right.
        /// </summary>
        public BinaryPolynomial RiseDegree(int degreeDifference)
        {
            degreeDifference.Should().BeGreaterOrEqualTo(0);
            (degreeDifference + this.Degree).Should().BeLessOrEqualTo(MaxDegree);

            var polynomialCopy = this.Copy();
            polynomialCopy.Polynomial = polynomialCopy.Polynomial << degreeDifference;
            return polynomialCopy;
        }

        public BinaryPolynomial Copy() => new BinaryPolynomial(this);

        /// <summary>
        /// Divides left polynomial with right and returns tuple (result, rest).
        /// </summary>
        public static Tuple<BinaryPolynomial, BinaryPolynomial> Division(BinaryPolynomial left, BinaryPolynomial right)
        {
            return Division(left.Copy(), right, left.Degree, new StringBuilder());
        }
        /// <summary>
        /// This method implements division trough recursion.
        /// </summary>
        public static Tuple<BinaryPolynomial, BinaryPolynomial> Division(BinaryPolynomial left, BinaryPolynomial right, int currentDegreeForLeft, StringBuilder aggregatedResult)
        {
            left.Should().NotBeNull();
            right.Should().NotBeNull();

            var isCurrentLeadingNumberOne = left[currentDegreeForLeft];
            if (currentDegreeForLeft < right.Degree)
                return Tuple.Create(new BinaryPolynomial(aggregatedResult.ToString()), new BinaryPolynomial(left.ToString()));

            if (isCurrentLeadingNumberOne)
            {
                left = left ^ right.RiseDegree(currentDegreeForLeft - right.Degree);
                aggregatedResult.Append("1");
            }
            else
            {
                aggregatedResult.Append("0");
            }

            return Division(left, right, currentDegreeForLeft - 1, aggregatedResult);
        }

        public static BinaryPolynomial operator ^(BinaryPolynomial left, BinaryPolynomial right)
        {
            left.Should().NotBeNull();
            right.Should().NotBeNull();

            return new BinaryPolynomial(left.Polynomial ^ right.Polynomial);
        }

        public override string ToString() => Convert.ToString(this.Polynomial, 2);

        public override bool Equals(object obj)
        {
            var objAsPolynomial = obj as BinaryPolynomial;
            if (objAsPolynomial == null)
                return false;

            return this.Equals(objAsPolynomial);
        }

        protected bool Equals(BinaryPolynomial other)
        {
            if (other == null)
                return false;
            return
                this.Polynomial == other.Polynomial;
        }

        public override int GetHashCode()
        {
            return (int) Polynomial;
        }
    }
}
