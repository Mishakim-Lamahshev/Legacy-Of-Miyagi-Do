using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public InputAction moveRight=new InputAction(type: InputActionType.Button);
    public InputAction moveLeft=new InputAction(type: InputActionType.Button);
    public InputAction jump=new InputAction(type: InputActionType.Button);
    public InputAction kick=new InputAction(type: InputActionType.Button);
    public InputAction punch=new InputAction(type: InputActionType.Button);
    private GameObject TurnManager;
    private TurnsController turnsManager;
    private Attack attackScript;

    public float moveSpeed = 90f;

    void OnEnable()
    {
        // Enable the Input System actions
        moveRight.Enable();
        moveLeft.Enable();
        jump.Enable();
        kick.Enable();
        punch.Enable();
    }

    void OnDisable()
    {
        // Disable the Input System actions
        moveRight.Disable();
        moveLeft.Disable();
        jump.Disable();
        kick.Disable();
        punch.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        attackScript=gameObject.GetComponent<Attack>();
        if(attackScript==null)
        {
            Debug.Log("Object "+ gameObject.tag+" has no attack script");
        }
        TurnManager=GameObject.FindGameObjectWithTag("Turn Manage");
        turnsManager= TurnManager.GetComponent<TurnsController>();
        if(turnsManager.getIndicator()==1){OnEnable();}
        
    }

    // Update is called once per frame
    void Update()
    {
       if(turnsManager.getIndicator()==1)
       {
        OnEnable();
        moveRight.performed += ctx => MovePlayer(new Vector3(1f, 0f, 0f));
        moveLeft.performed += ctx => MovePlayer(new Vector3(-1f, 0f, 0f));
        jump.performed += ctx => MovePlayer(new Vector3(0f, 7f, 0f));
        punch.performed += ctx => AttackEnemy("Punch");
        kick.performed += ctx => AttackEnemy("Kick");
       }
       else
       {
        OnDisable();
       } 
    }

    void MovePlayer(Vector3 movement)
    {
        // Translate the player's position based on input and speed
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        turnsManager.swapTurn();
    }

    void AttackEnemy(string attack)
    {
        Debug.Log("ATTACK IS :"+attack);
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
        turnsManager.swapTurn();
    }
}
