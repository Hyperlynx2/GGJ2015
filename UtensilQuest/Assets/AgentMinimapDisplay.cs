using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AgentMinimapDisplay : MonoBehaviour {
    public GameObject prefabAgentMapIcon;
    public GameObject miniMapIconInstance;
    private MiniMapController _controller;

    bool bIsFirstUpdate;
	// Use this for initialization
	void Start () {
        bIsFirstUpdate = true;
        _controller = GameObject.FindObjectOfType<MiniMapController>();

        

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 miniMapLocation;

	    if(bIsFirstUpdate)
        {
            miniMapLocation = _controller.GetObjectMapLocation(transform.position);
            //Debug.Log(miniMapLocation);
            GameObject miniMapIcon = Instantiate(prefabAgentMapIcon) as GameObject;
            miniMapIcon.transform.parent = _controller.gameObject.transform;
            miniMapIcon.transform.localPosition = miniMapLocation;

            miniMapIconInstance = miniMapIcon;

            SetMiniMapIconVisible(true);

            bIsFirstUpdate = false;
        }


        miniMapLocation = _controller.GetObjectMapLocation(transform.position);
        miniMapIconInstance.transform.localPosition = miniMapLocation;
	}

    public void SetMiniMapIconVisible(bool bVisible)
    {
        if(miniMapIconInstance)
            miniMapIconInstance.transform.GetChild(0).GetComponent<Image>().enabled = bVisible;
    }
}
