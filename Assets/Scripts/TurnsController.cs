using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnsController : MonoBehaviour
{
    public GameObject player1,player2;
    private TextMeshPro textGUI;

    private int indicator=1; // 1 for player1 , -1 for player2
    // Start is called before the first frame update
    void Start()
    {
        textGUI=GameObject.FindGameObjectWithTag("Turn Text").GetComponent<TextMeshPro>();
        uptadeGUI();
    }

    public void swapTurn()
    {
        indicator=indicator*-1;
        uptadeGUI();
    }

    public int getIndicator()
    {
        return indicator;
    }

    void uptadeGUI()
    {
        string str= "Turn: ";
        if(indicator==1){str+=player1.tag;}
        else{str+=player2.tag;}
        textGUI.text=str;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
