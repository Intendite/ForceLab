using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Slider sliderXP;
    public TMPro.TMP_Text levelText;

    // Update is called once per frame
    void Update()
    {
        UpdateXP(Player.Instance.xpData.currentXP);
        levelText.text = Player.Instance.xpData.currentLevel.ToString();
    }

    public void UpdateXP(int currentXP)
    {
        sliderXP.value = currentXP;
    }

    public void SetXP(int XP)
    {
        sliderXP.value = XP;
    }
}
