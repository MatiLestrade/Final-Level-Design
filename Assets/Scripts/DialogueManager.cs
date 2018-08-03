using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	private Queue<string> sentences;

	

	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
            Debug.Log("termina");
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
		dialogueText.text = sentence;
        Debug.Log("pasa el if");
        StopAllCoroutines(); //si al usuario se le da por adelantar el texto antes de que
                             //termine la animacion, la misma se corta y pasa a la sig
        StartCoroutine(Sentence(sentence));
    }
    IEnumerator Sentence(string sentence)
    {


        dialogueText.text = "";

        foreach (char a in sentence.ToCharArray()) //char array convierte un string en un char array
        {
            dialogueText.text += a;

            //loopea entre las letras(a)
            yield return new WaitForSeconds(0.055f);

        }

    }
    public void EndDialogue()
	{

	}
}
