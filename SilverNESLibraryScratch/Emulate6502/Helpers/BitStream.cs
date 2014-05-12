using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.Helpers
{
    //only support writing currently
    public class BitStream
    {
        private Stream _underlying;
        private int _currentInt = 0;
        private int _currentBits = 0;

        private bool IsByteAligned
        {
            get
            {
                return _currentBits == 0;
            }
        }

        public BitStream(Stream baseStream)
        {
            _underlying = baseStream;
        }

        public BitStream(byte[] data)
        {
            _underlying = new MemoryStream(data);
        }

        public void ReadBytes(byte[] array, int position, int length)
        {
            if (array.Length < (position + length))
            {
                throw new ArgumentOutOfRangeException("Array not big enough for operation");
            }

            //can we do an optimized read ?
            if (IsByteAligned)
            {
                _underlying.Read(array, 0, length);
            }
            else
            {
                for (int j = 0; j < length; j++)
                {
                    array[j + position] = (byte)Read(8);
                }
            }
        }

        public int Read(int bits)
        {
            int val = 0;
            int bit = 0;
            int original = bits;

            while (bits-- > 0)
            {
                bit = ReadBit();

                if (bit != -1)
                {
                    val <<= 1;
                    val = val | bit;
                }
                else
                {
                    break;
                }
            }

            return (original == (bits-1)) ? -1 : val;
        }

        private int ReadBit()
        {
            if (_currentBits == 0 && _underlying.Position != _underlying.Length)
            {
                _currentInt = _underlying.ReadByte();
                _currentBits = 8;
            }
            else if (_currentBits == 0)
            {
                return -1;
            }

            int nextBit = _currentInt & 0xC0;

            _currentInt <<= 1;
            _currentBits--;

            return nextBit >> 7;
        }

        public void Write(int value, int bits)
        {
            int mask = 1 << (bits-1);
            int bit = (value & mask) >> (bits-1);

            while (bits-- > 0)
            {
                WriteBit(bit);
                mask >>= 1;
                bit = (value & mask) >> (bits-1);
            }
        }

        private void WriteBit(int bit)
        {
            _currentInt <<= 1;
            _currentInt |= bit;
            _currentBits++;

            if (_currentBits == 8)
            {
                _currentBits = 0;
                _underlying.WriteByte((byte)_currentInt);
                _currentInt = 0;
            }
        }
    }
}
