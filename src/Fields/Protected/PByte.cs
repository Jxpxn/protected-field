using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Protected
{
    public class PByte : AbstractProtectedField<byte>
    {
        public PByte(byte value, AbstractEncryption encryptionInstance = null,
        AbstractEncryption protectionEncryptionInstance = null)
            : base(value, encryptionInstance, protectionEncryptionInstance) { }

        protected override byte[] getBytes(byte value)
        {
            return new byte[1] { value };
        }

        protected override byte getValue(byte[] buffer)
        {
            return buffer[0];
        }
    }
}
