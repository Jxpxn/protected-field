using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Protected
{
    public class PInt32 : AbstractProtectedField<int>
    {
        public PInt32(int value, AbstractEncryption encryptionInstance = null,
        AbstractEncryption protectionEncryptionInstance = null)
            :base(value, encryptionInstance, protectionEncryptionInstance) { }

        protected override byte[] getBytes(int value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override int getValue(byte[] buffer)
        {
            return BitConverter.ToInt32( buffer, 0 );
        }
    }
}
