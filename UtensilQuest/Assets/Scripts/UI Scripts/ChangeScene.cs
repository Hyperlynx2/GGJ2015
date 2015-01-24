using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
	public string sceneName;

	public void clickButton()
	{
		System.Console.WriteLine("ChangeScene");
		
		Application.LoadLevel(sceneName);
	}

	
}
