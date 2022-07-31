using NAudio.Wave;

namespace Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = args.Length > 0 ? args[0] : string.Empty;
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Provide a path to a FLAC file as an argument");
                return;
            }
            if (!System.IO.File.Exists(path))
            {
                Console.WriteLine("Cannot open {0}, exiting.", path);
                return;
            }

            using var reader = new FlakeFileReader(path);
            Console.WriteLine($"Playing {path}, length: {reader.Length}");
            using var player = new WaveOutEvent();
            player.PlaybackStopped += Player_PlaybackStopped;
            player.Init(reader);
            player.Play();
            Console.WriteLine(@"Press any key to stop playback...");
            Console.ReadKey();
        }

        private static void Player_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            Console.WriteLine(@"Playback stopped. Exception?");
            Console.WriteLine(e.Exception);
            Console.ReadKey();
        }
    }
}