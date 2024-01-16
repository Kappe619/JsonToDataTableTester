using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace Steigauf.MVVM
{
    public class ListBoxSelectedItemsBehavior
    {
        public static readonly DependencyProperty SelectedItemsChangedHandlerProperty =
            DependencyProperty.RegisterAttached("SelectedItemsChangedHandler",
                typeof(ICommand),
                typeof(ListBoxSelectedItemsBehavior),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnSelectedItemsChangedHandlerChanged)));

       
        public static ICommand GetSelectedItemsChangedHandler(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return element.GetValue(SelectedItemsChangedHandlerProperty) as ICommand;
        }

        public static void SetSelectedItemsChangedHandler(DependencyObject element, ICommand value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(SelectedItemsChangedHandlerProperty, value);
        }

        public static void OnSelectedItemsChangedHandlerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            System.Windows.Controls.ListBox thisControl = (System.Windows.Controls.ListBox)d;

            if (e.OldValue == null && e.NewValue != null)
            {
                thisControl.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(ItemsControl_SelectionChanged);
            }

            if (e.OldValue != null && e.NewValue == null)
            {
                thisControl.SelectionChanged -= new System.Windows.Controls.SelectionChangedEventHandler(ItemsControl_SelectionChanged);
            }
        }


        public static void ItemsControl_SelectionChanged(Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ListBox thisControl = (System.Windows.Controls.ListBox)sender;

            ICommand itemsChangedHandler = GetSelectedItemsChangedHandler(thisControl);

            itemsChangedHandler.Execute(thisControl.SelectedItems);
        }
    }
}
