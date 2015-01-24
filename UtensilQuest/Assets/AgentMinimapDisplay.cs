using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AgentMinimapDisplay : MonoBehaviour {
    public GameObject prefabAgentMapIcon;
    public GameObject miniMapIconInstance;
    private MiniMapController _controller;

    private GameObject meshNode;

    bool bIsFirstUpdate;
	// Use this for initialization
	void Start () {
        bIsFirstUpdate = true;
        _controller = GameObject.FindObjectOfType<MiniMapController>();


        meshNode = transform.Find("character_player").gameObject;
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

        SetMiniMapIconVisible(meshNode.renderer.isVisible);

	}

    public void SetMiniMapIconVisible(bool bVisible)
    {
        if (miniMapIconInstance)
        {

            miniMapIconInstance.transform.GetChild(0).GetComponent<Image>().enabled = bVisible;
            miniMapIconInstance.transform.GetChild(1).GetComponent<Text>().enabled = bVisible;
        }
    }
}
