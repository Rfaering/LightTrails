using Assets.Projects.Scripts;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ProjectListItem : MonoBehaviour
{
    private Project _project;
    private bool _deleteMode;
    private DeleteModeToggleButton _deleteToggleButton;

    public Color NormalModeColor;
    public Color DeleteModeColor;

    private void Start()
    {
        _deleteToggleButton = FindObjectOfType<DeleteModeToggleButton>();
        GetComponent<Button>().onClick.AddListener(ClickProjectItem);
    }

    private void ClickProjectItem()
    {
        if (_deleteMode)
        {
            _project.Delete();
            FindObjectOfType<ProjectList>().Reload();
        }
        else
        {
            Project.CurrentModel = _project;
            SceneManager.LoadScene("Scenes/Main");
        }
    }

    public void SetProject(Project project)
    {
        _project = project;
        GetComponentInChildren<Text>().text = project.Name;

        var localProjectFile = _project.GetThumbnail();

        if (File.Exists(localProjectFile))
        {
            Texture2D tex = new Texture2D(0, 0);
            var bytes = File.ReadAllBytes(localProjectFile);
            if (bytes.Any())
            {
                tex.LoadImage(bytes);
                var rawImage = GetComponentsInChildren<RawImage>().Last();
                rawImage.texture = tex;
                rawImage.SizeToBounds(150.0f, 150.0f);
            }
        }
    }

    void Update()
    {
        _deleteMode = _deleteToggleButton.DeleteMode;

        if (_deleteMode)
        {
            GetComponentInChildren<ProjectBackgroundImage>().GetComponent<Image>().color = DeleteModeColor;
            GetComponentInChildren<ProjectGarbageIcon>(true).gameObject.SetActive(true);
        }
        else
        {
            GetComponentInChildren<ProjectBackgroundImage>().GetComponent<Image>().color = NormalModeColor;
            GetComponentInChildren<ProjectGarbageIcon>(true).gameObject.SetActive(false);
        }
    }
}
