namespace CUETools.Codecs.FLAKE
{
    public class WAVWriterSettings : AudioEncoderSettings
    {
        public WAVWriterSettings()
            : this(null)
        {
        }

        public WAVWriterSettings(AudioPCMConfig pcm)
            : base(pcm)
        {
        }
    }
}
