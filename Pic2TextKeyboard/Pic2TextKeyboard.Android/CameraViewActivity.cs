using Android.App;
using Android.OS;

namespace Pic2TextKeyboard.Droid
{
    public class CameraViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CameraLayout);
        }
    }
}