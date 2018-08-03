using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChanger : MonoBehaviour {
    public Dialog dialogue;
    public static bool ready;
    private int stop;
    public bool game;
    public void Start()
    {

    }
    public void Update()
    {
        if (!game) { TriggerD(); game = true; }
        //TriggerD();
        //if (b1) TriggerD();
        //if (b2) TriggerD();
        //if (b3) TriggerD();
        //print(enemies);
        if (stop == 0) { ready = true; stop++; print("ready"); }
            //if(Input.GetKeyDown(KeyCode.F))
            //{
            //    ready = true;
            //}
        
    }
    public void TriggerD()
    {
            FindObjectOfType<dialogManager>().StartDialogue(dialogue);
            ready = false;
      
    }
}
