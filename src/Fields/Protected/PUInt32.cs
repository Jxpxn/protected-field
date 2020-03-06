using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Protected
{
    public class PUInt32 : AbstractProtectedField<uint>
    {
        public PUInt32(uint value, AbstractEncryption encryptionInstance = null,
        AbstractEncryption protectionEncryptionInstance = null)
            : base(value, encryptionInstance, protectionEncryptionInstance) { }

        protected override byte[] getBytes(uint value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override uint getValue(byte[] buffer)
        {
            return BitConverter.ToUInt32(buffer, 0);
        }
    }
}
