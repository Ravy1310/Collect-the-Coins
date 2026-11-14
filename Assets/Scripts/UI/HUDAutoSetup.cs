using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Dibuat sebagai komponen di GameObject mana saja di scene Level1.
/// Saat Start(), script ini otomatis membuat Canvas HUD dan CoinCounter
/// jika belum ada, sehingga tidak perlu setup manual di Editor.
/// FR-10
/// </summary>
public class HUDAutoSetup : MonoBehaviour
{
    private void Awake()
    {
        SetupCoinCounter();
        SetupHUD();
    }

    private void SetupCoinCounter()
    {
        if (CoinCounter.Instance != null) return;

        GameObject go = new GameObject("CoinCounter");
        go.AddComponent<CoinCounter>();
        DontDestroyOnLoad(go);
    }

    private void SetupHUD()
    {
        // Jangan buat duplikat jika sudah ada
        if (FindFirstObjectByType<CoinCounterView>() != null) return;

        // Canvas
        GameObject canvasGO = new GameObject("HUD_Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 10;

        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;

        canvasGO.AddComponent<GraphicRaycaster>();

        // Panel kiri atas sebagai background teks
        GameObject panelGO = new GameObject("CoinPanel");
        panelGO.transform.SetParent(canvasGO.transform, false);
        RectTransform panelRect = panelGO.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0, 1);
        panelRect.anchorMax = new Vector2(0, 1);
        panelRect.pivot     = new Vector2(0, 1);
        panelRect.anchoredPosition = new Vector2(20, -20);
        panelRect.sizeDelta = new Vector2(300, 80);

        // Teks koin
        GameObject coinTextGO = new GameObject("Text_CoinCount");
        coinTextGO.transform.SetParent(panelGO.transform, false);
        TextMeshProUGUI coinTMP = coinTextGO.AddComponent<TextMeshProUGUI>();
        coinTMP.text      = "Coins: 0 / 16";
        coinTMP.fontSize  = 28;
        coinTMP.color     = Color.white;
        coinTMP.fontStyle = FontStyles.Bold;
        RectTransform coinRect = coinTextGO.GetComponent<RectTransform>();
        coinRect.anchorMin        = Vector2.zero;
        coinRect.anchorMax        = Vector2.one;
        coinRect.offsetMin        = Vector2.zero;
        coinRect.offsetMax        = new Vector2(0, -40);

        // Teks objective
        GameObject objTextGO = new GameObject("Text_Objective");
        objTextGO.transform.SetParent(panelGO.transform, false);
        TextMeshProUGUI objTMP = objTextGO.AddComponent<TextMeshProUGUI>();
        objTMP.text     = "Objective: Collect all coins";
        objTMP.fontSize = 20;
        objTMP.color    = new Color(1f, 0.9f, 0.4f);
        RectTransform objRect = objTextGO.GetComponent<RectTransform>();
        objRect.anchorMin        = Vector2.zero;
        objRect.anchorMax        = Vector2.one;
        objRect.offsetMin        = new Vector2(0, 0);
        objRect.offsetMax        = new Vector2(0, -48);
        objRect.anchoredPosition = new Vector2(0, -48);

        // Pasang CoinCounterView ke canvas dan assign referensi
        CoinCounterView view = canvasGO.AddComponent<CoinCounterView>();
        view.SetReferences(coinTMP, objTMP);
    }
}
