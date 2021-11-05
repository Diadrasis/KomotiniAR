using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ARManager : MonoBehaviour
{
    #region Variables
    public ARSession aRSession;
    public Toggle aROnOffToggle;
    public Text text;
    #endregion

    #region Unity Fuctions
    private void Awake()
    {
        /*if (!aROnOffToggle.isOn)
            PauseAR();*/

        SubscribeButtons();
    }
    #endregion

    #region Methods
    void SubscribeButtons()
    {
        aROnOffToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(aROnOffToggle); });
    }

    void ToggleValueChanged(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            ResumeAR();
        }
        else
        {
            PauseAR();
        }
    }

    void PauseAR() { aRSession.enabled = false; }

    void ResumeAR() { aRSession.enabled = true; }

    void ResetAR() { aRSession.Reset(); }
    #endregion
}
