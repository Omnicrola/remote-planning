using System.Collections.Generic;
using System.ComponentModel;

namespace RemotePlanning.Ui.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        protected void FireOnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void SetPropertyField<T>(ref T field, T newValue, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                FireOnPropertyChanged(propertyName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}