using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject pcCharacter;
    private GameObject TurnManager;
    private TurnsController turnsManager;
    private float moveSpeed = 90f;
    private Rigidbody2D rb;
    private Attack attackScript;

    // Start is called before the first frame update
    void Start()
    {
        attackScript=gameObject.GetComponent<Attack>();
        rb = GetComponent<Rigidbody2D>();
        TurnManager = GameObject.FindGameObjectWithTag("Turn Manage");
        turnsManager = TurnManager.GetComponent<TurnsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (turnsManager.getIndicator() == -1)
        {

            // Check if it's not already performing an action
            if (!IsInvoking("PerformRandomAction"))
            {
                // Delay the random action
                Invoke("PerformRandomAction", 1.5f);
            }
        }
    }

    void PerformRandomAction()
    {
        StartCoroutine(RandomActionWithDelay());
    }

    IEnumerator RandomActionWithDelay()
    {
        float randomValue = Random.value;

        if (randomValue < 0.33f)
        {
            MoveRandomly();
            turnsManager.swapTurn();
        }
        else if (randomValue < 0.66f)
        {
            Jump();
            turnsManager.swapTurn();
        }
        else
        {
            Attack();
            turnsManager.swapTurn();
        }

        yield return null; // Ensure the coroutine completes
    }

    void MoveRandomly()
    {
        float horizontalInput = Random.Range(-1f, 1f);
        Vector3 movement = new Vector3(horizontalInput, 0f,0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    void Jump()
    {
        Vector3 movement = new Vector3(0f, 7f,0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    void Attack()
    {
        Debug.Log("Enemy Attack!");
        string[] attacks= new string[2]{"Punch","Kick"};
        float randomValue = Random.value;
        int index=0;
        string attack;
        if(randomValue>=0.5f){index=1;}
        attack=attacks[index];
        switch (attack)
        {
            case "Punch":
                Debug.Log("Punch case");
                if(attackScript.inRange(attack))
                {
                    attackScript.Punch();
                }
                else{Debug.Log("Attack "+attack+" is not in range");}
                break;
                
            case "Kick":
            Debug.Log("Kick case");
                if(attackScript.inRange(attack))
                {
                    attackScript.Kick();
                }
                else{Debug.Log("Attack "+attack+" is not in range");}
                break;
                
            default:
                break;
        }
    }
}
