using System;
using Android.App;
using IDScan.Droid.Services;
using IDScan.Interfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(KillApp))]
namespace IDScan.Droid.Services
{
    public class KillApp : IKillApp
    {
        public KillApp()
        {
        }

        public void closeApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}
