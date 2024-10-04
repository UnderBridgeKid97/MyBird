using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyBird
{
    public class TitleUI : MonoBehaviour
    {
        #region
        [SerializeField] private string loadToScene = "PlayScene";
        #endregion

        private void Update()
        {
           // 치트 -p
           if(Input.GetKeyDown(KeyCode.P))
            {
                ResetGameDate();
            }
        }

        public void Play()
        {
            SceneManager.LoadScene(loadToScene);
        }

        void ResetGameDate()
        {
            PlayerPrefs.DeleteAll();
        }
        }
    }
