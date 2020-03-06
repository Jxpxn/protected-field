using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Protected
{
    public class PInt64 : AbstractProtectedField<long>
    {
        public PInt64(long value, AbstractEncryption encryptionInstance = null,
        AbstractEncryption protectionEncryptionInstance = null)
            : base(value, encryptionInstance, protectionEncryptionInstance) { }

        protected override byte[] getBytes(long value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override long getValue(byte[] buffer)
        {
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
