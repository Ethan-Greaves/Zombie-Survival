using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI m_ScoreText;

    private void Awake()
    {
        //Get the text component of the game object
        m_ScoreText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_ScoreText.text = GameManager.Instance().GetScore().ToString();
    }
}
