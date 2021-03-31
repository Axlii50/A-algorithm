using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blank_SFML.Net.Classes
{
    class Grid
    {
        Texture text1 = new Texture("Textures/Path.png");
        Texture text2 = new Texture("Textures/UnDefined.png");
        Texture text3 = new Texture("Textures/Wall.png");
        public Node[,] grid;
        public static int gridSizeX = 30, gridSizeY = 25;

        public Grid()
        {
            grid = new Node[gridSizeX, gridSizeY];
            for (int i = 0; i < gridSizeX; i++)
            {
                for (int j = 0; j < gridSizeY; j++)
                {
                    grid[i, j] = new Node(true, new SFML.System.Vector2i(i * 32, j * 32), i, j, TileType.UnDefined);
                }
            }

            //grid[5, 5].type = TileType.Wall;
            //grid[5, 5].walkable = false;
            //grid[4, 5].type = TileType.Wall;
            //grid[4, 5].walkable = false;
            //grid[2, 5].type = TileType.Wall;
            //grid[2, 5].walkable = false;

        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }

        public void ChangeNodeType(SFML.System.Vector2i GridPos, TileType e)
        {
            grid[GridPos.X, GridPos.Y].type = e;
            grid[GridPos.X, GridPos.Y].walkable = grid[GridPos.X, GridPos.Y].type == TileType.Wall ? false : true;
        }

        public void Draw(RenderTarget e)
        {
            Sprite c = null;
            foreach (var x in grid)
            {
                switch (x.type)
                {
                    case TileType.Path:
                        c = new Sprite(text1) { Position = new SFML.System.Vector2f(x.worldPosition.X, x.worldPosition.Y) };
                        c.Draw(e, RenderStates.Default);
                        break;

                    case TileType.UnDefined:
                        c = new Sprite(text2) { Position = new SFML.System.Vector2f(x.worldPosition.X, x.worldPosition.Y) };
                        c.Draw(e, RenderStates.Default);
                        break;
                    case TileType.Wall:
                        c = new Sprite(text3) { Position = new SFML.System.Vector2f(x.worldPosition.X, x.worldPosition.Y) };
                        c.Draw(e, RenderStates.Default);
                        break;
                }
            }
        }
    }
}
