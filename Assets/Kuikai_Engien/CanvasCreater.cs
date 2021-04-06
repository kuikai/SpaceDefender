using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CanvasCreater : MonoBehaviour
{

    delegate void ButtonAction();
    public void CreateOptionsMenu()
    {

        GameObject Options = CreateGameObject("Options");

        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            eventSystem.transform.SetParent(Options.transform);
        }

        SetUpOptionsCanvas(Options);


        CreateTextGameObject(Options, Vector3.zero, new Vector2(200,200), "Overskrift", "Options", 60);


    }

    private static void SetUpOptionsCanvas(GameObject Options)
    {
        Options.AddComponent<Canvas>();
        Options.AddComponent<CanvasScaler>();
        Options.AddComponent<GraphicRaycaster>();

        Options.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        Options.GetComponent<Canvas>().pixelPerfect = false;
        Options.GetComponent<Canvas>().sortingOrder = 0;

        Options.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        Options.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
    }

    public void CreateIngameMenu()
   {


   }

    private static GameObject CreateGameObject(string name)
    {
        GameObject gameObject = new GameObject(name);
        gameObject.transform.position = GetSpawnPosition();

        return gameObject;
    }
    private static Vector3 GetSpawnPosition()
    {
        Transform sceneCamera = SceneView.lastActiveSceneView.camera.transform;
        return sceneCamera.position + (sceneCamera.forward * 4f);
    }
    /// <summary>
    /// Set up button with out ation
    /// </summary>
    /// <param name="ParrentTransfrom"></param>
    /// <param name="SetSize"></param>
    /// <param name="SetPosition"></param>
    /// <param name="ButtonName"></param>
    private static void SetUpButton(GameObject ParrentTransfrom, Vector2 SetSize, Vector3 SetPosition, string ButtonName)
    {
        GameObject ButtonObject = CreateGameObject(ButtonName);
        ButtonObject.transform.SetParent(ParrentTransfrom.transform);

      
        ButtonObject.AddComponent<Image>();
        ButtonObject.AddComponent<Button>();
        RectTransform button_Rect = ButtonObject.GetComponent<RectTransform>();
        button_Rect.sizeDelta = SetSize;
        button_Rect.transform.localPosition = SetPosition;

        GameObject ButtonText = CreateGameObject("Text");
        ButtonText.transform.SetParent(ButtonObject.transform);
        ButtonText.AddComponent<Text>();
        ButtonText.GetComponent<Text>().text = ButtonName;

        Text text = ButtonText.GetComponent<Text>();
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;
        text.fontSize = (int)SetSize.y / 2;

        RectTransform RectTransform = ButtonText.GetComponent<RectTransform>();
        RectTransform.sizeDelta = SetSize;
        RectTransform.transform.localPosition = new Vector3(0, 0, 0);
    }
    /// <summary>
    /// Button With action 
    /// </summary>
    /// <param name="ParrentTransfrom"></param>
    /// <param name="SetSize"></param>
    /// <param name="SetPosition"></param>
    /// <param name="ButtonName"></param>
    /// <param name="buttonAction"></param>
    private static void SetUpButton(GameObject ParrentTransfrom, Vector2 SetSize, Vector3 SetPosition, string ButtonName, ButtonAction buttonAction)
    {
        GameObject ButtonObject = CreateGameObject(ButtonName);
        ButtonObject.transform.SetParent(ParrentTransfrom.transform);
        ButtonObject.AddComponent<Image>();
        ButtonObject.AddComponent<Button>();
        RectTransform button_Rect = ButtonObject.GetComponent<RectTransform>();
        button_Rect.sizeDelta = SetSize;
        button_Rect.transform.localPosition = SetPosition;

        GameObject ButtonText = CreateGameObject("Text");
        ButtonText.transform.SetParent(ButtonObject.transform);
        ButtonText.AddComponent<Text>();
        ButtonText.GetComponent<Text>().text = ButtonName;

        Text text = ButtonText.GetComponent<Text>();
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;
        text.fontSize = (int)SetSize.y / 2;

        RectTransform RectTransform = ButtonText.GetComponent<RectTransform>();
        RectTransform.sizeDelta = SetSize;
        RectTransform.transform.localPosition = new Vector3(0, 0, 0);

    //    ButtonObject.GetComponent<Button>().onClick.AddListener(delegate { kage(); });
        //ButtonObject.GetComponent<Button>().onClick.AddListener(kage);
        Debug.Log("lalal");
    }
    /// <summary>
    /// Default add text
    /// </summary>
    /// <param name="Parrent"></param>
    private static void CreateTextGameObject(GameObject Parrent)
    {
        GameObject TextObject = CreateGameObject("Text");

        TextObject.transform.SetParent(Parrent.transform);

        TextObject.AddComponent<CanvasRenderer>();
        TextObject.AddComponent<Text>();

        Text text = TextObject.GetComponent<Text>();
        text.text = "Text";
        text.fontSize = 50;
        text.alignment = TextAnchor.MiddleCenter;
        RectTransform rectTransform_TextSpace = TextObject.GetComponent<RectTransform>();
        rectTransform_TextSpace.sizeDelta = new Vector2(200, 200);
        rectTransform_TextSpace.transform.localPosition = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// Add text customuse
    /// </summary>
    /// <param name="Parrent"></param>
    /// <param name="position"></param>
    /// <param name="Name"></param>
    /// <param name="StartText"></param>
    /// <param name="fontSize"></param>
    private static void CreateTextGameObject(GameObject Parrent, Vector3 position,Vector2 TextBoxSize, string Name, string StartText, int fontSize)
    {
        GameObject Text = CreateGameObject(Name);

        Text.transform.SetParent(Parrent.transform);

        Text.AddComponent<CanvasRenderer>();
        Text.AddComponent<Text>();

        Text text = Text.GetComponent<Text>();
        text.text = StartText;
        text.fontSize = fontSize;
        text.alignment = TextAnchor.MiddleCenter;
        RectTransform rectTransform_TextSpace = Text.GetComponent<RectTransform>();
        rectTransform_TextSpace.sizeDelta = TextBoxSize;
        rectTransform_TextSpace.transform.localPosition = position;
    }
    /// <summary>
    /// CreateImage
    /// </summary>
    /// <param name="Parretobject"></param>
    /// <param name="ImageName"></param>
    /// <param name="color"></param>
    /// <param name="Position"></param>
    /// <param name="size"></param>
    private static void CreateImage(GameObject Parretobject, string ImageName, Color color, Vector3 Position, Vector2 size)
    {
        GameObject GameObjectImage = CreateGameObject(ImageName);

        GameObjectImage.transform.localPosition = Vector3.zero;
        GameObjectImage.transform.SetParent(Parretobject.transform);
        GameObjectImage.AddComponent<CanvasRenderer>();
        GameObjectImage.AddComponent<RawImage>();
        RawImage image = GameObjectImage.GetComponent<RawImage>();

        image.color = color;

        RectTransform rect = GameObjectImage.GetComponent<RectTransform>();
        rect.sizeDelta = size;
        rect.transform.localPosition = Position;
    }
}
