using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {

        private Random rand = new Random();

        private int[,] board = new int[3, 3];
        private int roundCount = 0;
        private string[] xo = { "X", "O" };
        private int currentPlayer;
        private string[] buttonNames = { "Button00", "Button01", "Button02", "Button10", "Button11", "Button12", "Button20", "Button21", "Button22" };

        public MainWindow()
        {
            InitializeComponent();

            currentPlayer = rand.Next(2) + 1;
            NextTurn();
        }

        private void ClearBoard()
        {
            for (int i = 0; i < buttonNames.Length; i++)
            {
                dynamic button = this.FindName(buttonNames[i]);
                button.Content = null;
            }
            board = new int[3, 3];
            roundCount = 0;
        }

        private void CheckWinner()
        {
            bool won = false;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                 if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != 0)
                {
                    won = true;
                    break;
                }
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != 0)
                {
                    won = true;
                    break;
                }
            }

            if (board[0, 0] == board[1, 1] && board[1,1] == board[2, 2] && board[1, 1] != 0)
            {
                won = true;
            } 
            else if (board[2, 0] == board[1, 1] && board[1,1] == board[0, 2] && board[1, 1] != 0)
            {
                won = true;
            }

            if (won)
            {
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Play Again?", "Player " + currentPlayer + " Won!", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    ClearBoard();
                }
                else
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
            else if (roundCount == 9)
            {
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Play Again?", "It's a Tie!", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.No)
                {
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    ClearBoard();
                }
            }
        }

        private void NextTurn()
        {
            if(currentPlayer == 1)
            {
                currentPlayer = 2;
            } else
            {
                currentPlayer = 1;
            }

            PlayerTurn.Content = "Player " + currentPlayer;
            roundCount++;
        }

        private void GameButtonClicked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button) sender;
            int row = Grid.GetRow(btn) - 1;
            int column = Grid.GetColumn(btn);

            if (!(btn.Content is null))
            {
                return;
            }
            btn.Content = xo[currentPlayer - 1];
            board[row, column] = currentPlayer;

            CheckWinner();
            NextTurn();
        }

        private void MinimizeButtonClicked(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
