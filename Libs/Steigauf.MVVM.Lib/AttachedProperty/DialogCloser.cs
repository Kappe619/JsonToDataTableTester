using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Steigauf.MVVM
{
    public static class DialogCloser 
    {
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached(
                "DialogResult",
                typeof(bool?),
                typeof(DialogCloser),
                new PropertyMetadata(DialogResultChanged));

        private static void DialogResultChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var wndWindow = d as Window;
            Boolean blnIsModal = System.Windows.Interop.ComponentDispatcher.IsThreadModal;
            if (wndWindow != null)
                if (blnIsModal)
                    wndWindow.DialogResult = e.NewValue as Boolean?;
                else
                    wndWindow.Close();
        }
        public static void SetDialogResult(Window target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }
    }
}
