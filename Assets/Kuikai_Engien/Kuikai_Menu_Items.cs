using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public static class Kuikai_Menu_Items { 
 


   
    [MenuItem("GameObject/Kuikai/Canvas/Options", priority = 10)]
    public static void CreatOptionMenu()
    {
        CanvasCreater canvasCreater = new CanvasCreater() ;

        canvasCreater.CreateOptionsMenu();

    }
}
