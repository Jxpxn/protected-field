using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Protected
{
    public class PUInt64 : AbstractProtectedField<ulong>
    {
        public PUInt64(ulong value, AbstractEncryption encryptionInstance = null,
        AbstractEncryption protectionEncryptionInstance = null)
            : base(value, encryptionInstance, protectionEncryptionInstance) { }

        protected override byte[] getBytes(ulong value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override ulong getValue(byte[] buffer)
        {
            return BitConverter.ToUInt64(buffer, 0);
        }
    }
}
