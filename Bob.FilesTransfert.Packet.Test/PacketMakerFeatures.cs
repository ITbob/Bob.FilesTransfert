using System;
using System.Collections.Generic;
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
            var fileNamePacketmaker = new FileNamePacketMaker();
            fileNamePacketmaker.SetContent("bob");
            var packet = fileNamePacketmaker.GetPacket();

            var result = new List<Byte>();

            if(packet.Any())
            {
                for (int i = 1; i < packet.Length; i++)
                {
                    result.Add(packet[i]);
                }
            }

            Assert.AreEqual("bob", Encoding.ASCII.GetString(result.ToArray()));
        }
    }
}
