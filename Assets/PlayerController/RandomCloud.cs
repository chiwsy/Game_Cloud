using UnityEngine;
using System.Collections;

public class RandomCloud : MonoBehaviour {
	//GameObject Cloud;
	// Use this for initialization
	void Start () {
		gameObject.transform.localScale=Random.insideUnitSphere*10.0f;
	}
	void OnTriggerEnter(Collider colder){
		
	}
	public void die(){
		Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
