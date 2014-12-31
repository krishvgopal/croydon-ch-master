using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CollectionHubDocumentService
{
    public partial class Service1 : ServiceBase
    {
        private Timer t = new Timer(Convert.ToInt32(ConfigurationManager.AppSettings["PollInterval"]));

        public Service1()
        {
            InitializeComponent();
            t.Elapsed += t_Elapsed;
        }
        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            t.Stop();

            // CONNECT TO DATABASE

            // FIND JOBS TO PROCESS

                // DOWNLOAD FILES

                // CREATE BATCH FILE

                // CALL BATCH FILE

                // UPDATE STATUS IN DATABASE

            t.Start();
        }
        protected override void OnStart(string[] args)
        {
            t.Start();
        }
        protected override void OnStop()
        {
            t.Stop();
        }
    }
}
