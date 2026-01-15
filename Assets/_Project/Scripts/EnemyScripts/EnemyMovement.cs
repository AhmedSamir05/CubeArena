using UnityEngine;
using UnityEngine.AI;

//ToDo StateMachine
public class EnemyMovement : MonoBehaviour
{
    public float areaSize = 20f;
    public float moveRadius = 0.5f;
    public float waitTime = 5.5f;

    [Header("Speed Normalization")]
    public float maxSpeed = 2.5f;

    private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    private Vector3 lastPosition;
    private float timer;

    void Awake()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
    }

    private void OnEnable()
    {
        timer = 0;
        agent.avoidancePriority = Random.Range(30, 60);

        lastPosition = agent.transform.position;
    }

    void Update()
    {
        UpdateAnimatorSpeed();

        if (agent.isOnNavMesh && !agent.pathPending && agent.remainingDistance <= moveRadius)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                SetRandomDestination();
                timer = 0f;
            }
        }
    }

    void UpdateAnimatorSpeed()
    {
        // Calculate real movement speed from position delta
        Vector3 currentPosition = agent.transform.position;
        float distanceMoved = Vector3.Distance(currentPosition, lastPosition);
        float currentSpeed = distanceMoved / Time.deltaTime;

        lastPosition = currentPosition;

        // Normalize speed to 0–1
        float normalizedSpeed = Mathf.InverseLerp(0, maxSpeed, currentSpeed);

        // Optional dead-zone to prevent jitter
        if (currentSpeed < 0.05f)
            normalizedSpeed = 0f;

        animator.SetFloat("Speed", normalizedSpeed);
    }

    void SetRandomDestination()
    {
        Vector3 randomPoint = new Vector3(
            Random.Range(-areaSize / 2f, areaSize / 2f),
            0f,
            Random.Range(-areaSize / 2f, areaSize / 2f)
        );

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
