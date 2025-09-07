using UnityEngine;

public class MovingCloud : MonoBehaviour
{
    public RectTransform canvasRect;   // drag Canvas di Inspector
    public float speed = 30f;          // kecepatan awan (px per detik)
    public float extraOffset;   // jarak tambahan sebelum respawn (biar smooth)

    private RectTransform rt;
    private float width;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        width = rt.rect.width;

        StartLoop();
    }

    void StartLoop()
    {
        // mulai dari posisi X sekarang geser ke kanan sampai keluar canvas
        float targetX = canvasRect.rect.width / 2 + width + extraOffset;

        LeanTween.moveX(rt, targetX, GetDuration(targetX))
            .setEase(LeanTweenType.linear)
            .setOnComplete(() =>
            {
                // reset ke kiri (di luar canvas)
                float startX = -(canvasRect.rect.width / 2 + width + extraOffset);
                rt.anchoredPosition = new Vector2(startX, rt.anchoredPosition.y);

                // ulangi lagi
                StartLoop();
            });
    }

    float GetDuration(float targetX)
    {
        float distance = Mathf.Abs(targetX - rt.anchoredPosition.x);
        return distance / speed;
    }
}
