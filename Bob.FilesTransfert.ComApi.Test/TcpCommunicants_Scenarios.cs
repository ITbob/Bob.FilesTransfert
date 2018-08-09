using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.NUnit2;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [Label("send/receive basic message")]
        public void Handle_communication_with_simple_message()
        {
            Runner.RunScenario(
                given => this.Set_couple(8172,8173),
                then => this.Send_simple_message(),
                then => this.Sleep_200ms(),
                and => this.Receive_simple_message(),
                and => this.Close_couple()
            );
        }

        [Scenario]
        [Sequential]
        [Label("send/receive filename  packet ")]
        public void Handle_communication_with_filename_packet()
        {
            Runner.RunScenario(
                given => this.Set_couple(8177, 8178),
                then => this.Send_filename_packet(),
                then => this.Sleep_200ms(),
                and => this.Receive_filename_packet(),
                and => this.Close_couple()
            );
        }
    }
}
