﻿using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using MobileApp.iOS.Framework.Views;
using MobileApp.iOS.Glue;
using MobileApp.Views.Home;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;

namespace MobileApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        private iOSViewHost _host;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();

            ProjectGlue.Init();

            _host = ProjectGlue.Container.Resolve<iOSViewHost>();
            _host.Init<HomeViewModel>();

            return true;
        }
    }
}
