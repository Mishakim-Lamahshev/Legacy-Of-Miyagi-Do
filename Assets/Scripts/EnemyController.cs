using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject pcCharacter;
    private GameObject TurnManager;
    private TurnsController turnsManager;
    private float moveSpeed = 40f;
    private float jumpForce = 10f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
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
        Vector2 movement = new Vector2(horizontalInput, 0f);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        Debug.Log("Enemy Random Move!");
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        Debug.Log("Enemy Jump!");
    }

    void Attack()
    {
        Debug.Log("Enemy Attack!");
    }
}
