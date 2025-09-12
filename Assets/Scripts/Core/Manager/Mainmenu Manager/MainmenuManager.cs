using UnityEngine;
using UnityEngine.UI;

public class MainmenuManager : MonoBehaviour
{
    [Header("Before Main Menu Panel")]
    [SerializeField] GameObject headerMACA;
    [SerializeField] GameObject bodyMACA;
    [SerializeField] GameObject buttonMulai;

    [Header("Setting Panel")]
    [SerializeField] GameObject panelSetting;
    [SerializeField] Button settingButton;

    [Header("Main Menu Panel")]
    [SerializeField] GameObject mainMenuPanel;

    [Header("Latihan Panel")]
    [SerializeField] GameObject latihanPanel;

    // Private Bool
    bool isMulaiClicked;

    void Start()
    {
        // Mulai animasi tombol MULAI
        MovingButton();
    }

    private void MovingButton()
    {
        // Pastikan rotasi awal 0
        buttonMulai.transform.rotation = Quaternion.identity;

        // LeanTween rotasi Z ke 3 lalu -3 secara loop pingpong
        LeanTween.rotateZ(buttonMulai, 3f, 1.2f)
            .setEaseInOutSine()
            .setLoopPingPong();
    }

    public void OpenSetting()
    {
        // Disable Setting Button to prevent multiple clicks
        settingButton.interactable = false;
        // Disable Header and Body MACA
        headerMACA.SetActive(false);
        bodyMACA.SetActive(false);
        buttonMulai.SetActive(false);        

        // Enable Setting Panel
        panelSetting.SetActive(true);
        LeanTween.scale(panelSetting, new Vector3(0.56695f, 0.56695f, 0.56695f), 0.5f).setEase(LeanTweenType.easeOutBack);

        // Check if Mulai button was clicked
        if (isMulaiClicked == true)
        {
            // Disable Header and Body MACA
            headerMACA.SetActive(false);
            bodyMACA.SetActive(false);
            buttonMulai.SetActive(false);
        }
    }

    public void CloseSetting()
    {
        // Disable Setting Panel
        LeanTween.scale(panelSetting, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            panelSetting.SetActive(false);
            
            settingButton.interactable = true;

            // Enable Header, Body MACA, and Button Mulai
            headerMACA.SetActive(true);
            bodyMACA.SetActive(true);
            buttonMulai.SetActive(true);

            if (isMulaiClicked == true)
            {
                // Disable Header and Body MACA
                headerMACA.SetActive(false);
                bodyMACA.SetActive(false);
                buttonMulai.SetActive(false);
            }

        });
    }

    public void OpenLatihan()
    {
        LeanTween.scale(mainMenuPanel, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            mainMenuPanel.SetActive(false);
            // Enable Latihan Panel
            latihanPanel.SetActive(true);
            LeanTween.scale(latihanPanel, new Vector3(0.56695f, 0.56695f, 0.56695f), 0.5f).setEase(LeanTweenType.easeOutBack);
        });
    }

    public void BackLatihan()
    {
        LeanTween.scale(latihanPanel, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            latihanPanel.SetActive(false);
            // Enable Main Menu Panel
            mainMenuPanel.SetActive(true);
            LeanTween.scale(mainMenuPanel, new Vector3(0.56695f, 0.56695f, 0.56695f), 0.5f).setEase(LeanTweenType.easeOutBack);
        });
    }
    public void Mulai()
    {
        // Set isMulaiClicked to true
        isMulaiClicked = true;

        // Check isMulaiClicked
        if (isMulaiClicked == true)
        {
            // Disable Header and Body MACA
            headerMACA.SetActive(false);
            bodyMACA.SetActive(false);
            buttonMulai.SetActive(false);
        }
        
        // Enable Main Menu Panel
        mainMenuPanel.SetActive(true);
        LeanTween.scale(mainMenuPanel, new Vector3(0.56695f, 0.56695f, 0.56695f), 0.5f).setEase(LeanTweenType.easeOutBack);
    }
}
