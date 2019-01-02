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
    public class Game : INotifyPropertyChanged
    {
        private Player previousPlayer;
        private List<Field[]> board;

        public Player playerOne { get; set; }
        public Player playerTwo { get; set; }

        public List<Field[]> Board
        {
            get { return board; }
            set
            {
                board = value;
                OnPropertyChanged();
            }
        }

        public Game()
        {
            InitEmptyBoard();
            InitPlayers();
        }

        private void InitEmptyBoard()
        {
            Board = new List<Field[]>();
            for (int i = 0; i < 3; i++)
            {
                List<Field> fields = new List<Field>();
                for (int j = 0; j < 3; j++)
                {
                    Field f = new Field("", i, j);
                    fields.Add(f);
                }
                Board.Add(fields.ToArray());
            }
        }

        private void InitPlayers()
        {
            playerOne = new Player { Id = 1, Name = "P1", Marker = "X" };
            playerTwo = new Player { Id = 2, Name = "P2", Marker = "O" };
        }

        public void Play(Player player, Field field)
        {
            if (previousPlayer != null && player.Id == previousPlayer.Id)
            {
                throw new InvalidOperationException("Same_Player_Play_Twice");
            }

            if (Board[field.X][field.Y].Marker != string.Empty)
            {
                throw new InvalidOperationException("Marker_Already_Placed");
            }

            previousPlayer = player;
            Board[field.X][field.Y].Marker = player.Marker;
        }

        public Player GetPlayerByMarker(string winnerMarker)
        {
            Player winner = null;
            if (winnerMarker == playerOne.Marker)
            {
                winner = playerOne;
            }
            else if (winnerMarker == playerTwo.Marker)
            {
                winner = playerTwo;
            }

            return winner;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
