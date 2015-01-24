using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollingPreGame : MonoBehaviour 
{
	private float timer;
	public int target;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.GetComponent<RectTransform> ().Translate (Vector3.up * 30 * Time.smoothDeltaTime);
		timer += Time.smoothDeltaTime;
		if(timer > target)
		{
			Application.LoadLevel(Application.loadedLevel+1);
		}
	}
}
