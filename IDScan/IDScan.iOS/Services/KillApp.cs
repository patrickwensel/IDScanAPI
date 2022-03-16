using System;
using System.Threading;
using IDScan.Interfaces;
using IDScan.iOS.Services;

[assembly: Xamarin.Forms.Dependency(typeof(KillApp))]
namespace IDScan.iOS.Services
{
    public class KillApp : IKillApp
    {
        public KillApp()
        {
        }

        public void closeApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}
