using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    [Header("Level Data")]
    public LevelSO level;

    [Header("UI Refs")]
    public Image imageHolder;           // TMP/Image utk gambar soal
    public TMP_Text promptText;         // deskripsi
    public TMP_InputField inputField;   // jawaban ketik
    public Button submitBtn;
    public Button nextBtn;
    public TMP_Text feedbackText;       // benar/salah + penjelasan
    public TMP_Text progressText;       // "Soal 2/5"

    [Header("FX (opsional)")]
    public AudioSource audioSource;     // buat ttsClip jika ada

    // runtime
    private List<QuestionSO> _round;    // 5 soal terpilih
    private int _index = -1;
    private int _correct = 0;

    void Start()
    {
        BuildRound();
        NextQuestion();
        submitBtn.onClick.AddListener(Submit);
        nextBtn.onClick.AddListener(NextQuestion);
        nextBtn.gameObject.SetActive(false);
        feedbackText.text = "";
    }

    void BuildRound()
    {
        // salin pool
        var pool = new List<QuestionSO>(level.questionPool);
        if (level.shuffleEveryPlay) Shuffle(pool);
        // ambil N pertama sesuai batas
        int N = Mathf.Min(level.questionsPerRound, pool.Count);
        _round = pool.GetRange(0, N);
        _index = -1;
        _correct = 0;
    }

    void SetUI(QuestionSO q)
    {
        //  Animasi masuk (pop) gambar soal
        var rt = imageHolder.rectTransform;
        rt.localScale = Vector3.one * 0.95f;
        LeanTween.scale(rt, Vector3.one, 0.25f).setEaseOutBack();

        //  Update UI konten soal
        imageHolder.sprite = q.image;
        promptText.text = q.prompt;
        inputField.text = "";
        inputField.interactable = true;
        submitBtn.interactable = true;
        nextBtn.gameObject.SetActive(false);
        feedbackText.text = "";
        progressText.text = $"Soal {_index + 1}/{_round.Count}";

        inputField.Select();
        inputField.ActivateInputField();

        if (q.ttsClip && audioSource)
        {
            audioSource.clip = q.ttsClip;
            audioSource.Play();
        }
    }


    public void NextQuestion()
    {
        _index++;
        if (_index >= _round.Count)
        {
            FinishRound();
            return;
        }
        SetUI(_round[_index]);
    }

    public void Submit()
    {
        var q = _round[_index];
        var user = inputField.text;
        bool ok = AnswerUtils.Matches(user, q.answer);

        if (!ok && q.acceptedAnswers != null)
            foreach (var alt in q.acceptedAnswers)
                if (AnswerUtils.Matches(user, alt)) { ok = true; break; }

        if (ok) _correct++;

        // feedback
        inputField.interactable = false;
        submitBtn.interactable = false;
        nextBtn.gameObject.SetActive(true);

        var ans = $"Jawaban: <b>{q.answer}</b>";
        var note = string.IsNullOrWhiteSpace(q.prompt) ? "" : $"\n{q.prompt}";
        feedbackText.text = ok ? $"<color=#2ecc71>Benar!</color>\n{ans}{note}"
                               : $"<color=#e74c3c>Salah.</color>\n{ans}{note}";
    }

    void FinishRound()
    {
        // tampilkan ringkasan sederhana
        imageHolder.sprite = null;
        promptText.text = "";
        inputField.gameObject.SetActive(false);
        submitBtn.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);
        feedbackText.text = $"Selesai!\nNilai: <b>{_correct} / {_round.Count}</b>";

        // simpan progres
        var key = $"level_{level.name}_best";
        int best = PlayerPrefs.GetInt(key, 0);
        if (_correct > best) PlayerPrefs.SetInt(key, _correct);
        PlayerPrefs.Save();
    }

    // Fisher–Yates
    static void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
