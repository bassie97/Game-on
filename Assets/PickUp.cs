using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {
	public static gameObject ItemPickup;

	void OnTrigger(Collider2D pickup){
		ItemPickup = pickup;
	}