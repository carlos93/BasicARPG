using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarHandler : MonoBehaviour
{
    private Camera mainCamera;

    public Image healthBar;

    public Color color = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        healthBar.color = color;
        StartCoroutine(UpdateFacing());
    }

    IEnumerator UpdateFacing()
    {
        while (true)
        {
            transform.eulerAngles = mainCamera.transform.eulerAngles;
            yield return null;
        }
    }

    public void UpdateHealthPct(float healthPct)
    {
        healthBar.fillAmount = healthPct;
    }
}
