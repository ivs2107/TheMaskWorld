using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
    public Animator anim;

    private void Start()
    {
    }
    public void TriggerDialogue ()
	{
        if (anim != null)
        {
            anim.enabled = true;
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}
