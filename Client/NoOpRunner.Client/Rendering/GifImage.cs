using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace NoOpRunner.Client.Rendering
{
    public class GifImage : Image
    {
        private bool isInitialized;
        private GifBitmapDecoder gifDecoder;
        private Int32Animation animation;

        public int FrameIndex
        {
            get { return (int) GetValue(FrameIndexProperty); }
            set { SetValue(FrameIndexProperty, value); }
        }

        //Adjust frames per second
        private void Initialize()
        {
            gifDecoder = new GifBitmapDecoder(this.GifSource,
                BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            animation = new Int32Animation(0, gifDecoder.Frames.Count - 1,
                new Duration(new TimeSpan(0, 0, 0, gifDecoder.Frames.Count / 30,
                    (int) ((gifDecoder.Frames.Count / 10.0 - gifDecoder.Frames.Count / 30) * 1000))));
            animation.RepeatBehavior = RepeatBehavior.Forever;
            this.Source = gifDecoder.Frames[0];

            isInitialized = true;
        }

        static GifImage()
        {
            VisibilityProperty.OverrideMetadata(typeof(GifImage),
                new FrameworkPropertyMetadata(VisibilityPropertyChanged));
        }

        private static void VisibilityPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ((Visibility) e.NewValue == Visibility.Visible)
            {
                ((GifImage) sender).StartAnimation();
            }
            else
            {
                ((GifImage) sender).StopAnimation();
            }
        }

        public static readonly DependencyProperty FrameIndexProperty =
            DependencyProperty.Register("FrameIndex", typeof(int), typeof(GifImage),
                new UIPropertyMetadata(0, new PropertyChangedCallback(ChangingFrameIndex)));

        static void ChangingFrameIndex(DependencyObject obj, DependencyPropertyChangedEventArgs ev)
        {
            var gifImage = obj as GifImage;
            gifImage.Source = gifImage.gifDecoder.Frames[(int) ev.NewValue];
        }

        public Uri GifSource
        {
            get { return (Uri) GetValue(GifSourceProperty); }
            set { SetValue(GifSourceProperty, value); }
        }

        public static readonly DependencyProperty GifSourceProperty =
            DependencyProperty.Register("GifSource", typeof(Uri), typeof(GifImage),
                new UIPropertyMetadata(null, GifSourcePropertyChanged));

        private static void GifSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as GifImage).Initialize();
            (sender as GifImage).StartAnimation();
        }

        public void StartAnimation()
        {
            if (!isInitialized)
                this.Initialize();

            BeginAnimation(FrameIndexProperty, animation);
        }

        public void StopAnimation()
        {
            BeginAnimation(FrameIndexProperty, null);
        }
    }
}