using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnsController : MonoBehaviour
{
    public GameObject player1, player2;
    public GameObject kickText, punchText; // Reference to the kick and punch text GameObjects
    private TextMeshPro textGUI;
    private Attack attackScript;
    private int indicator = 1; // 1 for player1, -1 for player2

    // Start is called before the first frame update
    void Start()
    {
        textGUI = GameObject.FindGameObjectWithTag("Turn Text").GetComponent<TextMeshPro>();
        attackScript = player1.GetComponent<Attack>();

        // Initially, set the 3D texts as inactive
        kickText.SetActive(false);
        punchText.SetActive(false);

        UpdateGUI();
    }

    public void swapTurn()
    {
        indicator = -indicator;
        UpdateGUI();
    }

    public int getIndicator()
    {
        return indicator;
    }

    void UpdateGUI()
    {
        string str = "Turn: ";
        if (indicator == 1)
        {
            str += player1.tag;

            // Check if in range and set the 3D texts active accordingly
            if (attackScript.inRange("Punch"))
            {
                punchText.SetActive(true);
            }
            else
            {
                punchText.SetActive(false);
            }

            if (attackScript.inRange("Kick"))
            {
                kickText.SetActive(true);
            }
            else
            {
                kickText.SetActive(false);
            }
        }
        else
        {
            str += player2.tag;

            // If it's not player1's turn, set both texts as inactive
            kickText.SetActive(false);
            punchText.SetActive(false);
        }
        textGUI.text = str;
    }
}
