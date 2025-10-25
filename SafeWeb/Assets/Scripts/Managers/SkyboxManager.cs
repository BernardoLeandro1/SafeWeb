using System.Data.Common;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material skyBoxHomeDay;
    public Material skyBoxHomeNight;
    public Material skyBoxSchool;
    public Material skyBoxMall;
    public Material skyBoxJardim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ChangeSkyBox(string place)
    {
        if (place == "mall")
        {
            RenderSettings.skybox = skyBoxMall;
        }
        else if (place == "home")
        {
            RenderSettings.skybox = skyBoxHomeNight;
        }
        else if (place == "school")
        {
            RenderSettings.skybox = skyBoxSchool;
        }
        else if (place == "jardim")
        {
            RenderSettings.skybox = skyBoxJardim;
        }
        else if (place == "newday")
        {
            RenderSettings.skybox = skyBoxHomeDay;
        } 
        // Optionally update lighting to match the new skybox
        //DynamicGI.UpdateEnvironment();

    }
}
