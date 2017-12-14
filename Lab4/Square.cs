using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab4
{
    class Square
    {
        Point corner;
        Brush square_color, square_original_color, q_color, q_color_original;
        public bool Q;
        public bool hint_on;
        Font q_font = new Font("Arial", 30f, FontStyle.Bold);
        public int no_path = 0;

        public Square(int x, int y, Brush s_color, Brush font_color)
        {
            //set the corner
            corner.X = x;
            corner.Y = y;

            //set the square color
            square_color = s_color;
            square_original_color = square_color;

            //set the q color
            q_color = font_color;
            q_color_original = q_color;

            //initialze the bools
            Q = false;
            hint_on = false;

        }

        public void set_color()
        {
            if(no_path> 0)
            {
                if (hint_on)
                {
                    square_color = Brushes.Red;
                    q_color = Brushes.Black;
                }
                else
                {
                    square_color = square_original_color;
                    q_color = q_color_original;
                }   
            }
            else
            {
                square_color = square_original_color;
                q_color = q_color_original;
            }
        }


        public void paint_square(Graphics g)
        {
            set_color();//set the color of the rectangle and q

            g.FillRectangle(square_color, corner.X, corner.Y, 50, 50);

            g.DrawRectangle(Pens.Black, corner.X, corner.Y, 50, 50);

            if (Q)
                g.DrawString("Q", q_font, q_color, corner.X, corner.Y);
    

        }

        public void clear_square()
        {
            no_path = 0;
            Q = false;

        }
    }
}
