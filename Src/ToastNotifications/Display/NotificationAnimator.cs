using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ToastNotifications.Core;

namespace ToastNotifications.Display
{
    public class NotificationAnimator : INotificationAnimator
    {
        private readonly NotificationDisplayPart _displayPart;
        private readonly TimeSpan _showAnimationTime;
        private readonly TimeSpan _hideAnimationTime;

        public NotificationAnimator(NotificationDisplayPart displayPart, TimeSpan showAnimationTime, TimeSpan hideAnimationTime)
        {
            _displayPart = displayPart;
            _showAnimationTime = showAnimationTime;
            _hideAnimationTime = hideAnimationTime;
        }

        public void Setup()
        {
            var scale = new ScaleTransform(1, 0);
            _displayPart.RenderTransform = scale;
        }

        public void PlayShowAnimation()
        {
            var scale = (ScaleTransform)_displayPart.RenderTransform;
            scale.CenterY = _displayPart.ActualHeight;
            scale.CenterX = _displayPart.ActualWidth;

            var storyboard = new Storyboard();

            SetGrowYAnimation(storyboard);
            SetGrowXAnimation(storyboard);
            SetFadeInAnimation(storyboard);

            storyboard.Begin();
        }

        private void SetFadeInAnimation(Storyboard storyboard)
        {
            var fadeInAnimation = new DoubleAnimation
            {
                Duration = _showAnimationTime,
                From = 0,
                To = 1,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            storyboard.Children.Add(fadeInAnimation);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));
            Storyboard.SetTarget(fadeInAnimation, _displayPart);
        }

        private void SetGrowXAnimation(Storyboard storyboard)
        {
            var growXAnimation = new DoubleAnimation
            {
                Duration = _showAnimationTime,
                From = 0,
                To = 1,
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };
            storyboard.Children.Add(growXAnimation);
            Storyboard.SetTargetProperty(growXAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growXAnimation, _displayPart);
        }

        private void SetGrowYAnimation(Storyboard storyboard)
        {
            var growYAnimation = new DoubleAnimation
            {
                Duration = _showAnimationTime,
                From = 0,
                To = 1,
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };
            storyboard.Children.Add(growYAnimation);
            Storyboard.SetTargetProperty(growYAnimation, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTarget(growYAnimation, _displayPart);
        }

        public void PlayHideAnimation()
        {
            _displayPart.MinHeight = 0;
            var scale = (ScaleTransform)_displayPart.RenderTransform;
            scale.CenterY = _displayPart.ActualHeight;
            scale.CenterX = _displayPart.ActualWidth;

            var storyboard = new Storyboard();

            SetShrinkYAnimation(storyboard);
            SetShrinkXAnimation(storyboard);
            SetFadeoutAnimation(storyboard);

            storyboard.Begin();
        }

        private void SetFadeoutAnimation(Storyboard storyboard)
        {
            var fadeOutAnimation = new DoubleAnimation
            {
                Duration = _hideAnimationTime,
                From = 1,
                To = 0,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };

            storyboard.Children.Add(fadeOutAnimation);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath("Opacity"));
            Storyboard.SetTarget(fadeOutAnimation, _displayPart);
        }

        private void SetShrinkXAnimation(Storyboard storyboard)
        {
            var shrinkXAnimation = new DoubleAnimation
            {
                Duration = _hideAnimationTime,
                From = 1,
                To = 0,
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseIn }
            };

            storyboard.Children.Add(shrinkXAnimation);
            Storyboard.SetTargetProperty(shrinkXAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(shrinkXAnimation, _displayPart);
        }

        private void SetShrinkYAnimation(Storyboard storyboard)
        {
            var shrinkYAnimation = new DoubleAnimation
            {
                Duration = _hideAnimationTime,
                From = _displayPart.ActualHeight,
                To = 0,
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseIn }
            };

            storyboard.Children.Add(shrinkYAnimation);
            Storyboard.SetTargetProperty(shrinkYAnimation, new PropertyPath("Height"));
            Storyboard.SetTarget(shrinkYAnimation, _displayPart);
        }
    }
}