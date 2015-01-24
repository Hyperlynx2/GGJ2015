using UnityEngine;
using System;
using System.Collections.Generic;

namespace HackingGame
{
	enum DIRECTION
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	public class HackingBehaviourScript : MonoBehaviour
	{
		public const string PIVOT_POINT = "Pivot point";

		/// <summary>
		/// Prefab to use for game pieces.
		/// </summary>
		public GameObject piecePrefab;

		//private GameObject _pivotPoint;
		public GameObject _pivotPoint; //TODO: fix

		/// <summary>
		/// Number of columns in the grid.
		/// 
		/// 1 BASED
		/// </summary>
		public int gridColumns;

		/// <summary>
		/// Number of rows in the grid
		/// 
		/// 1 BASED
		/// </summary>
		public int gridRows;

		/// <summary>
		/// This field of play. Pieces the player has laid down and obstacle pieces.
		/// 
		/// 0 BASED.
		/// </summary>
		private PathPiece[,] _grid;

		/// <summary>
		/// All pieces available to the player.
		/// </summary>
		private List<PathPiece> _allPieces;

		/// <summary>
		/// Path to the end needs to start from here.
		/// 
		/// 0 BASED
		/// </summary>
		private int _startRow;

		/// <summary>
		/// Move off the board rightwards on this row to complete the maze.
		/// 
		/// 0 BASED.
		/// </summary>
		private int _goalRow;

		/*up    = +y
	  down  = -y
	  right = +x
	  left  = -x
	 */
		
		/*
	each node has 0-4 edges and knows its position in the grid.

	seeing if they've solved the puzzle:

	if a node is live, for each edge:
		look at the node in that direction on the grid. if it has an edge pointing backward:
			set it to live
			recurse
	 */
		
		
		/*Saint's Row maze:

	generate a random path from the start to the end, with a min and max length.

	pick some unchosen nodes at random and mark them as obstacles.

    each node in the path is a piece the player can then use. remove them from the board.

	 */

		private void BuildMaze()
		{
			_allPieces = new List<PathPiece>();
			_grid = new PathPiece[gridRows, gridColumns];
			
			System.Random random = new System.Random();

			_goalRow = random.Next(0, gridRows);
			_startRow = random.Next (0, gridRows);

			//build a path from the goal back towards the start

			Stack<PathPiece> path = new Stack<PathPiece>();

			PathPiece previousPiece = NewPiece();

			previousPiece.row = _goalRow;
			previousPiece.col = gridColumns - 1;

			_grid[previousPiece.row, previousPiece.col] = previousPiece;
			path.Push(previousPiece);

			bool reachedStart = false;
			while(!reachedStart)
			{
				PathPiece newPiece = NewPiece();
				newPiece.row = previousPiece.row;
				newPiece.col = previousPiece.col;

				DIRECTION dir = (DIRECTION)random.Next(0,4);

				switch(dir)
				{
				case DIRECTION.UP:
					newPiece.col++;
					break;

				case DIRECTION.RIGHT:
					newPiece.row++;
					break;

				case DIRECTION.DOWN:
					newPiece.col--;
					break;

				case DIRECTION.LEFT:
					newPiece.row--;
					break;
				}


				print("Next piece: (" + newPiece.row + "," + newPiece.col + ")?");

				//not off edge of board and no piece already there?
				if(newPiece.row >= 0 && newPiece.row < gridRows
				&& newPiece.col >= 0 && newPiece.col < gridColumns
				&& _grid[newPiece.row, newPiece.col] == null)
				{
					_grid[newPiece.row, newPiece.col] = newPiece;

					previousPiece = newPiece;
					path.Push(newPiece);

					if(newPiece.col == 0 && newPiece.row == _startRow)
					{
						reachedStart = true;
					}
				}
				else
				{
					print("...invalid piece");

					//run out of room? backtrack up the stack.
					if((previousPiece.row - 1 < 0 || _grid[previousPiece.row - 1, previousPiece.col] != null) //can't go left 
					&& (previousPiece.row + 1 >= gridRows || _grid[previousPiece.row + 1, previousPiece.col] != null) //can't go right
					&& (previousPiece.col - 1 < 0 || _grid[previousPiece.row, previousPiece.col - 1] != null ) //can't go down
					&& (previousPiece.col + 1 >= gridColumns || _grid[previousPiece.row, previousPiece.col + 1] != null)) //can't go up					   
					{
						print("backtracking");
						path.Pop();
						//_grid[currentPiece.row, currentPiece.col] = null; //don't clear this. use it to see where we've been
						previousPiece = path.Peek();
					}
				}
			}

			//TODO: now add obstacles.

			//first piece in the chain always plugs in to the left wall.
			path.Peek().allowLeft = true; 

			while(path.Count > 1)
			{
				//pop the piece from the pathway stack, and also remove it from the board.
				PathPiece current = path.Pop();
				_grid[current.row, current.col] = null;
				PathPiece next = path.Peek();

				if(next.row < current.row)
				{
					current.allowDown = true;
					next.allowUp = true;
				}
				else if(next.row > current.row)
				{
					current.allowUp = true;
					next.allowDown = true;
				}
				else if(next.col < current.col)
				{
					current.allowLeft = true;
					next.allowRight = true;
				}
				else if(next.col > current.col)
				{
					current.allowRight = true;
					next.allowLeft = true;
				}
				else
				{
					throw new Exception("Sanity check failure: two hack nodes at the same position!");
				}

				_allPieces.Add(current);
			}

			//last piece in the chain always plugs in right, to the end wall
			PathPiece last = path.Pop();
			last.allowRight = true;
			_allPieces.Add(last);
			_grid[last.row, last.col] = null;

			//TODO: shuffle the pieces?

		}

		/// <summary>
		/// Helper method to properly create a piece
		/// </summary>
		private PathPiece NewPiece()
		{
			GameObject newObject = (GameObject)Instantiate(piecePrefab);
			newObject.transform.SetParent(_pivotPoint.transform, false);
			PathPiece newPiece = newObject.GetComponent<PathPiece>();
			newPiece._board = this;
			
			newPiece.row = _goalRow;
			newPiece.col = gridColumns - 1;

			return newPiece;
		}

		// Use this for initialization
		void Start ()
		{
			//_pivotPoint = transform.Find(PIVOT_POINT).gameObject;
			BuildMaze();
		}
		
		// Update is called once per frame
		void Update ()
		{
			/*TODO: see if the maze has been solved. If so, send the "maze solved" signal to the server.
			Matt says this is all the networking we'll need, and that this can otherwise all happen clientside
			 */
		}
	}
}