using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // Tốc độ di chuyển mượt mà của camera
    public Vector3 offset; // Khoảng cách giữa camera và Player

    private Transform target; // Tham chiếu đến Transform của Player

    void LateUpdate()
    {
        if (target == null)
        {
            // Tìm Player bằng tag
            GameObject player = GameObject.FindGameObjectWithTag("Character");
            if (player != null)
            {
                target = player.transform;
            }
        }

        if (target != null)
        {
            // Tính toán vị trí mới cho camera dựa trên vị trí của Player và offset
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Đảm bảo camera luôn hướng về Player
            transform.LookAt(target);
        }
    }
}