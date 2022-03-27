using System;
using System.IO;
using System.Drawing;

namespace D2RImageManipulation
{
    public class Sprite
    {
        private byte[] bytes;

        public Sprite(byte[] bytes)
        {
            this.bytes = bytes;
        }

        private Sprite(String path)
        {
            if(!File.Exists(path))
            {
                throw new FileNotFoundException("", path);
            }
            bytes = File.ReadAllBytes(path);

            if(bytes == null)
            {
                throw new IOException(String.Format("Could not read bytes from {0}", path));
            }
        }

        public byte[] getBytes()
        {
            byte[] copy = new byte[this.bytes.Length];
            Array.Copy(this.bytes, copy, this.bytes.Length);
            return copy;
        }
    }
}
