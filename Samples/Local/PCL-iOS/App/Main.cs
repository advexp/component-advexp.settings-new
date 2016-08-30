using UIKit;
using Sample.Assembly.PCL;

namespace Sample.App.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            Advexp.SettingsModuleInitializer.Initialize();

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
