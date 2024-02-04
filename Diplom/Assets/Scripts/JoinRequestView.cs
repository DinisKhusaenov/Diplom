using System;
using UnityEngine;
using UnityEngine.UI;

public class JoinRequestView : MonoBehaviour
{
    public event Action YesClicked;

    [SerializeField] private Button _yes;
    [SerializeField] private Button _no;

    private void Awake()
    {
        Hide();
    }

    private void OnEnable()
    {
        _yes.onClick.AddListener(OnYesClicked);
        _no.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _yes.onClick.RemoveListener(OnYesClicked);
        _no.onClick.RemoveListener(Hide);
    }

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    private void OnYesClicked()
    {
        YesClicked?.Invoke();
        Hide();
    }
}
