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
			System.Diagnostics.Debug.WriteLine("hello");
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

		void GenerateScrollingHeader()
		{
			nfloat containerHeight = this.HeaderContainer.Frame.Height;
			nfloat containerWidth = this.HeaderContainer.Frame.Width;

			var scrollView = new UIScrollView(new CGRect(
				this.HeaderContainer.Frame.X, this.HeaderContainer.Frame.Y, containerWidth, containerHeight));
			scrollView.ContentSize = new CGSize(scrollView.Frame.Width * colors.Count, scrollView.Frame.Height);
			scrollView.PagingEnabled = true;
			scrollView.ScrollEnabled = true;
			scrollView.DirectionalLockEnabled = false;
			scrollView.ScrollsToTop = false;
			scrollView.ShowsHorizontalScrollIndicator = false;

			var colorLabels = new UILabel[colors.Count];
			for (int i = 0; i < colors.Count; i++)
			{
				var labelX = (i * containerWidth);
				var labelY = this.DoorName.Frame.Y;
				var colorLabel = new UILabel(
					new CGRect(labelX, 0, containerWidth, containerHeight));
				colorLabel.Text = colors[i];
				colorLabel.TextColor = UIColor.White;
				colorLabel.TextAlignment = UITextAlignment.Center;
				colorLabel.BackgroundColor = GetColor(colors[i]);
				colorLabels[i] = colorLabel;
			}

			Action updatedPage = () =>
			{
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

			scrollView.AddSubviews(colorLabels);

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

