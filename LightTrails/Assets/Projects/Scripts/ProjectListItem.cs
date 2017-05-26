﻿using Assets.Projects.Scripts;
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
            SceneManager.LoadScene("Scenes/Particles");
        }
    }

    public void SetProject(Project project)
    {
        _project = project;
        GetComponentInChildren<Text>().text = project.Name;

        var localProjectFile = _project.GetLocalImagePath();

        if (File.Exists(localProjectFile))
        {
            Texture2D tex = new Texture2D(0, 0);
            var bytes = File.ReadAllBytes(localProjectFile);
            tex.LoadImage(bytes);
            var rawImage = GetComponentsInChildren<RawImage>().Last();
            rawImage.texture = tex;
            rawImage.SizeToBounds(150.0f, 150.0f);
        }
    }

    void Update()
    {
        _deleteMode = _deleteToggleButton.DeleteMode;

        if (_deleteMode)
        {
            GetComponentsInChildren<Image>().Last().color = DeleteModeColor;
        }
        else
        {
            GetComponentsInChildren<Image>().Last().color = NormalModeColor;
        }
    }
}
