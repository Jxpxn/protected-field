using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class EFloat : AbstractField<float>
    {
        public EFloat(float value, AbstractEncryption encryptionInstance = null)
            : base(value, encryptionInstance) { }

        protected override byte[] getBytes(float value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override float getValue(byte[] buffer)
        {
            return BitConverter.ToSingle(buffer, 0);
        }
    }
}
