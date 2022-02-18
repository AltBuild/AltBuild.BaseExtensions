using System;
using System.Collections;
using System.Collections.Generic;

namespace AltBuild.BaseExtensions
{
    public static class StaticBasicType
    {
        public static int Get(Type type)
        {
            if (type.Equals(typeof(sbyte)))
                return sizeof(sbyte);

            else if (type.Equals(typeof(byte)))
                return sizeof(byte);

            else if (type.Equals(typeof(short)))
                return sizeof(short);

            else if (type.Equals(typeof(ushort)))
                return sizeof(ushort);

            else if (type.Equals(typeof(int)))
                return sizeof(int);

            else if (type.Equals(typeof(uint)))
                return sizeof(uint);

            else if (type.Equals(typeof(long)))
                return sizeof(long);

            else if (type.Equals(typeof(ulong)))
                return sizeof(ulong);

            else if (type.Equals(typeof(char)))
                return sizeof(char);

            else if (type.Equals(typeof(float)))
                return sizeof(float);

            else if (type.Equals(typeof(double)))
                return sizeof(double);

            else if (type.Equals(typeof(decimal)))
                return sizeof(decimal);

            else if (type.Equals(typeof(bool)))
                return sizeof(bool);

            return -1;
        }

        public static dynamic To(Type type, Span<byte> bytes)
        {
            if (type.Equals(typeof(sbyte)))
                return Convert.ToSByte(bytes[0]);

            else if (type.Equals(typeof(byte)))
                return Convert.ToByte(bytes[0]);

            else if (type.Equals(typeof(short)))
                return BitConverter.ToInt16(bytes);

            else if (type.Equals(typeof(ushort)))
                return BitConverter.ToUInt16(bytes);

            else if (type.Equals(typeof(int)))
                return BitConverter.ToInt32(bytes);

            else if (type.Equals(typeof(uint)))
                return BitConverter.ToUInt32(bytes);

            else if (type.Equals(typeof(long)))
                return BitConverter.ToInt64(bytes);

            else if (type.Equals(typeof(ulong)))
                return BitConverter.ToUInt64(bytes);

            else if (type.Equals(typeof(char)))
                return BitConverter.ToChar(bytes);

            else if (type.Equals(typeof(float)))
                return BitConverter.ToSingle(bytes);

            else if (type.Equals(typeof(double)))
                return BitConverter.ToDouble(bytes);

            else if (type.Equals(typeof(decimal)))
                return ToDecimal(bytes.ToArray());

            else if (type.Equals(typeof(bool)))
                return BitConverter.ToBoolean(bytes);

            throw new InvalidProgramException();
        }

        public static byte[] From(object value)
        {
            Type type = value.GetType();

            if (type.Equals(typeof(sbyte)))
                return BitConverter.GetBytes((sbyte)value);

            if (type.Equals(typeof(byte)))
                return BitConverter.GetBytes((byte)value);

            if (type.Equals(typeof(short)))
                return BitConverter.GetBytes((short)value);

            if (type.Equals(typeof(ushort)))
                return BitConverter.GetBytes((ushort)value);

            if (type.Equals(typeof(int)))
                return BitConverter.GetBytes((int)value);

            if (type.Equals(typeof(uint)))
                return BitConverter.GetBytes((uint)value);

            if (type.Equals(typeof(long)))
                return BitConverter.GetBytes((long)value);

            if (type.Equals(typeof(ulong)))
                return BitConverter.GetBytes((ulong)value);

            if (type.Equals(typeof(char)))
                return BitConverter.GetBytes((char)value);

            if (type.Equals(typeof(float)))
                return BitConverter.GetBytes((float)value);

            if (type.Equals(typeof(double)))
                return BitConverter.GetBytes((double)value);

            if (type.Equals(typeof(decimal)))
                return GetBytes((decimal)value);

            if (type.Equals(typeof(bool)))
                return BitConverter.GetBytes((bool)value);

            throw new InvalidProgramException();
        }

        /// <summary>
        /// Convert decimal to byte[].
        /// </summary>
        /// <param name="sourceDecimal"></param>
        /// <returns></returns>
        public static byte[] GetBytes(decimal sourceDecimal)
        {
            var results = new List<byte>();

            foreach (var i in decimal.GetBits(sourceDecimal))
                results.AddRange(BitConverter.GetBytes(i));

            return results.ToArray();
        }

        /// <summary>
        /// Convert byte[] to decimal.
        /// </summary>
        /// <param name="sourceBytes"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static decimal ToDecimal(byte[] sourceBytes)
        {
            if (sourceBytes.Length != 16)
                throw new InvalidOperationException("A decimal must be created from exactly 16 bytes.");

            Int32[] results = new Int32[4];

            for (int i = 0; i <= 15; i += 4)
                results[i / 4] = BitConverter.ToInt32(sourceBytes, i);

            return new Decimal(results);
        }
    }
}
