using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class EString : AbstractField<string>
    {
        public EString(string value, AbstractEncryption encryptionInstance = null)
            : base(value, encryptionInstance) { }

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
