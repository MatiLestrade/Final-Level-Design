using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialogue : MonoBehaviour {
    public GameObject dialogue;
    public static bool active;
    void Awake()
    {
        dialogue.SetActive(false);
    }
  

    void OnTriggerEnter(Collider collid)
    {
        if(collid.transform.tag == "Player")
        {
            dialogue.SetActive(true);
            Destroy(gameObject);
            active = true;
        }
    }
}
