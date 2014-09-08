using UnityEngine;
using System.Collections;


public class PlayerOrbit : MonoBehaviour {
	public GameObject Ground;
	public Camera PlayerCam;
	
	public float lookDamp=6.0f;
	
	public float disDamp=2.0f;
	public float rotDamp=3.0f;
	public float dist=10.0f;
	public float height=10.0f;
	
	public float vel=0.1f;
	// Use this for initialization
	public Vector3 gravity;
	public float energy;
	public float theta;
	public float phi;
	
	void Start () {
		//Vector3 dir=gameObject.transform.position-Ground.transform.position;
		//gravity=dir.normalized/dir.sqrMagnitude;
		//energy=10;
		theta=0;
		phi=Mathf.PI/2;
		gameObject.transform.position=new Vector3(5010.0f,0.0f,0.0f);
		PlayerCam.transform.position=new Vector3(5020.0f,0.0f,0.0f);
		PlayerCam.rigidbody.freezeRotation=true;
	}
	
	void OnCollisionEnter(Collision collision) {
		Collider col=collision.collider;
		if(col.CompareTag("Ground"))
		//ContactPoint contact = collision.contacts[0];
		//Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		//Vector3 pos = contact.point;
		//Instantiate(explosionPrefab, pos, rot) as Transform;
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
		
		Vector3 prePostion=gameObject.transform.position;
		
		
		theta+=.2f*Mathf.PI/360.0f*vel;
		
		phi+=.3f*Mathf.PI/360.0f*vel;
		if(theta>2*Mathf.PI)
			theta-=2*Mathf.PI;
		if(phi>2*Mathf.PI)
			phi-=2*Mathf.PI;
		
		gameObject.transform.position=Vector3.Scale( new Vector3(5050.0f,5050.0f,5050.0f)
		         ,new Vector3(Mathf.Sin (phi)*Mathf.Cos(theta),Mathf.Cos (phi),Mathf.Sin (phi)*Mathf.Sin(theta)));
		gameObject.transform.rotation=Quaternion.LookRotation(gameObject.transform.position-prePostion,gameObject.transform.position);
		Vector3 tarPostion=gameObject.transform.position-2*dist*gameObject.transform.forward+2*height*gameObject.transform.up;
		//gameObject.transform.
		//gameObject.transform.up=gameObject.transform.position.normalized;   
		//gameObject.transform.forward=gameObject.rigidbody.velocity.normalized;
		
		PlayerCam.transform.position=new Vector3(Mathf.Lerp(PlayerCam.transform.position.x,tarPostion.x,disDamp*Time.deltaTime),
		                                         Mathf.Lerp(PlayerCam.transform.position.y,tarPostion.y,disDamp*Time.deltaTime),
		                                         Mathf.Lerp(PlayerCam.transform.position.z,tarPostion.z,disDamp*Time.deltaTime));
		//PlayerCam.transform.position=PlayerCam.transform.position.normalized*gameObject.transform.position.magnitude*1.1f;
		Quaternion rotation = Quaternion.LookRotation(gameObject.transform.position - PlayerCam.transform.position,PlayerCam.transform.position);
		
		PlayerCam.transform.rotation = Quaternion.Slerp(PlayerCam.transform.rotation, rotation, Time.deltaTime * lookDamp);
		if((PlayerCam.transform.position-gameObject.transform.position).magnitude>dist+height){
			//PlayerCam.transform.position=tarPostion;
		}
	}
}
