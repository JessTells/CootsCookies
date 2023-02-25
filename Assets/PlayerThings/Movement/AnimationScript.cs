using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        
        Debug.Log(animator.GetBool("isRun"));
        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }


        animator.ResetTrigger("isPickUp");
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            animator.SetTrigger("isPickUp");
        }
    }



}
