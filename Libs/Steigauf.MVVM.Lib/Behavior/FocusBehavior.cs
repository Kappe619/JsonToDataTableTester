using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Steigauf.MVVM
{
    /// <summary>
    /// This attached Behavior will set the focus to the first element in the tab order.
    /// This could be used for example directly in the window tag or in a usercontrol tag!
    /// </summary>
    public static class FocusBehavior2
    {

        //public static readonly DependencyProperty FocusByNameProperty
        //    DependencyProperty.RegisterAttached(
        //    "FocusByName",
        //    typeof(string),
        //    typeof(Control),
        //    new FocusBehavior2("", OnFocusByNameChanged));

        //public static readonly DependencyProperty FocusFirstProperty =
    //        DependencyProperty.RegisterAttached(
    //            "FocusFirst",
    //            typeof(bool),
    //            typeof(Control),
    //            new PropertyMetadata(false, OnFocusFirstPropertyChanged));

    //    public static bool GetFocusFirst(Control control)
    //    {
    //        return (bool)control.GetValue(FocusFirstProperty);
    //    }

    //    public static void SetFocusFirst(Control control, bool value)
    //    {
    //        control.SetValue(FocusFirstProperty, value);
    //    }


    //    public static string GetFocusByName(Control control)
    //    {
    //        return (string)control.GetValue(FocusByNameProperty);
    //    }

    //    public static void SetFocusByName(Control control, string value)
    //    {
    //        control.SetValue(FocusByNameProperty, value);
    //    }

    //    static void OnFocusFirstPropertyChanged(
    //        DependencyObject obj, DependencyPropertyChangedEventArgs args)
    //    {
    //        Control control = obj as Control;
    //        if (control == null || !(args.NewValue is bool))
    //        {
    //            return;
    //        }

    //        if ((bool)args.NewValue)
    //        {
    //            control.Loaded += (sender, e) =>
    //                control.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
    //        }
    //    }


    //    static void OnFocusByNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    //    {
    //        Control control = obj as Control;
    //        if (control == null || string.IsNullOrEmpty(args.NewValue.ToString()))
    //        {
    //            return;
    //        }

    //        //Das Element finden
    //        //object element = control.FindName(args.NewValue.ToString());
    //        control.Loaded += (sender, e) => doFocusByName(control, args.NewValue.ToString());

    //    }


    //    static void doFocusByName(Control control, string value)
    //    {

    //        FrameworkElement element = control.FindName(value) as FrameworkElement;
    //        //System.Diagnostics.Debug.Print("Element: " + element);
    //        if (element != null)
    //        {
    //            element.Focus();

    //            if (element.GetType() == typeof(TextBox))
    //            {
    //                TextBox textBoxElement = element as TextBox;
    //                textBoxElement.SelectionStart = 0;
    //                textBoxElement.SelectionLength = textBoxElement.Text.Length;
    //            }
    //        }

    //    }
    }
}
