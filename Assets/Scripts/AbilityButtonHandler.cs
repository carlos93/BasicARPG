using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonHandler : MonoBehaviour
{
    public int ButtonId;
    public float initialCooldown = 6.0f;

    private Text text;

    private float cooldown;

    private Image image;

    public PlayerHandler player;

    private CooldownHandler cooldownHandler;

    private void Start()
    {
        cooldownHandler = CooldownHandler.instance;
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        cooldown = initialCooldown;
        text.text = cooldown.ToString();
    }

    private void Update()
    {
        cooldown = cooldownHandler.GetAbilityCooldown(ButtonId);
        if (cooldown <= 0.0f)
        {
            cooldown = initialCooldown;
            text.text = cooldown.ToString();
            image.color = Color.white;
            text.color = Color.black;
        }
        else
        {
            cooldown -= Time.deltaTime;
            text.text = cooldown.ToString("F1");
        }
    }

    public void ClickAbility()
    {
        if (cooldownHandler.HasCooldown(ButtonId))
            return;

        player.HandleSpell(ButtonId);

        image.color = new Color(0.9f, 0.1f, 0.1f, 0.9f);
        text.color = new Color(1.0f, 1.0f, 1.0f, 0.9f);
        cooldownHandler.AddCooldown(ButtonId);
    }
}
