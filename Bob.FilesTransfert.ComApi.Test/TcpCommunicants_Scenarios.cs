using Bob.FilesTransfert.ComApi.Communicants.TCP;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.NUnit2;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

[assembly: LightBddScopeAttribute]
namespace Bob.FileTransfet.ComApi.Test
{
    [TestFixture]
    [FeatureDescription(@"Send messages between two communicants 'receiver' and 'sender'")]
    public partial class TcpCommunicants_Features
    {

        [Scenario]
        [Sequential]
        [Label("send/receive filename packet ")]
        public void Handle_communication_with_filename_packet()
        {
            Action<Object, PacketContext> act = (o, e) => this.OnReceivedPacket(o, e);
            Runner.RunScenario(
                given => this.Set_couple(8177, 8178, act),
                then => this.Send_filename_packet(),
                then => this.Sleep_200ms(),
                and => this.Check_filename_packet(),
                and => this.Close_couple(),
                and => this.Unsubscrible(act)
            );
        }
    }
}
