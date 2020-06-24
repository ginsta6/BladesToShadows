using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        int type = Random.Range(1, 4);   // 1 - good   2 - bad   3 - neutral
        int imagenr = Random.Range(1, 4);
        image.sprite = Resources.Load<Sprite>("ClientSprites/" + type.ToString() + "/" + imagenr.ToString());
        Debug.Log("Type: " + type + "number: " + imagenr);
        //Debug.Log(Dialogue[type][imagenr]);
        text.text = Dialogue[type - 1][imagenr - 1];
    }
}
