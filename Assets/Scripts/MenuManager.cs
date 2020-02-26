using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField] AudioMixer m_MasterVolumeAudioMixer;
    [SerializeField] GameObject m_SettingsMenu;
    [SerializeField] TMPro.TMP_Dropdown m_ResolutionDropdown;

    private Resolution[] m_Resolutions;
    private int defaultResolution = 0;

    public void Start()
    {
        AddResolutionsToDropdown();
        SetDefaultResolution();
    }

    private void AddResolutionsToDropdown()
    {
        //Save all of the available resolutions when the scene loads up.
        m_Resolutions = Screen.resolutions;

        //clear all the preset options in the dropdown so there is a clean slate 
        m_ResolutionDropdown.ClearOptions();

        //Create a list of type strings 
        List<string> resolutionOptions = new List<string>();

        //Loop through the resolutions array and add the resolutions to the list as strings
        for (int i = 0; i < m_Resolutions.Length; i++)
        {
            string resolutionOption = m_Resolutions[i].width + " x " + m_Resolutions[i].height;
            resolutionOptions.Add(resolutionOption);

            //if the resolution index in the array is equal to the default resolution of the monitor
            if (m_Resolutions[i].width == Screen.currentResolution.width && m_Resolutions[i].height == Screen.currentResolution.height)
            {
                //Our variable will equal that resolution
                defaultResolution = i;
            }
        }

        //add these string type resolutions to the dropdown
        m_ResolutionDropdown.AddOptions(resolutionOptions);
    }

    private void SetDefaultResolution()
    {
        //set the resolution dropdown to select default resolution
        m_ResolutionDropdown.value = defaultResolution;

        //Display the value on screen
        m_ResolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = m_Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void AdjustVolume(float volume)
    {
        m_MasterVolumeAudioMixer.SetFloat("MasterVolParam", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}


