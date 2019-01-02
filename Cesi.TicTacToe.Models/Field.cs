using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Cesi.TicTacToe.Models.Annotations;

namespace Cesi.TicTacToe.Models
{
    public class Field : INotifyPropertyChanged
    {
        private string marker;

        public string Marker
        {
            get => marker;
            set
            {
                if (value == marker) return;
                marker = value;
                OnPropertyChanged();
            }
        }

        public int X { get; set; }
        public int Y { get; set; }

        public Field(string marker, int x, int y)
        {
            Marker = marker;
            X = x;
            Y = y;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
