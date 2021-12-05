using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] GameObject winPanel;

    [SerializeField] GameObject deathPanel;

    public void ToggleWinPanel()
    {
        winPanel.SetActive(!winPanel.activeSelf);
    }

    public void ToggleDeathPanel()
    {
        deathPanel.SetActive(!deathPanel.activeSelf);
    }




}
