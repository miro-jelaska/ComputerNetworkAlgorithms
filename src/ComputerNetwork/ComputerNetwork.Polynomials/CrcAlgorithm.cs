using System.Linq;

namespace ComputerNetwork.Polynomials
{
    public static class CrcAlgorithm
    {
        private static BinaryPolynomial GeneratorPolynomial { get; } = new BinaryPolynomial("11010");
        private static int Padding { get; } = GeneratorPolynomial.Degree - 1;
        public static BinaryPolynomial GenerateCrc(BinaryPolynomial polynomial)
        {
            var polynomialWithPaddingShift = polynomial.Copy().RiseDegree(Padding);
            var divisionResult = BinaryPolynomial.Division(polynomialWithPaddingShift, GeneratorPolynomial);

            divisionResult.Item2
                .ToString()
                .ToCharArray()
                .Reverse()
                .Zip(
                    second: Enumerable.Range(0, Padding),
                    resultSelector: (character, index) => new {character, index})
                .Aggregate(
                    seed: polynomialWithPaddingShift,
                    func: (aggregate, current) =>
                    {
                        aggregate[current.index + 1] = (current.character == '1');
                        return aggregate;
                    });

            return polynomialWithPaddingShift;
        }
        public static bool IsCrcValid(BinaryPolynomial polynomialForCheck)
        {
            var polynomial = polynomialForCheck.Copy();
            var divisionResult = BinaryPolynomial.Division(polynomial, GeneratorPolynomial);

            return divisionResult.Item2.IsNullPolynomial;
        }
    }
}
