using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;

namespace FieldProtection.Encryptions
{
    public class UselessEncryption : AbstractEncryption
    {
        public override void refreshKey() { }

        protected override byte[] abstractEncrypt(byte[] buffer) { return buffer; }

        protected override byte[] abstractDecrypt(byte[] buffer) { return buffer; }
    }
}
