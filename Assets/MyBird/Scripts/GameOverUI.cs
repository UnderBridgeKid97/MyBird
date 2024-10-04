using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

namespace MyBird
{
    public class GameOverUI : MonoBehaviour
    {
        public TextMeshProUGUI bestScore;
        public TextMeshProUGUI Score;
        public TextMeshProUGUI newText;

        [SerializeField] private string loadToScene = "TitleScene";

        private void OnEnable()
        {
            // 게임데이터 저장
            GameManager.BestScore = PlayerPrefs.GetInt("BestScore",0); //  저장된 데이터 가져오기 기본값 0

         //   Debug.Log($"load bestscore:{GameManager.BestScore}");

            if (GameManager.Score > GameManager.BestScore) // 저장된 데이터와 비교하기
            {
                GameManager.BestScore = GameManager.Score;
                PlayerPrefs.SetInt("BestScore", GameManager.BestScore);
                newText.text = "New";
               // Debug.Log($"save bestscore:{GameManager.BestScore}");
            }
           else if (GameManager.Score == GameManager.BestScore)
            {
                newText.text = "Same";
            }
            else
            {
                newText.text = "";
            }

            // ui 연결
            bestScore.text =GameManager.BestScore.ToString();
            Score.text =GameManager.Score.ToString();
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재씬 다시 불러오기 
        }

        public void Menu()
        {
            SceneManager.LoadScene(loadToScene);
        }
    }
}