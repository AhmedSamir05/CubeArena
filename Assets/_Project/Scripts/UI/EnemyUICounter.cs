using UnityEngine;
using UnityEngine.UI;
public class EnemyUICounter : MonoBehaviour
{
    Text counterText;
    [SerializeField] string postfix = " Enemies";
    private void Awake()
    {
        counterText = GetComponent<Text>();
    }
    private void OnEnable()
    {
        EnemyEvents.Instance.OnEnemyCountChanged += OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        counterText.text = value + postfix;
    }

    private void OnDisable()
    {
        EnemyEvents.Instance.OnEnemyCountChanged -= OnValueChanged;
    }
}
