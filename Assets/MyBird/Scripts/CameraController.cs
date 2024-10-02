using UnityEngine;

namespace MyBird
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]private Transform Player;
        [SerializeField]private float offset = 1.5f;
        private void LateUpdate()
        {
            // 플레이어 따라가기
            FollowPlayer();
        }
        void FollowPlayer()
        {
            transform.position = new Vector3(Player.position.x + offset, transform.position.y, transform.position.z);
        }
    }
}