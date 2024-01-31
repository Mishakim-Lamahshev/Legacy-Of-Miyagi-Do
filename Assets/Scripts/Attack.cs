using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject player, enemy;
    private float punchRange = 2.5f; 
    private float kickRange = 3.5f;
    private SpriteRenderer enemyRenderer; 
    private Color originalColor;
    private LifeManager lifeManage; 

    void Start()
    {
        lifeManage=enemy.GetComponent<LifeManager>();
        enemyRenderer = enemy.GetComponent<SpriteRenderer>();
        if (enemyRenderer == null)
        {
            Debug.Log("Enemy has no image");
        }
        originalColor = enemyRenderer.color;
    }

    public bool inRange(string attack)
    {
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = enemy.transform.position;
        float distance = Vector3.Distance(playerPos, enemyPos);
        Debug.Log("Distance: " + distance);

        switch (attack)
        {
            case "Punch":
                Debug.Log(distance + " <= " + punchRange + " ?????????");
                return distance <= punchRange;

            case "Kick":
                Debug.Log(distance + " <= " + kickRange + " ?????????");
                return distance <= kickRange;

            default:
                return true;
        }
    }

    public void Kick()
    {
        lifeManage.LoseLife(2);
        // Make the enemy character glow in red
        enemyRenderer.flipY = true;
        // Start the coroutine to restore the original color after 0.2 seconds
        StartCoroutine(RestoreOriginal(0.4f));
    }

    public void Punch()
    {
        lifeManage.LoseLife(1);
        // Make the enemy character glow in red
        enemyRenderer.flipY = true;
        // Start the coroutine to restore the original color after 0.2 seconds
        StartCoroutine(RestoreOriginal(0.2f));
    }

    IEnumerator RestoreOriginal(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Restore the original color of the enemy character
        enemyRenderer.flipY = false;
    }
}
