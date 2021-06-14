using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinSamples.Android.UI;
using XamarinSamples.Views.Controls;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace XamarinSamples.Android.UI
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            //Configure native control (TextBox)
            if(Control != null)
            {
                Control.Background = null;
            }

            // Configure Entry properties
            if(e.NewElement != null)
            {

            }
        }
    }
}