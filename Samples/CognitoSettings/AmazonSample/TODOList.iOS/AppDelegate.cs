﻿using Foundation;
using UIKit;
using Facebook.CoreKit;

namespace TODOList.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        public override UIWindow Window
        {
            get;
            set;
        }

        UINavigationController navigationController;
        UIViewController viewController;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // create a new window instance based on the screen size
            Profile.EnableUpdatesOnAccessTokenChange(true);
            Settings.AppId = TODOListPortableLibrary.Constants.FacebookAppId;
            Settings.DisplayName = TODOListPortableLibrary.Constants.FacebookAppName;

            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            viewController = new LoginViewController();
            navigationController = new UINavigationController(viewController);

            Window.RootViewController = navigationController;
            Window.MakeKeyAndVisible();

            return ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }

        internal void UpdateRootViewController(UIViewController vc)
        {
            viewController = vc;
            navigationController = new UINavigationController(viewController);
            Window.RootViewController = navigationController;
        }

    }
}