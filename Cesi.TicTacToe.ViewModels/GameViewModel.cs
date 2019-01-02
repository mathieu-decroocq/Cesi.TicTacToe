using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cesi.TicTacToe.Models;
using Cesi.TicTacToe.ViewModels.Common;
using Cesi.TicTacToe.ViewModels.Extensions;

namespace Cesi.TicTacToe.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private Player currentPlayer;
        private Game game;
        private bool hasWinner;
        private List<Field[]> board;

        public Game Game
        {
            get => game;
            set
            {
                game = value;
                OnPropertyChanged();
            }
        }

        public GameViewModel(INavigationService navigationService) : base(navigationService)
        {
            Game = new Game();
            currentPlayer = game.playerOne;
        }

        public void Play(Field field)
        {
            Game.Play(currentPlayer, field);
            currentPlayer = currentPlayer.Id == Game.playerOne.Id ? Game.playerTwo : Game.playerOne;
         
        }

        public Player GetWinner()
        {
            var winningMarker = GetIdenticalMarkerForAllLines();

            if (winningMarker == null)
            {
                winningMarker = GetIdenticalMarkerForAllColumns();
            }

            if (winningMarker == null)
            {
                winningMarker = GetIdenticalMarkerForDiagonal();
            }

            return game.GetPlayerByMarker(winningMarker);
            
        }

        public void Reset()
        {
            Game = new Game();
        }

        private string GetIdenticalMarkerForDiagonal()
        {
            Field[] markersLeftToRight = ExtractMarkerArrayByIterateBoardFromLeftToRight();

            var marker = CheckIfSameMarkerAndReturnIts(markersLeftToRight);

            if (marker == null)
            {
                var markerRightToLeft = ExtractMarkerArrayByIterateBoardFromRightToLeft();

                marker = CheckIfSameMarkerAndReturnIts(markerRightToLeft);
            }

            return marker;

            #region LocalMethods

            Field[] ExtractMarkerArrayByIterateBoardFromLeftToRight()
            {
                return Game.Board.Select((t, i) => t[i]).ToArray();
            }

            Field[] ExtractMarkerArrayByIterateBoardFromRightToLeft()
            {
                List<Field> markers = new List<Field>();
                for (int i = 0; i < Game.Board.Count; i++)
                {
                    int reverseColumnIndex = Game.Board.Count - i - 1;
                    markers.Add(Game.Board[i][reverseColumnIndex]);
                }

                return markers.ToArray();
            }

            #endregion
        }

        private string GetIdenticalMarkerForAllLines()
        {
            string marker = null;
            for (int lineIndex = 0; lineIndex < Game.Board.Count; lineIndex++)
            {
                var row = Game.Board.ElementAt(lineIndex);
                marker = CheckIfSameMarkerAndReturnIts(row);

                if (marker != null)
                {
                    break;
                }
            }

            return marker;
        }

        private string GetIdenticalMarkerForAllColumns()
        {
            IEnumerable<int> enumerable = Enumerable.Range(0, Game.Board.Count).ToList();
            string marker = null;
            for (int colIndex = 0; colIndex < Game.Board.Count; colIndex++)
            {
                var col = enumerable.Select(x => Game.Board[x][colIndex]).ToArray();
               // var col = Game.Board(colIndex);

                marker = CheckIfSameMarkerAndReturnIts(col);
                if (marker != null)
                {
                    break;
                }
            }

            return marker;
        }

        private string CheckIfSameMarkerAndReturnIts(Field[] array)
        {
            string first = array.First().Marker;
            return array.All(m => m.Marker == first) ? first : null;
        }
    }
}
