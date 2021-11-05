using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCharacter : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.25f + Time.deltaTime);

            int index = Random.Range(0, 3);
            animator.SetInteger("IdleIndex", index);
        }
    }
}