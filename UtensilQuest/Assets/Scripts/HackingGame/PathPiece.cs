using UnityEngine;

namespace HackingGame
{

	public class PathPiece : MonoBehaviour
	{
		public const float MULTIPLIER = 10.0f;

		/// <summary>
		/// Does this piece form part of the "live current"	from the start point?
		/// </summary>
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

		public HackingBehaviourScript _board;

		public PathPiece()
		{
			allowUp = false;
			allowRight = false;
			allowDown = false;
			allowLeft = false;
		}

		void Update()
		{
			transform.position = new Vector3(row * MULTIPLIER, col * MULTIPLIER, 0);
		}

	}
}

