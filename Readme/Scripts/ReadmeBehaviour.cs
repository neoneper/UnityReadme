using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReadmeSystem
{
    [AddComponentMenu("Readme")]
    public class ReadmeBehaviour : MonoBehaviour
    {

        public Readme readme;

        [HideInInspector]
        public bool showIconInHierarchy = true;      

    }
}