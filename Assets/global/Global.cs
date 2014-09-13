using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour
{
		public GameObject objToSpawn;
		public GameObject UFOs;
		public float timer;
		public float hunger;
		public float TotalTime;
		//public int ammo;
		//public Vector3 vel;
		//public float spawnPeriod;
		//public int numberSpawnedEachPeriod;
		public Vector3 originInScreenCoords;
		//public int score;
	
		public float barDisplay; //current progress
		public Vector2 pos = new Vector2 (20, 40);
		public Vector2 size = new Vector2 (160, 160);
		public Texture2D emptyTex;
		public Texture2D fullTex;
		public float hp;
		public Texture2D redTex;
		public Texture2D curTex;
		public int level;
		
		//Cloud control and generating;
		public int CloudNum=20;
		public int currentCloudNum=0;
		public float CloudtimePeriod=0;
		public float CloudrefreshTimeBound=3.0f;
		public float Radius=5000.0f;
		public GameObject Cam;
		
		public int lives;
		public int nuclearNum;
		public float cooling;
		public GUIStyle texCol;
		public Texture2D AmmoTex;
		public float ammoLeft;

		public AudioClip clip;
		void OnGUI ()
		{
				//draw the background:
				GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
				GUI.Box (new Rect (0, 0, size.x, size.y), emptyTex);
				GUI.BeginGroup (new Rect (0, 0, size.x * barDisplay, size.y));
				GUI.Box (new Rect (0, 0, size.x, size.y), curTex);
				GUI.Label (new Rect (size.x / 4, size.y / 5, size.x, size.y), "Hungery value", texCol);
				GUI.EndGroup ();
				GUI.EndGroup ();

//				GUI.BeginGroup (new Rect (pos.x, pos.y - size.y, size.x, size.y));
//				GUI.Box (new Rect (0, 0, size.x, size.y), emptyTex);
//				GUI.BeginGroup (new Rect (0, 0, size.x * hp, size.y));
//				GUI.Box (new Rect (0, 0, size.x, size.y), redTex);
//				GUI.Label (new Rect (size.x / 4, size.y / 5, size.x, size.y), "Fighters Left", texCol);
//				GUI.EndGroup ();
//				GUI.EndGroup ();
//
//				GUI.BeginGroup (new Rect (pos.x, pos.y + size.y, size.x, size.y));
//				GUI.Box (new Rect (0, 0, size.x, size.y), emptyTex);
//				GUI.BeginGroup (new Rect (0, 0, size.x * ammoLeft, size.y));
//				GUI.Box (new Rect (0, 0, size.x, size.y), AmmoTex);
//				GUI.Label (new Rect (size.x / 4, size.y / 5, size.x, size.y), "Ammo in hand", texCol);
//				GUI.EndGroup ();
//				GUI.EndGroup ();
		}

		// Use this for initialization
		void Start ()
		{
				//score = 0;
				timer = 5;
				//spawnPeriod = 5.0f;
				//numberSpawnedEachPeriod = 3;
				TotalTime = 0;
				//ammo = 400;
				nuclearNum = 3;
				cooling = 5000.0f;
				hunger = 100;
				pos = new Vector2 (20, 40);
				size = new Vector2 (360, 30);
				curTex = fullTex;
				level = 0;
				//AsterNum = 10 * (level + 1);
				//currentAsterNum = 0;

				lives = 10;
//		audio.loop = true;

		AudioSource.PlayClipAtPoint(clip,new Vector3(0,0,0));
				originInScreenCoords = Camera.main.WorldToScreenPoint (new Vector3 (0, 0, 0));
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
			CloudtimePeriod+=Time.fixedDeltaTime;
			if(CloudtimePeriod>CloudrefreshTimeBound){
				CloudtimePeriod=0.0f;
				while(currentCloudNum<CloudNum){
					Vector3 innerPos=new Vector3 (Random.Range(Radius,Radius+100.0f),
												Random.Range(0.0f,2*Mathf.PI),
												Random.Range(0.0f,Mathf.PI));
					Vector3 pos=new Vector3(innerPos.x*Mathf.Sin(innerPos.z)*Mathf.Cos(innerPos.y),
				                        innerPos.x*Mathf.Cos(innerPos.z),
				                        innerPos.x*Mathf.Sin (innerPos.z)*Mathf.Sin(innerPos.y));
					if(Physics.Raycast(pos,Cam.transform.position-pos)) continue;
					else{
						currentCloudNum++;
						Instantiate(objToSpawn,pos,Quaternion.identity);
					}
				}
			}
		}

		void Update ()
		{
		barDisplay = hunger / 100.0f;
		TotalTime += Time.deltaTime;
		if (TotalTime > 105.0f) {
			TotalTime = 0;
			hunger -= 5;
		}
		//TotalTime=timer%5;
		//if (TotalTime < Time.fixedTime)
		
		if (hunger > 100) 
			hunger = 100;
		
		if (barDisplay < .2)
			curTex = redTex;
		else if (barDisplay < .7)
			curTex = AmmoTex;
		else
			curTex = fullTex;

		if(hunger<0)
			Application.LoadLevel("HighScore");

		}

}
