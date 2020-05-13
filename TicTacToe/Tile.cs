/* Tic-Tac-Toe Application
 * PROG2370 Assignment 3
 * 
 * Class Name: Tile.cs
 * 
 * Revision History
 *      Tonnicca Gelacio, 2018-November-24: Created
 *      
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public class Tile : PictureBox
    {
        public int row;
        public int column;
        public int tileType;
    }
}
