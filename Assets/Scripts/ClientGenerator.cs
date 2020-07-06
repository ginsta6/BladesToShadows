using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class ClientGenerator : MonoBehaviour
{
    public Image image;
    public Text text;
    public string[] dialogueOptions1;
    public string[] dialogueOptions2;
    public string[] dialogueOptions3;
    List<string[]> Dialogue = new List<string[]>();

    private void Start()
    {
            Dialogue.Add(dialogueOptions1);
            Dialogue.Add(dialogueOptions2);
            Dialogue.Add(dialogueOptions3);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Generate();
        }
    }

    public void Generate()
    {
        int imagenr = Random.Range(1, 16);
        int dialogueNr = Random.Range(1, 10);
        image.sprite = Resources.Load<Sprite>("Avatars/" + imagenr.ToString());
        Debug.Log("Number: " + imagenr);
        //Debug.Log(Dialogue[type][imagenr]);
        //text.text = Dialogue[type - 1][imagenr - 1];
        text.text = GetDialogue(dialogueNr);
    }

    public string GetDialogue(int nr)
    {
        string final = "";

        final = File.ReadLines(Application.dataPath + "/Dialogue.txt").Skip(nr - 1).Take(1).First();


        return final;
    }

}
