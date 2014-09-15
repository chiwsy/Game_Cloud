using UnityEngine;
using System.Collections;
using System;


public class PlayerOrbit : MonoBehaviour {
	public GameObject Ground;
	public GameObject smoke;
	public GameObject rain;
	public GameObject glbObj;
	public AudioClip rainsound;
	public AudioClip windsound;
	
	public Camera PlayerCam;
	
	public float lookDamp=6.0f;
	
	public float disDamp=2.0f;
	public float rotDamp=3.0f;
	public float dist=10.0f;
	public float height=10.0f;
	
	public float vel=0.05f;
	public float maxvel=0.15f;
	// Use this for initialization
	public float turnDamp=5.0f*Mathf.PI/180.0f;
	public float gravity=9.8f;
	public float potential;
	public float kinetic;
	public float energy;
	public float eneloop;
	float Radius;

	float refre=3.0f;
	float cont=0.0f;
	float dirt=1.0f;
	//public float theta;
	//public float phi;
	//float dirt=1.0f;
	Vector3 innerVel=new Vector3(0.0f,0.1f,0.1f);//x for radius direction, y for theta, z for phi
	//Vector3 innerPos=new Vector3(5010.0f,0.0f,Mathf.PI/2.0f);
	//force and torque;
	public float fv=1.0f;
	public Vector3 friction=new Vector3(2.0f,1.0f,1.0f);
	public Vector3 restvel=new Vector3(0.0f,0.05f,0.05f);
	//public Vector3 rv;
	
	void Start () {
		//Vector3 dir=gameObject.transform.position-Ground.transform.position;
		//gravity=dir.normalized/dir.sqrMagnitude;
		//energy=10;
		//theta=0;
		//phi=Mathf.PI/2;
		gameObject.transform.position=new Vector3(5010.0f,0.0f,0.0f);
		PlayerCam.transform.position=new Vector3(5020.0f,0.0f,0.0f);
		//PlayerCam.rigidbody.freezeRotation=true;
		turnDamp=10.0f*Mathf.PI/180.0f;
		Radius=gameObject.transform.position.x;
		kinetic=0.5f*gameObject.rigidbody.mass*gravity*Radius;
		potential=2*kinetic;
		energy=kinetic+potential;
		eneloop=energy/3.0f;
		gameObject.rigidbody.velocity=new Vector3(0.0f,0.0f,1.0f)*Mathf.Sqrt(energy/3.0f*2.0f/gameObject.rigidbody.mass);
		gameObject.transform.rotation.SetLookRotation(gameObject.rigidbody.velocity,gameObject.transform.position);
		glbObj=GameObject.Find("GlobalObject");
	}
	
	void OnTriggerEnter(Collider col) {
		//Collider col=collision.collider;
		if(col.CompareTag("Ground")){
			Destroy(gameObject);
			Application.LoadLevel ("HighScore");
			}
		if(col.CompareTag("Cloud")){
			GameObject obj = GameObject.Find ("GlobalObject");
			Global g = obj.GetComponent<Global> ();
			g.currentCloudNum--;
			g.bonus++;
			eneloop+=1000.0f;
			gameObject.rigidbody.mass+=0.1f;
			if(eneloop>kinetic) eneloop=kinetic;
			//col.GetComponent<Global>().currentCloudNum--;
			col.GetComponent<RandomCloud>().die();
			}
	}
	void FixedUpdate(){
		eneloop-=50.0f*Time.fixedDeltaTime;
		if(eneloop<0) eneloop=0;
		glbObj.GetComponent<Global>().distance+=gameObject.rigidbody.velocity.magnitude*Time.fixedDeltaTime;
		foreach(Transform child in rain.transform){
			child.renderer.enabled=false;
			if(child.name=="FX_Rain"){
				child.position=gameObject.transform.position;
				child.rotation=gameObject.transform.rotation;
			}
			else{
				child.position=gameObject.transform.position.normalized*5000.0f;
				child.rotation=gameObject.transform.rotation;
			}
		}
		if(Input.GetAxisRaw("Vertical")>0){
			//innerVel.x-=innerPos.x*fv*Time.deltaTime;
			//gameObject.rigidbody.AddRelativeForce(-fv*gameObject.transform.up);
			gameObject.rigidbody.AddForce(-fv*gameObject.transform.up);
			foreach(Transform child in rain.transform){
				child.renderer.enabled=true;
				}
			AudioSource.PlayClipAtPoint(rainsound,gameObject.transform.position);
		}
		else if(Input.GetAxisRaw("Vertical")<0){
			//innerVel.x+=innerPos.x*fv*Time.deltaTime;
			gameObject.rigidbody.AddForce(fv*gameObject.transform.up);
			gameObject.rigidbody.mass-=0.01f*Time.fixedDeltaTime;
			if(gameObject.rigidbody.mass<0.1f) Application.LoadLevel ("HighScore");
			foreach(Transform child in rain.transform){
				child.renderer.enabled=true;
			}
			AudioSource.PlayClipAtPoint(rainsound,gameObject.transform.position);
		}
		if(Input.GetAxisRaw("Horizontal")>0){
			//gameObject.rigidbody.AddRelativeForce(-fv*gameObject.transform.right);
			Vector3 localvel=transform.InverseTransformDirection(gameObject.rigidbody.velocity);
			localvel=new Vector3(localvel.z*Mathf.Sin(turnDamp*Time.fixedDeltaTime),localvel.y,localvel.z*Mathf.Cos(turnDamp*Time.fixedDeltaTime));
			//transform.ro
			gameObject.rigidbody.velocity=transform.TransformDirection(localvel);
			AudioSource.PlayClipAtPoint(windsound,gameObject.transform.position);
		}
		else if(Input.GetAxisRaw("Horizontal")<0){
			//gameObject.rigidbody.AddForce(-fv*(gameObject.transform.right-gameObject.transform.forward).normalized);
			Vector3 localvel=transform.InverseTransformDirection(gameObject.rigidbody.velocity);
			localvel=new Vector3(localvel.z*Mathf.Sin(-turnDamp*Time.fixedDeltaTime),localvel.y,localvel.z*Mathf.Cos(turnDamp*Time.fixedDeltaTime));
			//transform.ro
			gameObject.rigidbody.velocity=transform.TransformDirection(localvel);
			AudioSource.PlayClipAtPoint(windsound,gameObject.transform.position);
		}	
		smoke.transform.localScale=new Vector3(10.0f,10.0f,10.0f)*gameObject.rigidbody.mass;
		
		gameObject.rigidbody.AddForce(-gameObject.transform.position.normalized*gravity);
		
		gameObject.transform.rotation=Quaternion.LookRotation(gameObject.rigidbody.velocity,gameObject.transform.position);
		
		
		
		Vector3 tarPostion=gameObject.transform.position-2*dist*gameObject.transform.forward+2*height*gameObject.transform.up;
		//gameObject.transform.
		//gameObject.transform.up=gameObject.transform.position.normalized;   
		//gameObject.transform.forward=gameObject.rigidbody.velocity.normalized;
		
		PlayerCam.transform.position=new Vector3(Mathf.Lerp(PlayerCam.transform.position.x,tarPostion.x,disDamp*Time.fixedDeltaTime),
		                                         Mathf.Lerp(PlayerCam.transform.position.y,tarPostion.y,disDamp*Time.fixedDeltaTime),
		                                         Mathf.Lerp(PlayerCam.transform.position.z,tarPostion.z,disDamp*Time.fixedDeltaTime));
		//PlayerCam.transform.position=PlayerCam.transform.position.normalized*gameObject.transform.position.magnitude*1.1f;
		Quaternion rotation = Quaternion.LookRotation(gameObject.transform.position - PlayerCam.transform.position,PlayerCam.transform.position);
		smoke.transform.position=gameObject.transform.position;
		smoke.transform.rotation=gameObject.transform.rotation;
		
		PlayerCam.transform.rotation = Quaternion.Slerp(PlayerCam.transform.rotation, rotation, Time.fixedDeltaTime * lookDamp);
		potential=gameObject.rigidbody.mass*gravity*gameObject.transform.position.magnitude;
		kinetic=0.5f*gameObject.rigidbody.mass*gameObject.rigidbody.velocity.sqrMagnitude;
		energy=kinetic+potential;
	}
	// Update is called once per frame
	void Update () {
		//innerVel=gameObject.rigidbody.velocity;
			foreach(Transform child in smoke.transform){
				child.renderer.enabled=true;
				child.renderer.material.color=new Color(0.9f,0.9f,0.9f,Mathf.Clamp( eneloop/kinetic,0.1f,1.0f));
			}
			//smoke.renderer.enabled=false;
			//child.renderer.material.color = new Color (1f, 1f, 1f, tr);
		
		//Vector3 prePostion=gameObject.transform.position;
		
			
	}
}
