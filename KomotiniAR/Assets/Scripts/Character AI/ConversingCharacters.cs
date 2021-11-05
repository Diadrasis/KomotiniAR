using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversingCharacters : MonoBehaviour
{
    #region Variables
    private readonly static float conversationTimeMin = 3f;
    private readonly static float conversationTimeMax = 10f;
    private ConversingCharacter[] conversingCharacters;
    private ConversingCharacter currentConversingCharacter;
    private Coroutine coroutine;

    // Events
    public delegate void ConversingAction(); // int _index
    public ConversingAction OnStartConversing;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        conversingCharacters = GetComponentsInChildren<ConversingCharacter>();
        ChangeConversingCharacter();
    }
    #endregion

    #region Methods
    private void ChangeConversingCharacter()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        int newConversingCharacterIndex = UnityEngine.Random.Range(0, conversingCharacters.Length);
        if (currentConversingCharacter != null)
            currentConversingCharacter.isConversing = false;
        currentConversingCharacter = conversingCharacters[newConversingCharacterIndex];
        currentConversingCharacter.isConversing = true;

        coroutine = StartCoroutine(WaitAndChange(UnityEngine.Random.Range(conversationTimeMin, conversationTimeMax)));
    }

    private IEnumerator WaitAndChange(float _waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(_waitTime);
            ChangeConversingCharacter();

            // Invoke event
            OnStartConversing?.Invoke();
        }
    }
    #endregion
}