using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Player m_Player;
    private TextMeshProUGUI m_Text;
        
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Text.text = m_Player.GetHealth().ToString();
    }
}
