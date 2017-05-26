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
            Image = new StoredItem();
            Effects = new StoredEffectItem[0];
        }

        public StoredItem Recorder;
        public StoredItem Image;
        public StoredEffectItem[] Effects;
    }

    [Serializable]
    public class StoredItem
    {

        public AttributesValues Attributes;
    }

    [Serializable]
    public class StoredEffectItem : StoredItem
    {
        // Position
        public float[] Position;
        public string Name;
    }
}
