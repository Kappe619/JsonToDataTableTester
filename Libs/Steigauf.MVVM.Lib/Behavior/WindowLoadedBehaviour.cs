using System.Windows;
using System.Windows.Input;

namespace Steigauf.MVVM
{
    public static class WindowLoadedBehaviour
    {
        public static DependencyProperty LoadedCommandProperty
        = DependencyProperty.RegisterAttached(
             "LoadedCommand",
             typeof(ICommand),
             typeof(WindowLoadedBehaviour),
             new PropertyMetadata(null, OnLoadedCommandChanged));

        private static void OnLoadedCommandChanged
             (DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = depObj as FrameworkElement;
            if (frameworkElement != null && e.NewValue is ICommand)
            {
                frameworkElement.Loaded
                  += (o, args) =>
                  {
                      (e.NewValue as ICommand).Execute(null);
                  };
            }
        }

        public static ICommand GetLoadedCommand(DependencyObject depObj)
        {
            return (ICommand)depObj.GetValue(LoadedCommandProperty);
        }

        public static void SetLoadedCommand(
            DependencyObject depObj,
            ICommand value)
        {
            depObj.SetValue(LoadedCommandProperty, value);
        }
    }
}
