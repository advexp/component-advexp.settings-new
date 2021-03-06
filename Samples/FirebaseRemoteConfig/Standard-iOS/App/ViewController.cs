﻿using System;
using UIKit;
using Advexp.FirebaseRemoteConfig.Plugin;
using Sample.FirebaseRemoteConfig.Assembly.Standard;
using Foundation;

namespace Sample.FirebaseRemoteConfig.App.iOS
{
    public partial class ViewController : UIViewController
    {
        //------------------------------------------------------------------------------
        protected ViewController(IntPtr handle) : base(handle)
        {
            AssemblyClass.SetCompletionHandler(CompletionHandler);
        }

        //------------------------------------------------------------------------------
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //UpdateUI();
        }

        //------------------------------------------------------------------------------
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        //------------------------------------------------------------------------------
        void CompletionHandler(FetchStatus status)
        {
            switch (status)
            {
                case FetchStatus.Success:
                    UpdateUI();
                    break;
                case FetchStatus.Throttled:
                    break;
                case FetchStatus.NoFetchYet:
                    break;
                case FetchStatus.Failure:
                    break;
            }

            UpdateStatusText(status);
        }

        //------------------------------------------------------------------------------
        partial void UIButton44Ua86gi_TouchUpInside(UIButton sender)
        {
            m_FetchStatus.Text = "Fetching...";

            AssemblyClass.Fetch();
        }

        //------------------------------------------------------------------------------
        void UpdateUI()
        {
            InvokeOnMainThread(() =>
            {
                AssemblyClass.LoadSettings();

                m_Label.Text = AssemblyClass.GetStringValue();
                m_CustomObject.Text = AssemblyClass.GetCustomObjectValue();
            });
        }

        //------------------------------------------------------------------------------
        void UpdateStatusText(FetchStatus status)
        {
            InvokeOnMainThread(() =>
            {
                switch (status)
                {
                    case FetchStatus.Success:
                        m_FetchStatus.Text = "Fetch status - Success";
                        break;
                    case FetchStatus.Throttled:
                        m_FetchStatus.Text = "Fetch status - Throttled";
                        break;
                    case FetchStatus.NoFetchYet:
                        m_FetchStatus.Text = "Status - NoFetchYet";
                        break;
                    case FetchStatus.Failure:
                        m_FetchStatus.Text = "Fetch status - Failure";
                        break;
                    default:
                        m_FetchStatus.Text = "Fetch status - Unknown status code";
                        break;
                }
            });
        }
    }
}
