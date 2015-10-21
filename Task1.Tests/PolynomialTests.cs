using System.Collections.Generic;
using NUnit.Framework;

namespace Task1.Tests
{
    [TestFixture]
    internal class PolynomialTests
    {
       public IEnumerable<TestCaseData> TestMul
        {
            get
            {
                yield return new TestCaseData(new Polynomial(0.0, 4, 3), new Polynomial(8, 3), new Polynomial(0, 0, 32, 36, 9)).Returns(true);
                yield return new TestCaseData(new Polynomial(4), new Polynomial(8, 3), new Polynomial(0, 0, 32, 36, 9)).Returns(false);
                yield return new TestCaseData(new Polynomial(-5), new Polynomial(8), new Polynomial(0, -40)).Returns(true);
                yield return new TestCaseData(new Polynomial(1,2), new Polynomial(3,4), new Polynomial(0, 3, 10, 8)).Returns(true);
            }
        }

        [Test, TestCaseSource(nameof(TestMul))]
        public bool Mul_Test(Polynomial a, Polynomial b,Polynomial result)
        {
            return result.Equals(a * b);
        }

        public IEnumerable<TestCaseData> TestPlus
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 4, 3), new Polynomial(8, 3), new Polynomial(9, 7, 3)).Returns(true);
                yield return new TestCaseData(new Polynomial(4), new Polynomial(8, 3), new Polynomial(12,3)).Returns(true);
                yield return new TestCaseData(new Polynomial(-5), new Polynomial(8), new Polynomial(40)).Returns(false);
                yield return new TestCaseData(new Polynomial(0, 2), new Polynomial(0,0,3, 4), new Polynomial(0, 2, 3, 4)).Returns(true);
                yield return new TestCaseData(new Polynomial(-5,-10), new Polynomial(-2), new Polynomial(-7,-10)).Returns(true);
            }
        }

        [Test, TestCaseSource(nameof(TestPlus))]
        public bool Plus_Test(Polynomial a, Polynomial b, Polynomial result)
        {
            return result.Equals(a + b);
        }

        public IEnumerable<TestCaseData> TestMinus
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 4, 3), new Polynomial(8, 3), new Polynomial(-7, 1, 3)).Returns(true);
                yield return new TestCaseData(new Polynomial(4), new Polynomial(8, 3), new Polynomial(-4, 3)).Returns(true);
                yield return new TestCaseData(new Polynomial(-5), new Polynomial(8), new Polynomial(13)).Returns(false);
                yield return new TestCaseData(new Polynomial(0, 2), new Polynomial(1, 0, 3, 4), new Polynomial(-1, 2, 3, 4)).Returns(true);
                yield return new TestCaseData(new Polynomial(-5,-8,-10), new Polynomial(-2,-1), new Polynomial(-3,-7,-10)).Returns(true);
            }
        }

        [Test, TestCaseSource(nameof(TestMinus))]
        public bool Minus_Test(Polynomial a, Polynomial b, Polynomial result)
        {
            return result.Equals(a - b);
        }
    }
}
