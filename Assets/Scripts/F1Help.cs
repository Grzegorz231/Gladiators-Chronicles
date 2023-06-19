using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F1Help : MonoBehaviour
{
    //контекстный помощь
    public GameObject f1helpMainMenu;
    public GameObject screenMainMenu;
    
    private void Update()
    {
        F1HelpMainMenu();
    }
    public void F1HelpMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (f1helpMainMenu.activeInHierarchy == false)
            {
                f1helpMainMenu.SetActive(true);
                screenMainMenu.SetActive(false);
            }
            else
            {
                f1helpMainMenu.SetActive(false);
                screenMainMenu.SetActive(true);
            }
        }
    }
}
