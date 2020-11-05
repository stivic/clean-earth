using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIMovement : MonoBehaviour
{
    
    private Animator animator;
    public AIPath aiPath;
    private Vector2 direction;
    
    
    // Cached property indexes
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int Moving = Animator.StringToHash("moving");
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = aiPath.desiredVelocity;
        if (direction != Vector2.zero)
        {
            direction = direction.normalized;
            animator.SetFloat(MoveX, direction.x);
            animator.SetFloat(MoveY, direction.y);
            animator.SetBool(Moving, true);
        }
        else
        {
            animator.SetBool(Moving, false);
        }
    }
}
