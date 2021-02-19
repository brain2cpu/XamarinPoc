using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinPoc.ViewModels
{
    class ViewModelBase : INotifyPropertyChanged
    {
        #region Implements INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
