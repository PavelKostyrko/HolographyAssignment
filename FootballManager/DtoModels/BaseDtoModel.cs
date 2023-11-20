using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FootballManager.DtoModels
{
    public abstract class BaseDtoModel : INotifyPropertyChanged
    {
        public int? Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
