using Audio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Menu.UI
{
    public class MenuSceneHandler : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject optionsMenu;
        [SerializeField] private GameObject quitMenu;
        
        public void StartGame()
        {
            SoundManager.Instance.SetDreamyFilter();
            SceneManager.LoadScene(1);
        }

        public void OpenMainMenu()
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            quitMenu.SetActive(false);
        }
        
        public void OpenOptions()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            quitMenu.SetActive(false);
        }

        public void OpenQuitConfirmation()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            quitMenu.SetActive(true);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
