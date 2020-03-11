using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class ESByte : AbstractField<sbyte>
    {
        public ESByte(sbyte value, AbstractEncryption encryptionInstance = null)
            : base(value, encryptionInstance) { }

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
