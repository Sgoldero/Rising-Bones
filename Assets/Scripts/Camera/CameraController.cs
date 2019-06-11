using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //Global vars
    [Range(0, 1)] public float deadZoneFactor = 0.2f;
    [Range(0, 1)] public float smoothFactor = 0.1f;
    public Transform target;

    private float dzH, dzW;
    private float halfSizeTarget;

    // Use this for initialization
    void Start()
    {
        dzH = Camera.main.orthographicSize * deadZoneFactor;
        dzW = Camera.main.aspect * dzH;

        halfSizeTarget = target.gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = 0, deltaY = 0;

        if (!IsInDeadZone(ref deltaX, ref deltaY))
        {
            Vector3 newPos = transform.position + new Vector3(deltaX, deltaY, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, smoothFactor);
        }

    }// End Update()

    private bool IsInDeadZone(ref float deltaX, ref float deltaY)
    {
        Bounds cameraBounds = new Bounds(transform.position, new Vector3(2f * (dzW + halfSizeTarget), 2f * (dzH + halfSizeTarget), 100f));

        if (!cameraBounds.Contains(target.position))
        {
            Vector3 cp = cameraBounds.ClosestPoint(target.position);
            deltaX = target.position.x - cp.x;
            deltaY = target.position.y - cp.y;
            return false;
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        dzH = Camera.main.orthographicSize * deadZoneFactor; // OrthographicSize = la mitad de la altura de la cámara
        dzW = Camera.main.aspect * dzH; // Aspect = anchura entre altura

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(2 * dzW, 2 * dzH, 10f));
    }
}
