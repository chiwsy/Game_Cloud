using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {
	public static int mode;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUILayout.BeginArea (new Rect (10, Screen.height / 2 + 100, Screen.width - 10, 200));

		if (GUILayout.Button ("Begin to Float")) {
			//mode=1;
			Application.LoadLevel("MainGameScene");
				}

//		if (GUILayout.Button ("Crazy Mode")) {
//			mode=2;
//			Application.LoadLevel("Roids");
//		}

		if (GUILayout.Button ("High score")) {
			Application.LoadLevel ("HighScore");
				}

		if(GUILayout.Button("Exit")){
			Application.Quit();
		}
		GUILayout.EndArea ();
	}
}
