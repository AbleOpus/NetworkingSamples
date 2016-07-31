using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAsyncClient
{
    class PersonPackage
    {
        public bool IsMale { get; set; }
        public ushort Age { get; set; }
        public string Name { get; set; }

        public PersonPackage(bool male, ushort age, string name)
        {
            IsMale = male;
            Age = age;
            Name = name;
        }

        /// <summary>
        /// Deserialize data received.
        /// </summary>
        public PersonPackage(byte[] data)
        {
            IsMale = BitConverter.ToBoolean(data, 0);
            Age = BitConverter.ToUInt16(data, 1);
            int nameLength = BitConverter.ToInt32(data, 3);
            Name = Encoding.ASCII.GetString(data, 7, nameLength);
        }

        /// <summary>
        ///  Serializes this package to a byte array.
        /// </summary>
        /// <remarks>
        /// Use the <see cref="Buffer"/> or <see cref="Array"/> class for better performance.
        /// </remarks>
        public byte[] ToByteArray()
        {
            List<byte> byteList = new List<byte>();
            byteList.AddRange(BitConverter.GetBytes(IsMale));
            byteList.AddRange(BitConverter.GetBytes(Age));
            byteList.AddRange(BitConverter.GetBytes(Name.Length));
            byteList.AddRange(Encoding.ASCII.GetBytes(Name));
            return byteList.ToArray();
        }
    }
}
