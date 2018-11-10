using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.InputMethodServices;
using Java.Lang;
using Android.Views.InputMethods;
using Android.Content;

namespace Pic2TextKeyboard.Droid
{
    [Service(Permission = "android.permission.BIND_INPUT_METHOD", Label = "EDMTKeyboard")]
    [MetaData("android.view.im", Resource = "@xml/method")]
    [IntentFilter(new string[] { "android.view.InputMethod" })]
    public class MainActivity : InputMethodService, KeyboardView.IOnKeyboardActionListener
    {
        private KeyboardView kv;
        private Keyboard keyboard;

        private bool isCaps = false;

        public override View OnCreateInputView()
        {
            kv = (KeyboardView)LayoutInflater.Inflate(Resource.Layout.Keyboard, null);
            keyboard = new Keyboard(this, Resource.Xml.Qwerty);
            kv.Keyboard = keyboard;
            kv.OnKeyboardActionListener = this;
            return kv;
        }

        public void OnKey([GeneratedEnum] Android.Views.Keycode primaryCode, [GeneratedEnum] Android.Views.Keycode[] keyCodes)
        {
            IInputConnection ic = CurrentInputConnection;

            if (ic == null)
                return;

            switch (primaryCode)
            {
                case Android.Views.Keycode.Del:
                    string selectedText = ic.GetSelectedText(GetTextFlags.None);

                    if (string.IsNullOrEmpty(selectedText))
                        ic.DeleteSurroundingText(1, 0);
                    else
                        ic.CommitText("", 1);

                    break;
                case Android.Views.Keycode.CapsLock:
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(CameraActivity));
                    intent.SetFlags(ActivityFlags.NewTask);
                    StartActivity(intent);
                    break;
                default:
                    char code = (char)primaryCode;
                    ic.CommitText(code.ToString(), 1);
                    break;
            }
        }

        public void OnPress([GeneratedEnum] Android.Views.Keycode primaryCode)
        {
        }

        public void OnRelease([GeneratedEnum] Android.Views.Keycode primaryCode)
        {
        }

        public void OnText(ICharSequence text)
        {
        }

        public void SwipeDown()
        {
        }

        public void SwipeLeft()
        {
        }

        public void SwipeRight()
        {
        }

        public void SwipeUp()
        {
        }
    }
}