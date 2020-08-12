using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class ClientGenerator : MonoBehaviour
{
    public Image clientSprite;
    public Text text;
    public GameObject slotOne;
    public GameObject slotTwo;
    private Image ImOne;
    private Image ImTwo;
    private int typeOne = 0;
    private int typeTwo = 0;           //0 - none, 1 - melee, 2 - ranged, 3 - armor
    public GameObject EndScreen;
    public Text score;


    private void Start()
    {
        ImOne = slotOne.GetComponent<Image>();
        ImTwo = slotTwo.GetComponent<Image>();
        Generate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Generate();
        }
    }

    public void SellToClient()
    {
        
        SellItem(slotOne, typeOne);
        if (slotTwo.activeInHierarchy)
        {
            SellItem(slotTwo, typeTwo);
        }


        if (Calendar.instance.CheckWeek())
        {
            if (Calendar.instance.CheckGameEnd())
            {
                SaveManager.instance.Save();
                EndScreen.SetActive(true);
                score.text = "Score: " + Coins.instance.GetBalance();
            }
            else
                SceneManager.LoadScene("Supply");
        }
        else
        {
            Inventory.instance.UpdateSlots();
            Coins.instance.UpdateUI();
            Generate();
            Calendar.instance.AddDay();
        }
        
        
    }


    private void SellItem(GameObject slot, int typeN)
    {
        if (slot.GetComponent<Drop>().InSlot == null)
        {
            return;
        }
        if (slot.GetComponent<Drop>().InSlot.type == typeN)
        {
            Coins.instance.ItemSold(slot.GetComponent<Drop>().InSlot.price * 1.5f);
        }
        else
        {
            Coins.instance.ItemSold(slot.GetComponent<Drop>().InSlot.price * 0.8f);
        }

        Inventory.instance.RemoveItem(slot.GetComponent<Drop>().InSlot);
        slot.GetComponent<Drop>().OnClickReturn();
    }

    public void Generate()
    {
        int imagenr = Random.Range(1, 16);
        int dialogueNr = Random.Range(1, 10);
        clientSprite.sprite = Resources.Load<Sprite>("Avatars/" + imagenr.ToString());
        text.text = GetDialogue(dialogueNr);
        GenerateNeeded(Random.Range(1,3));

    }

    public void GenerateNeeded(int number)
    {
        if (number == 1)
        {
            slotTwo.SetActive(false);
            typeOne = Random.Range(1, 4);
            ImOne.sprite = Resources.Load<Sprite>("Icons/" + typeOne.ToString());
        }
        else
        {
            slotTwo.SetActive(true);
            typeOne = Random.Range(1, 4);
            ImOne.sprite = Resources.Load<Sprite>("Icons/" + typeOne.ToString());
            typeTwo = Random.Range(1, 4);
            ImTwo.sprite = Resources.Load<Sprite>("Icons/" + typeTwo.ToString());
        }
    }

    public string GetDialogue(int nr)
    {
        string final = "";
        final = File.ReadLines(Application.dataPath + "/Dialogue.txt").Skip(nr - 1).Take(1).First();


        return final;
    }

}
