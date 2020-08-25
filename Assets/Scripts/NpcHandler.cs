using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

public enum PowerTypes : int
{
    Power_Mana = 0,
    Power_Rage = 1,
    Power_Energy = 2
}

public class NpcHandler : MonoBehaviour
{
    public int level;
    public new string name;
    public PowerTypes powerType;

    [Space]

    public HealthbarHandler healthBar;
    public float maxHealth = 100;
    public float health;
    public float maxPower = 100;
    public float power;
    public float powerRegenRate = 1.0f;

    [Space]

    public LayerMask enemiesMask;
    public GameObject goldPrefab;

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
            if (enemy.gameObject == gameObject)
                continue;

            NpcHandler npc = enemy.GetComponent<NpcHandler>();
            if (npc)
                npc.TakeDamage((spellId + 1) * 5.0f);
        }

        TakePower((spellId + 1) * 5);
    }

    public void Die()
    {
        health = 0;
        gameObject.SetActive(false);
        LootSystem lootSystem = LootSystem.instance;
        LootItems items = lootSystem.GetLootForCreature(1);

        float y = 0.0f;
        Vector3 newPos = transform.position;
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit))
        {
            y = hit.point.y;
            newPos.y = y;
        }

        GameObject gold = Instantiate(goldPrefab, newPos, transform.rotation);
        Destroy(gold, 5.0f);
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        float healthPct = health / maxHealth;

        if (healthBar)
            healthBar.UpdateHealthPct(healthPct);

        if (health <= 0.0f)
            Die();
    }
}
