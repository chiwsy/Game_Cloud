using UnityEngine;
using System.Collections;


public class PlayerOrbit : MonoBehaviour {
	public GameObject Ground;
	public GameObject smoke;
	
	public Camera PlayerCam;
	
	public float lookDamp=6.0f;
	
	public float disDamp=2.0f;
	public float rotDamp=3.0f;
	public float dist=10.0f;
	public float height=10.0f;
	
	public float vel=0.1f;
	public float maxvel=0.15f;
	// Use this for initialization
	public Vector3 gravity;
	public float energy;
	//public float theta;
	//public float phi;
	public float dirt=1.0f;
	public Vector3 innerVel=new Vector3(0.0f,0.1f,0.1f);//x for radius direction, y for theta, z for phi
	public Vector3 innerPos=new Vector3(5010.0f,0.0f,Mathf.PI/2.0f);
	//force and torque;
	public float fv=0.05f;
	public Vector3 friction=new Vector3(2.0f,1.0f,1.0f);
	public Vector3 restvel=new Vector3(0.0f,0.1f,0.1f);
	//public Vector3 rv;
	
	void Start () {
		//Vector3 dir=gameObject.transform.position-Ground.transform.position;
		//gravity=dir.normalized/dir.sqrMagnitude;
		//energy=10;
		//theta=0;
		//phi=Mathf.PI/2;
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
		
		if(Input.GetAxisRaw("Vertical")>0){
			innerVel.x-=innerPos.x*fv*Time.deltaTime;
			//gameObject.rigidbody.AddRelativeForce(-fv*gameObject.transform.up);
		}
		else if(Input.GetAxisRaw("Vertical")<0){
			innerVel.x+=innerPos.x*fv*Time.deltaTime;
			//gameObject.rigidbody.AddRelativeForce(fv*gameObject.transform.up);
		}
		if(Input.GetAxisRaw("Horizontal")>0){
			//gameObject.rigidbody.AddRelativeForce(-fv*gameObject.transform.right);
			if(Mathf.Abs(innerVel.z)<1e-6) innerVel.z+=dirt*fv*Time.deltaTime;
			else{
				float alpha=Mathf.Atan2(innerVel.y,dirt*innerVel.z);
				innerVel.y-=fv*Mathf.Cos(alpha)*Time.deltaTime;
				innerVel.z+=fv*Mathf.Sin(alpha)*Time.deltaTime;
			}
		}
		else if(Input.GetAxisRaw("Horizontal")<0){
			//gameObject.rigidbody.AddRelativeForce(fv*gameObject.transform.right);
			if(Mathf.Abs(innerVel.z)<1e-6) innerVel.z+=dirt*fv*Time.deltaTime;
			else{
				float alpha=Mathf.Atan2(innerVel.y,dirt*innerVel.z);
				innerVel.y+=fv*Mathf.Cos(alpha)*Time.deltaTime;
				innerVel.z-=fv*Mathf.Sin(alpha)*Time.deltaTime;
			}
		}		
		//theta+=.2f*Mathf.PI/360.0f*vel;
		if(Mathf.Abs(innerVel.y)>maxvel) innerVel.y=innerVel.y/Mathf.Abs(innerVel.y)*maxvel;
		if(Mathf.Abs(innerVel.z)>maxvel) innerVel.z=innerVel.z/Mathf.Abs(innerVel.z)*maxvel;
		float tmp=restvel.x;
		restvel=0.1f*new Vector3(0.0f,innerVel.y,innerVel.z).normalized;
		restvel.x=tmp;
		//phi+=.3f*Mathf.PI/360.0f*vel;
		innerPos+=Time.deltaTime*innerVel;
		//theta=innerPos.y;
		//phi=innerPos.z;
		float phi=innerPos.z;
		float theta=innerPos.y;
		
		if(innerPos.z>Mathf.PI){
			phi=2*Mathf.PI-innerPos.z;
			theta=innerPos.y+Mathf.PI;
		}
		else if(innerPos.z<0){
			phi=-innerPos.z;
			theta=innerPos.y+Mathf.PI;
		}
		else{
			phi=innerPos.z;
			theta=innerPos.y;
		}
		if(innerPos.y>2*Mathf.PI)
			innerPos.y-=2*Mathf.PI;
		if(innerPos.y<0)
			innerPos.y+=2*Mathf.PI;
		if(innerPos.z>2*Mathf.PI)
			innerPos.z-=2*Mathf.PI;
		if(innerPos.z<0)
			innerPos.z+=2*Mathf.PI;
		
		if(innerPos.z>Mathf.PI) dirt=-1.0f;
		else dirt=1.0f;
		
		innerVel=new Vector3(Mathf.Lerp(innerVel.x,restvel.x,friction.x*Time.deltaTime),
		                     Mathf.Lerp(innerVel.y,restvel.y,friction.y*Time.deltaTime),
		                     Mathf.Lerp(innerVel.z,restvel.z,friction.z*Time.deltaTime));
		
		gameObject.transform.position=Vector3.Scale( new Vector3(innerPos.x,innerPos.x,innerPos.x)
		                                            ,new Vector3(Mathf.Sin (phi)*Mathf.Cos(theta),
		                                            Mathf.Cos (phi),
		                                            Mathf.Sin (phi)*Mathf.Sin(theta)));
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
		smoke.transform.position=gameObject.transform.position;
		smoke.transform.rotation=gameObject.transform.rotation;
		PlayerCam.transform.rotation = Quaternion.Slerp(PlayerCam.transform.rotation, rotation, Time.deltaTime * lookDamp);
		if((PlayerCam.transform.position-gameObject.transform.position).magnitude>dist+height){
			//PlayerCam.transform.position=tarPostion;
		}
	}
}
