using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldProtection.Abstract
{
    public abstract class AbstractEncryption
    {
        public static bool isEncryptionType(Type type)
        {
            return type == null || type.IsSubclassOf( typeof(AbstractEncryption) );
        }

        public static AbstractEncryption get(Type type)
        {
            if(isEncryptionType(type))
            {
                return (AbstractEncryption)Activator.CreateInstance(type);
            }
            return null;
        }

        public abstract void refreshKey();

        protected abstract byte[] abstractEncrypt(byte[] buffer);
        protected abstract byte[] abstractDecrypt(byte[] buffer);

        public byte[] encrypt(byte[] buffer)
        {
            this.refreshKey();
            return this.abstractEncrypt( buffer );
        }

        public byte[] decrypt(byte[] buffer) { return this.abstractDecrypt( buffer ); }
    }
}
