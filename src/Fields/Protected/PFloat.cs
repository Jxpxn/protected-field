using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Protected
{
    public class PFloat : AbstractProtectedField<float>
    {
        public PFloat(float value, AbstractEncryption encryptionInstance = null,
        AbstractEncryption protectionEncryptionInstance = null)
            : base(value, encryptionInstance, protectionEncryptionInstance) { }

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
