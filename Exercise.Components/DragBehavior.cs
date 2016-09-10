using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Exercise.Components
{
    public class DragBehavior:Behavior<FrameworkElement>
    {


        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(DragBehavior), new FrameworkPropertyMetadata(0d,OnXChanged) {BindsTwoWayByDefault=true});

        private static void OnXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var db = d as DragBehavior;
            if (db == null) return;
            Canvas.SetLeft(db.AssociatedObject,(double) e.NewValue);
        }


        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Y.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(DragBehavior), new FrameworkPropertyMetadata(0d,OnYChanged) {BindsTwoWayByDefault=true});

        private Point _initialPosition;

        private static void OnYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var db = d as DragBehavior;
            if (db == null) return;
            Canvas.SetTop(db.AssociatedObject, (double) e.NewValue);
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!e.MouseDevice.Target.IsMouseCaptured) return;
            e.MouseDevice.Target.ReleaseMouseCapture();
            e.Handled = true;
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!e.MouseDevice.Target.IsMouseCaptured) return;
            var pos= e.GetPosition(ClosestCanvas);
            X = pos.X - _initialPosition.X;
            Y = pos.Y - _initialPosition.Y;
            e.Handled = true;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!e.MouseDevice.Target.CaptureMouse()) return;
            ClosestCanvas = FindParentOf<Canvas>(AssociatedObject) ;
            _initialPosition = e.GetPosition(AssociatedObject);
            e.Handled = true;
        }

        private T FindParentOf<T>(DependencyObject o)where T: DependencyObject => o == null || o.GetType().IsAssignableFrom(typeof(T))
            ? (T) o
            : FindParentOf<T>(VisualTreeHelper.GetParent(o));

        public Canvas ClosestCanvas { get; set; }
    }
}
