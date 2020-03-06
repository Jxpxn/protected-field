using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class EInt32 : AbstractField<int>
    {
        public EInt32(int value, AbstractEncryption encryptionInstance = null)
            :base(value, encryptionInstance) { }

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
