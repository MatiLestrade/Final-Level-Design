using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Treasure : MonoBehaviour {

	public AudioClip sound;

	private void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.tag == "Player")
		{
			//AudioSource.PlayClipAtPoint(sound, c.transform.position, 1);

			Destroy(this.gameObject);
		}
	}
}
