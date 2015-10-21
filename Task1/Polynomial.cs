using System;
using System.Linq;

namespace Task1
{
    public class Polynomial
    {
        public delegate double ArithmeticalOperation(double x, double y);
        private readonly double[] _multipliers;
        #region .ctors
        public Polynomial(params double[] multipliers)
        {
            if (multipliers.Length == 0)
                throw new ArgumentNullException("The value is null" + nameof(multipliers));
            _multipliers = new double[multipliers.Length];
            _multipliers = multipliers;
        }
        #endregion

        #region public methods
        public override int GetHashCode()
        {
            var code = _multipliers.Select((m, i) => m * (i + 1)).Sum();
            return (int)code;
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj?.GetHashCode();
        }

        public static bool operator ==(Polynomial a, Polynomial b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(Polynomial a, Polynomial b)
        {
            return !(a == b);
        }

        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            if (a == null)
                throw new ArgumentNullException("The value is null" + nameof(a));
            if (b == null)
                throw new ArgumentNullException("The value is null" + nameof(b));
            return Calculate(a, b, PlusOperation);
        }

        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            if (a == null)
                throw new ArgumentNullException("The value is null" + nameof(a));
            if (b == null)
                throw new ArgumentNullException("The value is null" + nameof(b));
            return Calculate(a, b, MinusOperation);
        }

        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            if (a == null)
                throw new ArgumentNullException("The value is null" + nameof(a));
            if (b == null)
                throw new ArgumentNullException("The value is null" + nameof(b));

            var multipliers = new double[a._multipliers.Length + b._multipliers.Length];

            for (int i = 0; i < a._multipliers.Length; i++)
                for (int j = 0; j < b._multipliers.Length; j++)
                {
                    multipliers[i + j + 1] += a._multipliers[i]*b._multipliers[j];
                }

            return new Polynomial(multipliers);
        }
        #endregion

        #region private methods
        private static double MinusOperation(double a, double b)
        {
            return a - b;
        }

        private static double PlusOperation(double a, double b)
        {
            return a + b;
        }

        private static Polynomial Calculate(Polynomial a, Polynomial b, ArithmeticalOperation methodArithmeticalOperation)
        {
            var multipliers = a._multipliers.Length > b._multipliers.Length ? new double[a._multipliers.Length] : new double[b._multipliers.Length];
            int i = 0;
            double m = 0.0;

            try
            {
                do
                {
                    m = methodArithmeticalOperation(a._multipliers[i], b._multipliers[i]);
                    multipliers[i] = m;
                    ++i;
                } while (i <= a._multipliers.Length && i <= b._multipliers.Length);
            }

            catch (IndexOutOfRangeException)
            {
                if (a._multipliers.Length - 1 < i)
                {
                    while (i < b._multipliers.Length)
                    {
                        multipliers[i] = b._multipliers[i];
                        ++i;
                    }
                }
                else
                {
                    while (i < a._multipliers.Length)
                    {
                        multipliers[i] = a._multipliers[i];
                        ++i;
                    }
                }
            }
            return new Polynomial(multipliers);
        }
        #endregion
    }
}
