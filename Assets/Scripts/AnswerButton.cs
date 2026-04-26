using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    //is told by the questionmanager if this answer is correct or wrong.
    private bool correctAnswer;
    //reference to the text that displays the answer.
    [SerializeField]
    private TextMeshProUGUI answerText;
    [SerializeField]
    private Image buttonImage;
    [SerializeField]
    private Button thisButton;
    [SerializeField]
    private GameManager gameManager;
    
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

    //method called when button is clicked, starts the OnAnswered Coroutine.
    public void OnClick()
    {
        StartCoroutine(OnAnswered());
    }    

    //Tells the GameManager if the answer is correct or not, colours the button correspondingly and sets the other ones on un-interactable.
    public IEnumerator OnAnswered()
    {
        gameManager.SetOtherButtonsInactive(thisButton);
        //checks if this is the correct answer using the boolean set by QuestionManager.
        if (correctAnswer)
        {
            buttonImage.color = Color.green;
            yield return new WaitForSeconds(gameManager.answerCoolDown);
            gameManager.AnsweredCorrectly();
            buttonImage.color = Color.white;
        }
        else
        {
            buttonImage.color = Color.red;
            yield return new WaitForSeconds(gameManager.answerCoolDown);
            gameManager.AnsweredIncorrectly();
            buttonImage.color = Color.white;
        }
    }
}
