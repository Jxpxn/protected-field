using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Protected
{
    public class PDouble : AbstractProtectedField<double>
    {
        public PDouble(double value, AbstractEncryption encryptionInstance = null,
        AbstractEncryption protectionEncryptionInstance = null)
            : base(value, encryptionInstance, protectionEncryptionInstance) { }

        protected override byte[] getBytes(double value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override double getValue(byte[] buffer)
        {
            return BitConverter.ToDouble(buffer, 0);
        }
    }
}
