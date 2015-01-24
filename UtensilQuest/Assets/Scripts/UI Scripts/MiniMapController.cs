using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour {

    private Canvas _canvas;

    public GameObject WorldNode;
    public Image MainMapImage;
    //public GameObject TestNode;

    public Transform MinMapBounds;
    public Transform MaxMapBounds;

    private Vector3 _worldMin;
    private Vector3 _worldMax;

    void Start()
    {
        _canvas = GetComponent<Canvas>();

        FindWorldBounds();


        //GetObjectMapLocation(TestNode.transform.position);
    }

    private void FindWorldBounds()
    {
        Vector3 min = new Vector3(100000, 10000, 10000);
        Vector3 max = new Vector3(-100000, -100000, -100000);

        for(int i = 0;i < WorldNode.transform.childCount;++i)
        {
            GameObject go = WorldNode.transform.GetChild(i).gameObject;
            Vector3 goMin = go.renderer.bounds.min;
            Vector3 goMax = go.renderer.bounds.max;

            if (goMin.x < min.x) min.x = goMin.x;
            if (goMin.y < min.y) min.y = goMin.y;
            if (goMin.z < min.z) min.z = goMin.z;

            if (goMax.x > max.x) max.x = goMax.x;
            if (goMax.y > max.y) max.y = goMax.y;
            if (goMax.z > max.z) max.z = goMax.z;
        }

        _worldMin = min;
        _worldMax = max;
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

    public Vector3 GetObjectMapLocation(Vector3 worldPosition)
    {
        float fXLoc = 1 - (worldPosition.x - _worldMin.x) / (_worldMax.x - _worldMin.x);
        float fZLoc = (worldPosition.z - _worldMin.z) / (_worldMax.z - _worldMin.z);

        float MapWidth = MaxMapBounds.localPosition.x - MinMapBounds.localPosition.x;
        float MapHeight = MaxMapBounds.localPosition.y - MinMapBounds.localPosition.y;

        RectTransform rt = MainMapImage.rectTransform;
        float fActualXPos = MinMapBounds.localPosition.x + (fXLoc * MapWidth);
        float fActualZPos = MinMapBounds.localPosition.y - (fZLoc * -MapHeight);
        //Debug.Log(MapWidth);

        Vector3 retVector = new Vector3(fActualXPos, fActualZPos, 0);

        //Debug.Log(retVector);


        return retVector;
    }
}
