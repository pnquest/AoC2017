using System;

namespace Day24
{
    public struct Link
    {
        public int S1 { get; }
        public int S2 { get; }
        public bool IsS1Used { get; private set; }
        public bool IsS2Used { get; private set; }
        public int Strength => S1 + S2;
        public Guid Id { get; }

        public void UseSide(int val)
        {
            if (!IsS1Used && S1 == val)
            {
                IsS1Used = true;
            }
            else if (!IsS2Used && S2 == val)
            {
                IsS2Used = true;
            }
            else
            {
                throw new ArgumentException("This value matches neither side");
            }
        }

        public int? FirstFreeSide()
        {
            if (!IsS1Used)
            {
                return S1;
            }
            if (!IsS2Used)
            {
                return S2;
            }

            return null;
        }

        public bool HasFreeSide(int val)
        {
            if (!IsS1Used && S1 == val)
            {
                return true;
            }
            return !IsS2Used && S2 == val;
        }

        public Link(int s1, int s2)
        {
            S1 = s1;
            S2 = s2;
            IsS2Used = false;
            IsS1Used = false;
            Id = Guid.NewGuid();
        }

        public bool Equals(Link other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Link && Equals((Link) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
