using UnityEngine;
using UnityEngine.UI;
public class Pause_ResumeUIButton : MonoBehaviour
{
    [SerializeField] Sprite pauseSprite, resumeSprite;
    Button pauseButton;
    private void Awake()
    {
        pauseButton = GetComponent<Button>();
        pauseButton.onClick.AddListener(GameManager.Instance.ChangePauseStatus);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPauseChanged += ImgSprite;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPauseChanged -= ImgSprite;
    }


    private void ImgSprite(bool isPaused)
    {
        if (isPaused)
        {
            pauseButton.image.sprite = resumeSprite;
        }
        else
        {
            pauseButton.image.sprite = pauseSprite;
        }
    }
}
