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
        private static Type _nullEncryption;
        private static Type _defaultEncryption;
        private static Type _defaultProtectionEncryption;
        private static Type _defaultProtectionValueEncryption;

        public static Type nullEncryption { get { return _nullEncryption; } }

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


        public static Type defaultProtectionValueEncryption
        {
            get
            {
                return _defaultProtectionValueEncryption;
            }
            set
            {
                if (value == null) { value = nullEncryption; }

                if (AbstractEncryption.isEncryptionType(value))
                {
                    _defaultProtectionValueEncryption = value;
                }
            }
        }
        #endregion

        static Config()
        {
            defaultIllegalModificationCallback = null;
            _nullEncryption = typeof( UselessEncryption );
            defaultEncryption = typeof( XOREncryption );
            defaultProtectionEncryption = typeof( XOREncryption );
            defaultProtectionValueEncryption = typeof(XOREncryption);
        }
    }
}
