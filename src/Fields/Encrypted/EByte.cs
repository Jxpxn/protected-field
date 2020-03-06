using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class EByte : AbstractField<byte>
    {
        public EByte(byte value, AbstractEncryption encryptionInstance = null)
            : base(value, encryptionInstance) { }

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
