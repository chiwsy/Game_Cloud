using UnityEngine;
using System.Collections;


public class PlayerOrbit : MonoBehaviour {
	public GameObject Ground;
	public Camera PlayerCam;
	
	public float lookDamp=6.0f;
	
	public float disDamp=2.0f;
	public float rotDamp=3.0f;
	public float dist=2.0f;
	
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
		gameObject.transform.position=new Vector3(505.0f,0.0f,0.0f);
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
		
		
		
		theta+=.2f*Mathf.PI/360.0f;
		
		phi+=.3f*Mathf.PI/360.0f;
		if(theta>2*Mathf.PI)
			theta-=2*Mathf.PI;
		if(phi>2*Mathf.PI)
			phi-=2*Mathf.PI;
		
		gameObject.transform.position=Vector3.Scale( new Vector3(505.0f,505.0f,505.0f)
		         ,new Vector3(Mathf.Sin (phi)*Mathf.Cos(theta),Mathf.Cos (phi),Mathf.Sin (phi)*Mathf.Sin(theta)));
		         
		Vector3 tarPostion=gameObject.transform.position-gameObject.rigidbody.velocity.normalized*dist;
		PlayerCam.transform.position=new Vector3(Mathf.Lerp(PlayerCam.transform.position.x,tarPostion.x,disDamp*Time.deltaTime),
		                                         Mathf.Lerp(PlayerCam.transform.position.y,tarPostion.y,disDamp*Time.deltaTime),
		                                         Mathf.Lerp(PlayerCam.transform.position.z,tarPostion.z,disDamp*Time.deltaTime));
		//PlayerCam.transform.position=gameObject.transform.position*1.05f;
		//Vector3 tarDir=gameObject.transform.position.normalized;
		//PlayerCam.transform.LookAt(Ground.transform.position);
		//PlayerCam.transform.rotation=Quaternion.Slerp(PlayerCam.transform.rotation,Quaternion.Euler(PlayerCam.transform.position.normalized),Time.deltaTime*lookDamp);
		Quaternion rotation = Quaternion.LookRotation(gameObject.transform.position - PlayerCam.transform.position);
		
		PlayerCam.transform.rotation = Quaternion.Slerp(PlayerCam.transform.rotation, rotation, Time.deltaTime * lookDamp);
		//PlayerCam.transform.forward=-gameObject.transform.position.normalized;
		//PlayerCam.transform.up=PlayerCam.transform.position.normalized;
		//PlayerCam.transform.RotateAround(PlayerCam.transform.position,
		//									gameObject.transform.position-PlayerCam.transform.position,
		//									Mathf.Acos(Vector3.Dot(PlayerCam.transform.up.normalized,PlayerCam.transform.position.normalized)));
		//PlayerCam.transform.LookAt(Ground.transform.position);
	}
}
