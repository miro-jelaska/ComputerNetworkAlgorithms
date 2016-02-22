using System;
using FluentAssertions;

namespace ComputerNetwork.Graphs
{
    public class Edge
    {
        private enum Sign
        {
            Positive,
            Negative
        }
        private Edge(int? weight, Sign sign = Sign.Positive)
        {
            this.weight = weight;
            this.sign = sign;
        }

        private int? weight { get; }
        private Sign sign { get; }


        public static Edge Some(int weight)
        {
            return new Edge(weight);
        }
        public static Edge PositiveInfinity()
        {
            return new Edge(null, Sign.Positive);
        }
        public static Edge NegativeInfinity()
        {
            return new Edge(null, Sign.Negative);
        }


        public bool IsFinite => this.weight != null;
        public bool IsPositiveInfinity => !this.IsFinite && this.sign == Sign.Positive;
        public bool IsNegativeInfinity => !this.IsFinite && this.sign == Sign.Negative;

        public int Get()
        {
            if(this.IsFinite)
                return (int)this.weight;
            throw new Exception();
        }
        public int GetOrElse(int elseWeight) => this.IsFinite ? (int)this.weight : elseWeight;

        public static Edge operator +(Edge left, Edge right)
        {
            left.Should().NotBeNull();
            right.Should().NotBeNull();

            var onePositiveInfinityOtherNegativeInfinity = !left.IsFinite && !right.IsNegativeInfinity &&
                                                           left.IsNegativeInfinity != right.IsPositiveInfinity;
            if(onePositiveInfinityOtherNegativeInfinity)
                throw new Exception("Sum of positive infinity and negative infinity is undefined.");

            var oneFinitOtherInfinite = left.IsFinite ^ right.IsFinite;
            if (oneFinitOtherInfinite)
            {
                return left.IsFinite ? right : left;
            }

            return new Edge(left.weight + right.weight);
        }

        public static bool operator <(Edge left, Edge right)
        {
            left.Should().NotBeNull();
            right.Should().NotBeNull();

            var bothAreFinite = left.IsFinite && right.IsFinite;
            if (bothAreFinite)
            {
                return left.weight < right.weight;
            }

            var bothAreInfinite = !left.IsFinite && !right.IsFinite;
            if (bothAreInfinite)
            {
                return left.IsNegativeInfinity && right.IsPositiveInfinity;
            }

            return left.IsNegativeInfinity || right.IsPositiveInfinity;
        }

        public static bool operator >(Edge left, Edge right)
        {
            left.Should().NotBeNull();
            right.Should().NotBeNull();

            var bothAreFinite = left.IsFinite && right.IsFinite;
            if (bothAreFinite)
            {
                return left.weight > right.weight;
            }

            var bothAreInfinite = !left.IsFinite && !right.IsFinite;
            if (bothAreInfinite)
            {
                return left.IsPositiveInfinity && right.IsNegativeInfinity;
            }

            return !(left.IsNegativeInfinity || right.IsPositiveInfinity);
        }
    }
}
