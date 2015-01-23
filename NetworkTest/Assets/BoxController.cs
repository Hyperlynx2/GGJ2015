using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour {
    public float fMoveSpeed = 10.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Network.isClient) return;

        Vector3 moveDir = new Vector3();

        if(Input.GetKey(KeyCode.W))
        {
            moveDir += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir += new Vector3(0, 0, -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDir += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir += new Vector3(-1, 0, 0);
        }

        moveDir.Normalize();
        moveDir *= fMoveSpeed;

        transform.position += moveDir;
	}
}
