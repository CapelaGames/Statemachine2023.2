using System.Collections;
using UnityEngine;

public class AiStateMachine : MonoBehaviour
{
    public enum State
    {
        Shoot, Run, Patrol, Wiggle
    }

    [SerializeField]
    private State _aiState = State.Shoot;

    [SerializeField]
    private Transform _player;

    private SpriteRenderer _sprite;

    [SerializeField]
    private Transform _eyes1;

    [SerializeField]
    private Transform _eyes2;


    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        NextState();
    }

    private void NextState()
    {
        switch (_aiState)
        {
            case State.Shoot:
                StartCoroutine(ShootState());
                break;
            case State.Run:
                StartCoroutine(RunState());
                break;
            case State.Patrol:
                StartCoroutine(PatrolState());
                break;
            case State.Wiggle:
                StartCoroutine(WiggleState());
                break;
            default:
                Debug.LogError("");
                break;
        }
    }

    private IEnumerator ShootState()
    {
        Debug.Log("Start Shoot");
        int colorChangeCount = 0;
        while (_aiState == State.Shoot)
        {
            yield return new WaitForSeconds(0.5f);
            _sprite.color = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f));
            colorChangeCount++;

            if (colorChangeCount >= 5)
            {
                _aiState = State.Patrol;
            }

            yield return null;
        }
        Debug.Log("End Shoot");
        NextState();
    }

    private IEnumerator WiggleState()
    {
        float startTime = Time.time;
        Vector3 e1 = _eyes1.transform.localScale;
        Vector3 e2 = _eyes2.transform.localScale;
        while (_aiState == State.Wiggle)
        {
            float wave = Mathf.Sin(Time.time * 30f) * 0.05f;
            float wave2 = Mathf.Cos(Time.time * 30f  + 0.5f) * 0.05f;

            _eyes1.transform.localScale += new Vector3(wave2, wave, 1) * 0.05f;
            _eyes2.transform.localScale += new Vector3(wave, wave2, 1) * 0.05f;

            if (Time.time - startTime > 2f)
            {
                _aiState = State.Run;
            }

            yield return null;
        }
        _eyes1.transform.localScale = e1;
        _eyes2.transform.localScale = e2;
        NextState();
    }

    private IEnumerator RunState()
    {
        Debug.Log("Start Run");
        float startTime = Time.time;
        while (_aiState == State.Run)
        {
            float wave = Mathf.Sin(Time.time * 30f) * 0.1f + 1f;
            float wave2 = Mathf.Cos(Time.time * 30f) * 0.1f + 1f;

            transform.localScale = new Vector3(wave2, wave, 1);

            float shimmy = Mathf.Sin(Time.time * 30f) * 0.9f + 0.3f;
            transform.position += transform.right * shimmy * Time.deltaTime;

            if (Time.time - startTime > 3f)
            {
                _aiState = State.Shoot;
            }
            yield return null;
        }
        transform.localScale = Vector3.one;
        Debug.Log("End Run");
        NextState();
    }


    private IEnumerator PatrolState()
    {
        Debug.Log("Start Paw Patrol");


        while (_aiState == State.Patrol)
        {
            transform.rotation *= Quaternion.Euler(0f, 0f, 50f * Time.deltaTime);

            // FROM A to B  -----   B - A
            Vector2 directionToPlayer = _player.position - transform.position;

            if (Vector2.Angle(transform.right, directionToPlayer) < 5f)
            {
                _aiState = State.Wiggle;
            }

            yield return null;
        }
        Debug.Log("End Patrol");
        NextState();
    }

}
