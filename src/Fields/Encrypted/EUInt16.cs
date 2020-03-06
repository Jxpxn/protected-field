using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class EUInt16 : AbstractField<ushort>
    {
        public EUInt16(ushort value, AbstractEncryption encryptionInstance = null)
            : base(value, encryptionInstance) { }

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
