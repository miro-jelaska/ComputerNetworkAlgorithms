using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;

namespace ComputerNetwork.Polynomials
{
    public class DecimalPolynomial
    {
        /// <summary>
        /// Constructs polynomial from space separated coefficients. Each number represents monomial in exact order. For missing monomial place 0.
        /// <example> 
        /// E.g. for polynomial
        /// 4x^3 + 2x - 1
        /// string is
        /// "4 0 2 -1"
        /// </example>
        /// </summary>
        public DecimalPolynomial(string separatedCoefficient) : this()
        {
            var getNextDegree = ((Func<Func<int>>)(() => {

                var nextDegree = 1;
                return () => nextDegree++;
            }))();

            separatedCoefficient
            .Trim()
            .Split(' ')
            .Reverse()
            .Select(x => new DecimalMonomial(int.Parse(x), getNextDegree()))
            .ToList()
            .ForEach(Polynomials.Add);

            this.Normalize();
            this.TrimLeadingZeroMonomials();
        }

        /// <summary>
        /// Constructs polynomial from list of monomials.
        /// </summary>
        public DecimalPolynomial(IEnumerable<DecimalMonomial> coefficients) : this()
        {
            Polynomials.AddRange(coefficients);
            this.Normalize();
            this.TrimLeadingZeroMonomials();
        }

        /// <summary>
        /// Constructs polynomial from existing polynomial.
        /// </summary>
        public DecimalPolynomial(DecimalPolynomial copySource) : this()
        {
            copySource.Should().NotBeNull();

            Polynomials = copySource.Polynomials;
            this.Normalize();
            this.TrimLeadingZeroMonomials();
        }

        /// <summary>
        /// Constructs null polynomial.
        /// </summary>
        public DecimalPolynomial()
        {
            this.Polynomials = new List<DecimalMonomial>();
        }

        public List<DecimalMonomial> Polynomials { get; private set; }

        /// <summary>
        /// Gets/sets value for monomial with selected degree.
        /// </summary>
        public DecimalMonomial this[int degree]
        {
            get { return this.Polynomials[degree - 1]; }
            set { this.Polynomials[degree - 1] = value; }
        }

        /// <summary>
        /// Degree is defined as max degree value of monomials. For nullmonomial degree is 0.
        /// </summary>
        public int Degree => Polynomials.Any() ? Polynomials.Max(x => x.Degree) : 0;
        public bool IsNullPolynomial => this.Degree == 0;

        /// <summary>
        /// Leading monomial is monomial with greatest degree.
        /// </summary>
        public DecimalMonomial LeadingMonomial => Polynomials.Last();
        public DecimalPolynomial AdditiveInverse => new DecimalPolynomial(Polynomials.Select(monomial => new DecimalMonomial(-1 * monomial.Value, monomial.Degree)));

        public DecimalPolynomial Copy()
        {
            return new DecimalPolynomial(this);
        }

        public DecimalPolynomial Multiply(DecimalMonomial monomial)
        {
            return new DecimalPolynomial(this.Polynomials.Select(x => x * monomial).ToList());
        }

        /// <summary>
        /// Rises degree of all monomials for degreeDifference.
        /// </summary>
        public DecimalPolynomial RiseDegreeBy(int degreeDifference)
        {
            return
                new DecimalPolynomial(
                    this.Polynomials
                    .Select(monomial => new DecimalMonomial(monomial.Value, monomial.Degree + degreeDifference))
                    .ToList()
                );
        }

        public DecimalPolynomial MultiplyBy(int valueMultiplicalDifference)
        {
            return
                new DecimalPolynomial(
                    this.Polynomials
                    .Select(monomial => new DecimalMonomial(monomial.Value * valueMultiplicalDifference, monomial.Degree))
                    .ToList()
                );
        }

        /// <summary>
        /// Inserts missing imaginary monomials and orders monomials.
        /// Imaginary monomials are those which have value of 0.
        /// Imaginary monomials simplify impelementation of operations over Polynomials.
        /// <example> 
        /// E.g. for polynomial
        /// 4x^3 + 2x - 1
        /// imaginary monomials are 0x^2 and 0x.
        /// </example>
        /// </summary>
        public void Normalize()
        {
            this.Polynomials =
                (from imaginaryMonomial in Enumerable.Range(1, this.Degree).Select(degree => new DecimalMonomial(0, degree))
                 join existingMonomial in this.Polynomials
                     on imaginaryMonomial.Degree equals existingMonomial.Degree
                     into existingImaginaryJoinGroup
                 from existingForImaginaryJoin in existingImaginaryJoinGroup.DefaultIfEmpty()
                 select existingForImaginaryJoin ?? imaginaryMonomial)
                 .ToList();
        }

        /// <summary>
        /// Removes leading zero monomials.
        /// <example> 
        /// E.g. for polynomial
        /// 0x^3 + 5x^2 + 7x - 1
        /// leading zero monomial is 0x^3. After trimming polynomial looks like
        /// 5x^2 + 7x - 1
        /// </example>
        /// </summary>
        public void TrimLeadingZeroMonomials()
        {
            this.Polynomials = this.Polynomials.Take(this.Polynomials.FindLastIndex(monomial => monomial.Value != 0) + 1).ToList();
        }

        public void Add(DecimalPolynomial summand)
        {
            summand.Polynomials.ForEach(this.Add);
        }

        public void Add(DecimalMonomial monomial)
        {
            if (monomial.Degree > this.Degree)
            {
                this.Polynomials.Add(monomial);
                this.Normalize();
            }
            else
            {
                this[monomial.Degree] = this[monomial.Degree] + monomial;
            }
        }

        public void Substract(DecimalPolynomial subtrahend)
        {
            subtrahend.AdditiveInverse.Polynomials.ForEach(this.Add);
            this.Normalize();
            this.TrimLeadingZeroMonomials();
        }

        public static DecimalPolynomial operator +(DecimalPolynomial left, DecimalPolynomial right)
        {
            var polynomialCopy = left.Copy();
            polynomialCopy.Add(right);
            return polynomialCopy;
        }
        public static DecimalPolynomial operator -(DecimalPolynomial left, DecimalPolynomial right)
        {
            var polynomialCopy = left.Copy();
            polynomialCopy.Substract(right);
            return polynomialCopy;
        }

        /// <summary>
        /// Divides left polynomial with right and returns tuple (result, rest).
        /// </summary>
        public static Tuple<DecimalPolynomial, DecimalPolynomial> Divide(DecimalPolynomial left, DecimalPolynomial right)
        {
            return Divide(left, right, new DecimalPolynomial());
        }
        /// <summary>
        /// This method implements division trough recursion.
        /// </summary>
        private static Tuple<DecimalPolynomial, DecimalPolynomial> Divide(DecimalPolynomial left, DecimalPolynomial right, DecimalPolynomial accumulatedResult)
        {
            var leftIsSmallerDegree = left.Degree < right.Degree;
            var isLeftDivisible = left.LeadingMonomial.Value / right.LeadingMonomial.Value != 0;
            if (leftIsSmallerDegree || !isLeftDivisible)
                return Tuple.Create(accumulatedResult, left);

            var degreeDifference = left.LeadingMonomial.Degree - right.LeadingMonomial.Degree;
            var valueMultiplicalDifference = left.LeadingMonomial.Value / right.LeadingMonomial.Value;
            var polynomialForSubstraction = right.MultiplyBy(valueMultiplicalDifference).RiseDegreeBy(degreeDifference);
            accumulatedResult.Add(new DecimalMonomial(valueMultiplicalDifference, degreeDifference + 1));

            return Divide(left - polynomialForSubstraction, right, accumulatedResult);
        }

        /// <summary>
        /// Returns polynomials string representation.
        /// </summary>
        public override string ToString() =>
                this.Polynomials.AsEnumerable()
                .Reverse()
                .Aggregate(
                    seed: new StringBuilder(),
                    func: (aggregate, currentMonomial) => aggregate.AppendFormat("{0} ", currentMonomial))
                .ToString();

        /// <summary>
        /// Two polynomials are equal if all of their monomials are equal.
        /// </summary>
        public override bool Equals(object obj)
        {
            var objAsPolynomial = obj as DecimalPolynomial;
            if (objAsPolynomial == null)
                return false;

            return this.Equals(objAsPolynomial);
        }

        protected bool Equals(DecimalPolynomial other)
        {
            if (other == null)
                return false;
            return
                this.Polynomials.Count == other.Polynomials.Count &&
                !this.Polynomials.Except(other.Polynomials).Any();
        }

        public override int GetHashCode()
        {
            return Polynomials?.GetHashCode() ?? 0;
        }
    }
}
