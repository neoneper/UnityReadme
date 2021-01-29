using System;
using UnityEngine;

namespace ReadmeSystem
{
    [CreateAssetMenu(fileName = "New_Readme.asset", menuName = "Readme")]
    public class Readme : ScriptableObject
    {

        public Texture2D icon;
        public string title;
        public Section[] sections;

        [HideInInspector]
        public bool loadedLayout = false;
        [HideInInspector]
        public bool showInEditMode = false;
        [HideInInspector]
        public bool isRoot = false;

        [ContextMenu("SetRoot", true)]
        public void SetIsRoot()
        {
            isRoot = true;
        }
        [ContextMenu("ResetRoot", true)]
        public void ResetIsRoot()
        {
            isRoot = false;
        }

        [Serializable]
        public class Section
        {

            [HideInInspector]
            public string name;
            public string heading;
            [TextArea] public string text;
            public string linkText, url;
        }
    }
}