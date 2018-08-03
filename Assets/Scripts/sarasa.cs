using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sarasa : MonoBehaviour {
    public GameObject smoke;
    public GameObject smoke2;
    public GameObject sound;
    public GameObject directionalLight;
    public GameObject sound2;
    private bool _active;
    private float _timer;
	// Use this for initialization
	void Start () {
        sound.SetActive(false);
        smoke.SetActive(false);
        smoke2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (_active && _timer <= 20) { if (sound2 != null) Destroy(sound2); if (!sound.activeSelf) sound.SetActive(true); if(!smoke.activeSelf)smoke.SetActive(true); if(!smoke2.activeSelf)smoke2.SetActive(true); directionalLight.transform.Rotate(new Vector3(Time.deltaTime * 15, 0, 0)); _timer += Time.deltaTime; }

        else _active = false; //no anda todavia

	}
    void OnTriggerEnter(Collider a)
    {
        if(a.transform.tag == "Player")
        {
            Debug.Log("sarasa");
            _active = true;
        }
}
}
