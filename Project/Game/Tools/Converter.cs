using System;
using System.Buffers.Binary;
using System.Text;

namespace Project
{
    public class Converter
    {
        private static StringBuilder stringBuilder = new StringBuilder();
        public static byte[] StringToBytes(string message)
            => Encoding.Unicode.GetBytes(message);
        public static string BytesToString(byte[] data, int bytes)
        {
            stringBuilder.Clear();
            return stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes)).ToString() + " ";
        }
        public static byte[] IntToBytes(int num)
            => BitConverter.GetBytes(num);
        public static int ByteToInt(byte[] arr)
            => BinaryPrimitives.ReadInt32LittleEndian(arr);
    }
}
