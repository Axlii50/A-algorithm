using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blank_SFML.Net.Classes
{
    class PathFinding
    {
		public void FindPath(Vector2i startPos, Vector2i targetPos, ref Grid grid)
		{
			Node startNode = grid.grid[startPos.X, startPos.Y];
			Node targetNode = grid.grid[targetPos.X, targetPos.Y];

			List<Node> openSet = new List<Node>();
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0)
			{
				Node node = openSet[0];
				for (int i = 1; i < openSet.Count; i++)
				{
					if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
					{
						if (openSet[i].hCost < node.hCost)
							node = openSet[i];
					}
				}

				openSet.Remove(node);
				closedSet.Add(node);

				if (node == targetNode)
				{
					RetracePath(startNode, targetNode, ref grid);
					return;
				}

				foreach (Node neighbour in grid.GetNeighbours(node))
				{
					if (!neighbour.walkable || closedSet.Contains(neighbour))
					{
						continue;
					}

					int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
					if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
					{
						neighbour.gCost = newCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = node;

						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);
					}
				}
			}
		}

		int GetDistance(Node nodeA, Node nodeB)
		{
			int dstX = Math.Abs(nodeA.gridX - nodeB.gridX);
			int dstY = Math.Abs(nodeA.gridY - nodeB.gridY);

			if (dstX > dstY)
				return 14 * dstY + 10 * (dstX - dstY);
			return 14 * dstX + 10 * (dstY - dstX);
		}

		void RetracePath(Node startNode, Node endNode, ref Grid grid)
		{
			List<Node> path = new List<Node>();
			Node currentNode = endNode;

			path.Add(startNode);

			while (currentNode != startNode)
			{
				path.Add(currentNode);
				currentNode = currentNode.parent;
			}
			path.Reverse();

			foreach (var x in grid.grid)
			{
				if (x.type != TileType.Wall)
				{
					x.walkable = true;
					x.type = TileType.UnDefined;
				}
			}

			foreach(var x in path)
            {
				grid.ChangeNodeType(new Vector2i(x.gridX, x.gridY), TileType.Path);
            }
		}
	}
}
