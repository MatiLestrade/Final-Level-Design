using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBricks : MonoBehaviour {
    public float Speed;
    public AudioClip sound;
    public bool fall_;
	private Vector3 posInitial;
    
	public float time;
	private float staticTime;
	// Use this for initialization
	void Start () {
        Speed = 50;
		staticTime = time;
		posInitial = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (fall_)
			time -= Time.deltaTime;

		if(time <=0)
		{
			transform.position += Vector3.down * Time.deltaTime * Speed;
			if (time <= -10)
			{
				fall_ = false;
				time = staticTime;
				transform.position = posInitial;
			}
		}

    }



    public void OnTriggerEnter(Collider a)
    {
        if(a.gameObject.tag == "Player")
        {
           
            fall_ = true;
			
            AudioSource.PlayClipAtPoint(sound, a.transform.position, 100);
        }
    }
}
