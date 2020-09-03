using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class ClientGenerator : MonoBehaviour
{
    public static ClientGenerator instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    public Image clientSprite;
    public Text text;
    public GameObject slotOne;
    public GameObject slotTwo;
    private Image ImOne;
    private Image ImTwo;
    private int typeOne = 0;
    private int typeTwo = 0;           //0 - none, 1 - melee, 2 - ranged, 3 - armor
    private bool Magic;
    public GameObject EndScreen;
    public Text score;
    public Text gameOver;
    public int personType;

    public Text[] values = new Text[3];
    private static string[] valuesS = new string[3];
    public Slider[] sliders = new Slider[3];
    private static float[] slidersF = new float[3];

    //Odds
    static int safe;
    static int narc;
    static int bad;


    public void ResetSliders()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(slidersF[i]);
            slidersF[i] = 0;
            valuesS[i] = "0";
        }
    }

    private void Start()
    {
        Debug.Log(safe + " " + narc + " " + bad);
        ImOne = slotOne.GetComponent<Image>();
        ImTwo = slotTwo.GetComponent<Image>();
        SetSliders();
        Generate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Generate();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(Magic);
        }
    }

    private bool CheckSlider(ref int nr)
    {
        for (int i = 0; i < 3; i++)
        {
            if (slidersF[i] == 1)
            {
                nr = i;
                return true;
            }
        }

        return false;
    }

    public void SellToClient() //separate in different methods, maybe stops not working randomly
    {
        //Slider adjustment

        if ((slotOne.GetComponent<Drop>().InSlot != null) || (slotTwo.GetComponent<Drop>().InSlot != null))
        {
            sliders[personType - 1].value += 0.1f;
            slidersF[personType - 1] = sliders[personType - 1].value;
            values[personType - 1].text = ((sliders[personType - 1].value) * 100).ToString();
            valuesS[personType - 1] = values[personType - 1].text;
        }

        //Magic item checking

        if ((slotOne.GetComponent<Drop>().InSlot != null) || (slotTwo.GetComponent<Drop>().InSlot != null))
        {
            if (((slotOne.GetComponent<Drop>().InSlot != null) && (slotOne.GetComponent<Drop>().InSlot.magic)) || ((slotTwo.GetComponent<Drop>().InSlot != null) && (slotTwo.GetComponent<Drop>().InSlot.magic)))
            {
                if (Magic) //Wants magic item
                {
                    SellItem(slotOne, typeOne);
                    if (slotTwo.activeInHierarchy)
                    {
                        SellItem(slotTwo, typeTwo);
                    }
                }
                else
                {
                    int rolled = RollDice(safe, narc, bad);
                    Debug.Log("Rolled: " + rolled);
                    switch (rolled)
                    {
                        case 1:
                            SellItem(slotOne, typeOne);
                            if (slotTwo.activeInHierarchy)
                            {
                                SellItem(slotTwo, typeTwo);
                            }
                            break;
                        case 2:
                            ChangeOdds(true);
                            NarcTimer.instance.SetTimer();
                            NarcTimer.instance.UpdateUI();
                            SellItem(slotOne, typeOne);
                            if (slotTwo.activeInHierarchy)
                            {
                                SellItem(slotTwo, typeTwo);
                            }
                            break;
                        case 3:
                            Debug.Log("YE FOHKED OHP MATE");
                            SellItem(slotOne, typeOne);
                            if (slotTwo.activeInHierarchy)
                            {
                                SellItem(slotTwo, typeTwo);
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            SellItem(slotOne, typeOne);
            if (slotTwo.activeInHierarchy)
            {
                SellItem(slotTwo, typeTwo);
            }
        }


        ////Win conditions
        //Sliders
        int nr = 0;
        if (CheckSlider(ref nr))
        {
            SaveManager.instance.Save();
            EndScreen.SetActive(true);
            score.text = " Score: " + Coins.instance.GetBalance();

            switch (nr)
            {
                case 0:
                    gameOver.text = "Townsfolk prevailed!";
                    break;
                case 1:
                    gameOver.text = "Army prevailed!";
                    break;
                case 2:
                    gameOver.text = "Rebels prevaled!";
                    break;
            }
        }
        //
        //Calendar
        if (Calendar.instance.CheckWeek())
        {
            if (Calendar.instance.CheckGameEnd())
            {
                SaveManager.instance.Save();
                EndScreen.SetActive(true);
                score.text = "Score: " + Coins.instance.GetBalance();
                gameOver.text = "The war is over!";
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

    public void SetSliders()
    {
        for (int i = 0; i < 3; i++)
        {
            sliders[i].value = slidersF[i];
            values[i].text = valuesS[i];
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
            Coins.instance.ItemSold(slot.GetComponent<Drop>().InSlot.price * 1.4f);
           
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
        personType = Random.Range(1, 4);
        int dialogueNr = Random.Range(1, 14);
        clientSprite.sprite = Resources.Load<Sprite>("Avatars/" + imagenr.ToString());
        GetDialogue(dialogueNr, personType);

    }

    public void GenerateNeeded(int number, string needed)
    {
        if (number == 1)
        {
            slotTwo.SetActive(false);
            typeOne = int.Parse(needed);
            ImOne.sprite = Resources.Load<Sprite>("Icons/" + typeOne.ToString());
        }
        else
        {
            slotTwo.SetActive(true);
            typeOne = int.Parse(needed[0].ToString());
            ImOne.sprite = Resources.Load<Sprite>("Icons/" + typeOne.ToString());
            typeTwo = int.Parse(needed[1].ToString());
            ImTwo.sprite = Resources.Load<Sprite>("Icons/" + typeTwo.ToString());
        }
    }

    public void GetDialogue(int nr, int type)
    {
        string Final = File.ReadLines(Application.dataPath + "/" + type + ".txt").Skip(nr - 1).Take(1).First();

        string[] split = Final.Split(';');
        Final = split[3];
        string Amount = split[1];
        string Needed = split[2];
        Magic = bool.Parse(split[0]);
        text.text = Final;
        GenerateNeeded(int.Parse(Amount), Needed);

    }

    private int RollDice(int safe, int narc, int bad)
    {
        int dice = Random.Range(0, 101);
        Debug.Log("Dice: " + dice);

        if (dice <= safe)
            return 1;
        if ((dice > safe) && (dice <= safe + narc))
            return 2;
        if (dice > safe + narc)
            return 3;

        return 0;
    }

    public void ChangeOdds(bool narcOdds)
    {
        if (narcOdds)
        {
            narc = 20;
            bad = 50;
        }
        else
        {
            safe = 30;
            narc = 60;
            bad = 10;
        }
        Debug.Log(safe + " " + narc + " " + bad);
    }

}
