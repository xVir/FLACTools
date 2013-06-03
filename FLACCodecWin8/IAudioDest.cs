namespace CUETools.Codecs
{
	public interface IAudioDest
	{
        AudioEncoderSettings Settings { get; }
        
        long FinalSampleCount { set; }

		void Write(AudioBuffer buffer);
		void Close();
		void Delete();
	}
}
