using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinSamples.Effects
{
    public class TouchRippleEffect : RoutingEffect
    {
        public TouchRippleEffect() : base("Samples.TouchRippleEffect")
        {
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached(
            "Command",
            typeof(ICommand),
            typeof(TouchRippleEffect),
            default(ICommand),
            propertyChanged: OnCommandChanged
        );

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached(
            "CommandParameter",
            typeof(object),
            typeof(TouchRippleEffect),
            null,
            propertyChanged: OnCommandParameterChanged
        );

        public static readonly BindableProperty LongPressCommandProperty = BindableProperty.CreateAttached(
            "LongPressCommand",
            typeof(ICommand),
            typeof(TouchRippleEffect),
            default(ICommand),
            propertyChanged: OnLongPressCommandChanged
        );

        public static readonly BindableProperty LongPressCommandParameterProperty = BindableProperty.CreateAttached(
            "LongPressCommandParameter",
            typeof(object),
            typeof(TouchRippleEffect),
            null,
            propertyChanged: OnLongPressCommandParameterChanged
        );

        public static void SetCommand(BindableObject bindable, ICommand value)
        {
            bindable.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(BindableObject bindable)
        {
            return (ICommand) bindable.GetValue(CommandProperty);
        }

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable);
        }

        public static void SetCommandParameter(BindableObject bindable, object value)
        {
            bindable.SetValue(CommandParameterProperty, value);
        }

        public static object GetCommandParameter(BindableObject bindable)
        {
            return bindable.GetValue(CommandParameterProperty);
        }

        private static void OnCommandParameterChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable);
        }

        public static void SetLongPressCommand(BindableObject bindable, ICommand value)
        {
            bindable.SetValue(LongPressCommandProperty, value);
        }

        public static ICommand GetLongPressCommand(BindableObject bindable)
        {
            return (ICommand) bindable.GetValue(LongPressCommandProperty);
        }

        private static void OnLongPressCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable);
        }

        public static void SetLongPressCommandParameter(BindableObject bindable, object value)
        {
            bindable.SetValue(LongPressCommandParameterProperty, value);
        }

        public static object GetLongPressCommandParameter(BindableObject bindable)
        {
            return bindable.GetValue(LongPressCommandParameterProperty);
        }

        private static void OnLongPressCommandParameterChanged(BindableObject bindable, object oldValue,
            object newValue)
        {
            AttachEffect(bindable);
        }

        public static readonly BindableProperty NormalColorProperty = BindableProperty.CreateAttached(
            "NormalColor",
            typeof(Color),
            typeof(TouchRippleEffect),
            Color.White,
            propertyChanged: OnNormalColorChanged
        );

        public static void SetNormalColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(NormalColorProperty, value);
        }

        public static Color GetNormalColor(BindableObject bindable)
        {
            return (Color) bindable.GetValue(NormalColorProperty);
        }

        static void OnNormalColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable);
        }

        public static readonly BindableProperty RippleColorProperty = BindableProperty.CreateAttached(
            "RippleColor",
            typeof(Color),
            typeof(TouchRippleEffect),
            Color.LightSlateGray,
            propertyChanged: OnRippleColorChanged
        );

        public static void SetRippleColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(RippleColorProperty, value);
        }

        public static Color GetRippleColor(BindableObject bindable)
        {
            return (Color) bindable.GetValue(RippleColorProperty);
        }

        static void OnRippleColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable);
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.CreateAttached(
            "SelectedColor",
            typeof(Color),
            typeof(TouchRippleEffect),
            Color.LightGreen,
            propertyChanged: OnSelectedColorChanged
        );

        public static void SetSelectedColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(SelectedColorProperty, value);
        }

        public static Color GetSelectedColor(BindableObject bindable)
        {
            return (Color) bindable.GetValue(SelectedColorProperty);
        }

        static void OnSelectedColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable);
        }

        static void AttachEffect(BindableObject bindable)
        {
            if (bindable is not VisualElement view || view.Effects.OfType<TouchRippleEffect>().Any())
                return;

            view.Effects.Add(new TouchRippleEffect());
        }
    }
}