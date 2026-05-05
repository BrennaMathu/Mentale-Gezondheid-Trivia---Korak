using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip correctAnswerClip;
    public AudioClip incorrectAnswerClip;
    public AudioClip winClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    

    void PlayCorrectAnswer()
    {
        audioSource.PlayOneShot(correctAnswerClip);
    }

    void PlayIncorrectAnswer()
    {
        audioSource.PlayOneShot(incorrectAnswerClip);
    }

    void PlayWin()
    {
        audioSource.PlayOneShot(winClip);
    }

    private void OnEnable()
    {
        AnswerButton.CorrectAnswerAction += PlayCorrectAnswer;
        AnswerButton.IncorrectAnswerAction += PlayIncorrectAnswer;
        GameManager.QuestionsFinished += PlayWin;
    }
    private void OnDisable()
    {
        AnswerButton.CorrectAnswerAction -= PlayCorrectAnswer;
        AnswerButton.IncorrectAnswerAction -= PlayIncorrectAnswer;
        GameManager.QuestionsFinished -= PlayWin;
    }
}
