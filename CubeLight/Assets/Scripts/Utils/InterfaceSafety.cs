using System;
using System.Linq;

namespace Assets.Scripts.Utils
{
    public static class InterfaceSafety
    {
        public static T ThrowIfMoreThanOne<T>(this T[] array)
        {
            if (array.Count() != 1)
            {
                throw new ArgumentException("Implementer object has non-existent or ambiguous IImplementable implementation.");
            }
            return array[0];
        }
    }
}
