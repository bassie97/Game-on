using UnityEngine;
using System.Collections;


public class Arm : MonoBehaviour { 
	public Transform BulletTrailPrefab;
	public float throwSpeed = 1000.0f;
	public GameObject throwObject;
	//private Vector3 transformRight;
	Transform firePoint;
	public void Start(){
		firePoint = transform.FindChild("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("no firepoint found");
		}
	}
	private void Update(){
		//Vector3 camPos = 
		//if (camPos.x > 25) {
		//	transformRight = firePoint.
		//} else if (camPos.x < 25) {
		//	transformRight = firePoint.right;
		//}
		if(Input.GetKeyDown(KeyCode.E))
		{
			throwAmmo();
		}
	}
	private void throwAmmo(){
        /*
		GameObject firePointClone;
		firePointClone = (GameObject) Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
		firePointClone.transform.SetParent(BulletTrailPrefab.transform);
		firePointClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * throwSpeed;
        */
	
	}


}
