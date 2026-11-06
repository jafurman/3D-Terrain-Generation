using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public const string BatteryPercentKey = "BatteryPercent";
    public const string Item1CountKey = "Item1Count";
    public const string Item2CountKey = "Item2Count";

    public TMP_Text batteryPercentText;
    public TMP_Text item1CountText;
    public TMP_Text item2CountText;

    void Start()
    {
        PlayerPrefs.SetFloat(BatteryPercentKey, 100f);
        /*
        if (!PlayerPrefs.HasKey(BatteryPercentKey))
        {
            PlayerPrefs.SetFloat(BatteryPercentKey, 100f);
        }
        */
        if (!PlayerPrefs.HasKey(Item1CountKey))
        {
            PlayerPrefs.SetInt(Item1CountKey, 0); 
        }
        if (!PlayerPrefs.HasKey(Item2CountKey))
        {
            PlayerPrefs.SetInt(Item2CountKey, 0); 
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        batteryPercentText.text = "Battery: " + GetBatteryPercent().ToString("F2") + "%";
        item1CountText.text = "Item 1: " + GetItem1Count().ToString();
        item2CountText.text = "Item 2: " + GetItem2Count().ToString();
    }

    // Getters
    public float GetBatteryPercent()
    {
        return PlayerPrefs.GetFloat(BatteryPercentKey);
    }

    public int GetItem1Count()
    {
        return PlayerPrefs.GetInt(Item1CountKey);
    }

    public int GetItem2Count()
    {
        return PlayerPrefs.GetInt(Item2CountKey);
    }

    // Setters
    public void SetBatteryPercent(float value)
    {
        PlayerPrefs.SetFloat(BatteryPercentKey, value);
    }

    public void SetItem1Count(int count)
    {
        PlayerPrefs.SetInt(Item1CountKey, count);
    }

    public void SetItem2Count(int count)
    {
        PlayerPrefs.SetInt(Item2CountKey, count);
    }

    public void SaveInventory()
    {
        PlayerPrefs.Save();
    }
}
