using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldProtection.Encryptions;

namespace FieldProtection.Abstract
{
    public abstract class AbstractModulableField<T> : AbstractField<T>
    {
        protected delegate bool fieldInteractCallback(ref T value, ref bool doInteraction);

        private List<fieldInteractCallback> setCallbacks;
        private List<fieldInteractCallback> getCallbacks;

        public override T value {
            get
            {
                T output = base.value;
                bool doInteraction = true;
                this.executeCallbacks(this.getCallbacks, ref output, ref doInteraction);
                if(doInteraction)
                {
                    return output;
                }
                return this.getDefaultValue();
            }
            set
            {
                bool doInteraction = true;
                this.executeCallbacks(this.setCallbacks, ref value, ref doInteraction);
                if(doInteraction)
                {
                    base.value = value;
                }
            }
        }

        public AbstractModulableField(T value, AbstractEncryption encryptionInstance = null)
            :base(value, encryptionInstance)
        {
            this.setCallbacks = new List<fieldInteractCallback>();
            this.getCallbacks = new List<fieldInteractCallback>();
        }

        protected virtual T getDefaultValue() { return default(T); }

        #region Callbacks
        protected void registerSetCallback(fieldInteractCallback callback) { this.setCallbacks.Add(callback); }
        protected void registerGetCallback(fieldInteractCallback callback) { this.getCallbacks.Add(callback); }

        protected void clearSetCallbacks(fieldInteractCallback callback) { this.setCallbacks.Clear(); }
        protected void clearGetCallbacks(fieldInteractCallback callback) { this.getCallbacks.Clear(); }

        protected void executeCallbacks(List<fieldInteractCallback> callbacks, ref T value, ref bool doInteraction)
        {
            if(callbacks != null && callbacks.Count > 0)
            {
                foreach(fieldInteractCallback callback in callbacks.ToList())
                {
                    if( !callback(ref value, ref doInteraction) ) { break; }
                }
            }
        }
        #endregion
    }
}
