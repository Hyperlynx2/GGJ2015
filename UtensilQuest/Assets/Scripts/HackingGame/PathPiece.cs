using UnityEngine;

namespace HackingGame
{

	public class PathPiece : MonoBehaviour
	{
		public bool live;

		public bool allowUp;
		public bool allowRight;
		public bool allowDown;
		public bool allowLeft;

		/// <summary>
		/// 0 BASED
		/// </summary>
		public int row;

		/// <summary>
		/// 0 BASED
		/// </summary>
		public int col;

		public PathPiece()
		{
			allowUp = false;
			allowRight = false;
			allowDown = false;
			allowLeft = false;
		}

		public PathPiece(PathPiece other)
		{
			this.allowUp = false;
			this.allowDown = false;
			this.allowRight = false;
			this.allowLeft = false;
			this.row = other.row;
			this.col = other.col;
		}
	}
}

