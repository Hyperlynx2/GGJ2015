using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour {

    private Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

	void Update()
    {
        if (Network.isServer) { return;  } //NO MAP FOR YOU!

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            _canvas.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _canvas.enabled = false;
        }
    }
}
