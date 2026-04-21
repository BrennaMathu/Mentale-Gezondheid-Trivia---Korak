using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Questions", order = 1)]
public class QuestionData : ScriptableObject
{
    public string category;
    public string question;
    [Tooltip("4 answers, put correct answer first")]
    public string[] answers;
}
