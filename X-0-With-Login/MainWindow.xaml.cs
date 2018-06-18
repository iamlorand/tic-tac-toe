using System;
using System.Collections.Generic;
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

namespace X_0_With_Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            newGame();
        }

        public bool isLogged { get; set; } = false;
        public string username { get; set; } = "";
        //private int player = 2;
        private int turns = 0;
        //true player turn / false cpu turn
        private bool onturn = true;
        private int s1 = 0;
        private int s2 = 0;
        private int sd = 0;
        /* Non CPU version
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Content == null) {
                if (player % 2 == 0)
                {
                    button.Content = "X";
                    player++;
                    turns++;
                }
                else
                {
                    button.Content = "0";
                    player++;
                    turns++;
                }
                if (checkDraw() == true)
                {
                    MessageBox.Show("Draw game!");
                    sd++;
                    newGame();
                }

                if (checkWinner() == true)
                {
                    if ((string)button.Content == "X")
                    {
                        MessageBox.Show("X won!");
                        s1++;
                        newGame();
                    }
                    else
                    {
                        MessageBox.Show("0 won!");
                        s2++;
                        newGame();
                    }
                }
            }                         
        }*/

        //exit
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, RoutedEventArgs e)
        {
            XWin.Text = "X: " + s1;
            OWin.Text = "O: " + s2;
            Draw.Text = "Draws: " + sd;
        }

        void newGame()
        {
            //player = 2;
            turns = 0;
            onturn = true;
            A00.Content = A01.Content = A02.Content = A10.Content = A11.Content = A12.Content = A20.Content = A21.Content = A22.Content = null;
            XWin.Text = "You: " + s1;
            OWin.Text = "CPU: " + s2;
            Draw.Text = "Draws: " + sd;
        }

        //new game
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            newGame();
        }

        bool checkDraw()
        {
            return ((turns == 9) && (checkWinner() == false)) ? true : false;
        }

        bool checkWinner()
        {
            //orizontal
            if ((A00.Content == A01.Content) && (A01.Content == A02.Content) && (A00.Content != null))
            {
                return true;
            }
            else if ((A10.Content == A11.Content) && (A11.Content == A12.Content) && (A10.Content != null))
            {
                return true;
            }
            else if ((A20.Content == A21.Content) && (A21.Content == A22.Content) && (A20.Content != null))
            {
                return true;
            }

            //vertical
            if ((A00.Content == A10.Content) && (A10.Content == A20.Content) && (A00.Content != null))
            {
                return true;
            }
            else if ((A01.Content == A11.Content) && (A11.Content == A21.Content) && (A01.Content != null))
            {
                return true;
            }
            else if ((A02.Content == A12.Content) && (A12.Content == A22.Content) && (A02.Content != null))
            {
                return true;
            }

            //diagonal
            if ((A00.Content == A11.Content) && (A11.Content == A22.Content) && (A00.Content != null))
            {
                return true;
            }
            else if ((A02.Content == A11.Content) && (A11.Content == A20.Content) && (A02.Content != null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //reset
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            s1 = s2 = sd = 0;
            newGame();
        }

        //logout
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.isLogged = false;
            LoginScreen ls = new LoginScreen();
            ls.Show();
            this.Close();            
        }

        //CPU version
        public Button CPUMoveRandom()
        {
            Button b = null;
            Panel MC = (Panel)this.Content;

            foreach (Button C in MC.Children)
            {
                b = C as Button;
                if (b != null)
                {
                    if (b.Content == null) { return b; }
                }
            }
            return b;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (b.Content == null)
            {
                if (onturn) b.Content = "X";
                else b.Content = "0";
                turns++;
                onturn = !onturn;
            }
            if (checkWinner() == true)
            {
                if(!onturn)
                {
                    s1++;
                    MessageBox.Show("You win!");
                    newGame();
                }
                else
                {
                    s2++;
                    MessageBox.Show("CPU wins!");
                    newGame();
                }
            }
            if (checkDraw() && !checkWinner())
            {
                sd++;
                MessageBox.Show("Its a draw!");
                newGame();
            }
            if (!onturn)
            {
                CPU().Content = "0";
                turns++;
                onturn = !onturn;
            }
            if (checkWinner() == true)
            {
                if (!onturn)
                {
                    s1++;
                    MessageBox.Show("You win!");
                    newGame();
                }
                else
                {
                    s2++;
                    MessageBox.Show("CPU wins!");
                    newGame();
                }
            }
            if (checkDraw() && !checkWinner())
            {
                sd++;
                MessageBox.Show("Its a draw!");
                newGame();
            }
        }

        public Button CPU()
        {
            Button b = null;
            if (!(bool)radioButton1.IsChecked)
            {
                b = CPUTryWinOrDefend("0");
                if (b != null) return b;
                else
                {
                    b = CPUTryWinOrDefend("X");
                    if (b != null) return b;
                    else return CPUMoveRandom();
                }
            }
            else if (!(bool)radioButton2.IsChecked)
            {
                b = CPUMoveRandom();
            }

            return b;
        }

        public Button CPUTryWinOrDefend(string s)
        {
            Button b = null;
            //orizontal 1
            if ((A00.Content == A01.Content) && ((string)A00.Content == s) && (A02.Content == null))
            {
                return A02;
            }
            else if ((A00.Content == A02.Content) && ((string)A00.Content == s) && (A01.Content == null))
            {
                return A01;
            }
            else if ((A01.Content == A02.Content) && ((string)A01.Content == s) && (A00.Content == null))
            {
                return A00;
            }
            //orizontal 2
            else if ((A10.Content == A12.Content) && ((string)A10.Content == s) && (A11.Content == null))
            {
                return A11;
            }
            else if ((A10.Content == A11.Content) && ((string)A10.Content == s) && (A02.Content == null))
            {
                return A02;
            }
            else if ((A12.Content == A11.Content) && ((string)A11.Content == s) && (A10.Content == null))
            {
                return A10;
            }
            //orizontal 3
            else if ((A20.Content == A22.Content) && ((string)A20.Content == s) && (A21.Content == null))
            {
                return A21;
            }
            else if ((A20.Content == A21.Content) && ((string)A20.Content == s) && (A22.Content == null))
            {
                return A22;
            }
            else if ((A22.Content == A21.Content) && ((string)A22.Content == s) && (A20.Content == null))
            {
                return A20;
            }

            //vertical 1
            if ((A00.Content == A10.Content) && ((string)A00.Content == s) && (A20.Content == null))
            {
                return A20;
            }
            else if ((A00.Content == A20.Content) && ((string)A00.Content == s) && (A10.Content == null))
            {
                return A10;
            }
            else if ((A20.Content == A10.Content) && ((string)A10.Content == s) && (A00.Content == null))
            {
                return A00;
            }
            //vertical 2
            else if ((A21.Content == A11.Content) && ((string)A11.Content == s) && (A01.Content == null))
            {
                return A01;
            }
            else if ((A21.Content == A01.Content) && ((string)A01.Content == s) && (A11.Content == null))
            {
                return A11;
            }
            else if ((A01.Content == A11.Content) && ((string)A01.Content == s) && (A21.Content == null))
            {
                return A21;
            }
            //vertical 3
            else if ((A22.Content == A12.Content) && ((string)A12.Content == s) && (A02.Content == null))
            {
                return A02;
            }
            else if ((A20.Content == A02.Content) && ((string)A02.Content == s) && (A11.Content == null))
            {
                return A11;
            }
            else if ((A02.Content == A11.Content) && ((string)A11.Content == s) && (A20.Content == null))
            {
                return A20;
            }

            //diagonal 1
            else if ((A00.Content == A11.Content) && ((string)A11.Content == s) && (A22.Content == null))
            {
                return A22;
            }
            else if ((A22.Content == A00.Content) && ((string)A22.Content == s) && (A11.Content == null))
            {
                return A11;
            }
            else if ((A22.Content == A11.Content) && ((string)A11.Content == s) && (A00.Content == null))
            {
                return A00;
            }
            //diagonal 2
            else if ((A20.Content == A11.Content) && ((string)A11.Content == s) && (A02.Content == null))
            {
                return A02;
            }
            else if ((A20.Content == A02.Content) && ((string)A02.Content == s) && (A11.Content == null))
            {
                return A11;
            }
            else if ((A02.Content == A11.Content) && ((string)A11.Content == s) && (A20.Content == null))
            {
                return A20;
            }

            else return null;
        }
    }
}
