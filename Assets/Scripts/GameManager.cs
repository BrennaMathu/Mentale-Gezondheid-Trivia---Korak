using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private QuestionManager questionManager;
    [SerializeField]
    private PowerUpManager powerUpManager;
    
    [SerializeField]
    private Button[] answerButtons;
    public float answerCoolDown;
    
    [Header("Answer Panel")]
    [SerializeField]
    private GameObject answerPanel;
    [SerializeField] 
    private GameObject nextQuestionButton;
    [SerializeField]
    private GameObject restartButton;
    
    [SerializeField] 
    private Image answerPanelStripes;
    [SerializeField] private Color32 correctColor;
    [SerializeField] private Color32 incorrectColor;
   

    [Header("Scoring")]
    private int score;
    private int highScore;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    
    [Header("Questions Finished")]
    [SerializeField]
    private GameObject finishedQuestionsPanel;
    public static event Action QuestionsFinished;
    
    //Opens the answerPanel for a correct answer and updates the score.
    public IEnumerator AnsweredCorrectly()
    {
        yield return new WaitForSeconds(answerCoolDown);
        answerPanel.SetActive(true);
        answerPanelStripes.color = correctColor;
        nextQuestionButton.SetActive(true);
        powerUpManager.ChangeCoinValue(powerUpManager.coinRewardValue);
        UpdateScore();
    }

    //Opens the answerPanel for an incorrect answer.
    public IEnumerator AnsweredIncorrectly()
    {
        yield return new WaitForSeconds(answerCoolDown);
        answerPanel.SetActive(true);
        answerPanelStripes.color = incorrectColor;
        restartButton.SetActive(true);
    }

    //resets the buttons and answerPanel and cycles to the next question unless there aren't any left.
    public void NextQuestion()
    {
        SetButtonsActive();
        AnswerPanelInactive();
        if (questionManager.questions.Count > 0)
        {
            questionManager.NextQuestion();
        }
        else {AllQuestionsFinished();}
    }

    //resets the score, scoreText, buttons and both panels to default. Also restarts the questionManager.
    public void Restart()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        //removed coin reset for a sense of progression.
        //powerUpManager.ChangeCoinValue(-powerUpManager.coinValue);
        SetButtonsActive();
        AnswerPanelInactive();
        finishedQuestionsPanel.SetActive(false);
        questionManager.RestartQuestions();
    }

    //sets the finished panel active.
    public void AllQuestionsFinished()
    {
        finishedQuestionsPanel.SetActive(true);
        QuestionsFinished?.Invoke();
    }
    
    //Sets every button other than the clicked button inactive to prevent double-clicking. Is called by the clicked AnswerButton.
    public void SetOtherButtonsInactive(Button clickedButton)
    {
        foreach (Button button in answerButtons)
        {
            if (button != clickedButton)
            {
                button.interactable = false;
            }
        }
    }

    //Sets all buttons active when a new question is loaded.
    private void SetButtonsActive()
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
            button.gameObject.GetComponent<Image>().color = Color.white;
        }
    }
    
    //sets all elements of the answerPanel inactive.
    private void AnswerPanelInactive()
    {
        nextQuestionButton.SetActive(false);
        restartButton.SetActive(false);
        answerPanel.SetActive(false);
    }

    //adds one to the score and the highScore if needed. Updates both of the texts.
    private void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "High Score: " + highScore;
        }
    }

    private void OnEnable()
    {
        AnswerButton.CorrectAnswerAction += StartAnsweredCorrectly;
        AnswerButton.IncorrectAnswerAction += StartAnsweredIncorrectly;
    }

    private void StartAnsweredCorrectly() { StartCoroutine(AnsweredCorrectly()); }
    private void StartAnsweredIncorrectly() { StartCoroutine(AnsweredIncorrectly()); }

    private void OnDisable()
    {
        AnswerButton.CorrectAnswerAction -= StartAnsweredCorrectly;
        AnswerButton.IncorrectAnswerAction -= StartAnsweredIncorrectly;
    }
}
