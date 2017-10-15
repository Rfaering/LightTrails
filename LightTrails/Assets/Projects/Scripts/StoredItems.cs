using System;
using UnityEngine;

namespace Assets.Projects.Scripts
{
    [Serializable]
    public class StoredItems
    {
        public StoredItems()
        {
            Recorder = new StoredItem();
            Images = new StoredImageItem[0];
            Effects = new StoredParticleItem[0];
        }

        public StoredItem Recorder;
        public StoredImageItem[] Images;
        public StoredParticleItem[] Effects;
    }

    [Serializable]
    public class StoredItem
    {
        public AttributesValues Attributes;
    }

    [Serializable]
    public class StoredIndexedItem : StoredItem
    {
        public int Index;
    }

    [Serializable]
    public class StoredImageItem : StoredIndexedItem
    {
        public string ImagePath;
        public string Shader;
    }

    [Serializable]
    public class StoredParticleItem : StoredIndexedItem
    {
        public float[] Position;
        public string Name;
    }
}
