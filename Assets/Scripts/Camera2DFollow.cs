using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {
	
	public Transform target;
    public Transform target1;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;
	public float yPosRestriction = -1;
	
	//float offsetZ;
	Vector3 lastTargetPosition;
	Vector3 currentVelocity;
	Vector3 lookAheadPos;

	float nextTimeToSearch = 0;
	
	// Use this for initialization
	void Start () {
        Debug.Log("Start");
		lastTargetPosition = (target.position + target1.position)/2;
		//offsetZ = (transform.position - ((target.position + target1.position) / 2)).z;
		transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {

		if (target == null || target1 == null) {
			FindPlayers ();
			return;
		}

		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (((target.position + target1.position) / 2) - lastTargetPosition).x;

	    bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget) {
			lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
		} else {
			lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);	
		}

        Vector3 aheadTargetPos = ((target.position + target1.position) / 2) + lookAheadPos + Vector3.forward;// * offsetZ;
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

		newPos = new Vector3 (newPos.x, Mathf.Clamp (newPos.y, yPosRestriction, Mathf.Infinity), -1);

		transform.position = newPos;
        Debug.Log(target.position + "and " + target1.position);
        Debug.Log(newPos);
		
		lastTargetPosition = ((target.position + target1.position) / 2);		
	}

	void FindPlayers () {
		if (nextTimeToSearch <= Time.time) {
			GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
			if (searchResult != null)
				target = searchResult.transform;
            searchResult = GameObject.FindGameObjectWithTag("Player1");
            if (searchResult != null)
                target1 = searchResult.transform;
            nextTimeToSearch = Time.time + 0.5f;
		}
	}
}
