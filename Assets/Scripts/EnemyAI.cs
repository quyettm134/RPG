using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Idle,
        Roaming
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Idle;
    }

    private void Start()
    {
        StartCoroutine(EnemyRoutine());
    }

    private IEnumerator EnemyRoutine()
    {
        while (true)
        {
            if (state == State.Idle)
            {
                enemyPathfinding.MoveTo(Vector2.zero);
                yield return new WaitForSeconds(Random.Range(1f, 3f));
            }

            else if (state == State.Roaming)
            {
                Vector2 roamPosition = GetRoamingPosition();
                enemyPathfinding.MoveTo(roamPosition);
                yield return new WaitForSeconds(Random.Range(2f, 4f));
            }

            state = Random.value < 0.5f ? State.Idle : State.Roaming;
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
