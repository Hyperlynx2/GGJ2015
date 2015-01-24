
namespace HackingGame
{


	public class PathPiece
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
	}
}

