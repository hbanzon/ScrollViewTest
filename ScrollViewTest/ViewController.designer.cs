// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ScrollViewTest
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel DoorName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView HeaderContainer { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DoorName != null) {
                DoorName.Dispose ();
                DoorName = null;
            }

            if (HeaderContainer != null) {
                HeaderContainer.Dispose ();
                HeaderContainer = null;
            }
        }
    }
}