using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class EDouble : AbstractField<double>
    {
        public EDouble(double value, AbstractEncryption encryptionInstance = null)
            : base(value, encryptionInstance) { }

        protected override byte[] getBytes(double value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override double getValue(byte[] buffer)
        {
            return BitConverter.ToDouble(buffer, 0);
        }
    }
}
