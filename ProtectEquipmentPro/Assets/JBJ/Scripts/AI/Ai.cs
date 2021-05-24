using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

public enum AI_STATE
{
    IDLE,
    PATROL,
    REACTION
}

public class Ai : MonoBehaviour
{
    public AI_STATE ai_state;

    [Header("AI Info")]
    [SerializeField] private float aiSpeed;
    [SerializeField] private Animator aiAnim;
    [SerializeField] private NavMeshAgent ai;
    [SerializeField] private Transform aiTransform;

    [Header ("플레이어 관련")]
    [SerializeField] private bool hitPlayer;
    [SerializeField] private Transform Player;

    [Header("Way Points")]
    public Transform wayPointParnet;
    public Transform[] wayPointTransforms;
    public Vector3[] wayPoints;
    [SerializeField] private int curWayPointIndex;


    [Header("플레이어 시야")]
    [Range(1f,5f)]
    [SerializeField] private float viewDistance;
    [SerializeField] private Light spotLight;
    [SerializeField] private float viewAngle;
    [SerializeField] private LayerMask viewMask;


    #region EDITOR
    [Button]
    public void SettingWayPoints()
    {
        wayPointTransforms = new Transform[wayPointParnet.childCount];
        for(int i = 0;i<wayPointTransforms.Length;i++)
        {
            wayPointTransforms[i] = wayPointParnet.GetChild(i);
        }

        wayPoints = new Vector3[wayPointTransforms.Length];
        for (int i = 0; i < wayPointTransforms.Length; i++)
            wayPoints[i] = wayPointTransforms[i].position;
    }

    [Button]
    public void SettingAi()
    {
        aiAnim = gameObject.GetComponent<Animator>();
        ai = gameObject.GetComponent<NavMeshAgent>();
        aiTransform = transform;
    }
    #endregion

    public void Init_Ai()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        viewAngle = spotLight.spotAngle;
        StartCoroutine(STATE_IDLE());
    }

    #region ai 애니메이션 종료 이벤트
    public void OnIdleAnimCompleted()
    {
        StopAllCoroutines();
        StartCoroutine(STATE_PATROL());
    }

    public void OnReactionAnimCompleted()
    {
        StopAllCoroutines();
        StartCoroutine(STATE_PATROL());
    }
    #endregion

    #region ai 움직임 코루틴
    IEnumerator STATE_IDLE()
    {
        aiAnim.SetTrigger("Idle");
        ai_state = AI_STATE.IDLE;
        
        while(ai_state == AI_STATE.IDLE)
        {
            if(CanSee(Player))
            {
                StartCoroutine(STATE_REATION());
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator STATE_PATROL()
    {
        aiAnim.ResetTrigger("Idle");
        aiAnim.SetTrigger("Walk");
        ai_state = AI_STATE.PATROL;

        float wayPointHitDistance = 1f;
        int wayPointLengths = wayPoints.Length;
        int tempWayPointIndex = 0;
        while(curWayPointIndex == tempWayPointIndex)
        {
            tempWayPointIndex = Random.Range(0,wayPointLengths);
        }

        Vector3 targetPoint = wayPoints[tempWayPointIndex];
        ai.isStopped = false;
        ai.SetDestination(targetPoint);

         curWayPointIndex = tempWayPointIndex;

        while(ai_state == AI_STATE.PATROL)
        {
            if (CanSee(Player))
            {
                StartCoroutine(STATE_REATION());
                yield break;
            }

            // 스타트포인트 가까워 졋을 떄
            if (Vector3.Distance(aiTransform.position.ModifiedY(0f), wayPoints[tempWayPointIndex].ModifiedY(0f)) < wayPointHitDistance)
            { 
                StartCoroutine(STATE_IDLE());
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator STATE_REATION()
    {
        aiAnim.SetTrigger("Reaction");
        ai_state = AI_STATE.REACTION;
        aiTransform.LookAt(Player);
        ai.isStopped = true;

        yield return null;
    }
    #endregion


    private bool CanSee(Transform target)
    {
        if (Vector3.Distance(aiTransform.position, target.position) < viewDistance)
        {
            Vector3 dirToTarget = (target.position - aiTransform.position).normalized;
            float angleBetweenGunardTarget = Vector3.Angle(aiTransform.forward, dirToTarget);
            if (angleBetweenGunardTarget < viewAngle / 2f)
            {
                if (!Physics.Linecast(aiTransform.position, target.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < wayPointTransforms.Length; i++)
            Gizmos.DrawSphere(wayPointTransforms[i].position, .2f);

        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);

    }

}
