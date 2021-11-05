using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ARMultipleRefImageManager : MonoBehaviour
{
    #region Variables
    public Toggle aROnOffToggle;
    public Text debugText;
    ARTrackedImageManager aRTrackedImageManager;

    // Prefabs
    public GameObject logoPrefab;
    public GameObject mapPrefab;

    string logoString = "Komotini_LogoMuseum";
    string mapString = "Komotini_Map";

    GameObject currentGameObjectLogo;
    GameObject currentGameObjectMap;

    bool aRIsOn;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        // Initialize variables
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
        debugText.text = "";
        aRIsOn = false;
        currentGameObjectLogo = null;
        currentGameObjectMap = null;

        // Subscribe buttons
        SubscribeButtons();
    }

    void OnEnable()
    {
        AddOnTrackedImagesChangedEvent();
    }

    void OnDisable()
    {
        SubtractOnTrackedImagesChanged();
    }
    #endregion

    #region Methods
    void AddOnTrackedImagesChangedEvent()
    {
        aRTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void SubtractOnTrackedImagesChanged()
    {
        aRTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void SubscribeButtons()
    {
        aROnOffToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(aROnOffToggle); });
    }

    void ToggleValueChanged(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            //debugText.text = "On";
            aRIsOn = true;
        }
        else
        {
            //debugText.text = "Off";
            aRIsOn = false;
            DestroyGameObjects();
        }
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        if (aRIsOn)
        {
            // Check if image is being tracked
            ARTrackedImage currentTrackedImage = null;
            foreach (ARTrackedImage trackedImage in aRTrackedImageManager.trackables)
            {
                // Check if tracked image is being tracked, can be multiple
                if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                {
                    if (trackedImage.referenceImage.name.Equals(logoString))
                    {
                        //debugText.text = "logo";

                        currentTrackedImage = trackedImage;
                    }
                    else if (trackedImage.referenceImage.name.Equals(mapString))
                    {
                        //debugText.text = "map";

                        currentTrackedImage = trackedImage;
                    }
                }
            }

            // If image is being tracked
            if (currentTrackedImage != null)
            {
                // Update
                UpdateCurrentTrackedImage(currentTrackedImage);
            }
            else
            {
                //debugText.text = "no current tracked image";
                DestroyGameObjects();
            }
        }
    }

    void UpdateCurrentTrackedImage(ARTrackedImage _currentTrackedImage)
    {
        if (_currentTrackedImage.referenceImage.name.Equals(logoString)) // Check logo
        {
            // check if is new
            if (currentGameObjectLogo == null)
            {
                // Instantiate game object
                currentGameObjectLogo = Instantiate(logoPrefab, _currentTrackedImage.transform.position, _currentTrackedImage.transform.rotation, _currentTrackedImage.transform);
                currentGameObjectLogo.transform.position = _currentTrackedImage.transform.position;
                currentGameObjectLogo.transform.rotation = _currentTrackedImage.transform.rotation;
            }
        }
        else if (_currentTrackedImage.referenceImage.name.Equals(mapString)) // Check map
        {
            // check if is new
            if (currentGameObjectMap == null)
            {
                // Instantiate game object
                currentGameObjectMap = Instantiate(mapPrefab, _currentTrackedImage.transform.position, _currentTrackedImage.transform.rotation, _currentTrackedImage.transform);
                currentGameObjectMap.transform.position = _currentTrackedImage.transform.position;
                currentGameObjectMap.transform.rotation = _currentTrackedImage.transform.rotation;
            }
        }
    }

    void DestroyGameObjects()
    {
        // Destroy Logo
        if (currentGameObjectLogo != null)
        {
            Destroy(currentGameObjectLogo.gameObject);
            currentGameObjectLogo = null;
        }

        // Destroy Map
        if (currentGameObjectMap != null)
        {
            Destroy(currentGameObjectMap.gameObject);
            currentGameObjectMap = null;
        }
    }
    #endregion
}
