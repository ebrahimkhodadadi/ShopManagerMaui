using Android.App;
using Android.Runtime;

namespace MauiApp.Platforms.Android
{
    [Application]
    public class MainApp : MauiApplication
    {
        public MainApp(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}