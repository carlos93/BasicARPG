using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralGame : MonoBehaviour
{
    public DetailsPanelHandler targetDetailsPanel;

    public LayerMask interactLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawLine(ray.origin, ray.GetPoint(100.0f));

            if (Physics.Raycast(ray, out RaycastHit hit, 100, interactLayer))
            {
                targetDetailsPanel.gameObject.SetActive(true); 
                Debug.Log(hit.transform.gameObject.name);
            }
            else
                targetDetailsPanel.gameObject.SetActive(false);

        }
    }
}
