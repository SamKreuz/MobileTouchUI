using System.Linq;
using UnityEngine;

public class Setup : MonoBehaviour
{
    public void Awake()
    {
        int targetFramerate = 120;
        var possibleResolutions = Screen.resolutions.ToList();
        var highestResolution = possibleResolutions.LastOrDefault();

        Screen.SetResolution(highestResolution.width, highestResolution.height, FullScreenMode.FullScreenWindow);
        Debug.Log($"Screen resolution set to {highestResolution.width} x {highestResolution.height}");

        Application.targetFrameRate = targetFramerate;
        Debug.Log($"Framerate set to {targetFramerate}");
    }
}
