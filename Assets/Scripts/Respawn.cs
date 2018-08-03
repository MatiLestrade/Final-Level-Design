using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	public GameObject Spawn;
	

	private void OnTriggerEnter(Collider b)
	{
		if (b.gameObject.tag == "Player")
		{
			b.transform.position = Spawn.transform.position;
		}
	}
}
