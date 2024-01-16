using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System;

namespace Steigauf.MVVM
{
    public abstract class DialogViewModelBase : WindowViewModelBase
    {

        #region Properties
        private String _displayName;
        /// <summary>
        /// DisplayName stellt den benutzerfreundlichen Namen des Objektes dar und git
        /// diesen zurück. Kindklassen können diesen setzen oder notwendigerweise
        /// überschreiben.
        /// </summary>
        public virtual string DisplayName
        {
            get { return this._displayName; }
            set
            {
                this._displayName = value;
                this.OnPropertyChanged("DisplayName");
            }
        }
        #endregion

        //private Steigauf.MVVM.Command.RelayCommand _closeCommand;
        //public event EventHandler RequestClose;

        
        ///// <summary>
        ///// OnRequestClose(); muss aus dem ViewModel aufgerufen werden um die View zu schließen.
        ///// VIEWMODEL.RequestClose += delegate (object sender, EventArgs args) { VIEW.Close(); }
        ///// </summary>
        //protected void OnRequestClose()
        //{
        //    if (RequestClose != null)
        //        RequestClose(this, EventArgs.Empty);
        //}

        #region "Property DialogResult"
        /// <summary>
        /// Property DialogResult wird vom DialogCloser benötigt
        /// </summary>
        bool? _DialogResult;
        public bool? DialogResult
        {
            get { return _DialogResult; }
            set
            {
                _DialogResult = value;
                OnPropertyChanged("DialogResult");
            }
        }

        #endregion

        //hat nicht funktioniert
        //    public virtual void CloseWindow(bool? result = true)
        //{
        //    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
        //    {
        //        DialogResult = DialogResult == null
        //            ? true
        //            : !DialogResult;
        //    }));
        //}

        #region CloseCommand

        //Funktioniert und wird im KonfigManager verwendet
        //Das Command CloseCommand benötigt den EventHandler OnClosingRequest und die Funktion OnClosingRequest
        Steigauf.MVVM.Command.DelegateCommand _closeCommand;
        public ICommand CloseCommand
    {
        get
        {
            if (this._closeCommand == null)
            {
                _closeCommand = new Steigauf.MVVM.Command.DelegateCommand(OnClosingRequest);
            }
            return this._closeCommand;
        }
    }

    #region Request Close Event
    public event System.EventHandler ClosingRequest;
    
    //Aufruf von außen vor ShowDialog(): VM.ClosingRequest += (sender, e) => ed.Close();
    protected virtual void OnClosingRequest()
    {
        System.EventHandler handler = this.ClosingRequest;
        if (handler != null)
        {
            handler(this, System.EventArgs.Empty);
        }
    }
    #endregion
    #endregion
  }
}
