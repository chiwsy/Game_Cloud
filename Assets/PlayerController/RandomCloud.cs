using UnityEngine;
using System.Collections;

public class RandomCloud : MonoBehaviour {
	//GameObject Cloud;
	// Use this for initialization
	public AudioClip ondeath;
	void Start () {
		//gameObject.transform.position=Random.insideUnitSphere*Radius+Random.Range(10.0f,1000.0f);
		gameObject.transform.localScale=Random.insideUnitSphere*10.0f;
		//gameObject.transform.rotation=Quaternion.LookRotation(Random.insideUnitSphere);
	}
	
	public void die(){
		AudioSource.PlayClipAtPoint(ondeath,gameObject.transform.position);
		Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
