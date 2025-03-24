using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class DialogueCharacter
{
    public string name;
    public Sprite icon;
    

}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public bool isRepeatable;
    public bool isInteractable = false;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!isRepeatable && DialogueManager.Instance.dialogueCountRepeat<1&& !isInteractable)
        {
            if (collision.tag == "Player")
            {
                TriggerDialogue();
            }
        }
        else if(isRepeatable&& !isInteractable)
        {
            if (collision.tag == "Player")
            {
                TriggerDialogue();
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        DialogueManager.Instance.StopDialogueDistance();

    }
    public void Interact(GameObject interactant)
    {
        Debug.Log("Interagiu Com o Dialogo");
        TriggerDialogue();
    }

}