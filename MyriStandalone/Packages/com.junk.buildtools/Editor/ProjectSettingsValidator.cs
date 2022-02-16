
using Unity.Assertions;
using Unity.Build;
using Unity.Build.Classic;
using UnityEditor;
using UnityEngine;

namespace Junk.BuildTools
{
    public static class ProjectSettingsValidator
    {
        [MenuItem("ProjectBuilder/Validate Player(Build) Settings")]
        public static void Validate()
        {
            // check if mono, not il2cpp
            // 
            
            var buildSettings = (BuildConfiguration)AssetDatabase.LoadAssetAtPath(PathTo.BuildConfigurationAsset(), typeof(BuildConfiguration)); 
            Assert.IsTrue(buildSettings.HasComponent<ClassicScriptingSettings>());
            
            //Debug.Log(buildSettings.GetComponent<ClassicScriptingSettings>().ScriptingBackend);
            
            var backend = PlayerSettings.GetScriptingBackend(UnityEditor.BuildTargetGroup.Standalone);
            
            if (backend != UnityEditor.ScriptingImplementation.Mono2x) 
            {
                Debug.LogError ("Warning: If the scripting backend is not Mono2x there may be problems");
            }

            //Debug.Log(backend);
            if (buildSettings.GetComponent<ClassicScriptingSettings>().ScriptingBackend != PlayerSettings.GetScriptingBackend(UnityEditor.BuildTargetGroup.Standalone))
            {
                Debug.LogError ("Error: Mismatching scripting backend");
            }
            
        }
        
        [MenuItem("ProjectBuilder/Select build configuration(asset)")]
        public static void SelectConfig()
        {
            // check if mono, not il2cpp
            // 
            
            var buildSettings = (BuildConfiguration)AssetDatabase.LoadAssetAtPath(PathTo.BuildConfigurationAsset(), typeof(BuildConfiguration));
            Selection.activeObject = buildSettings;

        }


    }
}
