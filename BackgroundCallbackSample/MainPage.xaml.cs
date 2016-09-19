using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BackgroundCallbackSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        String taskName = "myBackground";
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var taskBuilder = new BackgroundTaskBuilder();
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == taskName|| task.Value.Name=="")
                {
                    task.Value.Unregister(false);
                }
            }
            taskBuilder.Name = "myBackground";
            taskBuilder.TaskEntryPoint = "MyBackground.Class1";
            taskBuilder.SetTrigger(new SystemTrigger(SystemTriggerType.TimeZoneChange,false));
           var registration= taskBuilder.Register();
            registration.Completed += Registration_Completed;
        }

        private async void Registration_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            var settings = ApplicationData.Current.LocalSettings;
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                tbResult.Text=settings.Values["BackgroundResult"].ToString();
            });
        }
    }
}
