using System;
using UnityEngine;

namespace ReadmeSystem
{
    [CreateAssetMenu(fileName = "New_Readme.asset", menuName = "Readme")]
    public class Readme : ScriptableObject
    {

        public Texture2D icon;
        public string title;
        public Readme prevReadme;
        public Readme nextReadme;

        public Section[] sections;
       
 
        [HideInInspector]
        public bool isRoot = false;        

        [Serializable]
        public class Section
        {

            [HideInInspector]
            public string name;
            public string heading;
            [TextArea(5,255)]
            public string text;
            public string linkText, url;
        }
    }
}