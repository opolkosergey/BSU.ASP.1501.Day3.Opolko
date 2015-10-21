using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Polynomial
    {
        private readonly double[] _multipliers;

        public Polynomial(params double[] multipliers)
        {
            _multipliers = new double[multipliers.Length];
            _multipliers = multipliers;
        }

        public override int GetHashCode()
        {
            var code = _multipliers.Select((m, i) => m * (i + 1)).Sum();
            return (int)code;
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj?.GetHashCode();
        }

        public static bool operator == (Polynomial a, Polynomial b)
        {
            if(a == null || b == null) 
                throw new NullReferenceException();
            return a.Equals(b);
        }

        public static bool operator != (Polynomial a, Polynomial b)
        {
            if (a == null || b == null)
                throw new NullReferenceException();
            return a.Equals(b);
        }

    }
}
