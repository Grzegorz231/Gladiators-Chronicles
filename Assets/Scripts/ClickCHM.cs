using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ClickCHM : MonoBehaviour
{
    string filePath = Path.Combine(Application.streamingAssetsPath, "Spravka.chm");
    public void clickPDF()
    {
        System.Diagnostics.Process.Start(filePath);
    }
}
