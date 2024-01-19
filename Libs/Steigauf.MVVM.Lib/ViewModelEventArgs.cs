namespace Steigauf.MVVM.Lib
{
    public class ViewModelEventArgs : EventArgs
    {

        private bool _DialogResult;
        public bool DialogResult
        {
            get { return _DialogResult; }
            internal set { _DialogResult = value; }
        }

        private object _arg = 0;
        public object arg
        {
            get { return _arg; }
            internal set { _arg = value; }
        }

        public ViewModelEventArgs(bool bDialogResult, object arg)
        {
            DialogResult = bDialogResult;
            _arg = arg;
        }


    }
}
