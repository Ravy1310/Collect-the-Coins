#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEditor.SceneManagement;
using TMPro;

/// <summary>
/// Editor utility: buka MainMenu scene dan bangun UI-nya secara programatik.
/// Jalankan via menu Tools > Build Main Menu UI
/// </summary>
public static class MainMenuBuilder
{
    [MenuItem("Tools/Build Main Menu UI")]
    public static void BuildMainMenuUI()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        var mainMenuScene = EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
        if (!mainMenuScene.IsValid()) { Debug.LogError("MainMenu.unity not found!"); return; }

        // Bersihkan lama
        foreach (var name in new[] { "MainMenu_Canvas", "EventSystem" }) {
            var old = GameObject.Find(name);
            if (old != null) Object.DestroyImmediate(old);
        }

        // EventSystem
        var esGO = new GameObject("EventSystem");
        esGO.AddComponent<EventSystem>();
        esGO.AddComponent<StandaloneInputModule>();

        // Canvas
        var canvasGO = new GameObject("MainMenu_Canvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;
        canvasGO.AddComponent<GraphicRaycaster>();
        var ctrl = canvasGO.AddComponent<MainMenuController>();

        // Warna tema medieval
        var colBg     = new Color(0.06f, 0.04f, 0.02f, 1f);
        var colPanel  = new Color(0.10f, 0.07f, 0.03f, 0.97f);
        var colBtn    = new Color(0.22f, 0.14f, 0.04f, 1f);
        var colBtnH   = new Color(0.42f, 0.26f, 0.07f, 1f);
        var colGold   = new Color(1f, 0.85f, 0.3f, 1f);
        var colSilver = new Color(0.85f, 0.85f, 0.9f, 1f);

        // Background gelap
        var bg = new GameObject("Background");
        bg.transform.SetParent(canvasGO.transform, false);
        bg.AddComponent<Image>().color = colBg;
        StretchFull(bg.GetComponent<RectTransform>());

        // Panel utama
        var panel = new GameObject("MenuPanel");
        panel.transform.SetParent(canvasGO.transform, false);
        panel.AddComponent<Image>().color = colPanel;
        var panelRT = panel.GetComponent<RectTransform>();
        panelRT.anchorMin = panelRT.anchorMax = new Vector2(0.5f, 0.5f);
        panelRT.pivot = new Vector2(0.5f, 0.5f);
        panelRT.sizeDelta = new Vector2(500, 480);
        panelRT.anchoredPosition = Vector2.zero;

        // Border emas
        var border = new GameObject("Border");
        border.transform.SetParent(panel.transform, false);
        border.AddComponent<Image>().color = colGold;
        var bRT = border.GetComponent<RectTransform>();
        bRT.anchorMin = Vector2.zero; bRT.anchorMax = Vector2.one;
        bRT.offsetMin = new Vector2(-3,-3); bRT.offsetMax = new Vector2(3,3);
        border.transform.SetAsFirstSibling();
        border.GetComponent<Image>().raycastTarget = false;

        // Layout
        var vl = panel.AddComponent<VerticalLayoutGroup>();
        vl.padding = new RectOffset(55, 55, 45, 45);
        vl.spacing = 16;
        vl.childAlignment = TextAnchor.MiddleCenter;
        vl.childControlWidth = true;
        vl.childControlHeight = false;
        vl.childForceExpandWidth = true;

        // Judul
        var title = MakeText("Title", panel.transform, "\u2694  DOG KNIGHT  \u2694", 68, colGold, true);
        title.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 90);

        // Sub judul
        var sub = MakeText("Subtitle", panel.transform, "A Knight's Adventure", 28, colSilver, false);
        sub.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 36);
        sub.GetComponent<TextMeshProUGUI>().raycastTarget = false;

        // Separator
        MakeSep(panel.transform, colGold);

        // Tombol Start
        var btnStart = MakeButton("BtnStart", panel.transform, "\u25b6  Start Game", colBtn, colBtnH, colGold);
        btnStart.GetComponent<Button>().onClick.AddListener(ctrl.StartGame);

        // Separator
        MakeSep(panel.transform, colGold);

        // Tombol Quit
        var btnQuit = MakeButton("BtnQuit", panel.transform, "\u2715  Quit", colBtn, colBtnH, colGold);
        btnQuit.GetComponent<Button>().onClick.AddListener(ctrl.QuitGame);

        // Versi kecil
        var ver = MakeText("Version", panel.transform, "v1.0 - Dog Knight Quest", 20, new Color(0.5f,0.5f,0.4f,1f), false);
        ver.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 28);
        ver.GetComponent<TextMeshProUGUI>().raycastTarget = false;

        EditorSceneManager.MarkSceneDirty(mainMenuScene);
        EditorSceneManager.SaveScene(mainMenuScene);
        Debug.Log("Main Menu UI rebuilt with medieval theme!");
    }

    static GameObject MakeText(string name, Transform parent, string text, int fs, Color col, bool bold)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text = text; tmp.fontSize = fs;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = col;
        if (bold) tmp.fontStyle = FontStyles.Bold;
        return go;
    }

    static GameObject MakeButton(string name, Transform parent, string label, Color normal, Color hover, Color textCol)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var img = go.AddComponent<Image>(); img.color = normal;
        var btn = go.AddComponent<Button>();
        var cb = btn.colors;
        cb.normalColor = normal; cb.highlightedColor = hover;
        cb.pressedColor = new Color(0.6f, 0.38f, 0.08f, 1f);
        cb.fadeDuration = 0.08f;
        btn.colors = cb;
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 72);

        // Outline tipis
        var ol = new GameObject("Outline");
        ol.transform.SetParent(go.transform, false);
        ol.AddComponent<Image>().color = new Color(textCol.r, textCol.g, textCol.b, 0.4f);
        var olRT = ol.GetComponent<RectTransform>();
        olRT.anchorMin = Vector2.zero; olRT.anchorMax = Vector2.one;
        olRT.offsetMin = new Vector2(-1,-1); olRT.offsetMax = new Vector2(1,1);
        ol.GetComponent<Image>().raycastTarget = false;
        ol.transform.SetAsFirstSibling();

        var lbl = new GameObject("Label");
        lbl.transform.SetParent(go.transform, false);
        var tmp = lbl.AddComponent<TextMeshProUGUI>();
        tmp.text = label; tmp.fontSize = 36;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = textCol; tmp.fontStyle = FontStyles.Bold;
        var lrt = lbl.GetComponent<RectTransform>();
        lrt.anchorMin = Vector2.zero; lrt.anchorMax = Vector2.one;
        lrt.offsetMin = Vector2.zero; lrt.offsetMax = Vector2.zero;
        return go;
    }

    static void MakeSep(Transform parent, Color col)
    {
        var go = new GameObject("Separator");
        go.transform.SetParent(parent, false);
        go.AddComponent<Image>().color = new Color(col.r, col.g, col.b, 0.35f);
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 2);
        go.GetComponent<Image>().raycastTarget = false;
    }

    static void StretchFull(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero; rt.offsetMax = Vector2.zero;
    }
}
#endif
