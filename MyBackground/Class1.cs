using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MyBackground
{
    public sealed class Class1 : IBackgroundTask
    {
        BackgroundTaskDeferral deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            Random rand = new Random();
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values["BackgroundResult"] = "The Current Result is " + rand.Next(100);
            deferral.Complete();
        }
    }
}
