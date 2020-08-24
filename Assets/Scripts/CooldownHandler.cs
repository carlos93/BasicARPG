using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CooldownHandler : MonoBehaviour
{
    static public CooldownHandler instance;

    private Dictionary<int, float> cooldowns;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        cooldowns = new Dictionary<int, float>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCooldowns(Time.deltaTime);
    }

    public float GetAbilityDefaultCooldown()
    {
        return 6.0f;
    }

    public float GetAbilityCooldown(int abilityId)
    {
        float cooldown;
        cooldowns.TryGetValue(abilityId, out cooldown);
        return cooldown;
    }

    public void AddCooldown(int abilityId)
    {
        if (HasCooldown(abilityId))
            return;

        cooldowns.Add(abilityId, GetAbilityDefaultCooldown());
    }

    public bool HasCooldown(int abilityId)
    {
        return cooldowns.ContainsKey(abilityId);
    }

    public void HandleCooldowns(float diff)
    {
        for (int i = 0; i < cooldowns.Count; i++)
            HandleCooldown(cooldowns.ElementAt(i).Key, diff);
    }

    public void HandleCooldown(int abilityId, float diff)
    {
        if (!HasCooldown(abilityId))
            return;

        cooldowns[abilityId] -= diff;

        if (cooldowns[abilityId] <= 0.0f)
            cooldowns.Remove(abilityId);
    }
}
