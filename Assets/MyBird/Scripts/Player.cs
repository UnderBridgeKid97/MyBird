using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

namespace MyBird
{
    public class Player : MonoBehaviour
    {
        #region
        private Rigidbody2D rb2D;

        // 점프
        [SerializeField] private float jumpForce = 5f;
        private bool keyJump = false;   // 점프 키 입력 체크 

        // 회전속도
        private Vector3 birdRotain;
       [SerializeField]private float rotateSpeed = 5f;

        // 이동
        [SerializeField]private float moveSpeed = 5f;

        // 대기
        [SerializeField] private float readyForce = 1f;

        // 게임ui
        public GameObject readyUI;
        public GameObject gameoverUI;

        // 사운드
        private AudioSource audioSource;
        #endregion

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            // 키입력
            InputBird();

            // 버드 대기
            ReadyBrid();

            // 버드 이동
            MoveBird();

            // 버드 회전
            RotateBird();
        }

        private void FixedUpdate() //리지디 바디 연산은 여기서
        {
            // 점프실행
            if(keyJump)
            {
                JumpBird();
                Debug.Log("점프");
                keyJump=false;
            }

        }
        // 컨트롤 입력
        void InputBird()
        {
            if (GameManager.IsDeath)
                return;

#if UNITY_EDITOR
            // 점프: 스페이스바 또는 마우스 좌클릭
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0); // 0은 왼클릭
#else
            // 터치 인풋 처리
            if(Input.touchCount >0)
            {
               Touch touch= Input.GetTouch(0);

                if(touch.phase==TouchPhase.Began)
                {
                    keyJump |= true;
                }
            }

#endif

            if (GameManager.IsStart == false && keyJump)
            {
                MoveStartBird();

            }

        }

        // 버드 점프
        void JumpBird()
        {
            // 위쪽으로 힘을 주어 위쪽으로 이동
            //  rb2D.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            rb2D.velocity = Vector2.up * jumpForce;
        }
          
        // 버드 회전
        void RotateBird()
        {
            // up +30, down -90
            float degree = 0;
            if(rb2D.velocity.y >0)
            {
                degree = rotateSpeed; // +
            }
            else
            {
                degree = -rotateSpeed;  // -
            }

            float rotationZ = Mathf.Clamp(birdRotain.z + degree, -90f, 30f);
            birdRotain = new Vector3(0f, 0f, rotationZ);
            transform.eulerAngles = birdRotain; // or 쿼터니언 

        }

        // 버드 이동
        void MoveBird()
        {
            if (GameManager.IsStart==false||GameManager.IsDeath==true)
            {
                return;
               
            }

             transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
          
        }
        // 버드 대기
        void ReadyBrid()
        {
            if (GameManager.IsStart)
                return;


            // 위쪽으로 힘을 주어 제자리에 있기
            if (rb2D.velocity.y < 0f)
            {
                rb2D.velocity = Vector2.up * readyForce;
            }
        }

       // 버드죽기
       void DeathBrid()
        {
            // 두번 죽음 처리 방지
            if (GameManager.IsDeath)
            {
                return;
            }
            // Debug.Log("죽음!");
            GameManager.IsDeath = true;
            gameoverUI.SetActive(true);
        }
        // 점수 획득
        void GetPoint()
        {
            // 두번 죽음 처리 방지
            if (GameManager.IsDeath)
            {
                return;
            }
            GameManager.Score++;

            // 포인트 획득 사운드 플레이
            audioSource.Play();

            // 기둥을 10개 통과할 때 마다 10점 20점 30점 난이도 증가 
            if (GameManager.Score % 10==0)
            {
                SpawnManager.levelTimer += 0.05f;
            }

        }

      


        void MoveStartBird()
        {
            GameManager.IsStart = true;
            readyUI.SetActive(false);
        }

        // 버드 충돌 처리
        private void OnTriggerEnter2D(Collider2D collider)
        {

            if(collider.tag == "Pipe")
            {
                DeathBrid();
            }
            else if(collider.tag == "Point")
            {
                GetPoint();
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Ground")
            {
                DeathBrid();
            }

        }
    }
}

