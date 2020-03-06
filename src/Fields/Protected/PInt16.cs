using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Protected
{
    public class PInt16 : AbstractProtectedField<short>
    {
        public PInt16(short value, AbstractEncryption encryptionInstance = null,
        AbstractEncryption protectionEncryptionInstance = null)
            : base(value, encryptionInstance, protectionEncryptionInstance) { }

        protected override byte[] getBytes(short value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override short getValue(byte[] buffer)
        {
            return BitConverter.ToInt16(buffer, 0);
        }
    }
}
