using UnityEngine;
using System;
using System.Collections.Generic;

namespace HackingGame
{

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
			_grid = new PathPiece[gridRows, gridColumns];
			
			System.Random random = new System.Random();

			_goalRow = random.Next(0, gridRows);
			_startRow = random.Next (0, gridRows);

			//build a path from the goal back towards the start

			Stack<PathPiece> path = new Stack<PathPiece>();

			PathPiece currentPiece = NewPiece();

			currentPiece.row = _goalRow;
			currentPiece.col = gridColumns - 1;

			currentPiece.allowRight = true; //so that the current flows off the board to the right

			_grid[currentPiece.row, currentPiece.col] = currentPiece;
			path.Push(currentPiece);

			bool reachedStart = false;
			while(!reachedStart)
			{
				PathPiece newPiece = NewPiece();
				newPiece.row = currentPiece.row;
				newPiece.col = currentPiece.col;

				switch(random.Next(0, 4))
				{
				case 0: //up
					newPiece.col--;
					currentPiece.allowUp = true;
					newPiece.allowDown = true;
					break;

				case 1: //right
					newPiece.row++;
					currentPiece.allowRight = true;
					newPiece.allowLeft = true;
					break;

				case 2: //down
					newPiece.col++;
					currentPiece.allowDown = true;
					currentPiece.allowUp = true;
					break;

				case 3: //left
					newPiece.row--;
					currentPiece.allowLeft = true;
					newPiece.allowRight = true;
					break;
				}

				int newRow = newPiece.row;
				int newCol = newPiece.col;

				print("Next piece: (" + newRow + "," + newCol + ")?");

				//not off edge of board and no piece already there?
				if(newRow > 0 && newRow < gridRows
			    && newCol > 0 && newCol < gridColumns
			    && _grid[newRow, newCol] == null)
				{
					_grid[newRow, newCol] = newPiece;

					currentPiece = newPiece;
					path.Push(newPiece);

					if(newCol == 0 && newRow == _startRow)
					{
						newPiece.allowLeft = true;
						reachedStart = true;
					}
				}
				else
				{
					print("...invalid piece");

					//run out of room? backtrack up the stack.
					if((newRow - 1 < 0 || newCol >= gridColumns || _grid[newRow - 1, newCol] != null) //left

					&& (newRow + 1 > gridRows || newCol >= gridColumns || _grid[newRow + 1, newCol] != null) //right

					&& (newCol - 1 < 0 || newRow >= gridRows || _grid[newRow, newCol - 1] != null ) //down

				    && (newCol + 1 > gridColumns || newRow >= gridRows || _grid[newRow, newCol + 1] != null)) //up					   
					{
						print("backtracking");
						path.Pop();
						//_grid[currentPiece.row, currentPiece.col] = null; //don't clear this. use it to see where we've been
						currentPiece = path.Peek();
					}
				}
			}

			//TODO: now add obstacles.

			//TODO: now that the path is built, remove them from the board and put the pieces in a "pick bin" instead.

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