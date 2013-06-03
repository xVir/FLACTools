using System;
using System.IO;
using CUETools.Codecs;
using CUETools.Codecs.FLAKE;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class FlakeWriterTest
    {
        [TestMethod]
        public void TestWriting()
        {
            AudioBuffer buff = WAVReader.ReadAllSamples("test.wav", null);
            FlakeWriter target;

            target = new FlakeWriter("flakewriter0.flac", null, new FlakeWriterSettings { PCM = buff.PCM, EncoderMode = "7" });
            target.Settings.Padding = 1;
            target.DoSeekTable = false;
            //target.Vendor = "CUETools";
            //target.CreationTime = DateTime.Parse("15 Aug 1976");
            target.FinalSampleCount = buff.Length;
            target.Write(buff);
            target.Close();
            //CollectionAssert.AreEqual(File.ReadAllBytes("flake.flac"), File.ReadAllBytes("flakewriter0.flac"), "flakewriter0.flac doesn't match.");

            target = new FlakeWriter("flakewriter1.flac", null, new FlakeWriterSettings { PCM = buff.PCM, EncoderMode = "7" });
            target.Settings.Padding = 1;
            target.DoSeekTable = false;
            //target.Vendor = "CUETools";
            //target.CreationTime = DateTime.Parse("15 Aug 1976");
            target.Write(buff);
            target.Close();
            CollectionAssert.AreEqual(File.ReadAllBytes("flake.flac"), File.ReadAllBytes("flakewriter1.flac"), "flakewriter1.flac doesn't match.");
        }
    }
}
