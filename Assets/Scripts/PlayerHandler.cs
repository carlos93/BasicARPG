using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerTypes : int
{
    Power_Mana = 0,
    Power_Rage = 1,
    Power_Energy = 2
}

public class PlayerHandler : MonoBehaviour
{
    public int playerLevel;
    public string playerName;
    public PowerTypes powerType;

    [Space]

    public float maxHealth = 100;
    public float health;
    public float maxPower = 100;
    public float power;
    public float powerRegenRate = 1.0f;

    [Space]

    public LayerMask enemiesMask;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        power = maxPower;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        UpdatePower();
    }

    private float GetPowerGainPerSecond()
    {
        switch (powerType)
        {
            case PowerTypes.Power_Mana:
                return 1.0f;
            case PowerTypes.Power_Energy:
                return 10.0f;
            case PowerTypes.Power_Rage:
            default:
                return 0.0f;
        }
    }

    private void UpdatePower()
    {
        if (GetPowerPct() == 1.0f)
            return;

        power += GetPowerGainPerSecond() * powerRegenRate * Time.fixedDeltaTime;
        power = Mathf.Clamp(power, 0, maxPower);
    }

    public void TakePower(float powerAmount)
    {
        power = Mathf.Clamp(power - powerAmount, 0, maxPower);
    }

    public float GetHealthPct()
    {
        return health / maxHealth;
    }

    public float GetPowerPct()
    {
        return power / maxPower;
    }

    public void HandleSpell(int spellId)
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 10.0f, enemiesMask);
        foreach (Collider enemy in enemies)
        {
            EnemyScript e = enemy.GetComponent<EnemyScript>();
            if (e)
                e.TakeDamage((spellId + 1) * 5.0f);
        }

        TakePower((spellId + 1) * 5);
    }
}
