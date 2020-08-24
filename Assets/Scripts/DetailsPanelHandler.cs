using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailsPanelHandler : MonoBehaviour
{
    public PlayerHandler player;

    public TextMeshProUGUI npcLevel;
    public TextMeshProUGUI npcName;

    public Image healthBar;
    public Image powerBar;

    public Color healthColor = Color.green;
    public Color powerColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.color = healthColor;
        powerBar.color = SelectEnergyColorBasedOnPowerType();

        npcLevel.text = player.playerLevel.ToString();
        npcName.text = player.playerName;

        StartCoroutine(UpdateHealthAndPower());
    }

    IEnumerator UpdateHealthAndPower()
    {
        while (true)
        {
            healthBar.fillAmount = player.GetHealthPct();
            powerBar.fillAmount = player.GetPowerPct();
            yield return null;
        }
    }

    Color SelectEnergyColorBasedOnPowerType()
    {
        switch (player.powerType)
        {
            case PowerTypes.Power_Mana:
                return Color.blue;
            case PowerTypes.Power_Energy:
                return Color.yellow;
            case PowerTypes.Power_Rage:
                return Color.red;
            default:
                return Color.gray;
        }
    }
}
