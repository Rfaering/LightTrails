using Assets.Projects.Scripts;
using System.IO;
using UnityEngine;

public class ProjectList : MonoBehaviour
{
    public GameObject Prefab;

    // Use this for initialization
    void Start()
    {
        Reload();
    }

    public void Reload()
    {
        var projects = Path.Combine(Application.persistentDataPath, PersistenceConsts.ProjectsDirectoryPath);
        Directory.CreateDirectory(projects);

        var directories = Directory.GetDirectories(projects);

        foreach (var item in GetComponentsInChildren<ProjectListItem>())
        {
            DestroyObject(item.gameObject);
        }

        foreach (var item in directories)
        {
            var project = Project.CreateFromPath(item);

            if (project == null)
            {
                Debug.Log("Project contains error, creating new project " + item);
                continue;
            }

            CreateProjectItem(project);
        }
    }

    private void CreateProjectItem(Project project)
    {
        var newObject = Instantiate(Prefab);
        newObject.transform.SetParent(gameObject.transform);
        newObject.GetComponent<ProjectListItem>().SetProject(project);
    }
}
