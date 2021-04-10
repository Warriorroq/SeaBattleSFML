using System;
using System.Buffers.Binary;
using System.Linq;
using System.Text;

namespace Project
{
    public class CommandConverter
    {
        private static StringBuilder stringBuilder = new StringBuilder();
        public static byte[] StringToBytes(string message)
            =>Concatenate(IntToBytes(1),Encoding.Unicode.GetBytes(message));
        public static byte[] Concatenate(byte[] first, byte[] second)
        {
            if (first == null)
                return second;
            
            if (second == null)
                return first;
            
            return first.Concat(second).ToArray();
        }
        public static byte[] ShotToBytes(int type, int[] arr)
        {
            byte[] bytes = IntToBytes(type);
            foreach (int num in arr)            
                bytes = Concatenate(bytes, IntToBytes(num));
            return bytes;
        }
        public static string BytesToString(byte[] data, int bytes)
        {
            stringBuilder.Clear();
            return stringBuilder.Append(Encoding.Unicode.GetString(data, 4, bytes - 4)).ToString() + " ";
        }
        public static byte[] RemoveBytes(int startIndex, int count, byte[] arr)
        {
            byte[] newArr = new byte[count];
            for (int i = startIndex; i < startIndex + count; i++)
                newArr[i - startIndex] = arr[i];
            return newArr;
        }
        public static int[] BytesToShot(byte[] data)
        {
            return new int[] {BytesToInt(RemoveBytes(4,4,data)), BytesToInt(RemoveBytes(8, 4, data))};
        }
        public static byte[] IntToBytes(int num)
            => BitConverter.GetBytes(num);
        public static int BytesToInt(byte[] arr)
            => BinaryPrimitives.ReadInt32LittleEndian(arr);
    }
}
