using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public XPData xpData;
    public Slider sliderXP;
    public TMPro.TMP_Text levelText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Update XP Bar
        sliderXP.value = xpData.currentXP;
        levelText.text = xpData.currentLevel.ToString();
    }

    public void GainXP(int XP)
    {
        xpData.currentXP += XP;
    }

    void LevelUp()
    {
        xpData.currentXP = 0;
        xpData.currentLevel += 1;
    }
}
