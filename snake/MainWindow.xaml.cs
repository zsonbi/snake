using System.Windows;

using System.Windows.Input;

namespace snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game(GameField, ScoreLabel);
            game.run();
        }

        //Keyboard control
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    game.ChangeDirection(0);
                    break;

                case Key.W:
                    game.ChangeDirection(1);
                    break;

                case Key.D:
                    game.ChangeDirection(2);
                    break;

                case Key.S:
                    game.ChangeDirection(3);
                    break;

                case Key.Left:
                    game.ChangeDirection(0);
                    break;

                case Key.Up:
                    game.ChangeDirection(1);
                    break;

                case Key.Right:
                    game.ChangeDirection(2);
                    break;

                case Key.Down:
                    game.ChangeDirection(3);
                    break;

                default:
                    break;
            }
        }

        //-------------------------------------------------------------
        //Starts a new game on click event
        private void newGameBtn_Click(object sender, RoutedEventArgs e)
        {
            game.NewGame();
        }
    }
}