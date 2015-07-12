using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexer
{
    public class Interval<T> where T : IComparable<T>
    {
        public Interval(T min, T max)
        {
            Min = min;
            Max = max;
        }

        public T Min { get; set; }

        public T Max { get; set; }

        public override string ToString()
        {
            return String.Format("[{0} - {1}]", Min, Max);
        }

        public Boolean IsValid()
        {
            return Min.CompareTo(Max) <= 0;
        }

        public Boolean Contains(T value)
        {
            return (Min.CompareTo(value) <= 0) && (value.CompareTo(Max) <= 0);
        }

        public Boolean IsInside(Interval<T> Range)
        {
            return this.IsValid() && Range.IsValid() && Range.Contains(this.Min) && Range.Contains(this.Max);
        }
    }
}
