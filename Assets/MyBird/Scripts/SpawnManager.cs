using UnityEngine;

namespace MyBird
{
    public class SpawnManager : MonoBehaviour
    {
        #region Variables

        // 파이프 프리팹
        public GameObject spawnprefab;

        // 스폰 타이머 
        [SerializeField] private float spawnTimer = 1f;
        private float countdown = 0f;

        [SerializeField] private float maxSpawnTimer = 1.05f;
        [SerializeField] private float minSpawnTimer = 0.95f;
        public static float levelTimer = 0f;

        // 스폰 지점
        [SerializeField]private float maxspawnY = 3.5f;
        [SerializeField] private float minspawnY = -1.5f;

        #endregion

        private void Start()
        {
            // 초기화
            countdown = spawnTimer;
        }

        private void Update()
        {
            // 게임 진행 체크
            if(GameManager.IsStart == false || GameManager.IsDeath == true)
            {
                return;
            }

            // 스폰타이머
            if (countdown <= 0f)
            {
                // 스폰
                SpawanPipe();

                //타이머 초기화
                //  countdown = spawnTimer;
                countdown = Random.Range(minSpawnTimer, maxSpawnTimer-levelTimer);  // 1.05f~ 0.95f => 1.05~0.90f
            }
            countdown -= Time.deltaTime;
        }

        void SpawanPipe()
        {
            float spawnY = transform.position.y + Random.Range(minspawnY, maxspawnY);
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0f); // 위치값 보정
            Instantiate(spawnprefab, spawnPosition, Quaternion.identity);
        }
    }
}
