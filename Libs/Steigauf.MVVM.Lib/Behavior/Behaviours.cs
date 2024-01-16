using System.Windows;
using System.Windows.Input;
namespace Steigauf.MVVM
{
    public static class Behaviours
    {
        #region SDSBehaviour
        public static readonly DependencyProperty SDSBehaviourProperty =
            DependencyProperty.RegisterAttached("SDSBehaviour", typeof(ICommand), typeof(Behaviours),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.None,
                    OnSDSBehaviourChanged));
        public static ICommand GetSDSBehaviour(DependencyObject d)
        {
            return (ICommand)d.GetValue(SDSBehaviourProperty);
        }
        public static void SetSDSBehaviour(DependencyObject d, ICommand value)
        {
            d.SetValue(SDSBehaviourProperty, value);
        }
        private static void OnSDSBehaviourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            System.Windows.Controls.Grid g = d as System.Windows.Controls.Grid;
            if (g != null)
            {
                g.Drop += (s, a) =>
                {
                    ICommand iCommand = GetSDSBehaviour(d);
                    if (iCommand != null)
                    {
                        if (iCommand.CanExecute(a.Data))
                        {
                            iCommand.Execute(a.Data);
                        }
                    }
                };
            }
            else
            {
                throw new System.Exception("Non grid");
            }
        }
        #endregion
    }
}
