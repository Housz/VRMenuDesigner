using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Modifier : EditorWindow
{
    [MenuItem("MenuDesigner/Modifier")]
    public static void ShowWindow()
    {
        GetWindow<Modifier>("Modifier");
    }


    string currButtonText;
    int currButtonType = 0;
    Sprite currIcon;

    string currMenuText;
    GameObject currButtonPrefab;
    GameObject _buttonSubMenu;


    // button type, 0: trigger function; 1: trigger sub menu
    int[] _types = { 0, 1 };
    string[] _typeNames = new string[] { "Functional", "Hierarchical" };

    // Maximum number of buttons
    int BUTTONNUMMAX_DEFAULT = 20;
    int BUTTONNUMMAX_WIN10 = 9;
    int BUTTONNUMMAX_RING = 20;

    void OnGUI()
    {
        // The currently selected object from Hierarchy or Scene
        GameObject currSelection = Selection.activeGameObject;

        // Button Modifier
        if (currSelection != null && (currSelection.tag == "DefaultButton" || currSelection.tag == "Win10StyleButton" || currSelection.tag == "RingButton"))
        {
            GUILayout.Label("Modify a Button", EditorStyles.boldLabel);

            switch (currSelection.tag)
            {
                case "DefaultButton":
                    {
                        var ButtonScript = currSelection.GetComponent<DefaultButton>();

                        // 1. change type
                        currButtonType = EditorGUILayout.IntPopup("Button Type: ", currButtonType, _typeNames, _types);

                        if(currButtonType == 1) // Hierarchical Button
                        {
                            _buttonSubMenu = (GameObject)EditorGUILayout.ObjectField(_buttonSubMenu, typeof(GameObject), true);
                        }
                        if (GUILayout.Button("ChangeType"))
                        {
                            //Debug.Log("The text of Button has been changed to: \"" + _typeNames[ButtonType] + "\"");

                            if (currButtonType == 1)
                            {
                                ButtonScript.SetAllowSubMenu(true);
                                ButtonScript.SetSubMenuRef(_buttonSubMenu);
                            }
                            else
                            {
                                ButtonScript.SetAllowSubMenu(false);
                                ButtonScript.SetSubMenuRef(null);
                            }
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        // 2. change text
                        currButtonText = EditorGUILayout.TextField("Button Text: ", currButtonText);
                        if (GUILayout.Button("ChangeText"))
                        {
                            ButtonScript.SetText(currButtonText);
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        // 3. change icon
                        currIcon = (Sprite)EditorGUILayout.ObjectField(currIcon, typeof(Sprite), true);
                        if (GUILayout.Button("ChangeIcon"))
                        {
                            ButtonScript.SetIcon(currIcon);
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        // 4. remove this button
                        if (GUILayout.Button("REMOVE this Button"))
                        {
                            var btn = currSelection.GetComponent<Button>();
                            btn.onClick.RemoveAllListeners();

                            currSelection.SendMessageUpwards("RemoveOneButton");
                            DestroyImmediate(currSelection);
                        }

                        break;
                    }
                case "Win10StyleButton":
                    {
                        var ButtonScript = currSelection.GetComponent<Win10StyleButton>();

                        // 1. change type
                        currButtonType = EditorGUILayout.IntPopup("Button Type: ", currButtonType, _typeNames, _types);

                        if (currButtonType == 1) // Hierarchical Button
                        {
                            _buttonSubMenu = (GameObject)EditorGUILayout.ObjectField(_buttonSubMenu, typeof(GameObject), true);
                        }
                        if (GUILayout.Button("ChangeType"))
                        {
                            //Debug.Log("The text of Button has been changed to: \"" + _typeNames[ButtonType] + "\"");

                            if (currButtonType == 1)
                            {
                                ButtonScript.SetAllowSubMenu(true);
                                ButtonScript.SetSubMenuRef(_buttonSubMenu);
                            }
                            else
                            {
                                ButtonScript.SetAllowSubMenu(false);
                                ButtonScript.SetSubMenuRef(null);
                            }
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        // 2. change text
                        currButtonText = EditorGUILayout.TextField("Button Text: ", currButtonText);
                        if (GUILayout.Button("ChangeText"))
                        {
                            ButtonScript.SetText(currButtonText);
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        // 3.change icon
                        currIcon = (Sprite)EditorGUILayout.ObjectField(currIcon, typeof(Sprite), true);
                        if (GUILayout.Button("ChangeIcon"))
                        {
                            ButtonScript.SetIcon(currIcon);
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        // 4. remove this button
                        if (GUILayout.Button("REMOVE this Button"))
                        {
                            var btn = currSelection.GetComponent<Button>();
                            btn.onClick.RemoveAllListeners();

                            DestroyImmediate(currSelection);
                        }

                        break;
                    }
                case "RingButton":
                    {
                        var ButtonScript = currSelection.GetComponent<RingButton>();

                        // 1. change text
                        currButtonText = EditorGUILayout.TextField("Button Text: ", currButtonText);
                        if (GUILayout.Button("ChangeText"))
                        {
                            ButtonScript.SetText(currButtonText);
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        // 2. change icon
                        currIcon = (Sprite)EditorGUILayout.ObjectField(currIcon, typeof(Sprite), true);
                        if (GUILayout.Button("ChangeIcon"))
                        {
                            ButtonScript.SetIcon(currIcon);
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        // 3. remove this button
                        if (GUILayout.Button("REMOVE this Button"))
                        {
                            var btn = currSelection.GetComponent<Button>();
                            btn.onClick.RemoveAllListeners();

                            DestroyImmediate(currSelection);
                        }

                        break;
                    }
            }//switch case

        }//Button Modifier


        // Menu Modifier
        if (currSelection != null && (currSelection.tag == "DefaultMenu" || currSelection.tag == "Win10StyleMenu" || currSelection.tag == "RingMenu"))
        {
            GUILayout.Label("Modify a Menu", EditorStyles.boldLabel);

            switch (currSelection.tag)
            {
                case "DefaultMenu":
                    {
                        var MenuScript = currSelection.GetComponent<DefaultMenu>();

                        // 1. change title
                        currMenuText = EditorGUILayout.TextField("Menu Title: ", currMenuText);
                        if (GUILayout.Button("Change Menu Title"))
                        {
                            MenuScript.SetTitle(currMenuText);
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        // 2. add a button

                        // judge button count
                        GUILayout.Label("ADD a Button", EditorStyles.boldLabel);

                        var trans = currSelection.transform;
                        bool _exceededMax = false;
                        foreach (Transform currTrans in trans)
                        {
                            if (currTrans.tag == "ButtonPanel")
                            {
                                if (currTrans.childCount >= BUTTONNUMMAX_DEFAULT)
                                {
                                    _exceededMax = true;
                                    GUILayout.Label("    WARNING: Too many buttons!", EditorStyles.boldLabel);
                                    break;
                                }
                            }
                        }
                        if (_exceededMax)
                            break;

                        currButtonText = EditorGUILayout.TextField("    Name: ", currButtonText);
                        currButtonPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/xRMenuDesigner/Resources/Prefabs/DefaultMenu/DefaultButton.prefab", typeof(GameObject));
                        currButtonType = EditorGUILayout.IntPopup("    Type: ", currButtonType, _typeNames, _types);
                        currIcon = (Sprite)EditorGUILayout.ObjectField(currIcon, typeof(Sprite), true);
                        if (currButtonType == 1)
                        {
                            _buttonSubMenu = (GameObject)EditorGUILayout.ObjectField(_buttonSubMenu, typeof(GameObject), true);
                        }
                        
                        if (GUILayout.Button("ADD a Button"))
                        {
                            GameObject tempButton;
                            DefaultButton tempButtonScirpt;

                            foreach (Transform currTrans in trans)
                            {
                                if (currTrans.tag == "ButtonPanel")
                                {
                                    tempButton = Instantiate(currButtonPrefab, currTrans);
                                    tempButtonScirpt = tempButton.GetComponent<DefaultButton>();
                                    if (currButtonType == 1)
                                    {
                                        tempButtonScirpt.SetAllowSubMenu(true);
                                    }
                                    else
                                    {
                                        tempButtonScirpt.SetAllowSubMenu(false);
                                        tempButtonScirpt.SetSubMenuRef(null);
                                    }

                                    tempButtonScirpt.SetIcon(currIcon);
                                    tempButtonScirpt.SetText(currButtonText);
                                    break;
                                }
                            }
                            // auto resize
                            MenuScript.AddOneButton();
                        }
                        break;
                    }

                case "Win10StyleMenu":
                    {
                        var MenuScript = currSelection.GetComponent<Win10StyleMenu>();

                        // 1. change title
                        currMenuText = EditorGUILayout.TextField("Menu Title: ", currMenuText);
                        if (GUILayout.Button("Change Menu Title"))
                        {
                            MenuScript.SetTitle(currMenuText);
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        // 2. add a button

                        // judge button count
                        GUILayout.Label("ADD a Button", EditorStyles.boldLabel);

                        var trans = currSelection.transform;
                        bool _exceededMax = false;
                        foreach (Transform currTrans in trans)
                        {
                            if (currTrans.tag == "ButtonPanel")
                            {
                                if (currTrans.childCount >= BUTTONNUMMAX_WIN10)
                                {
                                    _exceededMax = true;
                                    GUILayout.Label("    WARNING: Too many buttons!", EditorStyles.boldLabel);
                                    break;
                                }
                            }
                        }
                        if (_exceededMax)
                            break;

                        currButtonText = EditorGUILayout.TextField("    Name: ", currButtonText);
                        currButtonPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/xRMenuDesigner/Resources/Prefabs/Win10StyleMenu/Win10StyleButton.prefab", typeof(GameObject));
                        currButtonType = EditorGUILayout.IntPopup("    Type: ", currButtonType, _typeNames, _types);
                        currIcon = (Sprite)EditorGUILayout.ObjectField(currIcon, typeof(Sprite), true);

                        if (currButtonType == 1)
                        {
                            _buttonSubMenu = (GameObject)EditorGUILayout.ObjectField(_buttonSubMenu, typeof(GameObject), true);
                        }
                        if (GUILayout.Button("ADD a Button"))
                        {
                            GameObject tempButton;
                            Win10StyleButton tempButtonScirpt;

                            foreach (Transform currTrans in trans)
                            {
                                if (currTrans.tag == "ButtonPanel")
                                {
                                    tempButton = Instantiate(currButtonPrefab, currTrans);
                                    tempButtonScirpt = tempButton.GetComponent<Win10StyleButton>();
                                    if (currButtonType == 1)
                                    {
                                        tempButtonScirpt.SetAllowSubMenu(true);
                                    }
                                    else
                                    {
                                        tempButtonScirpt.SetAllowSubMenu(false);
                                        tempButtonScirpt.SetSubMenuRef(null);
                                    }
                                    tempButtonScirpt.SetIcon(currIcon);
                                    tempButtonScirpt.SetText(currButtonText);
                                    break;
                                }
                            }

                        }
                        break;
                    }

                case "RingMenu":
                    {
                        // 1. add a button

                        // judge button count
                        GUILayout.Label("ADD a Button", EditorStyles.boldLabel);

                        var trans = currSelection.transform;
                        bool _exceededMax = false;
                        foreach (Transform currTrans in trans)
                        {
                            if (currTrans.tag == "ButtonPanel")
                            {
                                if (currTrans.childCount >= BUTTONNUMMAX_RING)
                                {
                                    _exceededMax = true;
                                    GUILayout.Label("    WARNING: Too many buttons!", EditorStyles.boldLabel);
                                    break;
                                }
                            }
                        }
                        if (_exceededMax)
                            break;

                        currButtonText = EditorGUILayout.TextField("    Name: ", currButtonText);
                        currButtonPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/xRMenuDesigner/Resources/Prefabs/RingMenu/RingButton.prefab", typeof(GameObject));
                        currIcon = (Sprite)EditorGUILayout.ObjectField(currIcon, typeof(Sprite), true);

                        if (currButtonType == 1)
                        {
                            _buttonSubMenu = (GameObject)EditorGUILayout.ObjectField(_buttonSubMenu, typeof(GameObject), true);
                        }
                        if (GUILayout.Button("ADD a Button"))
                        {
                            GameObject tempButton;

                            RingButton tempButtonScirpt;
                            foreach (Transform currTrans in trans)
                            {
                                if (currTrans.tag == "ButtonPanel")
                                {
                                    tempButton = Instantiate(currButtonPrefab, currTrans);
                                    tempButtonScirpt = tempButton.GetComponent<RingButton>();
                                    tempButtonScirpt.SetIcon(currIcon);
                                    tempButtonScirpt.SetText(currButtonText);
                                    break;
                                }
                            }
                        }
                        break;
                    }
            }
        }//Menu Modifier

    }//onGUI()

}
