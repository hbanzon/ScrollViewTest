using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

namespace ScrollViewTest
{
	public partial class ViewController : UIViewController
	{

		List<string> colors = new List<string>() { "blue", "red", "yellow" };
		int currentColorPage = 0;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			GenerateScrollingHeader();
		}

		UIColor GetColor(string name)
		{
			if (name == "blue")
			{
				return UIColor.Blue;
			}
			else if (name == "red")
			{
				return UIColor.Red;
			}
			else if (name == "yellow")
			{
				return UIColor.Yellow;
			}
			return UIColor.DarkGray;
		}


		UIView BuildDoorHeader(string color, string name, int index)
		{
			nfloat containerHeight = this.HeaderContainer.Frame.Height;
			nfloat containerWidth = this.HeaderContainer.Frame.Width;

			var doorHeaderX = (index * containerWidth) + (this.DoorHeaderView.Frame.X / 2);
			var doorHeaderY = this.DoorHeaderView.Frame.Y;
			var doorHeader = new UIView(
				new CGRect(doorHeaderX, doorHeaderY, 
				           this.DoorHeaderView.Frame.Width, this.DoorHeaderView.Frame.Height));
			doorHeader.BackgroundColor = GetColor(colors[index]);

			/*
			var labelX = (index * containerWidth);
			var labelY = this.DoorName.Frame.Y;
			var label1 = new UILabel(
				new CGRect(labelX, 0, containerWidth, containerHeight));
			label1.Text = colors[index];
			label1.TextAlignment = UITextAlignment.Center;
			*/

			//doorHeaderView.AddSubview(label1);
			return doorHeader;
		}

		void GenerateScrollingHeader()
		{
			nfloat containerHeight = this.HeaderContainer.Frame.Height;
			nfloat containerWidth = this.HeaderContainer.Frame.Width;

			var scrollView = new UIScrollView(new CGRect(
				this.HeaderContainer.Frame.X, this.HeaderContainer.Frame.Y, 
				containerWidth, containerHeight));
			scrollView.ContentSize = new CGSize(scrollView.Frame.Width * colors.Count, scrollView.Frame.Height);
			scrollView.PagingEnabled = true;
			scrollView.ScrollEnabled = true;
			scrollView.DirectionalLockEnabled = false;
			scrollView.ScrollsToTop = false;
			scrollView.ShowsHorizontalScrollIndicator = false;

			//var colorLabels = new UILabel[colors.Count];
			var doorHeaders = new UIView[colors.Count];
			for (int i = 0; i < colors.Count; i++)
			{
				var doorHeader = BuildDoorHeader(colors[i], "Door " + i, i);
				doorHeaders[i] = doorHeader;
			}

			Action updatedPage = () =>
			{
				System.Diagnostics.Debug.WriteLine("scrolled");
				var alertController = UIAlertController.Create("Swiped", "You Swiped", UIAlertControllerStyle.Alert);
				alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (action) => Console.WriteLine("OK Clicked.")));
				PresentViewController(alertController, true, null);
			};


			// Move to current color
			scrollView.ContentOffset = new CGPoint(
				currentColorPage * scrollView.Frame.Size.Width,
				scrollView.ContentOffset.Y
			);

			// Adjust background parallax image

			//nfloat xInit = scrollView.ContentOffset.X;
			//CGRect imageFrameInit = backgroundImageView.Frame;
			//imageFrameInit.X = -xInit / parallaxMultiplier;
			//backgroundImageView.Frame = imageFrameInit;

			// Hook up scrolling to changing pages
			scrollView.Scrolled += (sender, e) =>
			{
				System.Diagnostics.Debug.WriteLine("scrolled");
				nfloat x = scrollView.ContentOffset.X;
				//CGRect imageFrame = backgroundImageView.Frame;
				//imageFrame.X = -x / parallaxMultiplier;

				//backgroundImageView.Frame = imageFrame;

				int currentPage = currentColorPage;
				currentColorPage = (int)Math.Floor(scrollView.ContentOffset.X / scrollView.Frame.Size.Width);
				if (currentPage != currentColorPage)
				{
					// TODO perform an action updatedPage();
				}
			};

			scrollView.AddSubviews(doorHeaders);

			while (HeaderContainer.Subviews.Length > 0)
			{
				HeaderContainer.Subviews[0].RemoveFromSuperview();
			}
			HeaderContainer.AddSubview(scrollView);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

