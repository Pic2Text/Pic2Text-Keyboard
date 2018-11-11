using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace Pic2TextKeyboard.Droid
{
    [Activity(Label = "CameraActivity")]
    public class CameraActivity : Activity
    {
        private ImageView _imageView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.CameraLayout);
            if (IsThereAnAppToTakePictures())
            {
                Button button = FindViewById<Button>(Resource.Id.myButton);
                _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
                TakeAPicture(null, new System.EventArgs());
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 100 && resultCode == Result.Ok)
            {
                if (data != null && data.Extras != null)
                {
                    Bitmap imageBitmap = (Bitmap)data.Extras.Get("data");
                    _imageView.SetImageBitmap(imageBitmap);
                }
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void TakeAPicture(object sender, System.EventArgs eventArgs)
        {
            try
            {
                StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
                StrictMode.SetVmPolicy(builder.Build());
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                if (intent.ResolveActivity(PackageManager) != null)
                {
                    StartActivityForResult(intent, 100);
                }
            }
            catch (System.Exception e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }
        }
    }
}