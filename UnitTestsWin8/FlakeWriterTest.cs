using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUETools.Codecs;
using CUETools.Codecs.FLAKE;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage;

namespace UnitTestsWin8
{
    [TestClass]
    public class FlakeWriterTest
    {
        [TestMethod]
        public async Task TestWriting()
        {

            var wavStream =
                await
                (await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///test.wav")))
                    .OpenStreamForReadAsync();

            var expectedBytes =
                ReadFully(await
               (await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///flake.flac")))
                   .OpenStreamForReadAsync());


            var buff = WAVReader.ReadAllSamples(wavStream);
            FlakeWriter target;

            var outputStream = new MemoryStream();

            target = new FlakeWriter(outputStream, new FlakeWriterSettings { PCM = buff.PCM, EncoderMode = "7" });
            target.Settings.Padding = 1;
            target.DoSeekTable = false;
            target.Write(buff);
            target.Close();

            outputStream.Seek(0, SeekOrigin.Begin);

            var resultContent = outputStream.ToArray();
            var outStream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync("out.flac",
                                                                        CreationCollisionOption.ReplaceExisting);
            outStream.Write(resultContent, 0, resultContent.Length);
            outStream.Dispose();
            Debug.WriteLine(ApplicationData.Current.LocalFolder.Path);
            

            CollectionAssert.AreEqual(expectedBytes, outputStream.ToArray(), "result and expected doesn't match.");
        }

        public byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
