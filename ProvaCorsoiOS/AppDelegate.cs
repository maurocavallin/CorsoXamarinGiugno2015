using Foundation;
using UIKit;
using EsempioProgrammaConForms;

namespace ProvaCorsoiOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate // superclass new in 1.3

	{
		// class-level declarations
		/*
		public override UIWindow Window {
			get;
			set;
		}*/

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());  // method is new in 1.3


			return base.FinishedLaunching (application, launchOptions);
		}
		 
	}
}


