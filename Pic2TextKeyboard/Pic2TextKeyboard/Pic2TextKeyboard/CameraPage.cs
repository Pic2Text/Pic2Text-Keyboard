using System;
using Xamarin.Forms;
using System.Text;
using System.Collections.Generic;


namespace Pic2TextKeyboard
{
    public class CameraPage : ContentPage
    {
        public delegate void PhotoResultEventHandler(PhotoResultEventArgs result);

        public event PhotoResultEventHandler OnPhotoResult;

        public void SetPhotoResult(byte[] image, int width = -1, int height = -1)
        {
            if (OnPhotoResult != null)
            {
                OnPhotoResult.Invoke(new PhotoResultEventArgs(image, width, height));
            }
        }

        public void Cancel()
        {
            if (OnPhotoResult != null)
            {
                OnPhotoResult.Invoke(new PhotoResultEventArgs());
            }
        }
    }


    public class PhotoResultEventArgs : EventArgs
    {
        public byte[] Image { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool Success { get; private set; }

        public PhotoResultEventArgs()
        {
            Success = false;
        }

        public PhotoResultEventArgs(byte[] image, int width, int height)
        {
            Success = true;
            Image = image;
            Width = width;
            Height = height;
        }
    }
}
