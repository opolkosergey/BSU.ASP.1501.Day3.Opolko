using System;
using System.Linq;

namespace Task1
{
	public class Polynomial
	{
		public delegate double ArithmeticalOperation(double x, double y);

		private readonly double[] _multipliers;

		public int Degree => _multipliers.Length;

		#region .ctors

		public Polynomial(params double[] multipliers)
		{
			if (multipliers == null)
			{
				throw new ArgumentNullException("The value is null" + nameof(multipliers));
			}

			_multipliers = multipliers;
		}

		#endregion

		public double this[int i]
		{
			get
			{
				return _multipliers[i];
			}
			set
			{
				if (i != _multipliers.Length - 1)
					_multipliers[i] = value;
			}
		}

		#region public methods

		public override int GetHashCode()
		{
			if (_multipliers.Length == 1)
			{
				return (int)_multipliers[0];
			}

			if (_multipliers.Count(m => m == 0) == _multipliers.Length - 1)
			{
				return (int)_multipliers.First(m => m != 0);
			}

			var code = _multipliers.Select((m, i) => m * (i + 1)).Sum();

			return (int)code % _multipliers.Length;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			return GetHashCode() == obj.GetHashCode();
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
			{
				throw new ArgumentNullException("The value is null" + nameof(a));
			}

			if (b == null)
			{
				throw new ArgumentNullException("The value is null" + nameof(b));
			}

			return Calculate(a, b, PlusOperation);
		}

		public static Polynomial operator +(Polynomial a, int b)
		{
			if (a == null)
			{
				throw new ArgumentNullException("The value is null" + nameof(a));
			}

			var multiplies = new double[a._multipliers.Length];

			for (int i = 0; i < a._multipliers.Length; i++)
			{
				multiplies[i] = a._multipliers[i];
			}

			multiplies[0] += b;

			return new Polynomial(multiplies);
		}

		public static Polynomial operator -(Polynomial a, Polynomial b)
		{
			if (a == null)
			{
				throw new ArgumentNullException("The value is null" + nameof(a));
			}

			if (b == null)
			{
				throw new ArgumentNullException("The value is null" + nameof(b));
			}

			return Calculate(a, b, MinusOperation);
		}

		public static Polynomial operator *(Polynomial a, Polynomial b)
		{
			if (a == null)
			{
				throw new ArgumentNullException("The value is null" + nameof(a));
			}

			if (b == null)
			{
				throw new ArgumentNullException("The value is null" + nameof(b));
			}

			var multipliers = new double[a._multipliers.Length + b._multipliers.Length];

			for (int i = 0; i < a._multipliers.Length; i++)
			{
				for (int j = 0; j < b._multipliers.Length; j++)
				{
					multipliers[i + j + 1] += a._multipliers[i] * b._multipliers[j];
				}
			}				

			return new Polynomial(multipliers);
		}

		public static Polynomial operator *(Polynomial a, int k)
		{
			if (a == null)
			{
				throw new ArgumentNullException("The value is null" + nameof(a));
			}

			var multiplies = new double[a._multipliers.Length];

			for (int i = 0; i < a._multipliers.Length; i++)
			{
				multiplies[i] = a._multipliers[i] * k;
			}
				
			return new Polynomial(multiplies);
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
			double m;

			do
			{
				if (a._multipliers.Length - 1 < i)
				{
					AppendMulsOver(ref multipliers, i, b._multipliers);
					return new Polynomial(multipliers);
				}

				if (b._multipliers.Length - 1 < i)
				{
					AppendMulsOver(ref multipliers, i, a._multipliers);
					return new Polynomial(multipliers);
				}

				m = methodArithmeticalOperation(a._multipliers[i], b._multipliers[i]);
				multipliers[i] = m;
				++i;
			} while (i <= a._multipliers.Length && i <= b._multipliers.Length);

			return new Polynomial(multipliers);
		}

		private static void AppendMulsOver(ref double[] multipliers, int index, double[] source)
		{
			while (index < source.Length)
			{
				multipliers[index] = source[index];
				++index;
			}
		}

		#endregion
	}
}
