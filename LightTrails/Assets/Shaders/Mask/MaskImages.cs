using Assets.Projects.Scripts;
using System.Collections.Generic;
using UnityEngine;

public static class MaskImages
{
    public static Texture2D[] Masks = Resources.LoadAll<Texture2D>("Masks");

    public static Texture2D[] AllMasks
    {
        get
        {
            List<Texture2D> masks = new List<Texture2D>();
            masks.AddRange(Masks);

            if (Project.CurrentModel != null)
            {
                if (Project.CurrentModel.Masks == null)
                {
                    Project.CurrentModel.LoadMasks();
                }

                masks.AddRange(Project.CurrentModel.Masks);
            }

            return masks.ToArray();
        }
    }
}