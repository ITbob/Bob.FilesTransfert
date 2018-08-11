using Bob.Transferts.Client;
using Bob.Transferts.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Transferts.Option
{
    public class OptionFormController
    {
        public OptionForm View { get; set; }
        private Int32 _clientCount = 0;
        public OptionFormController(OptionForm view)
        {
            this.View = view;
            this.View.clientButton.Click += Button1_Click;
            this.View.serverButton.Click += Button2_Click;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var view = new ServerForm();
            var controller = new ServerFormController(view);
            view.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _clientCount += 1;
            var view = new ClientForm();
            var controller = new ClientFormController(view, _clientCount);
            view.Show();
            
        }
    }
}
