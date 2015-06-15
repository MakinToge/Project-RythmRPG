using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

namespace AudioFileData
{
    class Effectstream : WaveStream // not definitive name
    {
        public WaveStream SourceStream
        {
            get;
            set;
        }

        public Effectstream(WaveStream stream)
        {
            this.SourceStream = stream;
        }

        //must override
        public override long Length
        {
            get { return SourceStream.Length; }
        }

        //must override
        public override long Position
        {
            get
            {
                return SourceStream.Position;
            }
            set
            {
                SourceStream.Position = value;
            }
        }
        
        public override WaveFormat WaveFormat
        {
            get { return SourceStream.WaveFormat; }
        }

        //must override
        public override int Read(byte[] buffer, int offset, int count)
        {
            Console.WriteLine("DirectSoundOut requested {0} bytes", count);
            int read = SourceStream.Read(buffer,offset,count);

            //read = how many bytes we read from the stream, and there are 4 bytes by sample, so read / 4 = number of samples we are reading
            for (int i = 0; i < read / 4; i++)
            {
                float sample = BitConverter.ToSingle(buffer, i * 4);  // i * 4 = 4 bytes by floating point number for each sample  !!!!!!!!!!!!!!!!!! THAT'S THE THINGS

                sample = sample * 0.5f; // effect, reduce volume

                byte[] bytes = BitConverter.GetBytes(sample);

                //bytes.CopyTo(buffer,i * 4); // volume is indeed divide by 2, but this method is too slow
                buffer[i * 4] = bytes[0];
                buffer[i * 4 + 1] = bytes[1];
                buffer[i * 4 + 2] = bytes[2];
                buffer[i * 4 + 3] = bytes[3];
                // 4 times because 4 bytes
            }

            return read;
        }
    }
}
