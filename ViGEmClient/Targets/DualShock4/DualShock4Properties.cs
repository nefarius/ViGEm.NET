using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Nefarius.ViGEm.Client.Targets.DualShock4
{
    public abstract class DualShock4Property : IComparable
    {
        [UsedImplicitly]
        protected DualShock4Property()
        {
        }

        protected DualShock4Property(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; }

        public int Id { get; }

        public int CompareTo(object other)
        {
            return Id.CompareTo(((DualShock4.DualShock4Property)other).Id);
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : DualShock4.DualShock4Property
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as DualShock4.DualShock4Property;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }
    }
}
