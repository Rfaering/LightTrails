using Assets.Projects.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class CreateProjectButton : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(CreateProject);
    }

    private void CreateProject()
    {
        var projectDialog = FindObjectOfType<NewProjectDialog>();
        //var newProjectDialog = FindObjectOfType<NewProjectImagePicker>();

        Project.CreateNew(projectDialog.GetProjectName(), null).Save();
        FindObjectOfType<ProjectList>().Reload();
        projectDialog.Close();
    }
}

