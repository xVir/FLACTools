using CUETools.Codecs.FLAKE;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    internal class FlakeReaderTest
    {
        private void ReadFile(FlakeReader reader)
        {
            var streamInfo = reader.PCM;
            var len = 65546 * streamInfo.BitsPerSample / 8 * streamInfo.ChannelCount;
            var audioBuffer = new AudioBuffer(streamInfo, len);
            int br = 0;
            do
            {
                br = reader.Read(audioBuffer, 4096);
            } while (br > 0);
        }

        [Test]
        public void ReadFlac8000()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.8000.flac", null);
            ReadFile(reader);
        }

        [Test]
        public void ReadFlac11025()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.11025.flac", null);
            ReadFile(reader);
        }

        [Test]
        public void ReadFlac16000()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.16000.flac", null);
            ReadFile(reader);
        }

        [Test]
        public void ReadFlac22050()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.22050.flac", null);
            ReadFile(reader);
        }

        [Test]
        public void ReadFlac24000()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.24000.flac", null);
            ReadFile(reader);
        }

        [Test]
        public void ReadFlac32000()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.32000.flac", null);
            ReadFile(reader);
        }

        [Test]
        public void ReadFlac44100()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.44100.flac", null);
            ReadFile(reader);
        }

        [Test]
        public void ReadFlac48000()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.48000.flac", null);
            ReadFile(reader);
        }

        [Test]
        public void ReadFlac88200()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.88200.flac", null);
            ReadFile(reader);
        }

        [Test]
        public void ReadFlac96000()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.96000.flac", null);
            ReadFile(reader);
        }

        [Test]
        public void ReadFlac176400()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.176400.flac", null);
            ReadFile(reader);
        }

        [Test]
        public void ReadFlac192000()
        {
            var reader = new FlakeReader(@".\data\mozo-guapo.192000.flac", null);
            ReadFile(reader);
        }

    }
}
