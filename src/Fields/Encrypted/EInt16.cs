using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class EInt16 : AbstractField<short>
    {
        public EInt16(short value, AbstractEncryption encryptionInstance = null)
            : base(value, encryptionInstance) { }

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
