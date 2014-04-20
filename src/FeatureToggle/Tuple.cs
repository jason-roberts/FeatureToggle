using System;

namespace JasonRoberts.FeatureToggle
{
    public class Tuple<T1, T2> : IEquatable<Tuple<T1, T2>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }

        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public bool Equals(Tuple<T1, T2> other)
        {
            return Item1.Equals(other.Item1) && Item2.Equals(other.Item2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as Tuple<T1, T2>;

            if (other == null)
                return false;

            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return Item1.GetHashCode() ^ Item2.GetHashCode();
        }
    } 
}
