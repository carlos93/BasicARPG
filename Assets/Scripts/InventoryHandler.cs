using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryHandler : MonoBehaviour
{
    public GameObject inventory;

    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInventory()
    {
        inventory.SetActive(true);
        opened = true;
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
        opened = false;
    }
}
