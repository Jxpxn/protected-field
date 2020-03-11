using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using FieldProtection.Abstract;

namespace FieldProtection.Fields.Encrypted
{
    public class EStruct<T> : AbstractField<T>
    {
        public EStruct(T value, AbstractEncryption encryptionInstance = null)
            : base(value, encryptionInstance) 
        { }

        protected override byte[] getBytes(T value)
        {
            int sizeOfT = Marshal.SizeOf(typeof(T));
            byte[] output = new byte[sizeOfT];
            IntPtr address = Marshal.AllocHGlobal(sizeOfT);
            Marshal.StructureToPtr<T>(value, address, false);
            Marshal.Copy(address, output, 0, sizeOfT);
            Marshal.FreeHGlobal(address);
            return output;
        }

        protected override T getValue(byte[] buffer)
        {
            int sizeOfT = Marshal.SizeOf(typeof(T));
            IntPtr address = Marshal.AllocHGlobal(sizeOfT);
            Marshal.Copy(buffer, 0, address, sizeOfT);
            T out_ = Marshal.PtrToStructure<T>(address);
            Marshal.FreeHGlobal(address);
            return out_;
        }
    }
}
