using System;
using System.Diagnostics;
using System.Reflection;
using Junk.BuildTools;
using Unity.Build.Classic;
using Unity.Build.Common;
using UnityEditor.AddressableAssets.Settings;
using Debug = UnityEngine.Debug;
using Directory = UnityEngine.Windows.Directory;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// ReSharper disable UnusedMember.Global

namespace Unity.Build
{
    public static class ProjectBuilder
    {
        [MenuItem("ProjectBuilder/Build(Latest git)")]
        public static void NewPipelineBuild()
        {
            Save();
            Versioning.ClearConsole();
            SemVer.BumpPatch();
            AddressableAssetSettings.BuildPlayerContent();
            LoadResourceFile();
            OpenBuildFolder();
        }

        [MenuItem("ProjectBuilder/Delete(Latest build)")]
        public static void DeleteBuild()
        {
            var path = System.IO.Directory.GetCurrentDirectory() + "\\" + "Release" + "\\";
            FileManagement.DeleteDirectory(path, true);
        }
        
        
        [MenuItem("ProjectBuilder/Open Build Folder")]
        public static void Open()
        {
            OpenBuildFolder();
        }

        private static void LoadResourceFile()
        {
            var buildConfiguration = (BuildConfiguration)AssetDatabase.LoadAssetAtPath(PathTo.BuildConfigurationAsset(), typeof(BuildConfiguration));
            if (buildConfiguration.HasComponent<GeneralSettings>())
            {
                //var liveLink = buildConfiguration.GetComponent
                // USING System.Reflection GET Unity.Scenes.Editor.Build.LiveLink Type
                foreach (var component in buildConfiguration.GetComponents())
                {
                    if (component.GetType().ToString().Equals("Unity.Scenes.Editor.Build.LiveLink"))
                    {
                        Debug.LogWarning("Live Link may not work currently.");
                        break;
                    }
                }
                
                var      settings      = buildConfiguration.GetComponent<GeneralSettings>();
                settings.Version       = new Version(SemVer.Version().Major, SemVer.Version().Minor, SemVer.Version().Patch);
                
                buildConfiguration.SetComponent(settings);
                buildConfiguration.SaveAsset();
                AssetDatabase.Refresh();
            }
            buildConfiguration.Build();


        }

        private static void Save()
        {
            AssetDatabase.SaveAssets();
            EditorSceneManager.SaveOpenScenes();
        }

        
        private static void OpenBuildFolder()
        {
            var path = System.IO.Directory.GetCurrentDirectory() + "\\" + "Release" + "\\";
            if (Directory.Exists(path))
            {
                var processStartInfo = new ProcessStartInfo
                {
                    Arguments = path,
                    FileName  = "explorer.exe"
                };

                Process.Start(processStartInfo);
            }
        }
    }
}