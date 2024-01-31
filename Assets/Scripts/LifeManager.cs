using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int life;
    public string LoseScene;

    // Call this method whenever the character loses a life
    public void LoseLife(int damage)
    {
        life=life-damage;
        if (life <= 0)
        {
            SceneManager.LoadScene(LoseScene);
        }
    }
}
