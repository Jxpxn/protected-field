# Simple Protected Field

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

A simple field protection to prevent illegal memory write/read and client-side MySQL injections

  - Encrypted field
  - Protected field
  - Detection callback


You can also:
  - Create your own encrypted/protected field
  - Create your own abstract field type
  - Create your own encryption method

### Installation

Import the [library](https://github.com/Jxpxn/protected-field/releases) into your .NET Framework project.

And use the following namespaces :

```sh
using FieldProtection.Fields.Encrypted;
using FieldProtection.Fields.Protected;
```

### Type

This library contains these default types :

| Encrypted Class | Protected Class | Type | Length
| ------ | ------ | ------ | ------ |
| EByte | PByte | unsigned byte | 0x1
| EDouble | PDouble | double | 0x8
| EFloat | PFloat | float | 0x4
| EInt16 | PInt16 | signed short | 0x2
| EInt32 | PInt32 | signed int32 | 0x4
| EInt64 | PInt64 | signed int64 | 0x8
| EString | PString | string | ???
| EUInt16 | PUInt16 | unsigned short | 0x2
| EUInt32 | PUInt32 | unsigned int32 | 0x4
| EUInt64 | PUInt64 | unsigned int64 | 0x8


#### Variable Usage


#Signature: Constructor(T value, AbstractEncryption encryptionInstance = null);

Declare encrypted variable:
```sh
EInt32 myInt = new EInt32(100);
```
#Signature: Constructor(T value, AbstractEncryption encryptionInstance = null, AbstractEncryption protectionEncryptionInstance = null);

Declare protected variable:
```sh
PInt32 myInt = new PInt32(100);
```
#Signature: T value { get; set; }

Get value of variable:
```sh
Console.WriteLine(
    "The current value of myInt is : " + 
    myInt.value.ToString()
);
```

#Signature: T value { get; set; }

Set value of variable:
```sh
myInt.value = 666;
myInt.value++;
myInt.value--;
myInt.value += 10;
myInt.value *= 1;
myInt.value -= 10;
```

#Signature: bool detected { get; }

Check if a protected variable has been externally edited:
```sh
if(myInt.detected) {
    Console.WriteLine("Detected");
    return;
}
```

#### - Field's encryption method Usage

Namespace:
```sh
using FieldProtection.Encryptions;
```

Default encryption methods:
* XOREncryption
* UselessEncryption

#Signature: Constructor(T value, AbstractEncryption encryptionInstance = null);

Attach encryption method to encrypted field:
```sh
EInt32 myInt = new EInt32(
    100,
    new XOREncryption ()
);
```

#Signature: Constructor(T value, AbstractEncryption encryptionInstance = null);

Attach null encryption method to field:
```sh
EInt32 myInt = new EInt32(
    100,
    new UselessEncryption ()
);
```

#Signature: Constructor(T value, AbstractEncryption encryptionInstance = null, AbstractEncryption protectionEncryptionInstance = null);

Attach encryption method to protected field:
```sh
PInt32 myInt = new PInt32(
    100,
    new XOREncryption ()
);
```

#Signature: Constructor(T value, AbstractEncryption encryptionInstance = null, AbstractEncryption protectionEncryptionInstance = null);

Attach protection encryption method to protected field:
```sh
PInt32 myInt = new PInt32(
    100,
    null,
    new XOREncryption ()
);
```

#### - Protection Callbacks Usage

The protection callbacks will be called when the library detects a modification.

Namespace:
```sh
using FieldProtection.Utils;
```

IllegalModification Delegates:
```sh
delegate bool illegalModificationCallback<T>(ref T value, ref T rightValue, ref bool patchValue);
delegate bool basicIllegalModificationCallback(ref bool patchValue);
```

Create Typed Illegal Modification Callback:
```sh
public bool onQueryModification(ref string value, string rightValue, ref bool patchValue) {
    Console.WriteLine("You tried to edit the query ['" + rightValue + "'] to ['" + value + "']");
    Console.WriteLine("The query has been redefined to ['" + rightValue + "']");
}
```

Create Basic Illegal Modification Callback:
```sh
public bool onIllegalModification(ref bool patchValue) {
    patchValue = false;
    Console.WriteLine("A modification has been detected but the value wouldn't be redefined");
}
```

#Signature: public void registerIllegalModificationCallback(Callbacks.illegalModificationCallback<T> callback);
  
Attach typed illegalModificationCallback to a protected field:
```sh
PString query = new PString("SELECT * FROM users;");
query.registerIllegalModificationCallback( onQueryModification );
```

#Signature: public void registerIllegalModificationCallback(Callbacks.basicIllegalModificationCallback callback);

Attach basic illegalModificationCallback to a protected field:
```sh
PString query = new PString("SELECT * FROM users;");
query.registerIllegalModificationCallback( onIllegalModification );
```

#Signature: public void clearIllegalModificationCallbacks();

Clear attached illegalModificationCallbacks of a protected field:
```sh
query.clearIllegalModificationCallbacks();
```

#### - Library Config Usage
Namespace:
```sh
using FieldProtection;
```

Default Config:
- defaultEncryption = XOREncryption
- defaultProtectionEncryption = XOREncryption
- defaultIllegalModificationCallback = null

#Signature: Type defaultEncryption { get; set; }

Define default encrypted field encryption method:
```sh
Config.defaultEncryption = UselessEncryption;
```

#Signature: Type defaultProtectionEncryption { get; set; }

Define default protected field protection encryption method:
```sh
Config.defaultProtectionEncryption = UselessEncryption;
```

#Signature: Callbacks.basicIllegalModificationCallback defaultIllegalModificationCallback { get; set; }

Define base IllegalModificationCallback for protection fields:
```sh
Config.defaultIllegalModificationCallback = delegate(ref bool patchValue) {
    patchValue = false;
    Console.WriteLine("A modification has been detected but the value wouldn't be redefined");
};
```

#### Create Encryption Method
Namespace:
```sh
using FieldProtection.Abstract;
```

Example Class: 
```sh
public class MyCustomEncryption : AbstractEncryption {
    private static Random random;
    private byte key = 0x0;
    
    static MyCustomEncryption() {
        random = new Random();
    }

    /*
     * Add Byte to Byte
     * Example: 10 + 10 = 20, 250 + 5 = 255, 250 + 6 = 1
    */
    private static byte addByte(byte x, byte y) {
        ushort z = x + y;
        return z > 0xFF ? z - 0xFF : z;
    }
    
    /*
     * Substract Byte to Byte
     * Example: 30 - 10 = 20, 255 - 5 = 250, 10 - 11 = 254
    */
    private static byte subByte(byte x, byte y) {
        short z = x - y;
        return z < 0 ? 0xFF + z : z;
    }

    public override void refreshKey() { 
        // Randomize key between 1 and 254
        this.key = random.Next(0x1, 0xFE);
    }

    /*
     * Foreach byte in buffer -> byte += this.key
     */
    protected override byte[] abstractEncrypt(byte[] buffer) { 
        // Create new byteArray with param buffer Length of Length
        byte[] outBuffer = new byte[ buffer.Length ];
        // For each byte in param buffer
        for(int i = 0; i < buffer.Length; i++) {
            // Define outBuffer[i] to the encrypted byte
            outBuffer[i] = addByte(buffer[i], this.key);
        }
        return outBuffer;
    }
    
    /*
     * Foreach byte in buffer -> byte -= this.key
     */
    protected override byte[] abstractDecrypt(byte[] buffer) { 
        // Create new byteArray with param buffer Length of Length
        byte[] outBuffer = new byte[ buffer.Length ];
        // For each byte in param buffer
        for(int i = 0; i < buffer.Length; i++) {
            // Define outBuffer[i] to the unencrypted byte
            outBuffer[i] = subByte(buffer[i], this.key);
        }
        return outBuffer;
    }
}
```

Abstract methods:
- abstractEncrypt -> byte[] abstractEncrypt(byte[] buffer)

    -> This method is called at each encryption request and must return the encrypted buffer of the value injected in the buffer parameter.

- abstractDecrypt -> byte[] abstractDecrypt(byte[] buffer)

    -> This method is called at each decryption request and must return the decrypted buffer of the encrypted value injected in the buffer parameter.

- refreshKey -> void refreshKey()

    -> This method is called before each encryption request, this method is optional and can be ignored.

Schema of Get/Set:
- Set(value):

    -> refreshKey(), encryptedBuffer = abstractEncrypt( value )
- Get():

    -> return abstractDecrypt( encryptedBuffer )

#### Create Encrypted/Protected Type
Namespace:
```sh
using FieldProtection.Abstract;
```

Example Class: 
```sh
/*
 * Example Struct
 * Size: 0x20 | 32 bytes
 */
public struct MyStruct {
    double x;
    double y;
    double z;
    float yaw;
    float pitch;
}

public class EMyStruct : AbstractField<MyStruct> {
    private static readonly int size = 0x20;

    public EMyStruct(MyStruct value, AbstractEncryption encryptionInstance = null)
        : base(value, encryptionInstance) { }

    protected override byte[] getBytes(MyStruct value)
    {
        byte[] buffer = new byte[size];
        IntPtr pointer = Marshal.AllocHGlobal( size );
        
        Marshal.Copy(buffer, 0, pointer, size);
        
        return buffer;
    }

    protected override MyStruct getValue(byte[] buffer)
    {
        // Malloc (buffer.Length) bytes
        IntPtr pointer = Marshal.AllocHGlobal( buffer.Length );
        // Foreach byte in buffer
        for(int i = 0; i < buffer.Length; i++) {
            // Manually write (pointer + index) content to buffer[index]
            Marshal.WriteByte( pointer + i, buffer[i] );
        }
        // Return the conversion of [pointer to pointer + sizeOf(MyStruct)] to MyStruct
        return (MyStruct)Marshal.PtrToStruct( pointer, typeof(MyStruct) );
    }
}
```

Abstract methods:
- getBytes -> byte[] getBytes(T value)

    -> This method is called at each value set and must return the injected value converted in array of bytes.

- getValue -> T getValue(byte[] buffer)

    -> This method is called at each value get and must return the injected buffer converted in T type;

----


**Free Library ^^**
