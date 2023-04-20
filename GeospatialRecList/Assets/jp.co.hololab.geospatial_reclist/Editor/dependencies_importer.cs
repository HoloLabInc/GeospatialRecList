#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace HoloLab.DependencyPackage
{
    public class dependencies_importer
    {
        // Unity Package Manager用URLを追加してください。
        private static List<string> PackageURLs = new List<string>
        {
            "https://github.com/google-ar/arcore-unity-extensions.git",
            "https://git@github.com/akihiro0105/UnityPackageManagerEditor.git?path=/Assets/com.akihiro.upmeditor/"
        };

        // HoloLabToolkitのURL
        private static string hololabToolkitURL =
            "https://github.com/HoloLabInc/HoloLabToolkit.git?path=Assets/HoloLab.Toolkit.Core";

        #region Importer

        private static AddRequest addRequest;
        private static ListRequest listRequest;
        private static List<string> bufferList = new List<string>();

        [InitializeOnLoadMethod]
        public static void LoadPackage()
        {
            EditorApplication.LockReloadAssemblies();
            listRequest = Client.List(true);
            EditorApplication.update += ListProgress;
        }

        private static void importList()
        {
            if (bufferList.Count == 0)
            {
                EditorApplication.UnlockReloadAssemblies();
                return;
            }
            
            var url = bufferList.FirstOrDefault();
            if (!string.IsNullOrEmpty(url))
            {
                bufferList.RemoveAt(0);
                addRequest = Client.Add(url);
                EditorUtility.DisplayProgressBar("jp.co.hololab.dependency-package",
                    $"Importing package", 100);
                EditorApplication.update += AddProgress;
            }
            else
            {
#if UNITY_2020_3_OR_NEWER
                Client.Resolve();
#endif
                EditorApplication.UnlockReloadAssemblies();
            }
        }

        private static void AddProgress()
        {
            if (addRequest.IsCompleted)
            {
                if (addRequest.Status == StatusCode.Success)
                {
                    Debug.Log("Installed: " + addRequest.Result.name);
                }
                else if (addRequest.Status >= StatusCode.Failure)
                {
                    Debug.Log(addRequest.Error.message);
                }

                EditorApplication.update -= AddProgress;
                EditorUtility.ClearProgressBar();
                importList();
            }
        }

        private static void ListProgress()
        {
            if (listRequest.IsCompleted)
            {
                if (listRequest.Status == StatusCode.Failure)
                {
                    Debug.LogError($"Failure {listRequest.Error.message}");
                }
                else
                {
                    bufferList.Clear();
                    // packageIDに一つもURLが含まれていない場合追加する
                    foreach (var packageURL in PackageURLs)
                    {
                        if (listRequest.Result.Select(item => item.packageId)
                            .Count(item => item.Contains(packageURL)) == 0)
                        {
                            bufferList.Add(packageURL);
                        }
                    }
                    
                    importList();
                    EditorApplication.update -= ListProgress;
                }
            }
        }

        #endregion

        [MenuItem("HoloLab/Import HoloLabToolkit")]
        private static void AddHoloLabToolkit()
        {
            bufferList = new List<string> { hololabToolkitURL };
            importList();
        }
    }
}
#endif