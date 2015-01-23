using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
	public string sceneName;

	public void clickButton()
	{
		System.Console.WriteLine("ChangeScene");
		Debug.Log("debug.log");
		print("print");
		Application.LoadLevel(sceneName);
	}

	
}
