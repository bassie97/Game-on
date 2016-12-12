using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody2D))]
public class Ammo : MonoBehaviour {
    public int damage = 30;
    public Vector3 direction;
	public float speed = 5f;
	private Rigidbody2D arigidBody;
	private float timeTolive = 4f;
	private float timeToStop = 3f;
	private float timeCount = 0f;
	void Start () {
		arigidBody = GetComponent<Rigidbody2D> ();
	}
	void Update(){
		timeCount += Time.deltaTime;
		if(timeCount > timeTolive)
		{
			GameObject.Destroy(this.gameObject);    
		}
	}
	public void Initialize(Vector3 direction){
		this.direction = direction;
	}
	void FixedUpdate(){
		arigidBody.velocity = direction * speed;
		arigidBody.drag = arigidBody.drag + 2f;
		arigidBody.gravityScale = arigidBody.gravityScale + 0.2f;
	}

}
