using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blank_SFML.Net.Classes
{
	public enum TileType
    {
		Path,
		UnDefined,
		Wall
    }
	public class Node
	{
		public TileType type;

		public bool walkable;
		public Vector2i worldPosition;
		public int gridX;
		public int gridY;

		public int gCost;
		public int hCost;
		public Node parent;

		public Node(bool _walkable, Vector2i _worldPos, int _gridX, int _gridY, TileType e)
		{
			type = e;
			walkable = _walkable;
			worldPosition = _worldPos;
			gridX = _gridX;
			gridY = _gridY;
		}

		public int fCost
		{
			get
			{
				return gCost + hCost;
			}
		}
	}
}
