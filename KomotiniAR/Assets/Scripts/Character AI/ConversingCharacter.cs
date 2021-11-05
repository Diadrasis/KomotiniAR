using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversingCharacter : MonoBehaviour
{
    #region Variables
    Animator animator;

    [HideInInspector]
    public bool isConversing = false;
    #endregion

    #region Unity Functions
    void Awake()
    {
        isConversing = false;
        animator = GetComponent<Animator>();

        ConversingCharacters conversingCharacters = transform.parent.GetComponent<ConversingCharacters>();
        conversingCharacters.OnStartConversing += SetConversing;
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.25f + Time.deltaTime);

            if (isConversing)
            {
                int index = Random.Range(0, 2);
                animator.SetInteger("TalkIndex", index);
            }
            else
            {
                int index = Random.Range(0, 4);
                animator.SetInteger("IdleIndex", index);
            }

        }
    }
    #endregion

    #region Methods
    void SetConversing()
    {
        animator.SetBool("IsConversing", isConversing);
    }
    #endregion
}