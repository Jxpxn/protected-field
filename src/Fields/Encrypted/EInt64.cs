using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class EInt64 : AbstractField<long>
    {
        public EInt64(long value, AbstractEncryption encryptionInstance = null)
            : base(value, encryptionInstance) { }

        protected override byte[] getBytes(long value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override long getValue(byte[] buffer)
        {
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
