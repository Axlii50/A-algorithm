using Blank_SFML.Net.Classes;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Windows.Forms;

namespace Blank_SFML.Net
{
    class Program
    {
        static RenderWindow _Window = new RenderWindow(new VideoMode((uint)Screen.PrimaryScreen.Bounds.Width, (uint)Screen.PrimaryScreen.Bounds.Height), "Smlf Window", Styles.Fullscreen);
        static bool IsRunning = true;
        public static int Width = 0;
        public static int Height = 0;

        static Grid grid = new Grid();

        static void Main(string[] args)
        {
            Height = Screen.PrimaryScreen.Bounds.Height;
            Width = Screen.PrimaryScreen.Bounds.Width;

            _Window.SetVerticalSyncEnabled(true);

            #region Events
            _Window.KeyPressed += _Window_KeyPressed;
            _Window.MouseButtonPressed += _Window_MouseButtonPressed;
            #endregion

            while (IsRunning)
            {
                _Window.DispatchEvents();

                _Window.Clear(Color.Black);

                PathFinding x = new PathFinding();
                x.FindPath(new SFML.System.Vector2i(0, 0), new SFML.System.Vector2i(29, 24), ref grid);

                grid.Draw(_Window);

                _Window.Display();
            }
        }

        private static void _Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            int x = e.X / 32;
            int y = e.Y / 32;

            if (x <= Grid.gridSizeX && y <= Grid.gridSizeY)
            {
                grid.ChangeNodeType(new SFML.System.Vector2i(x, y), grid.grid[x, y].type == TileType.Wall ? TileType.UnDefined : TileType.Wall);
            }
        }

        private static void _Window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                IsRunning = false;

            if(e.Code == Keyboard.Key.F)
            {
                PathFinding x = new PathFinding() ;
                x.FindPath(new SFML.System.Vector2i(0, 0), new SFML.System.Vector2i(9, 9),ref grid);
                
            }

            if(e.Code == Keyboard.Key.G)
            {
                

            }
        }
    }
}
