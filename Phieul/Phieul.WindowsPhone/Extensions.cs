using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Phieul {
    public static class Extensions {
        public static void ScaleTransform(this UIElement e, double to, double XOrigin = .5, double YOrigin = .5) {
            e.RenderTransform = new CompositeTransform();
            e.RenderTransformOrigin = new Point(XOrigin, YOrigin);

            var story = new Storyboard();

            var xAni = new DoubleAnimation() { To = to, Duration = TimeSpan.FromMilliseconds(200) };
            var yAni = new DoubleAnimation() { To = to, Duration = TimeSpan.FromMilliseconds(200) };

            story.Children.Add(xAni);
            story.Children.Add(yAni);

            Storyboard.SetTarget(story, e);
            Storyboard.SetTargetProperty(xAni, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)");
            Storyboard.SetTargetProperty(yAni, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");

            story.Begin();
        }
    }
}
