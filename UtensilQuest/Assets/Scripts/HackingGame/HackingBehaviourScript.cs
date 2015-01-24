using UnityEngine;
using System;
using System.Collections.Generic;

namespace HackingGame
{

	public class HackingBehaviourScript : MonoBehaviour
	{
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

			//build a path from the goal back towards the start

			Stack<PathPiece> path = new Stack<PathPiece>();

			PathPiece currentPiece = new PathPiece(_goalRow, gridColumns -1);
			currentPiece.allowRight = true;
			_grid[currentPiece.row, currentPiece.col] = currentPiece;

			path.Push(currentPiece);

			bool reachedStart = false;
			while(!reachedStart)
			{
				int nextRow = currentPiece.row;
				int nextCol = currentPiece.col;

				switch(random.Next(0, 4))
				{
				case 0: //up
					nextCol--;
					break;

				case 1: //right
					nextRow++;
					break;

				case 2: //down
					nextCol++;
					break;

				case 3: //left
					nextRow--;
					break;
				}

				if(nextRow > 0 && nextRow < gridRows
				&& nextCol > 0 && nextCol < gridColumns
				&& _grid[nextRow, nextCol] == null)
				{
				}
			}

		}






		// Use this for initialization
		void Start ()
		{
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
	}
}