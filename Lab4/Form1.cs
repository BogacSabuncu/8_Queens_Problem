using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {

        Square[,] board = new Square[8, 8];
        int no_queens = 0;

        public Form1()
        {
            InitializeComponent();

            //set up the board
            set_board();
        }

        public void set_board()
        {
            //set up the board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                        board[i, j] = new Square(100 + (50 * i), 100 + (50 * j), Brushes.White, Brushes.Black);
                    else
                        board[i, j] = new Square(100 + (50 * i), 100 + (50 * j), Brushes.Black, Brushes.White);
                }
            }
        } 

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //paint the baord
            foreach (Square i in board)
            {
                i.paint_square(e.Graphics);
            }

            e.Graphics.DrawString("You have " + no_queens + " queens on the board.", Font, Brushes.Black, new Point(250, 35));//print out the no of queens
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                foreach (Square i in board)
                {
                    i.hint_on = true;
                    Invalidate();
                }
            }
            else
            {
                foreach (Square i in board)
                {
                    i.hint_on = false;
                    Invalidate();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(Square i in board)
            {
                i.clear_square();
         
            }
            no_queens = 0;
            Invalidate();

        }

        public bool check_place(int x, int y)
        {
            if (board[x, y].Q == true)// if the square is full
            {
                {
                    return false;
                }
            }
            for (int i = x; i < 8; i++)//check the right side
            {
                if (board[i, y].Q == true)
                {
                    return false;
                }
            }
            for (int i = x; i >= 0; i--)//check the left side
            {
                if (board[i, y].Q == true)
                {
                    return false;
                }
            }
            for (int i = y; i < 8; i++)//check the top side
            {
                if (board[x, i].Q == true)
                {
                    return false;
                }
            }
            for (int i = y; i >= 0; i--)//check the left side
            {
                if (board[x, i].Q == true)
                {
                    return false;
                }
            }
            
            for (int i = x, j = y; (i < 8-x && j >= 0); i++, j--)//check top right
            {
                if(board[i,j].Q == true)
                {
                    return false;
                }
            }
            
            for (int i = x, j = y;(i < 8-x && j < 8-y); i++, j++)//check bottom right
            {
                if (board[i, j].Q == true)
                {
                    return false;
                }
            }
            for (int i = x, j = y; (i >=0 && j >= 0); i--, j--)//check top left
            {
                if (board[i, j].Q == true)
                {
                    return false;
                }
            }
            for (int i = x, j = y; (i >= 0 && j< 8-y); i--, j++)//check top right
            {
                if (board[i, j].Q == true)
                {
                    return false;
                }
            }
            return true;
        }

        public void on_path(int x, int y, bool on)
        {

            for (int i = x; i < 8; i++)//check the right side
            {
                if (on)
                    board[i, y].no_path++;
                else
                    board[i, y].no_path--;
            }
            for (int i = x; i >= 0; i--)//check the right side
            {
                if (on)
                    board[i, y].no_path++;
                else
                    board[i, y].no_path--;
            }
            for (int i = y; i < 8; i++)//check the right side
            {
                if (on)
                    board[x, i].no_path++;
                else
                    board[x, i].no_path--;
            }
            for (int i = y; i >= 0; i--)//check the right side
            {
                if (on)
                    board[x, i].no_path++;
                else
                    board[x, i].no_path--;
            }
            for (int i = x, j = y; (i < 8 && j >= 0); i++, j--)//check top right
            {
                if (on)
                    board[i, j].no_path++;
                else
                    board[i, j].no_path--;
            }
            for (int i = x, j = y; (i < 8 && j < 8); i++, j++)//check top right
            {
                if (on)
                    board[i, j].no_path++;
                else
                    board[i, j].no_path--;
            }
            for (int i = x, j = y; (i >= 0 && j < 8); i--, j++)//check top right
            {
                if (on)
                    board[i, j].no_path++;
                else
                    board[i, j].no_path--;
            }
            for (int i = x, j = y; (i >= 0 && j >= 0); i--, j--)//check top right
            {
                if (on)
                    board[i, j].no_path++;
                else
                    board[i, j].no_path--;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //reduce the coordinates to squares
            int click_x = (e.X - 100)/50;
            int click_y = (e.Y - 100)/50;

            bool good_place, good_click;
            good_click = false;

            if(click_x >= 0 && click_x < 8 && click_y >= 0 && click_y < 8)
            {
                good_click = true;
            }

            if (good_click)
            {
                if (e.Button == MouseButtons.Left)
                {
                    good_place = check_place(click_x, click_y);//check if its a good place

                    if (good_place)
                    {
                        board[click_x, click_y].Q = true;

                        no_queens++;

                        on_path(click_x, click_y, true);

                        if (no_queens == 8)//Game finished
                            MessageBox.Show("You Did It!");

                        Invalidate();
                    }
                    else
                        System.Media.SystemSounds.Beep.Play();
                }
                if (e.Button == MouseButtons.Right)
                {
                    if (board[click_x, click_y].Q == true)
                    {
                        board[click_x, click_y].Q = false;

                        on_path(click_x, click_y, false);

                        no_queens--;
                        Invalidate();
                    }
                }
            }
        }
    }
}
