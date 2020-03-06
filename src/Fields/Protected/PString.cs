using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Protected
{
    public class PString : AbstractProtectedField<string>
    {
        public PString(string value, AbstractEncryption encryptionInstance = null,
        AbstractEncryption protectionEncryptionInstance = null)
            : base(value, encryptionInstance, protectionEncryptionInstance) { }

        protected override byte[] getBytes(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        protected override string getValue(byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer);
        }
    }
}
