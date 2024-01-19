using System.Windows;
using System.Windows.Input;

namespace Steigauf.MVVM
{
    public static class DataGridSelectedItemsBehavior
    {
        public static readonly DependencyProperty SelectedItemsChangedHandlerProperty =
            DependencyProperty.RegisterAttached("SelectedItemsChangedHandler",
                typeof(ICommand),
                typeof(DataGridSelectedItemsBehavior),
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

            System.Windows.Controls.DataGrid dataGrid = (System.Windows.Controls.DataGrid)d;

            if (e.OldValue == null && e.NewValue != null)
            {
                dataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(ItemsControl_SelectionChanged);
            }

            if (e.OldValue != null && e.NewValue == null)
            {
                dataGrid.SelectionChanged -= new System.Windows.Controls.SelectionChangedEventHandler(ItemsControl_SelectionChanged);
            }
        }


        public static void ItemsControl_SelectionChanged(Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            System.Windows.Controls.DataGrid dataGrid = (System.Windows.Controls.DataGrid)sender;

            ICommand itemsChangedHandler = GetSelectedItemsChangedHandler(dataGrid);

            itemsChangedHandler.Execute(dataGrid.SelectedItems);
        }
    }
}
