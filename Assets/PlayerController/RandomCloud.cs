using UnityEngine;
using System.Collections;

public class RandomCloud : MonoBehaviour {
	//GameObject Cloud;
	// Use this for initialization
	
	void Start () {
		//gameObject.transform.position=Random.insideUnitSphere*Radius+Random.Range(10.0f,1000.0f);
		gameObject.transform.localScale=Random.insideUnitSphere*10.0f;
	}
	
	public void die(){
		Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
