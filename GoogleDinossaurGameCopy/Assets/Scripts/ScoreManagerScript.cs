using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManagerScript : MonoBehaviour
{
    
    void Start()
    {
        int highscore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
        GetComponent<TextMeshProUGUI>().SetText(highscore.ToString("D5"));
    }
}
