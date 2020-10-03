using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FollowOnSpline : MonoBehaviour
{
    public Transform[] Points;
    public GameObject AudioSource;
    public Transform playerPos;
    public float SpeedModifier = 4f;
    private int _previuousClosetsIndex = 0;
    private Vector3 _previousPosition = new Vector3();

    void Start()
    {
        SetSourcePosition(playerPos.position);
    }

    void Update()
    {
        SetSourcePosition(AudioSource.transform.position);
    }

    private void SetSourcePosition(Vector3 positionToCalcFrom)
    {
        int closetsIndex = ClosetsTransform(Points, positionToCalcFrom);
        int otherIndex;
     
        Vector3 position;
        if (closetsIndex == 0)
        {
            otherIndex = closetsIndex + 1;
            position = GetPositionOnLine(Points[closetsIndex].position, Points[otherIndex].position, playerPos.position);
        }

        else if (closetsIndex == Points.Length - 1)
        {
            otherIndex = closetsIndex - 1;
            position = GetPositionOnLine(Points[closetsIndex].position, Points[otherIndex].position, playerPos.position);
        }

        else
        {
            float dotOver = DotProduct(Points[closetsIndex].position, Points[closetsIndex + 1].position, playerPos.position);
            float dotUnder = DotProduct(Points[closetsIndex].position, Points[closetsIndex - 1].position, playerPos.position);

            if (dotOver > dotUnder)
            {
                otherIndex = closetsIndex + 1;
                position = GetPositionOnLine(Points[closetsIndex].position, Points[otherIndex].position, playerPos.position);
            }

            else
            {
                otherIndex = closetsIndex - 1;
                position = GetPositionOnLine(Points[closetsIndex].position, Points[otherIndex].position, playerPos.position);
            }
        }

        AudioSource.transform.position = Vector3.MoveTowards(
                AudioSource.transform.position,
                LimitVector(Points[closetsIndex].position, Points[otherIndex].position, position),            
                Time.deltaTime * SpeedModifier);

        _previuousClosetsIndex = closetsIndex;
    }

    private float DotProduct(Vector3 v1, Vector3 v2, Vector3 playerPosition)
    {
        Vector3 v1v2Line = (v2 - v1).normalized;
        Vector3 vectorToPlayer = playerPosition - v1;

        return Vector3.Dot(v1v2Line, vectorToPlayer.normalized);
    }

    private Vector3 GetPositionOnLine(Vector3 v1, Vector3 v2, Vector3 playerPosition)
    {
        Vector3 v1v2Line = (v2 - v1).normalized;
        Vector3 vectorToPlayer = playerPosition - v1;

        Vector3 product = v1v2Line * Vector3.Dot(vectorToPlayer, v1v2Line) + v1;

        return product;
    }

    private float LimitFloat(float v1, float v2, float p)
    {
        if (p > v1 && p > v2)
            return Mathf.Max(v1, v2);
        else if (p < v1 && p < v2)
            return Mathf.Min(v1, v2);
        else
            return p;
    }

    private Vector3 LimitVector(Vector3 v1, Vector3 v2, Vector3 p)
    {
        p.x = LimitFloat(v1.x, v2.x, p.x);
        p.z = LimitFloat(v1.z, v2.z, p.z);
        p.y = LimitFloat(v1.y, v2.y, p.y);
        return p;
    }

    private int ClosetsTransform(Transform[] t, Vector3 p)
    {
        float closets = Mathf.Infinity;
        int closetsIndex = -1;

        for (int i = 0; i < t.Length; i++)
        {
            float distanceSqr = (t[i].position - p).sqrMagnitude;
            if (distanceSqr < closets)
            {
                closets = distanceSqr;
                closetsIndex = i;
            }
        }
        return closetsIndex;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < Points.Length; i++)
        {
            Gizmos.color = Color.white;
            if (i + 1 < Points.Length)
                Gizmos.DrawLine(Points[i].position, Points[i + 1].position);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Points[i].position, 0.5f);
        }
    }
}
