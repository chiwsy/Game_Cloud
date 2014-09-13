using UnityEngine;
using System.Collections;
using System.IO;

public class HighScore : MonoBehaviour
{
		GUIText scoreText;
		Global globalObj;
		private string line;

		void OnGUI ()
		{
				GUILayout.BeginArea (new Rect (10, Screen.height / 2 + 100, Screen.width - 10, 200));
				if (GUILayout.Button ("Title Page")) {
					Application.LoadLevel ("TitleScreen");
				}

				if (GUILayout.Button ("New Game")) {
					Application.LoadLevel ("MainGameScene");
				}
		
				if (GUILayout.Button ("Exit")) {
						Application.Quit ();
				}
				GUILayout.EndArea ();
		}

		// Use this for initialization
		void Start ()
		{
				scoreText = gameObject.GetComponent<GUIText> ();
				GameObject g = GameObject.Find ("GlobalObject");
				globalObj = g.GetComponent<Global> ();
				StreamReader sr = new StreamReader (".\\high.txt");
				line = sr.ReadLine ();
				while (line != null) {
						//write the lie to console window
						//Console.WriteLine(line);
						//Read the next line
						line += '\n' + sr.ReadLine ();
				}
		
				//close the file
				sr.Close ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				ScoreUI.scoreText.text = "";
				string tmp;
				scoreText.text = "Your score!\n" + ScoreUI.Score.ToString () + "\n\n\n\nHigh score list:\n";
				for (int i=ScoreUI.ScoreList.Count-1; i>=0&&i>ScoreUI.ScoreList.Count-15; i--) {
						tmp = (ScoreUI.ScoreList.Count-i).ToString () + ".\t\t\t" + ScoreUI.ScoreList [i].ToString () + '\n';
						scoreText.text += tmp;
				}
			
		}
}

