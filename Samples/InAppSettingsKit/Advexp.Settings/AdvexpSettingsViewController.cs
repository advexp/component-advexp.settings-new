using System;
using Foundation;
using MonoTouch.Dialog;
using UIKit;

namespace InAppSettingsKitSample
{
    class AdvexpSettingsViewController : DialogViewController
    {
        BooleanElement m_airplaneMode = null;
        BooleanElement m_notifications = null;

        FloatElementEx m_brightness = null;
        BooleanElement m_autoBrightness = null;

        BooleanElement m_bluetooth = null;

        BooleanElement m_locationService = null;

        RootElement m_autoLock = null;

        RootElement m_homeButtonDoubleClick = null;

        //------------------------------------------------------------------------------
        public AdvexpSettingsViewController() : base(null, true)
        {
            AdvexpSettings.LoadSettings();

            this.Root = CreateRoot();

            NSNotificationCenter.DefaultCenter.AddObserver(
                UIApplication.DidBecomeActiveNotification,
                (notify) =>
            {
                InvokeOnMainThread(() => 
                {
                    UpdateAdvexpSettings();
                });
            });
        }

        //------------------------------------------------------------------------------
        RootElement CreateRoot()
        {
            m_airplaneMode = new BooleanElement("Airplane Mode", AdvexpSettings.AirplaneMode);
            m_airplaneMode.ValueChanged += (object sender, EventArgs e) => 
            {
                AdvexpSettings.AirplaneMode = m_airplaneMode.Value;
                AdvexpSettings.SaveSetting(s => AdvexpSettings.AirplaneMode);
            };

            m_notifications = new BooleanElement("Notifications", AdvexpSettings.Notifications);
            m_notifications.ValueChanged += (object sender, EventArgs e) => 
            {
                AdvexpSettings.Notifications = m_notifications.Value;
                AdvexpSettings.SaveSetting(s => AdvexpSettings.Notifications);
            };

            m_brightness = new FloatElementEx(AdvexpSettings.Brightness, 
                                              lockable: false, 
                                              valueChanged: (val) => 
            {
                AdvexpSettings.Brightness = val;
                AdvexpSettings.SaveSetting(s => AdvexpSettings.Brightness);
            });

            m_autoBrightness = new BooleanElement("Auto-brightness", AdvexpSettings.AutoBrightness);
            m_autoBrightness.ValueChanged += (object sender, EventArgs e) => 
            {
                AdvexpSettings.AutoBrightness = m_autoBrightness.Value;
                AdvexpSettings.SaveSetting(s => AdvexpSettings.AutoBrightness);
            };

            return new RootElement("Advexp.Settings") {
                new Section() {
                    m_airplaneMode,
                    new RootElement("Notifications", 0, 0) {
                        new Section(null, "Turn off Notifications to disable Sounds " +
                                    "Alerts and Home Screen Badges for the applications below.") {
                            m_notifications
                        }
                    }
                },
                new Section() {
                    new RootElement("Brightness") {
                        new Section() {
                            m_brightness,
                            m_autoBrightness,
                        }
                    },
                },
                new Section() {
                    CreateGeneralSection(),
                }
            };
        }

        //------------------------------------------------------------------------------
        RootElement CreateGeneralSection()
        {
            m_bluetooth = new BooleanElement("Bluetooth", AdvexpSettings.Bluetooth);
            m_bluetooth.ValueChanged += (object sender, EventArgs e) => 
            {
                AdvexpSettings.Bluetooth = m_bluetooth.Value;
                AdvexpSettings.SaveSetting(s => AdvexpSettings.Bluetooth);
            };

            m_locationService = new BooleanElement("Location Services", AdvexpSettings.LocationServices);
            m_locationService.ValueChanged += (object sender, EventArgs e) => 
            {
                AdvexpSettings.LocationServices = m_locationService.Value;
                AdvexpSettings.SaveSetting(s => AdvexpSettings.LocationServices);
            };

            Action<RadioElementEx, EventArgs> saveAutoLockDelegate = (sender, e) => 
            {
                AdvexpSettings.AutoLock = (AutoLock)m_autoLock.RadioSelected;
                AdvexpSettings.SaveSetting(s => AdvexpSettings.AutoLock);
            };
            m_autoLock = new RootElement("Auto-Lock", new RadioGroup((int)AdvexpSettings.AutoLock)) {
                new Section() {
                    new RadioElementEx("Never", saveAutoLockDelegate),
                    new RadioElementEx("1 Minute", saveAutoLockDelegate),
                    new RadioElementEx("2 Minutes", saveAutoLockDelegate),
                    new RadioElementEx("3 Minutes", saveAutoLockDelegate),
                    new RadioElementEx("4 Minutes", saveAutoLockDelegate),
                    new RadioElementEx("5 Minutes", saveAutoLockDelegate),
                }
            };

            Action<RadioElementEx, EventArgs> saveHomeButtonDoubleClickDelegate = (sender, e) => 
            {
                AdvexpSettings.HomeButtonDoubleClick = (HomeButtonDoubleClick)m_homeButtonDoubleClick.RadioSelected;
                AdvexpSettings.SaveSetting(s => AdvexpSettings.HomeButtonDoubleClick);
            };
            m_homeButtonDoubleClick = new RootElement("Home Button", new RadioGroup((int)AdvexpSettings.HomeButtonDoubleClick)) {
                new Section("Double-click the Home Button for:") {
                    new RadioElementEx("Home", saveHomeButtonDoubleClickDelegate),
                    new RadioElementEx("Search", saveHomeButtonDoubleClickDelegate),
                    new RadioElementEx("Phone favorites", saveHomeButtonDoubleClickDelegate),
                    new RadioElementEx("Camera", saveHomeButtonDoubleClickDelegate),
                    new RadioElementEx("iPod", saveHomeButtonDoubleClickDelegate),
                }
            };

            return new RootElement("General") {
                new Section() {
                    new RootElement("Bluetooth", 0, 0) {
                        new Section() {
                            m_bluetooth
                        }
                    },
                    m_locationService,
                },
                new Section() {
                    m_autoLock,
                    m_homeButtonDoubleClick,
                },
            };
        }

        //------------------------------------------------------------------------------
        void UpdateAdvexpSettings()
        {
            AdvexpSettings.LoadSettings();

            m_airplaneMode.Value = AdvexpSettings.AirplaneMode;
            m_notifications.Value = AdvexpSettings.Notifications;

            m_brightness.SetValue(AdvexpSettings.Brightness);
            m_autoBrightness.Value = AdvexpSettings.AutoBrightness;

            m_bluetooth.Value = AdvexpSettings.Bluetooth;

            m_locationService.Value = AdvexpSettings.LocationServices;

            m_autoLock.RadioSelected = (int)AdvexpSettings.AutoLock;

            m_homeButtonDoubleClick.RadioSelected = (int)AdvexpSettings.HomeButtonDoubleClick;
        }
    }
}
