using System;
using System.Collections.Generic;
using System.Linq;
using Cesi.TicTacToe.Models;
using Cesi.TicTacToe.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cesi.TicTacToe.UnitTests
{
    public class TicTacToeTests
    {
        public List<Player> Players;
        public string[,] Board;
        internal void Initialize()
        {
            Players = new List<Player>
            {
                new Player {Id = 1, Name = "P1", Marker = "X"},
                new Player {Id = 2, Name = "P2", Marker = "O"}
            };
        }

        internal Player PlayerOne
        {
            get
            {
                return Players.Single(p => p.Id == 1);
            }
        }

        internal Player PlayerTwo
        {
            get
            {
                return Players.Single(p => p.Id == 2);
            }
        }

        // todo faire un test par cas de victoire avec jeu pré-initialisé
        [TestClass]
        public class GameTests : TicTacToeTests
        {
            public GameTests()
            {
                Initialize();
            }

            [TestMethod]
            public void NoWinner()
            {
                GameViewModel game = new GameViewModel();

                Assert.IsNull(game.GetWinner());
            }

            [TestMethod]
            public void P1Wins_AllMarkersInFirstLine()
            {
                Board = new[,] { { "X", "X", "X" }, { "O", "O", "" }, { "", "", "O" } };

                GameViewModel game = GetGame();

                Assert.IsTrue(game.GetWinner() == PlayerOne);
            }

            [TestMethod]
            public void P2Wins_AllMarkersInSecondLine()
            {
                Board = new[,] { { "X", "", "X" }, { "O", "O", "O" }, { "", "", "X" } };

                GameViewModel game = GetGame();

                Assert.True(game.GetWinner() == PlayerTwo);
            }

            [TestMethod]
            public void P1Wins_AllMarkersInThirdLine()
            {
                Board = new[,] { { "", "", "O" }, { "O", "O", "" }, { "X", "X", "X" } };

                Game game = GetGame();

                Assert.True(game.GetWinner() == PlayerOne);
            }

            [TestMethod]
            public void P2Wins_AllMarkersInFirstColumn()
            {
                Board = new[,] { { "O", "X", "X" }, { "O", "O", "" }, { "O", "", "X" } };

                Game game = new Game(Board, Players[0], Players[1]);

                Assert.True(game.GetWinner() == PlayerTwo);
            }

            [TestMethod]
            public void P2Wins_AllMarkersInSecondColumn()
            {
                Board = new[,] { { "X", "O", "X" }, { "O", "O", "" }, { "", "O", "X" } };

                Game game = GetGame();

                Assert.True(game.GetWinner() == PlayerTwo);
            }

            [TestMethod]
            public void P1Wins_AllMarkersInThirdColumn()
            {
                Board = new[,] { { "X", "O", "X" }, { "O", "", "X" }, { "", "O", "X" } };

                Game game = GetGame();

                Assert.True(game.GetWinner() == PlayerOne);
            }

            [TestMethod]
            public void DiagonalAllX_Winner()
            {
                Board = new[,]
                {
                    { "X", "", "" },
                    { "", "X", "" },
                    { "", "", "X" }
                };
                Game game = GetGame();
                Assert.Equal(PlayerOne.Id, game.GetWinner()?.Id);
            }

            [TestMethod]
            public void DiagonalAllO_Winner()
            {
                Board = new[,] 
                {
                    { "", "", "O" },
                    { "", "O", "" },
                    { "O", "", "" }

                };
                Game game = GetGame();
                Assert.Equal(PlayerTwo.Id, game.GetWinner()?.Id);
            }

            private GameViewModel GetGame()
            {
                return new Game(Board, PlayerOne, PlayerTwo);
            }
        }

        // TODO faire 6 tests par joueur et cas de victoire en utilisant la méthode game.Play() avec alternace P1 / P2
        // TODO faire des tests pour toutes les regles du fichier spec/rules
        public class AcceptanceTests : TicTacToeTests
        {
            public AcceptanceTests()
            {
                Initialize();
            }

            [TestMethod(DisplayName = "P1 win with all markers in diagonale")]
            public void P1Wins_AllMarkersDiagonale()
            {
                Game game = new Game();
                game.Play(PlayerOne, 1, 1);
                game.Play(PlayerTwo, 2, 1);

                game.Play(PlayerOne, 1, 2);
                game.Play(PlayerTwo, 0, 2);

                game.Play(PlayerOne, 1, 0);
                game.Play(PlayerTwo, 2, 2);

                game.Play(PlayerOne, 2, 0);

                Player winner = game.GetWinner();

                Assert.Equal(PlayerOne.Id, winner?.Id);
            }

            [TestMethod(DisplayName = "Thrown exception if same player play twice")]
            public void ThrowException_If_Same_Player_Play_Twice()
            {
                Game game = new Game();
                game.Play(PlayerOne, 0, 0);

                Assert.Throws<InvalidOperationException>(() => game.Play(PlayerOne, 1, 0));
            }

            [TestMethod(DisplayName = "Throw exception if marker already placed")]
            public void ThrowException_If_Marker_Already_Placed()
            {
                Game game = new Game();
                game.Play(PlayerOne, 0, 0);
                game.Play(PlayerTwo, 0, 1);

                Assert.Throws<InvalidOperationException>(() => game.Play(PlayerOne, 0, 0));
            }
        }
    }
}
