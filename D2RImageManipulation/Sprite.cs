using System;
using System.IO;
using System.Drawing;

namespace D2RImageManipulation
{
    public class Sprite
    {
        private byte[] bytes;
        private const int HEIGHT_INDEX = 0xC;
        private const int HEIGHT_LENGTH = 4;
        private const int FRAME_COUNT_INDEX = 0x14;
        private const int FRAME_COUNT_LENGTH = 4;
        private const int VERSION_INDEX = 4;
        private const int VERSION_LENGTH = 2;
        private const int FRAME_WIDTH_INDEX = 6;
        private const int FRAME_WIDTH_LENGTH = 2;
        private const int WIDTH_INDEX = 8;
        private const int WIDTH_LENGTH = 4;
        private const int HEADER_SIZE = 0x28;

        public Sprite(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public Sprite()
        {
            bytes = new byte[HEADER_SIZE];
            Array.Copy(new byte[] { (byte)'S', (byte)'p', (byte)'A', (byte)'1' }, bytes, 4);
        }

        public Sprite(String path)
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

        public uint FrameCount
        {
            get
            {
                return BitConverter.ToUInt32(bytes, FRAME_COUNT_INDEX);
            }

            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, bytes, FRAME_COUNT_INDEX, FRAME_COUNT_LENGTH);
            }
        }

        public ushort Version
        {
            get
            {
                return BitConverter.ToUInt16(bytes, VERSION_INDEX);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, bytes, VERSION_INDEX, VERSION_LENGTH);
            }
        }

        public int Width
        {
            get
            {
                return BitConverter.ToInt32(bytes, WIDTH_INDEX);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, bytes, WIDTH_INDEX, WIDTH_LENGTH);
            }
        }

        public ushort FrameWidth
        {
            get
            {
                return BitConverter.ToUInt16(bytes, FRAME_WIDTH_INDEX);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, bytes, FRAME_WIDTH_INDEX, FRAME_WIDTH_LENGTH);
            }
        }

        public int Height
        {
            get
            {
                return BitConverter.ToInt32(bytes, HEIGHT_INDEX);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, bytes, HEIGHT_INDEX, HEIGHT_LENGTH);
            }
        }

        public byte[] Pixels
        {
            get
            {
                var pixels = new byte[bytes.Length - HEADER_SIZE];
                Array.Copy(bytes, HEADER_SIZE, pixels, 0, pixels.Length);
                return pixels;
            }
            set
            {
                var oldBytes = bytes;
                bytes = new byte[value.Length + HEADER_SIZE];
                Array.Copy(oldBytes, bytes, HEADER_SIZE);
                Array.Copy(value, 0, bytes, HEADER_SIZE, value.Length);
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
