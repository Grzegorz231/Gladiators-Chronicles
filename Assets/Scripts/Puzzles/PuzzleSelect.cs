using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSelect : MonoBehaviour
{
    public GameObject startPanel;
    public void SetPuzzlePhoto(Image Photo)
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject.Find("Piece (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Photo.sprite;
        }
        startPanel.SetActive(false);
    }
}