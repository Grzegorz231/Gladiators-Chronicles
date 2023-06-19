
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPuzzle : MonoBehaviour
{
   
    void Update()
    {
        if (PiecesScript.countPieces == 16)
        {         
            SceneManager.LoadScene("Level2");
        }
    }
}