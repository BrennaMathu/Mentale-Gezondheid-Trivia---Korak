using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    //is told by the questionmanager if this answer is correct or wrong.
    private bool correctAnswer;
    //reference to the text that displays the answer.
    [SerializeField]
    private TextMeshProUGUI answerText;

    //called by QuestionManager to set the answerText
    public void SetAnswerText(string newAnswerText)
    {
        answerText.text = newAnswerText;
    }

    //called by QuestionManager to set if this is the correct answer or not.
    public void SetCorrectAnswer(bool newCorrectAnswer)
    {
        correctAnswer = newCorrectAnswer;
    }

    //method called when button is clicked, checks if this is the correct answer using the boolean set by QuestionManager.
    public void OnClick()
    {
        if (correctAnswer)
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Wrong");
        }
    }
}
