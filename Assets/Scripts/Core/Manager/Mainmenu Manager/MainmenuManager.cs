using UnityEngine;

public class MainmenuManager : MonoBehaviour
{
    [Header("Before Main Menu Panel")]
    [SerializeField] GameObject headerMACA;
    [SerializeField] GameObject bodyMACA;
    [SerializeField] GameObject buttonMulai;

    [Header("Setting Panel")]
    [SerializeField] GameObject panelSetting;

    [Header("Main Menu Panel")]
    [SerializeField] GameObject mainMenuPanel;

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
        // Disable Header and Body MACA
        headerMACA.SetActive(false);
        bodyMACA.SetActive(false);
        buttonMulai.SetActive(false);        

        // Enable Setting Panel
        panelSetting.SetActive(true);
        LeanTween.scale(panelSetting, new Vector3(0.56695f, 0.56695f, 0.56695f), 0.5f).setEase(LeanTweenType.easeOutBack);
    }

    public void CloseSetting()
    {
        // Disable Setting Panel
        LeanTween.scale(panelSetting, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            panelSetting.SetActive(false);

            // Enable Header, Body MACA, and Button Mulai
            headerMACA.SetActive(true);
            bodyMACA.SetActive(true);
            buttonMulai.SetActive(true);
            
        });
    }

    public void Mulai()
    {
        // Disable Header and Body MACA
        headerMACA.SetActive(false);
        bodyMACA.SetActive(false);
        buttonMulai.SetActive(false);
        // Enable Main Menu Panel
        mainMenuPanel.SetActive(true);
        LeanTween.scale(mainMenuPanel, new Vector3(0.56695f, 0.56695f, 0.56695f), 0.5f).setEase(LeanTweenType.easeOutBack);
    }
}
