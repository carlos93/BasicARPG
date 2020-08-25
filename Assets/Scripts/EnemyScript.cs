using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class EnemyScript : MonoBehaviour
{
    private Transform target;

    public float secondsForIgnore = 5.0f;
    public float moveSpeed = 5.0f;
    public float reachDistance = 3.0f;

    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
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
            float dist = Vector3.Distance(transform.parent.position, target.position);
            if (dist > reachDistance)
            {
                float y = 0.0f;
                if (Physics.Raycast(transform.parent.position, -transform.parent.up, out RaycastHit hit))
                    y = hit.point.y;

                transform.parent.LookAt(target);
                transform.parent.eulerAngles = new Vector3(0.0f, transform.rotation.eulerAngles.y, 0.0f);
                transform.parent.position += transform.forward * moveSpeed * Time.deltaTime;
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
}
