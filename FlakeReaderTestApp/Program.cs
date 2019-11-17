using System;
using Colladeo.FlakeNAudioAdapter;
using NAudio.Wave;

namespace FlakeReaderTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // const string file = @"C:\Users\Tomas\Music\Library\Juan D'Arienzo\Todo de Juan 1\9 de julio 93057_RP.flac";
            const string file = @"C:\Users\Tomas\Music\Library\José García\Nocturno de tango 1942-1943 (orq 211)\18 - Zorro plateado.flac";
            using (var reader = new FlakeFileReader(file))
            {
                Console.WriteLine($"Length: {reader.Length}");
                using (var player = new WaveOutEvent())
                {
                    // reader.Position = reader.Length/2 + 5;
                    player.PlaybackStopped += Player_PlaybackStopped;
                    player.Init(reader);
                    player.Play();
                    Console.WriteLine("Press any key to stop playback...");
                    Console.ReadKey();
                }
            }
        }

        private static void Player_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            Console.WriteLine("Playback stopped. Exception?");
            Console.WriteLine(e.Exception);
            Console.ReadKey();
        }
    }
}
