using UnityEngine;
using UnityEngine.UI;
public class WaveUISkipper : MonoBehaviour
{
    [SerializeField] int waveSkip = 1;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);


    }

    private void OnEnable()
    {
        //for previous button
        if (waveSkip < 0)
        {
            GameManager.Instance.OnNewWave += ButtonState;
        }
    }

    private void OnClick()
    {
        GameManager.Instance.NewWave(GameManager.Instance.currentWave + waveSkip);
    }

    private void ButtonState(int currentWave)
    {
        if(currentWave <=1)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    private void OnDisable()
    {
        //for previous button
        if (waveSkip < 0)
        {
            GameManager.Instance.OnNewWave -= ButtonState;
        }
    }

}
