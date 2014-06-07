using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NAudio.Wave;

namespace MTT
{
    public class SoundManager
    {
        public static void TrimWavFile(string inPath, string outPath, TimeSpan cutFromStart, TimeSpan cutFromEnd)
        {
            using (WaveFileReader reader = new WaveFileReader(inPath))
            {
                using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
                {
                    int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;

                    int startPos = (int)cutFromStart.TotalMilliseconds * bytesPerMillisecond;
                    startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

                    int endBytes = (int)cutFromEnd.TotalMilliseconds * bytesPerMillisecond;
                    endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
                    int endPos = (int)reader.Length - endBytes;

                    TrimWavFile(reader, writer, startPos, endPos);
                }
            }
        }

        private static void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            reader.Position = startPos;
            byte[] buffer = new byte[1024];
            while (reader.Position < endPos)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.WriteData(buffer, 0, bytesRead);
                    }
                }
            }
        }

        public byte[] TrimWavByteArray(string inPath, TimeSpan cutFromStart, TimeSpan cutFromEnd)
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

        public void play_Wav(string soundFile)
        {
            using (var wfr = new WaveFileReader(soundFile))
            using (WaveChannel32 wc = new WaveChannel32(wfr) { PadWithZeroes = false })
            using (var audioOutput = new DirectSoundOut())
            {
                audioOutput.Init(wc);
                audioOutput.Play();
                while (audioOutput.PlaybackState != PlaybackState.Stopped)
                {
                    Thread.Sleep(20);
                }
                audioOutput.Stop();
            }
        }
    }
}
