using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Creator: EditorWindow
{
    [MenuItem("MenuDesigner/Menu Creator")]
    public static void ShowWindow()
    {
        GetWindow<Creator>("Menu Creator");
    }


    // Maximum number of buttons
    private static int MAXBUTTONNUM = 20;
    int BUTTONNUMMAX_DEFAULT = 20;
    int BUTTONNUMMAX_WIN10 = 9;
    int BUTTONNUMMAX_RING = 20;

    int ButtonNum;
    int[] _ButtonTypes = new int[MAXBUTTONNUM];
    string[] _ButtonNames = new string[MAXBUTTONNUM];
    GameObject[] _ButtonSubMenus = new GameObject[MAXBUTTONNUM];
    Sprite[] _ButtonIcons = new Sprite[MAXBUTTONNUM];
    string TitleText;
    Vector2 scrollPosition;

    GameObject ButtonPrefab;
    GameObject MenuPrefab;

    string[] MenuTypes = new string[] { "Default Menu", "Win10Style Menu", "Ring Menu", "Radial Menu" };
    int MenuType;
    bool isRootMenu;
    string NameText;

    // button type, 0: trigger function; 1: trigger sub menu
    int[] _types = { 0, 1 };
    string[] _typeNames = new string[] { "Functional", "Hierarchical" };

    void OnGUI()
    {
        GUILayout.Label("Creat VR menus", EditorStyles.boldLabel);

        NameText = EditorGUILayout.TextField("Menu Name: ", NameText);
        MenuType = EditorGUILayout.Popup("Menu Type: ", MenuType, MenuTypes);

        switch (MenuType)
        {
            case 0: // default menu
                {
                    // is root menu?
                    isRootMenu = EditorGUILayout.Toggle("Root Menu? ", isRootMenu);

                    // menu title
                    TitleText = EditorGUILayout.TextField("Menu Title: ", TitleText);

                    // buttons
                    ButtonNum = EditorGUILayout.IntField("Number of Buttons: ", ButtonNum);
                    if (ButtonNum > BUTTONNUMMAX_DEFAULT) ButtonNum = BUTTONNUMMAX_DEFAULT;
                    else if (ButtonNum < 0) ButtonNum = 0;

                    EditorGUILayout.Space();
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition);

                    int[] ButtonTypes = new int[ButtonNum];
                    string[] ButtonNames = new string[ButtonNum];
                    GameObject[] ButtonSubMenus = new GameObject[ButtonNum];
                    Sprite[] ButtonIcons = new Sprite[ButtonNum];

                    GUILayout.BeginVertical();

                    for (int i = 0; i < ButtonNum; i++)
                    {
                        EditorGUILayout.LabelField("Button " + i);

                        // button names
                        _ButtonNames[i] = EditorGUILayout.TextField("    Name: ", _ButtonNames[i]);
                        // button types
                        _ButtonTypes[i] = EditorGUILayout.IntPopup("    Type: ", _ButtonTypes[i], _typeNames, _types);
                        if (_ButtonTypes[i] == 1) // Hierarchical button
                        {
                            _ButtonSubMenus[i] = (GameObject)EditorGUILayout.ObjectField("    Sub Menu Ref: ", _ButtonSubMenus[i], typeof(GameObject), true);

                            ButtonSubMenus[i] = _ButtonSubMenus[i];
                        }

                        // button icons
                        _ButtonIcons[i] = (Sprite)EditorGUILayout.ObjectField(_ButtonIcons[i], typeof(Sprite), true);

                        ButtonNames[i] = _ButtonNames[i];
                        ButtonTypes[i] = _ButtonTypes[i];
                        ButtonIcons[i] = _ButtonIcons[i];
                    }

                    EditorGUILayout.Space();

                    if (GUILayout.Button("Create a Menu!"))
                    {
                        //Debug.Log("Num of Btns: " + ButtonNum);

                        MenuPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/xRMenuDesigner/Resources/Prefabs/DefaultMenu/DefaultMenu.prefab", typeof(GameObject));

                        GameObject tempMenu = Instantiate(MenuPrefab);
                        tempMenu.name = NameText;

                        var MenuScript = tempMenu.GetComponent<DefaultMenu>();
                        MenuScript.InitMenu(ButtonNum, isRootMenu);
                        MenuScript.SetTitle(TitleText);

                        // create Buttons
                        for (int i = 0; i < ButtonNum; i++)
                        {
                            ButtonPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/xRMenuDesigner/Resources/Prefabs/DefaultMenu/DefaultButton.prefab", typeof(GameObject));

                            var trans = tempMenu.transform;
                            foreach (Transform currTrans in trans)
                            {
                                if (currTrans.tag == "ButtonPanel")
                                {
                                    GameObject tempButton = Instantiate(ButtonPrefab, currTrans);
                                    var ButtonScript = tempButton.GetComponent<DefaultButton>();
                                    ButtonScript.SetText(ButtonNames[i]);
                                    ButtonScript.SetIcon(ButtonIcons[i]);
                                    ButtonScript.SetSubMenuRef(ButtonSubMenus[i]);
                                }
                            }
                        }
                    }

                    GUILayout.EndScrollView();
                    GUILayout.EndVertical();
                    break;
                }

            case 1: // win10style menu
                {
                    // is root menu?
                    isRootMenu = EditorGUILayout.Toggle("Root Menu? ", isRootMenu);

                    // menu title
                    TitleText = EditorGUILayout.TextField("Menu Title: ", TitleText);

                    // buttons
                    ButtonNum = EditorGUILayout.IntField("Number of Buttons: ", ButtonNum);
                    if (ButtonNum > BUTTONNUMMAX_WIN10) ButtonNum = BUTTONNUMMAX_WIN10;
                    else if (ButtonNum < 0) ButtonNum = 0;

                    EditorGUILayout.Space();
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition);

                    int[] ButtonTypes = new int[ButtonNum];
                    string[] ButtonNames = new string[ButtonNum];
                    GameObject[] ButtonSubMenus = new GameObject[ButtonNum];
                    Sprite[] ButtonIcons = new Sprite[ButtonNum];

                    GUILayout.BeginVertical();

                    for (int i = 0; i < ButtonNum; i++)
                    {
                        EditorGUILayout.LabelField("Button " + i);

                        // button names
                        _ButtonNames[i] = EditorGUILayout.TextField("    Name: ", _ButtonNames[i]);
                        // button types
                        _ButtonTypes[i] = EditorGUILayout.IntPopup("    Type: ", _ButtonTypes[i], _typeNames, _types);
                        if (_ButtonTypes[i] == 1) // Hierarchical button
                        {
                            _ButtonSubMenus[i] = (GameObject)EditorGUILayout.ObjectField("    Sub Menu Ref: ", _ButtonSubMenus[i], typeof(GameObject), true);

                            ButtonSubMenus[i] = _ButtonSubMenus[i];
                        }

                        // button icons
                        _ButtonIcons[i] = (Sprite)EditorGUILayout.ObjectField(_ButtonIcons[i], typeof(Sprite), true);

                        ButtonNames[i] = _ButtonNames[i];
                        ButtonTypes[i] = _ButtonTypes[i];
                        ButtonIcons[i] = _ButtonIcons[i];
                    }

                    EditorGUILayout.Space();

                    if (GUILayout.Button("Create a Menu!"))
                    {
                        //Debug.Log("Num of Btns: " + ButtonNum);

                        MenuPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/xRMenuDesigner/Resources/Prefabs/Win10StyleMenu/Win10StyleMenu.prefab", typeof(GameObject));

                        GameObject tempMenu = Instantiate(MenuPrefab);
                        tempMenu.name = NameText;

                        var MenuScript = tempMenu.GetComponent<Win10StyleMenu>();
                        MenuScript.InitMenu(ButtonNum, isRootMenu);
                        MenuScript.SetTitle(TitleText);

                        // create Buttons
                        for (int i = 0; i < ButtonNum; i++)
                        {
                            ButtonPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/xRMenuDesigner/Resources/Prefabs/Win10StyleMenu/Win10StyleButton.prefab", typeof(GameObject));

                            var trans = tempMenu.transform;
                            foreach (Transform currTrans in trans)
                            {
                                if (currTrans.tag == "ButtonPanel")
                                {
                                    GameObject tempButton = Instantiate(ButtonPrefab, currTrans);
                                    var ButtonScript = tempButton.GetComponent<Win10StyleButton>();
                                    ButtonScript.SetText(ButtonNames[i]);
                                    ButtonScript.SetIcon(ButtonIcons[i]);
                                    ButtonScript.SetSubMenuRef(ButtonSubMenus[i]);
                                }
                            }
                        }
                    }

                    GUILayout.EndScrollView();
                    GUILayout.EndVertical();
                    break;
                }
            case 2: // ring menu
                {
                    // is root menu?
                    isRootMenu = EditorGUILayout.Toggle("Root Menu? ", isRootMenu);

                    // buttons
                    ButtonNum = EditorGUILayout.IntField("Number of Buttons: ", ButtonNum);
                    if (ButtonNum > BUTTONNUMMAX_RING) ButtonNum = BUTTONNUMMAX_RING;
                    else if (ButtonNum < 0) ButtonNum = 0;

                    EditorGUILayout.Space();
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition);

                    string[] ButtonNames = new string[ButtonNum];
                    Sprite[] ButtonIcons = new Sprite[ButtonNum];

                    GUILayout.BeginVertical();

                    for (int i = 0; i < ButtonNum; i++)
                    {
                        EditorGUILayout.LabelField("Button " + i);

                        // button names
                        _ButtonNames[i] = EditorGUILayout.TextField("    Name: ", _ButtonNames[i]);

                        // button icons
                        _ButtonIcons[i] = (Sprite)EditorGUILayout.ObjectField(_ButtonIcons[i], typeof(Sprite), true);

                        ButtonNames[i] = _ButtonNames[i];
                        ButtonIcons[i] = _ButtonIcons[i];
                    }

                    EditorGUILayout.Space();

                    if (GUILayout.Button("Create a Menu!"))
                    {
                        //Debug.Log("Num of Btns: " + ButtonNum);

                        MenuPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/xRMenuDesigner/Resources/Prefabs/RingMenu/RingMenu.prefab", typeof(GameObject));

                        GameObject tempMenu = Instantiate(MenuPrefab);
                        tempMenu.name = NameText;

                        // create Buttons
                        for (int i = 0; i < ButtonNum; i++)
                        {
                            ButtonPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/xRMenuDesigner/Resources/Prefabs/RingMenu/RingButton.prefab", typeof(GameObject));

                            var trans = tempMenu.transform;
                            foreach (Transform currTrans in trans)
                            {
                                if (currTrans.tag == "ButtonPanel")
                                {
                                    GameObject tempButton = Instantiate(ButtonPrefab, currTrans);
                                    var ButtonScript = tempButton.GetComponent<RingButton>();
                                    ButtonScript.SetText(ButtonNames[i]);
                                    ButtonScript.SetIcon(ButtonIcons[i]);
                                }
                            }
                        }
                    }

                    GUILayout.EndScrollView();
                    GUILayout.EndVertical();
                    break;
                }
            case 3: // radial menu
                {
                    if (GUILayout.Button("Create a Menu!"))
                    {
                        MenuPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/xRMenuDesigner/Resources/Prefabs/RadialMenu/RadialMenu.prefab", typeof(GameObject));

                        GameObject tempMenu = Instantiate(MenuPrefab);
                    }

                    break;
                }
        }



    }

}
