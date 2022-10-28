using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_Presentation.ViewModel
{
    // This class implements INotifyPropertyChanged
    // to support one-way and two-way bindings
    // (such that the UI element updates when the source
    // has been changed dynamically)
    public class ViewModelBase : INotifyPropertyChanged
    {
        // Declare the event
        public event PropertyChangedEventHandler? PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
