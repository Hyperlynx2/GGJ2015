using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Controls the security camera, pans left and right with a delay and the ends.
public class SecurityCamera : MonoBehaviour 
{
    public float DownRotAngle = 35.0f;
    public float RotAngle = 60.0f; 
	public float rotSpeed = 0.001f;
	private float waitTimer = 0.0f;
	public int waitFor = 1;

    public Image miniMapImage;
    private Camera _camera;

    private Quaternion _leftRotDir;
    private Quaternion _rightRotDir;

	public enum rotDirection
	{
		left, right,
	}

	public rotDirection currentDirection;
	// Use this for initialization
	void Start () 
	{
        _camera = transform.GetChild(0).gameObject.GetComponent<Camera>();


        Quaternion downRot = Quaternion.AngleAxis(-DownRotAngle, transform.right);
        transform.rotation = transform.rotation * downRot;

        Quaternion leftRot = Quaternion.AngleAxis(-(RotAngle * 0.5f), transform.up);
        _leftRotDir = transform.rotation * leftRot;

        Quaternion rightRot = Quaternion.AngleAxis((RotAngle * 0.5f), transform.up);
        _rightRotDir = transform.rotation * rightRot;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_camera.enabled)
		{
			if(Input.GetMouseButtonDown(0))
			{
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit))
				{
					if(hit.collider.GetComponent<Door>())
					{
						hit.collider.GetComponent<Door>().SendMessage("Locking", !hit.collider.GetComponent<Door>().isLocked);
					}
				}
			}
		}
		//I'm panning left
		if(currentDirection == rotDirection.left)
        {
            Debug.Log(Quaternion.Dot(transform.rotation, _leftRotDir));
            if(Quaternion.Dot(transform.rotation, _leftRotDir) > 0.999)
            {
                waitTimer += Time.smoothDeltaTime;
                if (waitTimer >= waitFor)
                {
                    waitTimer = 0;
                    currentDirection = rotDirection.right;
                }
            }
            else
            {
                Quaternion rot = Quaternion.AngleAxis(-Time.deltaTime * rotSpeed, transform.up);
                transform.rotation = transform.rotation * rot;
            }
			////transform.rotation.Set(transform.rotation.x, transform.rotation.y + (rotSpeed * Time.smoothDeltaTime), 0, transform.rotation.w);
            //if (transform.rotation.eulerAngles.y <= leftCap)
            //{
            //    waitTimer += Time.smoothDeltaTime;
            //    if (waitTimer >= waitFor)
            //    {
            //        waitTimer = 0;
            //        currentDirection = rotDirection.right;
            //    }
            //}
            //else
			//{
			//	transform.Rotate(Vector3.down * rotSpeed * Time.smoothDeltaTime);
			//}
		}
		//I'm panning right
		if(currentDirection == rotDirection.right)
		{
            if (Quaternion.Dot(transform.rotation, _rightRotDir) > 0.999)
            {
                waitTimer += Time.smoothDeltaTime;
                if (waitTimer >= waitFor)
                {
                    waitTimer = 0;
                    currentDirection = rotDirection.left;
                }
            }
            else
            {
                Quaternion rot = Quaternion.AngleAxis(Time.deltaTime * rotSpeed, transform.up);
                transform.rotation = transform.rotation * rot;
            }
			//transform.rotation.Set(transform.rotation.x, transform.rotation.y - (rotSpeed * Time.smoothDeltaTime), 0, transform.rotation.w);
			//if(transform.rotation.eulerAngles.y >= rightCap)
			//{
			//	waitTimer += Time.smoothDeltaTime;
			//	if(waitTimer >= waitFor)
			//	{
			//		waitTimer = 0;
			//		currentDirection = rotDirection.left;
			//	}
			//}
			//else
			//{
			//	transform.Rotate(Vector3.up * rotSpeed * Time.smoothDeltaTime);
			//}
		}
			
	}
}
