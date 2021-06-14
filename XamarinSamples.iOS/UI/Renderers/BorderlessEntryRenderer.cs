using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinSamples.iOS.UI.Renderers;
using XamarinSamples.Views.Controls;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace XamarinSamples.iOS.UI.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            //Configure Native control (UITextField)
            if (Control != null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
            }
        }
    }
}