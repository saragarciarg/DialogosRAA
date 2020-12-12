using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class DialogueManager : MonoBehaviour
{
    public NPC npc;
    public TMP_Text npcName;
    public Image npcImage;

    public GameObject player;

    Queue<string> sentences;

    public GameObject dialoguePanel;
    public TMP_Text displayText;

    public GameObject optionsPanel;
    public Button button1;
    public Button button2;
    public Button button3;
    public Text op1;
    public Text op2;
    public Text op3;

    string activeSentence;
    public float typingSpeed;
    bool chatting;

    Queue<AudioClip> voices;
    AudioSource myAudio;
    AudioClip speakSound;


    void Start()
    {
        sentences = new Queue<string>();
        voices = new Queue<AudioClip>();
        myAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!chatting && other.CompareTag("Player"))
        {   
            chatting = true;
            player = other.gameObject;
            dialoguePanel.SetActive(true);
            optionsPanel.SetActive(false);
            npcName.text = npc.name;
            string url = "Sprites/" + npc.icon;
            npcImage.sprite = Resources.Load<Sprite>(url);
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        sentences.Clear();
        voices.Clear();
        
        foreach (string sentence in npc.GetSentences())
        {
            sentences.Enqueue(sentence);
        }
        foreach (AudioClip voice in npc.GetAudios())
        {
            voices.Enqueue(voice);
        }    
        
        DisplayNextSentence();
       
    }

    public  void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            displayText.text = activeSentence;
            myAudio.clip = speakSound;
            return;
        }

        activeSentence = sentences.Dequeue();
        speakSound = voices.Dequeue();
        myAudio.clip = speakSound;
    
        StopAllCoroutines();
        StartCoroutine(TypeTheSentence(activeSentence));
    }

    IEnumerator TypeTheSentence(string sentence)
    {
        displayText.text = "";
        myAudio.Play();
        foreach (char letter in sentence.ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        myAudio.Stop();

        if (npc.GetOptions().Length > 0)
            ShowOptions(); 
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);

        string[] options = npc.GetOptions();

        op1.text = options[0];
        op2.text = options[1];
        op3.text = options[2];
    }

   void OnTriggerStay(Collider other)
    {
       if (chatting && other.CompareTag("Player"))
       {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ChooseOption(1);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                ChooseOption(2);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                ChooseOption(3);
            }
        }
    }

    private void ChooseOption(int button)
    {
        switch (button)
        {
            case 1:
                NextSentence(1);
                break;
            case 2:
                NextSentence(2);
                break;
            case 3:
                NextSentence(3);
                break;
        }
    }

    private void NextSentence(int curResponseTracker)
    {
        displayText.text = npc.GetSentence(curResponseTracker);
        myAudio.clip = npc.GetAudio(curResponseTracker);
        myAudio.Play(); 

        optionsPanel.SetActive(false);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ClosePanel();
            npc.ChangeRound();
            chatting = false;
            myAudio.Stop();    
            StopAllCoroutines();
           
        }
    }

     private void ClosePanel() 
    {
        dialoguePanel.SetActive(false);
        optionsPanel.SetActive(false);   
         
    }
}
