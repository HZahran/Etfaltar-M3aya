using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{

    void Start()
    {
        Cursor.visible = true;
        GetComponent<Text>().text = "Purity Reached " + PlayerPrefs.GetInt("score") + " %";
    }
}
