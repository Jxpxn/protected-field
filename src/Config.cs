using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Abstract;
using FieldProtection.Encryptions;
using FieldProtection.Utils;

namespace FieldProtection
{
    public static class Config
    {
        public static Callbacks.basicIllegalModificationCallback
            defaultIllegalModificationCallback;

        #region defaultEncryptionsType
        private static Type nullEncryption;
        private static Type _defaultEncryption;
        private static Type _defaultProtectionEncryption;

        public static Type defaultEncryption
        {
            get
            {
                return _defaultEncryption;
            }
            set
            {
                if (value == null) { value = nullEncryption; }

                if (AbstractEncryption.isEncryptionType( value ))
                {
                    _defaultEncryption = value;
                }
            }
        }

        public static Type defaultProtectionEncryption
        {
            get
            {
                return _defaultProtectionEncryption;
            }
            set
            {
                if(value == null) { value = nullEncryption; }

                if(AbstractEncryption.isEncryptionType( value ))
                {
                    _defaultProtectionEncryption = value;
                }
            }
        }
        #endregion

        static Config()
        {
            defaultIllegalModificationCallback = null;
            nullEncryption = typeof( UselessEncryption );
            defaultEncryption = typeof( XOREncryption );
            defaultProtectionEncryption = typeof( XOREncryption );
        }
    }
}
