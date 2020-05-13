/* Tic-Tac-Toe Application
 * PROG2370 Assignment 3
 * 
 * Form Name: TicTacToe.cs
 * 
 * Revision History
 *      Tonnicca Gelacio, 2018-November-24: Created
 *      Tonnicca Gelacio, 2018-November-25: Code Updated
 *      Tonnicca Gelacio, 2018-November-28: Code Updated
 *      Tonnicca Gelacio, 2018-November-29: Debugging
 *      Tonnicca Gelacio, 2018-December-01: UI Updated
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    /// <summary>
    /// Class for tic-tac-toe game application.
    /// </summary>
    public partial class GameBoard : Form
    {
        #region Declarations

        //Declaration - enum
        enum MyTile
        {
            X,
            O
        }

        //Declarations and Initializations - Global Variables
        MyTile currentTile;
        Tile[,] gameBoard;
        int[,] boardContent;
        int winner;
        int movesCounter = 0;
        Boolean winnerFound = false;
        Boolean vacant = true;

        //Declarations - Constants
        private const int INIT_TOP = 20;
        private const int INIT_LEFT = 20;
        private const int WIDTH = 100;
        private const int HEIGHT = 100;

        private const int MAX_MOVES = 9;
        private const int NUMBER_OF_ROWS = 3;
        private const int NUMBER_OF_COLUMNS = 3;

        #endregion

        #region Private Methods
        /// <summary>
        /// Check if game has a winner.
        /// </summary>
        /// <param name="selectedTile">Selected tile.</param>
        /// <param name="winner">Reference variable to winner.</param>
        /// <returns>True/False if winner is found.</returns>
        public Boolean CheckWinner(Tile selectedTile, ref int winner)
        {
            //Declarations and Initializations
            Boolean winnerFound;
            bool rowWinner = false;
            bool columnWinner = false;
            bool diagonalWinner = false;

            //Check Rows
            if ((boardContent[selectedTile.row, 0] == boardContent[selectedTile.row, 1]) &
                (boardContent[selectedTile.row, 1] == boardContent[selectedTile.row, 2]))
            {
                rowWinner = true;
                winner = selectedTile.tileType;
            }

            //Check Columns
            if ((boardContent[0, selectedTile.column] == boardContent[1, selectedTile.column]) &
                (boardContent[1, selectedTile.column] == boardContent[2, selectedTile.column]))
            {
                columnWinner = true;
                winner = selectedTile.tileType;
            }

            //Check Diagonal
            if ((boardContent[0, 0] == boardContent[1, 1] &
                boardContent[1, 1] == boardContent[2, 2]) ||
                (boardContent[2, 0] == boardContent[1, 1] &
                boardContent[1, 1] == boardContent[0, 2]))
            {
                diagonalWinner = true;
                winner = boardContent[1, 1];
            }

            if (rowWinner == true || columnWinner == true || diagonalWinner == true)
            {
                winnerFound = true;
            }

            else
            {
                winnerFound = false;
            }

            return winnerFound;
        }

        /// <summary>
        /// Check if there are vacant spots left on the game board.
        /// </summary>
        /// <returns>True/False if there are vacancies.</returns>
        public Boolean CheckVacant()
        {
            //Declarations
            int vacantCount;
            Boolean vacant;

            //Initializations
            vacantCount = 0;
            vacant = true;

            //Check for vacant slots
            for (int i = 0; i < boardContent.GetLength(0); i++)
            {
                for (int j = 0; j < boardContent.GetLength(1); j++)
                {
                    if (boardContent[i, j] == 0)
                    {
                        vacantCount++;
                    }
                }
            }

            if (vacantCount > 0)
            {
                vacant = true;
            }

            else
            {
                vacant = false;
            }

            return vacant;
        }

        /// <summary>
        /// Generate the game board.
        /// </summary>
        public void GenerateGameBoard()
        {
            //Initializations
            gameBoard = new Tile[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];
            boardContent = new int[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];

            int x = INIT_LEFT;
            int y = INIT_TOP;

            //Generate picture boxes
            for (int i = 0; i < NUMBER_OF_ROWS; i++)
            {
                for (int j = 0; j < NUMBER_OF_COLUMNS; j++)
                {
                    Tile b = new Tile();
                    gameBoard[i, j] = b;
                    boardContent[i, j] = 0;

                    //Set Properties of Tiles
                    b.Left = x;
                    b.Top = y;
                    b.Width = WIDTH;
                    b.Height = HEIGHT;
                    //b.BorderStyle = BorderStyle.Fixed3D;
                    b.BorderStyle = BorderStyle.FixedSingle;

                    b.row = i;
                    b.column = j;

                    //Add New Controls to Panel
                    pnlLayout.Controls.Add(b);

                    //Assign Event Handler
                    b.Click += B_Click;

                    x += WIDTH;
                }

                //Modify Next Tile
                x = INIT_LEFT;
                y += HEIGHT;
            }
        }

        /// <summary>
        /// Executes once game is over.
        /// </summary>
        public void EndGame()
        {
            //clear array
            for (int i = 0; i < boardContent.GetLength(0); i++)
            {
                for (int j = 0; j < boardContent.GetLength(1); j++)
                {
                    boardContent[i, j] = 0;
                    gameBoard[i, j].tileType = 0;
                }
            }

            //remove  tiles from controls
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    pnlLayout.Controls.Remove(gameBoard[i, j]);
                }
            }

            //set movesCounter to zero
            movesCounter = 0;

            //start new game
            GenerateGameBoard();
        }
        #endregion

        /// <summary>
        /// The default constructor of the class.
        /// </summary>
        public GameBoard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load event of the main form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            GenerateGameBoard();
        }

        /// <summary>
        /// Click event handler of the generated tiles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_Click(object sender, EventArgs e)
        {
            Tile b = (Tile)sender;

            //Check if selected tile is empty
            if (boardContent[b.row, b.column] == 0)
            {
                movesCounter++;

                //Set Tile to X or O
                if (movesCounter % 2 != 0)
                {
                    currentTile = MyTile.X;
                }

                else
                {
                    currentTile = MyTile.O;
                }

                //Change tile image and set tileType property
                switch (currentTile)
                {
                    case MyTile.X:
                        b.Image = TicTacToe.Properties.Resources.x;
                        b.tileType = 1;
                        boardContent[b.row, b.column] = b.tileType;
                        break;

                    case MyTile.O:
                        b.Image = TicTacToe.Properties.Resources.o;
                        b.tileType = 2;
                        boardContent[b.row, b.column] = b.tileType;
                        break;

                    default:
                        break;
                }

                //Check for winner
                if ((movesCounter >= 5) & (movesCounter <= MAX_MOVES))
                {
                    winnerFound = CheckWinner(b, ref winner);
                    vacant = CheckVacant();

                    if (winnerFound == true)
                    {
                        if (winner == 1)
                        {
                            MessageBox.Show("X wins!", "Game Over",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            EndGame();
                        }

                        else if (winner == 2)
                        {
                            MessageBox.Show("O wins!", "Game Over",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            EndGame();
                        }
                    }

                    //winnerFound = false
                    else
                    {
                        if (vacant == false)
                        {
                            MessageBox.Show("Tie.", "Game Over",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            EndGame();
                        }
                    }
                }
            }
        }


    }
}
