using Assets.Projects.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Close);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    private void Close()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            Project.CurrentModel = null;
            SceneManager.LoadScene("Scenes/Projects");
        }
    }
}


