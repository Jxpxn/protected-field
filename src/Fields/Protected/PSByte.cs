using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Protected
{
    public class PSByte : AbstractProtectedField<sbyte>
    {
        public PSByte(sbyte value, AbstractEncryption encryptionInstance = null,
        AbstractEncryption protectionEncryptionInstance = null)
            : base(value, encryptionInstance, protectionEncryptionInstance) { }

        protected override byte[] getBytes(sbyte value)
        {
            return new byte[1] { Convert.ToByte(value) };
        }

        protected override sbyte getValue(byte[] buffer)
        {
            return Convert.ToSByte(buffer[0]);
        }
    }
}
