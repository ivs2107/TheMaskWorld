using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	//public Animator animator;
	private Dialogue dialogue;
	public DialogueTrigger dialogueTriggertoStart;
	private Queue<string> sentences;
	public AudioClip clip;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		dialogueTriggertoStart.TriggerDialogue();
		GameObject.FindGameObjectWithTag("MusicTag").GetComponent<AudioSource>().clip = clip;
		GameObject.FindGameObjectWithTag("MusicTag").GetComponent<AudioSource>().Play();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		//do Animation
		this.dialogue = dialogue;
		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	public void RequestSendSentenceToEveryone()
    {
		if (Client.instance.myId == 1)
		{
			ClientSend.DisplayNextSentenceForAll();
		}
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		if(this.dialogue.nextDialogueTrigger == null && Client.instance.myId == 1)
        {
			ClientSend.ChangeScene(9);
        }
        else
        {
			try
			{
				this.dialogue.nextDialogueTrigger.TriggerDialogue();
			}
            catch { }
		}

	}

}
