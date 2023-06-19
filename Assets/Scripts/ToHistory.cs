using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToHistory : MonoBehaviour
{
    public void ToHistoryClick()
    {
        SceneManager.LoadScene("History");
    }

}
