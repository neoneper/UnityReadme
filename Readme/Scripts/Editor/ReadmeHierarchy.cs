using ReadmeSystem;
using ReadmeSystem.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ReadmeHierarchy
{
    private static Dictionary<int, ReadmeBehaviour> readmeIconInstances = new Dictionary<int, ReadmeBehaviour>();

    static ReadmeHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= DrawHierarchyItem;
        EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyItem;
    }


    private static void DrawHierarchyItem(int instanceID, Rect selectionRect)
    {
        ReadmeBehaviour rb = null;


        if (readmeIconInstances.TryGetValue(instanceID, out rb))
        {
            //Remove it not exit more
            if(rb==null)
            {
                readmeIconInstances.Remove(instanceID);
                return;
            }

            //Remove if not exist readme more
            if(rb.readme==null)
            {
                readmeIconInstances.Remove(instanceID);
                return;
            }

            //remove if not allowed
            if (rb.showIconInHierarchy==false)
            {
                readmeIconInstances.Remove(instanceID);
                return;
            }


            //remove if icon has removed
            if (rb.readme.icon == null)
            {
                readmeIconInstances.Remove(instanceID);
                return;
            }

            //Add Icon
            Rect frame = new Rect(selectionRect);
            {
                frame.x += frame.width - 20f;
                frame.width = 18f;
            }

            GUI.Label(frame, rb.readme.icon, GUI.skin.label);
            return;
        }

        if (ReadmeBehaviourEditor.TryGetReadmeBehaviour(instanceID, out rb))
        {
            readmeIconInstances.Add(instanceID, rb);
           
        }


    }

    /*
    else if (selectionRect.Contains(Event.current.mousePosition))
    {
        if (GUI.Button(frame, "♡️", GUI.skin.label))
        {
            favoriteInstances.Add(instanceID);
        }
    }*/
}

