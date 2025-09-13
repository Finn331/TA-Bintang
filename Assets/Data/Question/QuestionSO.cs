using UnityEngine;

[CreateAssetMenu(menuName = "MACA/Question", fileName = "Q_New")]
public class QuestionSO : ScriptableObject
{
    [Header("Konten")]
    public Sprite image;                // gambar jeruk dll
    [TextArea] public string prompt;    // deskripsi/penjelasan
    public string answer;               // jawaban utama (mis: "jeruk")

    [Header("Jawaban Tambahan (opsional)")]
    public string[] acceptedAnswers;    // sinonim/variasi ejaan
    public AudioClip ttsClip;           // opsional: audio/hints
}
