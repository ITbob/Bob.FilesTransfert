using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bob.FilesTransfet.PacketHandler.Maker;
using NUnit.Framework;

namespace Bob.FilesTransfert.PacketMaker.Test
{
    [TestFixture]
    public class PacketMakerFeatures
    {

        [Test]
        public void Should_make_bob_a_filename_packet()
        {
            var header = new Header()
            {
                Content = 0xAA
            };

            var packets = PacketHelper.Frame(header, Encoding.ASCII.GetBytes("bob"));
            var result = PacketHelper.Content(header, packets);

            Assert.AreEqual("bob", Encoding.ASCII.GetString(result.ToArray()));
        }

        //
        //@"C:\Users\Freddy\Desktop\Sandbox\Destination\bestPractices.txt";

        [Test]
        public void Should_make_a_file_packet()
        {
            var header = new Header()
            {
                Content = 0xAA
            };
            
            //it's very bad, should be a file from the project
            var path = @"C:\Users\Freddy\Desktop\Sandbox\bertrand.jpg"; 
            var bytes = File.ReadAllBytes(path);

            var packets = PacketHelper.Frame(header, bytes);
            var result = PacketHelper.Content(header, packets);

            Assert.AreEqual(bytes, result);
        }
    }
}
