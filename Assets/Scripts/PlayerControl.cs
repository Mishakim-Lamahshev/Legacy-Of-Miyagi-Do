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
        moveRight.performed += ctx => MovePlayer(new Vector3(1f, 0f, 0f),"RIGHT STEP ACCEPTED");
        moveLeft.performed += ctx => MovePlayer(new Vector3(-1f, 0f, 0f),"LEFT STEP ACCEPTED");
        jump.performed += ctx => MovePlayer(new Vector3(0f, 7f, 0f),"JUMP ACCEPTED");
        punch.performed += ctx => AttackEnemy("Punch");
        kick.performed += ctx => AttackEnemy("Kick");
       }
       else
       {
        OnDisable();
       } 
    }

    void MovePlayer(Vector3 movement,string log)
    {
        // Translate the player's position based on input and speed
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        Debug.Log(log);
        turnsManager.swapTurn();
        Debug.Log("SWAPPED TURN!");
    }

    void AttackEnemy(string attack)
    {
        Debug.Log(attack+" ACCEPTED");
        turnsManager.swapTurn();
        Debug.Log("SWAPPED TURN!");

    }
}
