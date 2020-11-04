using System;
using System.Collections;
using UnityEditor.iOS;
using UnityEngine;

public enum PlayerState
{
	walk, 
	teleport
}
public class PlayerMovement : MonoBehaviour
{

	public PlayerState currentState;
	public float speed;
	private Rigidbody2D myRigidbody;
	private Vector3 change;
	private Animator animator;
	public GameObject teleportationOrb;
	private TeleportationOrb projectile;
	
	// Cached property indexes
	private static readonly int MoveX = Animator.StringToHash("moveX");
	private static readonly int MoveY = Animator.StringToHash("moveY");
	private static readonly int Moving = Animator.StringToHash("moving");

	// Start is called before the first frame update
    private void Start()
    {
	    animator = GetComponent<Animator>();
	    myRigidbody = GetComponent<Rigidbody2D>();
	    currentState = PlayerState.walk;
	    animator.SetFloat(MoveX, 0);
	    animator.SetFloat(MoveY, -1);
    }

    // Update is called once per frame

    private void Update()
    {
	    change = Vector3.zero;
	    change.x = Input.GetAxisRaw("Horizontal");
	    change.y = Input.GetAxisRaw("Vertical");
	    if (Input.GetButtonDown("teleport") && currentState != PlayerState.teleport)
	    {
		    StartCoroutine(TeleportCo());
	    }

    }
    private void FixedUpdate()
    {
	   
	    UpdateAnimationAndMove(); 
	    
    }

    private void UpdateAnimationAndMove()
    {
	    if (change != Vector3.zero)
	    {
		    MoveCharacter();
		    animator.SetFloat(MoveX, change.x);
		    animator.SetFloat(MoveY, change.y);
		    animator.SetBool(Moving, true);
	    }
	    else
	    {
		    animator.SetBool(Moving, false);
	    }
    }

    private void MoveCharacter()
    {
	    myRigidbody.MovePosition(
		    transform.position + change.normalized * (Time.deltaTime * speed)
		    );
    }

    private IEnumerator TeleportCo()
    {
	    currentState = PlayerState.teleport; 
	    yield return null;
	    MakeTeleportationOrb();
	    yield return new WaitForSeconds(projectile.duration);
	    currentState = PlayerState.walk;
	    
    }

    private void MakeTeleportationOrb()
    {
	    Vector2 temp = new Vector2(animator.GetFloat(MoveX), animator.GetFloat(MoveY)).normalized;
	    projectile = 
		    Instantiate(teleportationOrb, transform.position, Quaternion.identity).GetComponent<TeleportationOrb>();
	    projectile.Setup(temp);
    }
    
    
}

	
