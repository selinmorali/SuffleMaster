using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private Transform[] _controlPoints;
    private Vector3 _gizmosPos;

    private void OnDrawGizmos()
    {
        for (float i = 0; i < 1; i+= 0.05f)
        {
            _gizmosPos = Mathf.Pow(1 - i, 3) * _controlPoints[0].position + 3 * Mathf.Pow(1 - i, 2) * i * _controlPoints[1].position + 3 * (1 - i) * Mathf.Pow(i, 2) * _controlPoints[2].position + Mathf.Pow(i, 3) * _controlPoints[3].position;

            Gizmos.DrawSphere(_gizmosPos, 0.25f);
        }

        Gizmos.DrawLine(new Vector3(_controlPoints[0].position.x, _controlPoints[0].position.y, _controlPoints[0].position.z), new Vector3(_controlPoints[1].position.x, _controlPoints[1].position.y, _controlPoints[1].position.z));

        Gizmos.DrawLine(new Vector3(_controlPoints[2].position.x, _controlPoints[2].position.y, _controlPoints[2].position.z), new Vector3(_controlPoints[3].position.x, _controlPoints[3].position.y, _controlPoints[3].position.z));
    }
}
