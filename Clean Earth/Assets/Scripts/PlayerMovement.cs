using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float speed;
	private Rigidbody2D _myRigidbody;
	private Vector3 _change;
	private Animator _animator;
	
	// Cached property indexes
	private static readonly int MoveX = Animator.StringToHash("moveX");
	private static readonly int MoveY = Animator.StringToHash("moveY");
	private static readonly int Moving = Animator.StringToHash("moving");

	// Start is called before the first frame update
    private void Start()
    {
	    _animator = GetComponent<Animator>();
	    _myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
	    _change = Vector3.zero;
	    _change.x = Input.GetAxisRaw("Horizontal");
	    _change.y = Input.GetAxisRaw("Vertical");
	    UpdateAnimationAndMove();
    }

    private void UpdateAnimationAndMove()
    {
	    if (_change != Vector3.zero)
	    {
		    MoveCharacter();
		    _animator.SetFloat(MoveX, _change.x);
		    _animator.SetFloat(MoveY, _change.y);
		    _animator.SetBool(Moving, true);
	    }
	    else
	    {
		    _animator.SetBool(Moving, false);
	    }
    }

    private void MoveCharacter()
    {
	    _myRigidbody.MovePosition(
		    transform.position + _change.normalized * (Time.deltaTime * speed)
		    );
    }
}
