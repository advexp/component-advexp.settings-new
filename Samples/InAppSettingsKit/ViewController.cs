using System;
using Foundation;
using UIKit;

using InAppSettingsKit;
using CoreGraphics;

namespace InAppSettingsKitSample
{
    partial class ViewController : UITableViewController, ISettingsDelegate
    {
        private AppSettingsViewController appSettingsViewController;

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public AppSettingsViewController AppSettingsViewController
        {
            get
            {
                if (appSettingsViewController == null)
                {
                    appSettingsViewController = new AppSettingsViewController();
                    appSettingsViewController.Delegate = this;

                    // Uncomment to not display InAppSettingsKit credits for creators:
                    // But we encourage you no to uncomment. Thank you!
                    //appSettingsViewController.ShowCreditsFooter == false;
                }
                return appSettingsViewController;
            }
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);

            switch (indexPath.Row)
            {
                case 0: // show InAppSettings
                    AppSettingsViewController.ShowDoneButton = false;
                    AppSettingsViewController.NavigationItem.RightBarButtonItem = null;
                    NavigationController.PushViewController(AppSettingsViewController, true);
                    break;

                case 1: // show Advexp.Settings
                    var advexpController = new AdvexpSettingsViewController();
                    NavigationController.PushViewController(advexpController, true);
                    break;
            }
        }

        [Export("tableView:heightForSpecifier:")]
        public virtual nfloat GetHeightForSpecifier(UITableView tableView, SettingsSpecifier specifier)
        {
            if (specifier.Key == "customCell")
            {
                return 44 * 3;
            }
            return 0;
        }

        [Export("tableView:cellForSpecifier:")]
        public virtual UITableViewCell GetCellForSpecifier(UITableView tableView, SettingsSpecifier specifier)
        {
            var cell = (CustomViewCell)tableView.DequeueReusableCell(specifier.Key);

            if (cell == null)
            {
                cell = NSBundle.MainBundle.LoadNib("CustomViewCell", this, null).GetItem<CustomViewCell>(0);
                cell.TextView.Changed += delegate
                {
                    NSUserDefaults.StandardUserDefaults.SetString(cell.TextView.Text, "customCell");
                    NSNotificationCenter.DefaultCenter.PostNotificationName(SettingsStore.AppSettingChangedNotification, (NSString)"customCell");
                };
            }
            var text = NSUserDefaults.StandardUserDefaults.StringForKey(specifier.Key) ?? specifier.DefaultStringValue;
            cell.TextView.Text = text;
            cell.SetNeedsLayout();

            return cell;
        }

        // Settings delegate

        public void SettingsViewControllerDidEnd(AppSettingsViewController sender)
        {
            DismissViewController(true, null);

            // your code here to reconfigure the app for changed settings (modal only)
        }
    }
}
