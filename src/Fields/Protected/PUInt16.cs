using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Protected
{
    public class PUInt16 : AbstractProtectedField<ushort>
    {
        public PUInt16(ushort value, AbstractEncryption encryptionInstance = null,
        AbstractEncryption protectionEncryptionInstance = null)
            : base(value, encryptionInstance, protectionEncryptionInstance) { }

        protected override byte[] getBytes(ushort value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override ushort getValue(byte[] buffer)
        {
            return BitConverter.ToUInt16(buffer, 0);
        }
    }
}
