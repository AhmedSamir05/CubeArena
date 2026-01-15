using UnityEngine;
using UnityEngine.UI;
public class WaveUICounter : MonoBehaviour
{
    [SerializeField] string prefix = "Wave: ";
    Text waveText;
    private void Awake()
    {
        waveText = GetComponent<Text>();
    }
    private void OnEnable()
    {
        GameManager.Instance.OnNewWave += UpdateWave;
    }

    void UpdateWave(int waveName)
    {
        waveText.text = prefix + waveName;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnNewWave -= UpdateWave;
    }
}
