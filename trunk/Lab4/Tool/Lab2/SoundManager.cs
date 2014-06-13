using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NAudio.Wave;
using System.IO;

namespace MTT
{
    public class SoundManager
    {        
        public static byte[] TrimWavByteArray(string inPath, TimeSpan cutFromStart, TimeSpan cutFromEnd)
        {
            byte[] buffer;
            using (WaveFileReader reader = new WaveFileReader(inPath))
            {
                int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;

                int startPos = (int)cutFromStart.TotalMilliseconds * bytesPerMillisecond;
                startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

                int endBytes = (int)cutFromEnd.TotalMilliseconds * bytesPerMillisecond;
                endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
                int endPos = endBytes;//(int)reader.Length - endBytes;

                reader.Position = startPos;
                // byte[] buffer = new byte[1024];
                while (reader.Position < endPos)
                {
                    int bytesRequired = (int)(endPos - reader.Position);
                    if (bytesRequired > 0)
                    {
                        // int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                        buffer = new byte[bytesRequired];
                        int bytesRead = reader.Read(buffer, 0, bytesRequired);
                        if (bytesRead > 0)
                        {
                            return buffer;
                        }
                    }
                }
                return new byte[1];
            }
        }

        public static bool PlayWavFile(string soundFile)
        {
            if (!File.Exists(soundFile))
                return false;

            using (var wfr = new WaveFileReader(soundFile))
            using (WaveChannel32 wc = new WaveChannel32(wfr) { PadWithZeroes = false })
            using (var audioOutput = new DirectSoundOut())
            {
                audioOutput.Init(wc);
                audioOutput.Play();
                while (audioOutput.PlaybackState != PlaybackState.Stopped)
                {
                    Thread.Sleep(50);
                }
                audioOutput.Stop();
            }
            return true;
        }

        public static byte[] Merge(byte[] buffer, byte[] add)
        {
            var length = buffer.Length + add.Length;
            var merge = new byte[length];
            System.Buffer.BlockCopy(buffer, 0, merge, 0, buffer.Length);
            System.Buffer.BlockCopy(add, 0, merge, buffer.Length, add.Length);
            return merge;
        }
    }
}
