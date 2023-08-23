using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine : MonoBehaviour
{
    public enum State
    {
        FullHealth
    }

    [SerializeField]
    private State _aiState = State.FullHealth;

    void Start()
    {
        NextState();
    }

    private void NextState()
    {
        switch (_aiState)
        {
            case State.FullHealth:
                StartCoroutine(FullHealthState());
                break;
        }
    }

    IEnumerator FullHealthState()
    {
        while(_aiState == State.FullHealth)
        {
            yield return null;
        }
        NextState();
    }
}
