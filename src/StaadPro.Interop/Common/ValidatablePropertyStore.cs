using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StaadPro.Interop.Common
{
    /// <summary>
    /// Thread-safe lightweight property store providing change notification.
    /// </summary>
    public abstract class ValidatablePropertyStore : INotifyPropertyChanged
    {
        private readonly object _syncLock = new object();
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>(StringComparer.Ordinal);

        public event PropertyChangedEventHandler PropertyChanged;

        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            lock (_syncLock)
            {
                if (_properties.TryGetValue(propertyName, out object value))
                {
                    return (T)value;
                }
                return default;
            }
        }

        protected bool Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            lock (_syncLock)
            {
                if (_properties.TryGetValue(propertyName, out object existingValue))
                {
                    if (EqualityComparer<T>.Default.Equals((T)existingValue, value))
                    {
                        return false;
                    }
                }

                _properties[propertyName] = value;
            }

            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
