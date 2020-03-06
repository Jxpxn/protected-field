using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldProtection.Abstract;

namespace FieldProtection.Encryptions
{
    public class XOREncryption : AbstractEncryption
    {
        #region Static
        private static Random random;

        static XOREncryption()
        {
            random = new Random();
        }

        private static byte getRandomByte(byte max = 0xFF, byte min = 0x1)
        {
            if (min >= max) { return max; }
            return (byte)random.Next(min, max);
        }

        private static byte[] xor(byte[] buffer, byte xor)
        {
            byte[] output = new byte[buffer.Length];

            for (int i = 0; i < buffer.Length; i++)
            {
                output[i] = (byte)(buffer[i] ^ xor);
            }

            return output;
        }
        #endregion

        private byte[] keys;

        public XOREncryption()
            :base()
        {
            this.keys = new byte[2] { 0x0, 0x0 };
        }

        public override void refreshKey()
        {
            this.keys[0] = getRandomByte();
            this.keys[1] = getRandomByte(this.keys[0]);
        }

        protected override byte[] abstractEncrypt(byte[] buffer)
        {
            return xor( buffer, (byte)(this.keys[0] + this.keys[1]) );
        }

        protected override byte[] abstractDecrypt(byte[] buffer)
        {
            return this.abstractEncrypt( buffer );
        }
    }
}
