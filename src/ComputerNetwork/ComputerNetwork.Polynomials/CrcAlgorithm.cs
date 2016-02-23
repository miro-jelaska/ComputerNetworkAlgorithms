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

            return polynomialWithPaddingShift + divisionResult.Item2;
        }
        public static bool IsCrcValid(BinaryPolynomial polynomialForCheck)
        {
            var polynomial = polynomialForCheck.Copy();
            var divisionResult = BinaryPolynomial.Division(polynomial, GeneratorPolynomial);

            return divisionResult.Item2.IsNullPolynomial;
        }
    }
}
