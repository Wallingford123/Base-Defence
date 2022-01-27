using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject startingPanel;
    private GameObject currentPanel;

    private void Start()
    {
        currentPanel = startingPanel;
    }
    public void PanelOpen(GameObject panel)
    {
        currentPanel.SetActive(false);
        panel.SetActive(true);
        currentPanel = panel;
    }
}
