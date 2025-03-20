using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    public GameObject dialogueBoxUI;
    public RectTransform dialogueBoxTransform;

    public int dialogueCountRepeat;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.2f;

    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();

    }
    private void Update()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            DisplayNextDialogueLine();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        dialogueBoxUI.SetActive(isDialogueActive);
        //animator.Play("show");

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        if (currentLine.character.icon != null && currentLine.character.name != null)
        {
            characterIcon.sprite = currentLine.character.icon;
            characterName.text = currentLine.character.name;
        }

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            AttBoxDialogueSize(dialogueArea);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBoxUI.SetActive(isDialogueActive);
        dialogueCountRepeat++;
        //animator.Play("hide");
    }
    public void StopDialogueDistance()
    {
        isDialogueActive = false;
        dialogueBoxUI.SetActive(isDialogueActive);
    }
    void AttBoxDialogueSize(TMP_Text dialogueText)
    {
        Vector2 tamanhoTexto = new Vector2(dialogueText.preferredWidth, dialogueText.preferredHeight);
        dialogueBoxTransform.sizeDelta = tamanhoTexto;
    }
}