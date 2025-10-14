using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // a Player
    public Vector3 offset;       // eltolás a Playerhez képest
    public float smoothSpeed = 5f; // mennyire simán kövesse

    void LateUpdate()
    {
        if (target == null) return;

        // kívánt pozíció
        Vector3 desiredPosition = target.position + offset;

        // sima mozgás (lerp)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
