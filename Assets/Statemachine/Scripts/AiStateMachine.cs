using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    Idle, Shoot, RunAway
}

public class AiStateMachine : MonoBehaviour
{
    public State AiState = State.RunAway;

    private SpriteRenderer _sprite;

    public GameObject _player;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        NextState();
    }

    private void NextState()
    {
        switch (AiState)
        {
            case State.Idle:
                StartCoroutine(IdleState());
                break;
            case State.Shoot:
                StartCoroutine(ShootState());
                break;
            case State.RunAway:
                StartCoroutine(RunAwayState());
                break;
        }
    }
    private IEnumerator IdleState()
    {
        Debug.Log("Entering Idle State");
        while (AiState == State.Idle)
        {
            _sprite.transform.rotation *= Quaternion.Euler(0, 0, 50f * Time.deltaTime);

            Vector2 directionToPlayer = _player.transform.position - gameObject.transform.position;
                
            if (Vector2.Angle(gameObject.transform.right, directionToPlayer) < 5f)
            {
                AiState = State.RunAway;
            }
            yield return null;
        }
        Debug.Log("Ending Idle State");
        NextState();
    }

    private IEnumerator ShootState()
    {
        Debug.Log("Entering Shoot State");
        int colorChangeCount = 0;
        while(AiState == State.Shoot)
        {
            yield return new WaitForSeconds(0.5f);
            _sprite.color = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f));
            colorChangeCount++;

            if(colorChangeCount == 5)
            {
                AiState = State.Idle;
            }
            yield return null;
        }
        Debug.Log("Ending Shoot State");
        NextState();
    }

    private IEnumerator RunAwayState()
    {
        Debug.Log("Entering RunAway State");
        float startTime = Time.time;
        while (AiState == State.RunAway)
        {
            float wave = Mathf.Sin(Time.time * 30f) * 0.1f + 1f;
            float wave2 = Mathf.Cos(Time.time * 30f) * 0.1f + 1f;
            transform.localScale = new Vector3 (wave2, wave,1);

            float moveWave = Mathf.Sin(Time.time * 30f) * 0.9f + 0.3f;
            transform.position += transform.right * moveWave * 5f * Time.deltaTime;

            if ((Time.time - startTime) > 3f)
            {
                AiState = State.Shoot;
            }

            yield return null;
        }
        Debug.Log("Ending RunAway State");
        NextState();
    }
}

