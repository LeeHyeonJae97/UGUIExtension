using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private float radStart => _start * Mathf.Deg2Rad;
    private float radEnd => _end * Mathf.Deg2Rad;

    [SerializeField] private float _start;
    [SerializeField] private float _end;
    [SerializeField] private float _multiplier;

    private void OnDrawGizmos()
    {
        Vector2 start = Vector2.zero;
        Vector2 end = Vector2.right;

        Vector2[] points = new Vector2[20];

        Gizmos.DrawWireSphere(new Vector3(start.x, Mathf.Sin(radStart) * _multiplier), .1f);
        Gizmos.DrawWireSphere(new Vector3(end.x, Mathf.Sin(radEnd) * _multiplier), .1f);

        for (int i = 0; i < points.Length; i++)
        {
            float x = Mathf.Lerp(start.x, end.x, (float)i / points.Length);
            float y = Mathf.Sin(Mathf.Lerp(radStart, radEnd, (float)i / points.Length)) * _multiplier;

            Gizmos.DrawWireSphere(new Vector3(x, y), .1f);
        }
    }
}
