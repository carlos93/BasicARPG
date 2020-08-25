using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralGame : MonoBehaviour
{
    public DetailsPanelHandler targetDetailsPanel;

    public LayerMask interactLayer;

    Shader generalShader;
    Shader outlinedShader;

    Renderer targetRenderer;

    public Color firstOutlineColor = Color.red;
    public float firstOutlineWidth = 0.05f;

    public Color secondOutlineColor = Color.blue;
    public float secondOutlineWidth = 0.0f;

    private void Start()
    {
        generalShader = Shader.Find("Standard");
        outlinedShader = Shader.Find("Outlined/UltimateOutline");
        LootSystem lootSystem = new LootSystem();
    }

    public void ToggleOutline(Renderer renderer, bool show)
    {
        renderer.material.shader = show ? outlinedShader : generalShader;

        if (show)
        {
            renderer.material.SetColor("_FirstOutlineColor", firstOutlineColor);
            renderer.material.SetFloat("_FirstOutlineWidth", firstOutlineWidth);
            renderer.material.SetColor("_SecondOutlineColor", secondOutlineColor);
            renderer.material.SetFloat("_SecondOutlineWidth", secondOutlineWidth);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, interactLayer))
            {
                NpcHandler npc = hit.transform.GetComponent<NpcHandler>();
                if (!npc)
                    return;

                if (npc.GetComponent<PlayerHandler>())
                    return;

                if (!targetRenderer || targetRenderer.gameObject != npc.gameObject)
                {
                    if (targetRenderer)
                        ToggleOutline(targetRenderer, false);

                    targetRenderer = npc.GetComponentInChildren<MeshRenderer>();
                    ToggleOutline(targetRenderer, true);
                }

                targetDetailsPanel.UpdateData(npc);
                targetDetailsPanel.gameObject.SetActive(true);
            }
            else
            {
                if (targetRenderer)
                {
                    ToggleOutline(targetRenderer, false);
                    targetRenderer = null;
                }

                targetDetailsPanel.gameObject.SetActive(false);
            }

        }
    }
}
