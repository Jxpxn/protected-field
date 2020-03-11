using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Encryptions;
using FieldProtection.Utils;
using FieldProtection;

namespace FieldProtection.Abstract
{
    public abstract class AbstractProtectedField<T> : AbstractModulableField<T>
    {
        #region Static
        protected static bool arrayIsEqual(byte[] a, byte[] b)
        {
            bool output = false;
            if (a.Length == b.Length)
            {
                output = true;
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i])
                    {
                        output = false;
                        break;
                    }
                }
            }
            return output;
        }
        #endregion

        private byte[] protectionBuffer;
        private bool _detected;
        private AbstractEncryption _protectionEncryptionInstance;
        private List<Callbacks.illegalModificationCallback<T>> illegalModificationCallbacks;

        protected AbstractEncryption protectionEncryptionInstance { get { return this._protectionEncryptionInstance; } }

        public bool detected { get { return this._detected; } }

        protected virtual T protectionValue
        {
            get
            {
                return this.getValue( this.getDecryptedProtectionBuffer() );
            }
            set
            {
                this.setProtectionValue( value );
            }
        }

        public AbstractProtectedField(T value, AbstractEncryption encryptionInstance = null,
            AbstractEncryption protectionEncryptionInstance = null)
            : base(value, encryptionInstance)
        {
            this.illegalModificationCallbacks = new List<Callbacks.illegalModificationCallback<T>>();
            this._detected = false;

            this._protectionEncryptionInstance = protectionEncryptionInstance ??
                AbstractEncryption.get(Config.defaultProtectionEncryption)
            ;

            if(encryptionInstance == null)
            {
                this.encryptionInstance = AbstractEncryption.get(Config.defaultProtectionValueEncryption);
            }

            this.registerSetCallback( this.onFieldSet );
            this.registerGetCallback( this.onFieldGet );

            if(Config.defaultIllegalModificationCallback != null)
            {
                this.registerIllegalModificationCallback(Config.defaultIllegalModificationCallback);
            }

            this.setProtectionValue( value );
        }

        private bool onFieldSet(ref T value, ref bool doInteraction)
        {
            this.setProtectionValue(value);
            return true;
        }

        private bool onFieldGet(ref T value, ref bool doInteraction)
        {
            byte[] cleanedProtectionBuffer = this.getDecryptedProtectionBuffer();
            if(!arrayIsEqual(this.getBytes(value), cleanedProtectionBuffer))
            {
                this._detected = true;

                bool patchValue = true;
                T rightValue = this.getValue(cleanedProtectionBuffer);

                this.executeIllegalModificationCallbacks(ref value, ref rightValue, ref patchValue);

                if(patchValue) { value = rightValue; }
            }

            this.value = value;
            return true;
        }

        private void setProtectionValue( T value )
        {
            this.protectionBuffer = this.protectionEncryptionInstance.encrypt(this.getBytes(value));
        }

        private byte[] getDecryptedProtectionBuffer() { return this.protectionEncryptionInstance.decrypt(this.protectionBuffer); }

        #region Callbacks
        public void registerIllegalModificationCallback(Callbacks.illegalModificationCallback<T> callback)
        {
            this.illegalModificationCallbacks.Add(callback);
        }

        public void registerIllegalModificationCallback(Callbacks.basicIllegalModificationCallback callback)
        {
            this.registerIllegalModificationCallback(
                delegate (AbstractProtectedField<T> target, ref T value, ref T rightValue, ref bool patchValue)
                {
                    return callback(ref patchValue);
                }
            );
        }

        public void clearIllegalModificationCallbacks() { this.illegalModificationCallbacks.Clear(); }

        private void executeIllegalModificationCallbacks(ref T value, ref T rightValue, ref bool patchValue)
        {
            if (this.illegalModificationCallbacks.Count > 0)
            {
                foreach (Callbacks.illegalModificationCallback<T> callback in this.illegalModificationCallbacks.ToList())
                {
                    if (!callback(this, ref value, ref rightValue, ref patchValue)) { break; }
                }
            }
        }
        #endregion
    }
}
