using System.Windows;

using System.Windows.Input;

namespace snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game Game;

        public MainWindow()
        {
            InitializeComponent();
            Game = new Game(GameField, ScoreLabel);
            Game.run();
        }

        //Keyboard control
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    Game.ChangeDirection(0);
                    break;

                case Key.W:
                    Game.ChangeDirection(1);
                    break;

                case Key.D:
                    Game.ChangeDirection(2);
                    break;

                case Key.S:
                    Game.ChangeDirection(3);
                    break;

                case Key.Left:
                    Game.ChangeDirection(0);
                    break;

                case Key.Up:
                    Game.ChangeDirection(1);
                    break;

                case Key.Right:
                    Game.ChangeDirection(2);
                    break;

                case Key.Down:
                    Game.ChangeDirection(3);
                    break;

                default:
                    break;
            }
        }
    }
}