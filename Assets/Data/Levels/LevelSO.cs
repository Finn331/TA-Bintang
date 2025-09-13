using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MACA/Level", fileName = "Level_New")]
public class LevelSO : ScriptableObject
{
    [Min(1)] public int questionsPerRound = 5;
    public List<QuestionSO> questionPool; // drag & drop bank soal untuk level ini
    public bool shuffleEveryPlay = true;  // acak setiap main
}
