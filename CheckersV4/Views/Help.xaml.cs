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
using System.Windows.Shapes;

namespace CheckersV4.Views
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help()
        {
            InitializeComponent();
            HelpMessage();
        }

        private void HelpMessage()
        {
            txtName.Text= "Checkers Sharp";
            txtHelp.Text = "Draughts or checkers is a group of strategy board games for two players which involve diagonal" +
                " moves of uniform game pieces and mandatory captures by jumping over opponent pieces. Draughts developed from alquerque." +
                " The name 'draughts' derives from the verb to draw or to move, whereas 'checkers' derives from the checkered board which" +
                " the game is played on.The most popular forms are English draughts, also called American checkers, played on an 8×8 checkerboard;" +
                " Russian draughts, also played on an 8×8, and international draughts, played on a 10×10 board.There are many other variants played" +
                " on 8×8 boards.Canadian checkers and Singaporean/ Malaysian checkers(also locally known as dum) are played on a 12×12 board." +
                " English draughts was weakly solved in 2007 by a team of Canadian computer scientists led by Jonathan Schaeffer.From the standard" +
                " starting position, both players can guarantee a draw with perfect play.";
            txtEmail.Text = "florin.arhip@stundet.unitbv.ro";
            txtDetails.Text = "Arhip Florin, 10LF291";

        }
    }
}
