using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class EUInt64 : AbstractField<ulong>
    {
        public EUInt64(ulong value, AbstractEncryption encryptionInstance = null)
            : base(value, encryptionInstance) { }

        protected override byte[] getBytes(ulong value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override ulong getValue(byte[] buffer)
        {
            return BitConverter.ToUInt64(buffer, 0);
        }
    }
}
