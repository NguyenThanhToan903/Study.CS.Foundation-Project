using UnityEngine;

public class SettingController : MonoBehaviour
{
    [SerializeField]
    private GameObject settingPanel;

    public void SettingsControl()
    {
        Debug.Log("Settings button pressed");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClip);
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
}
