using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailsPanelHandler : MonoBehaviour
{
    public NpcHandler target;

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

        if (target)
        {
            UpdateData(target);
            StartCoroutine(UpdateHealthAndPower());
        }
    }

    IEnumerator UpdateHealthAndPower()
    {
        while (true)
        {
            healthBar.fillAmount = target.GetHealthPct();
            powerBar.fillAmount = target.GetPowerPct();
            yield return null;
        }
    }

    Color SelectEnergyColorBasedOnPowerType(PowerTypes powerType)
    {
        switch (powerType)
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

    public void OnDisable()
    {
        StopCoroutine(UpdateHealthAndPower());
    }

    public void OnEnable()
    {
        StartCoroutine(UpdateHealthAndPower());
    }

    public void UpdateData(NpcHandler npc)
    {
        target = npc;
        npcLevel.text = target.level.ToString();
        npcName.text = target.name;
        powerBar.color = SelectEnergyColorBasedOnPowerType(target.powerType);
    }
}
