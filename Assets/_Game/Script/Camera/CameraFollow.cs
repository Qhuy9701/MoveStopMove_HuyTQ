using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // đối tượng player (người chơi)
    public float smoothSpeed = 0.125f; // tốc độ di chuyển camera
    public Vector3 offset; // khoảng cách giữa camera và player

    void Start()
    {
        // Nếu target là null, tìm đối tượng Player bằng tag "Player"
        if (target == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER);
            if (playerObject != null)
            {
                target = playerObject.transform;
            }
            else
            {
                Debug.LogWarning("Không tìm thấy đối tượng Player.");
            }
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset; // vị trí camera cần đến
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // di chuyển camera một cách mượt mà
            transform.position = smoothedPosition; // gán vị trí camera hiện tại thành vị trí mới
        }
    }
}
