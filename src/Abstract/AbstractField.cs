using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Encryptions;
using FieldProtection.Fields;

namespace FieldProtection.Abstract
{
    public abstract class AbstractField<T>
    {
        private byte[] buffer;
        private AbstractEncryption _encryptionInstance;

        protected AbstractEncryption encryptionInstance { 
            get { return this._encryptionInstance; }
            set
            {
                T val = this.getValue(this.getDecryptedBuffer());
                this._encryptionInstance = value;
                this.setValue(val);
            }
        }

        public virtual T value { get { return this.getValue(this.getDecryptedBuffer()); } set { this.setValue(value); } }

        protected abstract T getValue(byte[] buffer);
        protected abstract byte[] getBytes(T value);

        private void setValue(T value)
        {
            this.buffer = this.encryptionInstance.encrypt( this.getBytes( value ) );
        }

        public AbstractField(T value, AbstractEncryption encryptionInstance = null)
        {
            this._encryptionInstance = encryptionInstance ?? AbstractEncryption.get(Config.defaultEncryption);

            this.setValue( value );
        }

        protected byte[] getDecryptedBuffer() { return this.encryptionInstance.decrypt( this.buffer ); }
    }
}
