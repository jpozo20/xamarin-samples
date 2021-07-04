using System;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinSamples.Android.UI.Effects;
using XamarinSamples.Effects;
using AView = Android.Views.View;
using AColor = Android.Graphics.Color;
using XView = Xamarin.Forms.View;
using R = XamarinSamples.Android.Resource;

[assembly: ResolutionGroupName("Samples")]
[assembly: ExportEffect(typeof(PlatformTouchRippleEffect), nameof(TouchRippleEffect))]

namespace XamarinSamples.Android.UI.Effects
{
    public class PlatformTouchRippleEffect : PlatformEffect
    {
        private float _prevY;
        private bool _hasMoved;
        private bool _isLongPress;

        private Drawable _normalDrawable;
        private Drawable _activatedDrawable;
        private RippleDrawable _rippleDrawable;

        private TouchRippleEffect _effect;

        public bool IsDisposed => (Container as IVisualElementRenderer)?.Element == null;

        protected override void OnAttached()
        {
            if (Container != null)
            {
                SetBackgroundDrawables();
                Container.HapticFeedbackEnabled = false;
                var command = TouchRippleEffect.GetCommand(Element);
                if (command != null)
                {
                    Container.Clickable = true;
                    Container.Focusable = false;
                    Container.Touch += OnTouch;
                }

                var longPressCommand = TouchRippleEffect.GetLongPressCommand(Element);
                if (longPressCommand != null)
                {
                    Container.LongClickable = true;
                    Container.LongClick += ContainerOnLongClick;
                }
            }
        }

        private void ContainerOnLongClick(object sender, AView.LongClickEventArgs e)
        {
            // Notify to the user that the item was selected
            (sender as AView)?.PerformHapticFeedback(FeedbackConstants.LongPress);

            // If item is currently selected, disable long click
            if (_hasMoved)
            {
                _hasMoved = false;
                return;
            }

            var command = TouchRippleEffect.GetLongPressCommand(Element);
            var param = TouchRippleEffect.GetLongPressCommandParameter(Element);
            command?.Execute(param);
            _isLongPress = true;

            // Set the Container.Activated and the selected color drawable
            // so we know the item is selected on a next touch
            if (Container.Background is StateListDrawable drawable)
            {
                drawable.SetState(new[] {global::Android.Resource.Attribute.StateActivated});
                Container.Activated = true;
            }

            // Bubble up the event to avoid touch errors 
            e.Handled = false;
            _hasMoved = false;
        }

        private void OnTouch(object sender, AView.TouchEventArgs e)
        {
            e.Handled = false;
            var currentY = e.Event.GetY();

            var action = e.Event?.Action;
            switch (action)
            {
                case MotionEventActions.Down:
                    _prevY = e.Event.GetY();
                    break;


                case MotionEventActions.Move:
                {
                    // Remove the Pressed state if the user moves the finger
                    // before the long click event is fired
                    var diffY = currentY - _prevY;
                    var absolute = Math.Abs(diffY);
                    if (absolute > 8) _hasMoved = true;
                    if (_hasMoved && Container.Background is StateListDrawable drawable)
                    {
                        Container.Pressed = false;

                        // var states = drawable.GetState();
                        // var context = Xamarin.Essentials.Platform.AppContext;
                        // foreach (var state in states)
                        // {
                        //     var stateName = context.Resources.GetResourceName(state);
                        //     System.Diagnostics.Debug.WriteLine(stateName);
                        // }

                        // Keep the NormalColor on scroll
                        if (!Container.Activated)
                        {
                            drawable.SetState(new[] {global::Android.Resource.Attribute.Enabled});
                            drawable.JumpToCurrentState();
                        }
                        else
                        {
                            // Keep the SelectedColor when moving scrolling with a selected item
                            drawable.SetState(new[] {global::Android.Resource.Attribute.StateActivated});
                            drawable.JumpToCurrentState();
                        }
                    }

                    break;
                }

                case MotionEventActions.Up:
                {
                    // The TouchEvent is called again after a long click
                    // here we ensure it's only called when not doing a long click
                    if (!_isLongPress)
                    {
                        if (_hasMoved) return;
                        if (Container.Activated) Container.Activated = false;
                        var command = TouchRippleEffect.GetCommand(Element);
                        var param = TouchRippleEffect.GetCommandParameter(Element);
                        command?.Execute(param);
                    }

                    // If a long click was fired, set the flag to false
                    // so we can correctly register a single click again
                    _isLongPress = false;
                    _hasMoved = false;
                    break;
                }

                case MotionEventActions.Cancel:
                {
                    _isLongPress = false;
                    _hasMoved = false;
                    break;
                }
            }
        }

        private void SetBackgroundDrawables()
        {
            var normalColor = TouchRippleEffect.GetNormalColor(Element).ToAndroid();
            var rippleColor = TouchRippleEffect.GetRippleColor(Element).ToAndroid();
            var selectedColor = TouchRippleEffect.GetSelectedColor(Element).ToAndroid();

            var stateList = new StateListDrawable();
            _normalDrawable = new GradientDrawable(GradientDrawable.Orientation.LeftRight,
                new int[] {normalColor, normalColor});
            _rippleDrawable = new RippleDrawable(ColorStateList.ValueOf(rippleColor), _normalDrawable, null);
            _activatedDrawable = new GradientDrawable(GradientDrawable.Orientation.LeftRight,
                new int[] {selectedColor, selectedColor});

            stateList.AddState(new[] {global::Android.Resource.Attribute.Enabled}, _normalDrawable);
            stateList.AddState(new[] {global::Android.Resource.Attribute.StatePressed}, _rippleDrawable);
            stateList.AddState(new[] {global::Android.Resource.Attribute.StateActivated}, _activatedDrawable);

            Container.SetBackground(stateList);
        }

        protected override void OnDetached()
        {
            if (IsDisposed) return;

            _normalDrawable?.Dispose();
            _activatedDrawable?.Dispose();
            _rippleDrawable?.Dispose();
            Container.Touch -= OnTouch;
            Container.LongClick -= ContainerOnLongClick;
        }
    }
}