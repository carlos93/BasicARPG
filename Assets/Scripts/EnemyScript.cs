using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class EnemyScript : MonoBehaviour
{
    private Transform target;

    public HealthbarHandler healthBar;

    public float maxHealth = 100;
    public float health;

    public float secondsForIgnore = 5.0f;
    public float moveSpeed = 5.0f;
    public float reachDistance = 3.0f;

    public int level = 90;

    public TextMeshProUGUI textLevel;

    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        textLevel.text = level.ToString();
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ChaseTarget()
    {
        while (active)
        {
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist > reachDistance)
            {
                transform.LookAt(target);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }

            yield return null;
        }
    }

    private IEnumerator IgnoreTarget()
    {
        while (true)
        {
            yield return null;
        }
    }

    private void StartAttack(Transform attacker)
    {
        target = attacker;
        StartCoroutine(ChaseTarget());
        StartCoroutine(IgnoreTarget());
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHandler player = other.gameObject.GetComponent<PlayerHandler>();
        if (!player)
            return;

        if (!target)
            StartAttack(other.transform);
    }

    public void Die()
    {
        active = false;
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        float healthPct = health / maxHealth;
        healthBar.UpdateHealthPct(healthPct);

        if (health <= 0.0f)
        {
            Die();
        }
    }
}
