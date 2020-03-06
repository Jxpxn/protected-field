using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldProtection.Utils
{
    public static class Callbacks
    {
        public delegate bool illegalModificationCallback<T>(ref T value, ref T rightValue, ref bool patchValue);
        public delegate bool basicIllegalModificationCallback(ref bool patchValue);
    }
}
