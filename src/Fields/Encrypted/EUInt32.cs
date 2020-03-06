using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class EUInt32 : AbstractField<uint>
    {
        public EUInt32(uint value, AbstractEncryption encryptionInstance = null)
            : base(value, encryptionInstance) { }

        protected override byte[] getBytes(uint value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override uint getValue(byte[] buffer)
        {
            return BitConverter.ToUInt32(buffer, 0);
        }
    }
}
