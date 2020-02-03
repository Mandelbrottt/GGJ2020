using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public Button startButton;

    public string gameScene;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(onStartClick);
    }

    private void onStartClick() 
    {
        SceneManager.LoadScene(gameScene);
    }

}
