using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameData.Tools
{
    /// <summary>
    /// Implements INotifyPropertyChanged not notify the view whenever a property is set.
    /// </summary>
    public class NotifyProperty : INotifyPropertyChanged
    {
        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Protected Methods

        protected void Notify([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)     //Used a generic type so that it notifies all types of fields.
        {
            var flag = !EqualityComparer<T>.Default.Equals(field, value);
            if (flag)
            {
                field = value;
                Notify(propertyName);
            }
            return flag;
        }

        #endregion Protected Methods
    }
}