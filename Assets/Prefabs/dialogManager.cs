using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dialogManager : MonoBehaviour {

    public GameObject dialogueContainer;
    public Queue<string> sentences;
    public Text nameText;
    public Text dialogText;
    [Header("segundo dialogo")]
    public float timer;
    

    void Start() {
        sentences = new Queue<string>();
        
    }

    void Update()
    {

        timer += Time.deltaTime;
        if(timer >= 12f) { timer = 0; NextDialogue(); }
       
    }
 

    public void StartDialogue(Dialog dialogue)
    {

            nameText.text = dialogue.name;
   //else y ahi hago que diga otra cosa

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    
        NextDialogue();
    }
  
    public void NextDialogue()
    {
            timer = 0;
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            dialogText.text = sentence;
            //Debug.Log(sentence);
            StopAllCoroutines(); //si al usuario se le da por adelantar el texto antes de que
                                 //termine la animacion, la misma se corta y pasa a la sig
            StartCoroutine(Sentence(sentence));
    }

    IEnumerator Sentence(string sentence)
    {

    
            dialogText.text = "";
  
            foreach (char a in sentence.ToCharArray()) //char array convierte un string en un char array
            {
                dialogText.text += a;
             
                //loopea entre las letras(a)
                yield return new WaitForSeconds(0.045f);

            }
        
    }
    public void EndDialogue()
    {
        Debug.Log("fin del dialogo");
        dialogueContainer.SetActive(false);
    }
}
