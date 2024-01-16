using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Windows;


namespace Steigauf.MVVM.Service
{
    [ContractClassFor(typeof(IDialogService))]
    abstract class IDialogServiceContract : IDialogService
    {
        /// <summary>
        /// Gets the registered views.
        /// </summary>
        public ReadOnlyCollection<FrameworkElement> Views
        {
            get { return default(ReadOnlyCollection<FrameworkElement>); }
        }


        /// <summary>
        /// Registers a View.
        /// </summary>
        /// <param name="view">The registered View.</param>
        public void Register(FrameworkElement view)
        {
            Contract.Requires(view != null);
            Contract.Requires(!Views.Contains(view));
        }


        /// <summary>
        /// Unregisters a View.
        /// </summary>
        /// <param name="view">The unregistered View.</param>
        public void Unregister(FrameworkElement view)
        {
            Contract.Requires(Views.Contains(view));
        }


        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <remarks>
        /// The dialog used to represent the ViewModel is retrieved from the registered mappings.
        /// </remarks>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="viewModel">The ViewModel of the new dialog.</param>
        /// <returns>
        /// A nullable value of type bool that signifies how a window was closed by the user.
        /// </returns>
        public bool? ShowDialog(object ownerViewModel, object viewModel)
        {
            Contract.Requires(ownerViewModel != null);
            Contract.Requires(viewModel != null);

            return default(bool?);
        }
        /// <summary>
        /// Verhält sich genauso wie ShowDialog mit dem Unterschied,
        /// dass keiner modaler Dialog angezeigt wird.
        /// </summary>
        public bool? Show(object ownerViewModel, object viewModel)
        {
            Contract.Requires(ownerViewModel != null);
            Contract.Requires(viewModel != null);

            return default(bool?);
        }


        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="viewModel">The ViewModel of the new dialog.</param>
        /// <typeparam name="T">The type of the dialog to show.</typeparam>
        /// <returns>
        /// A nullable value of type bool that signifies how a window was closed by the user.
        /// </returns>
        public bool? ShowDialog<T>(object ownerViewModel, object viewModel) where T : Window
        {
            Contract.Requires(ownerViewModel != null);
            Contract.Requires(viewModel != null);

            return default(bool?);
        }
        /// <summary>
        /// Verhält sich genauso wie ShowDialog<T> mit dem Unterschied,
        /// dass keiner modaler Dialog angezeigt wird.
        /// </summary>
        public void Show<T>(object ownerViewModel, object viewModel) where T : Window
        {
            Contract.Requires(ownerViewModel != null);
            Contract.Requires(viewModel != null);
        }



        /// <summary>
        /// Finds window corresponding to specified ViewModel.
        /// </summary>
        public Window FindOwnerWindow(object viewModel)
        {
            Contract.Requires(viewModel != null);

            return default(Window);
        }
    }
}
