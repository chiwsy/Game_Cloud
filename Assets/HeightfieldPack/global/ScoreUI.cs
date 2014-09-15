using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreUI : MonoBehaviour {
//	Global globalObj;
	public static GUIText scoreText;
	public static int Score;
	public static List<int> ScoreList=new List<int>();
	// Use this for initialization
	void Awake(){
		DontDestroyOnLoad (this);
	}

	void Start () {
		GameObject g = GameObject.Find ("GlobalObject");
		Global globalObj = g.GetComponent<Global> ();
//		lastScore = 0;

		scoreText = gameObject.GetComponent<GUIText> ();
	}

	void FixedUpdate(){

		}
	// Update is called once per frame
	void Update () {
				GameObject g = GameObject.Find ("GlobalObject");
				Global globalObj = g.GetComponent<Global> ();
				//Score = globalObj.score;
				scoreText.text = Score.ToString ();
				//ScoreList.Add (Score);
				//ScoreList.Sort ();
//				if (SystemInfo.deviceType == DeviceType.Handheld) {
//						scoreText.text = 
//						globalObj.TotalTime.ToString()+'\n'
//					+"Accleration:\t"+Input.acceleration.x.ToString()+ '\n'
//					+"Accleration:\t"+Input.acceleration.y.ToString()+ '\n'
//					+"Accleration:\t"+Input.acceleration.z.ToString()+ '\n'
//					+"Machine gun: \t "+globalObj.cooling.ToString()+'/'+((globalObj.level+1)*5000.0f).ToString()+'\n'
//								+"Curr Aste Num:\t" + globalObj.currentAsterNum.ToString () + '\n'
//								+ "Lives left:   \t" + globalObj.lives.ToString () + '\n'
//								+ "Level:        \t" + globalObj.level.ToString () + '\n'
//								+ "Total Score:  \t" + globalObj.score.ToString () + '\n'
//								+ "Screen region:\t" + Screen.height.ToString () + '\t' + Screen.width.ToString () + '\n'
//								+ "Current coord:\t" + Camera.main.WorldToScreenPoint (globalObj.vel).ToString () + '\n'
//								+ "Ammo Number:  \t" + globalObj.ammo.ToString ();
//				}
		}
}
