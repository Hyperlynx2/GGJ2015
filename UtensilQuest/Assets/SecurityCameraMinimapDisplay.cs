using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecurityCameraMinimapDisplay : MonoBehaviour {
    public GameObject prefabMiniMapIcon;
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
	    if(bIsFirstUpdate)
        {
            Vector3 miniMapLocation = _controller.GetObjectMapLocation(transform.position);
            //Debug.Log(miniMapLocation);
            GameObject miniMapIcon = Instantiate(prefabMiniMapIcon) as GameObject;
            miniMapIcon.transform.parent = _controller.gameObject.transform;
            miniMapIcon.transform.localPosition = miniMapLocation;

            miniMapIconInstance = miniMapIcon;
            string id = gameObject.name.Substring("SecurityCam-Top-0".Length, 2);
            int iID = int.Parse(id);
            string actualID = (iID - 1).ToString();
            if (iID - 1 < 10) actualID = "0" + actualID;
            //Debug.Log(id);
            Text label = miniMapIcon.transform.GetChild(1).GetComponent<Text>();
            label.text = actualID;

            SetMiniMapIconVisible(false);

            bIsFirstUpdate = false;
        }
	}

    public void SetMiniMapIconVisible(bool bVisible)
    {
        if(miniMapIconInstance)
            miniMapIconInstance.transform.GetChild(0).GetComponent<Image>().enabled = bVisible;
    }
}
