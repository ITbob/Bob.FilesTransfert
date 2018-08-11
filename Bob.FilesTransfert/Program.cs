using Bob.Transferts;
using Bob.Transferts.Client;
using Bob.Transferts.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bob.FileTransfert
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Provider.Init();

            var view = new OptionForm();
            var option = new OptionFormController(view);

            Application.Run(view);
        }
    }
}
