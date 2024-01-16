using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Steigauf.MVVM.Command
{
    public class EventCommand : ICommand
    {
        public Action<object> action
        { get; set; }

        public EventCommand()
        {

        }

        public EventCommand(Action<object> _action)
        {
            action = _action;
        }

        public bool CanExecute(object parameter)
        {
            if (action != null)
            {
                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            if (action != null)
            {
                action(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }


    public class ListBoxBehavior
    {
        private static DependencyProperty _property;

        private static readonly RoutedEvent _doubleClickEvent = ListBox.MouseDoubleClickEvent;

        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommand",
            typeof(ICommand), typeof(ListBoxBehavior),
            new PropertyMetadata(new PropertyChangedCallback(DoubleClickCallBack)));



        public static void SetDoubleClickCommand(UIElement obj, ICommand value)
        {
            obj.SetValue(DoubleClickCommandProperty, value);
        }

        public static ICommand GetDoubleClickCommand(UIElement obj)
        {
            return (ICommand)obj.GetValue(DoubleClickCommandProperty);
        }
        
        static void DoubleClickCallBack(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            UIElement element = obj as UIElement;
            _property = args.Property;

            if (element != null)
            {
                if (args.OldValue != null)
                {
                    element.AddHandler(_doubleClickEvent, new RoutedEventHandler(EventHandler));
                }

                if (args.NewValue != null)
                {
                    element.AddHandler(_doubleClickEvent, new RoutedEventHandler(EventHandler));
                }
            }
        }

        public static void EventHandler(object sender, RoutedEventArgs e)
        {
            DependencyObject obj = sender as DependencyObject;
            if (obj != null)
            {
                ICommand command = obj.GetValue(_property) as ICommand;

                if (command != null)
                {
                    if (command.CanExecute(e))
                    {
                        //ListBox lb = e.OriginalSource as ListBox;

                        //if (lb != null)
                        //{
                            command.Execute(string.Empty);
                        //}
                    }
                }
            }
        }


        private static DependencyProperty _propertyMouseUp;

        private static readonly RoutedEvent _MouseUpEvent = ListBox.MouseUpEvent;

        public static readonly DependencyProperty MouseUpCommandProperty =
            DependencyProperty.RegisterAttached("MouseUpCommand",
            typeof(ICommand), typeof(ListBoxBehavior),
            new PropertyMetadata(new PropertyChangedCallback(MouseUpCallBack)));



        public static void SetMouseUpCommand(UIElement obj, ICommand value)
        {
            obj.SetValue(MouseUpCommandProperty, value);
        }

        public static ICommand GetMouseUpCommand(UIElement obj)
        {
            return (ICommand)obj.GetValue(MouseUpCommandProperty);
        }

        static void MouseUpCallBack(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            UIElement element = obj as UIElement;
            _propertyMouseUp = args.Property;

            if (element != null)
            {
                if (args.OldValue != null)
                {
                    element.AddHandler(_MouseUpEvent, new RoutedEventHandler(MouseUpEventHandler));
                }

                if (args.NewValue != null)
                {
                    element.AddHandler(_MouseUpEvent, new RoutedEventHandler(MouseUpEventHandler));
                }
            }
        }

        public static void MouseUpEventHandler(object sender, RoutedEventArgs e)
        {
            DependencyObject obj = sender as DependencyObject;
            if (obj != null)
            {
                ICommand command = obj.GetValue(_propertyMouseUp) as ICommand;

                if (command != null)
                {
                    if (command.CanExecute(e))
                    {
                        //ListBox lb = e.OriginalSource as ListBox;

                        //if (lb != null)
                        //{
                        command.Execute(e);
                        //}
                    }
                }
            }
        }

    }

}
