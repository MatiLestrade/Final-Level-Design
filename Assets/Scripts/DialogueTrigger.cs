using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    private bool _startDialogue;
	public Dialogue dialogue;
  void Awake()
    {
        _startDialogue = true;
    }
    void Update()
    {
        if (_startDialogue)
        {
            TriggerDialogue();
            Debug.Log("entra al update");
            return;
        }
    }
	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        _startDialogue = false;
    }
}
