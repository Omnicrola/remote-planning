using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RemotePlanning.Ui
{
    /// <summary>
    /// Interaction logic for EditableLabel.xaml
    /// </summary>
    public partial class EditableLabel : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isEditing;
        private string _changedText;

        public EditableLabel()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public string Text
        {
            get { return (string)base.GetValue(TextProperty); }
            set { base.SetValue(TextProperty, value); }
        }
        public bool IsEditing
        {
            get { return _isEditing; }
            set { SetPropertyField(ref _isEditing, value); }
        }

        public string ChangedText
        {
            get { return _changedText; }
            set { SetPropertyField(ref _changedText, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(EditableLabel));

        private void Text_OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangedText = Text;
            IsEditing = true;
            EditingBox.Focus();
        }

        private void Text_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Text = ChangedText;
                IsEditing = false;
            }
            else if (e.Key == Key.Escape)
            {
                ChangedText = Text;
                IsEditing = false;
            }
        }

        protected void SetPropertyField<T>(ref T field, T newValue, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
