using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RealtimeRendering
{
    public static class Save
    {
        #region Json

        public static void SaveToJson(this object obj, string path, bool prettyPrint = false, bool debugSavePath = false)
        {
            string json = JsonUtility.ToJson(obj, prettyPrint);
            File.WriteAllText(path, json);
            if (debugSavePath)
            {
                DebugSavePath(path);
            }
            RefreshEditor();

        }

        public static void SaveToJson(this object obj, string name, SavePath savePathType, bool prettyPrint = false, bool debugSavePath = false)
        {
            string path = GetFilePath(savePathType, name, FileType.json);
            SaveToJson(obj, path, prettyPrint, debugSavePath);
        }

        #endregion

        #region Bytes

        public static void SaveToBytes(this object obj, string path, bool debugSavePath = false)
        {
            File.WriteAllBytes(path, ObjectToBytes(obj));
            if (debugSavePath)
            {
                DebugSavePath(path);
            }
            RefreshEditor();
        }

        public static void SaveToBytes(this object obj, string name, SavePath savePathType, bool debugSavePath = false)
        {
            string path = GetFilePath(savePathType, name, FileType.txt);
            SaveToBytes(obj, path, debugSavePath);
        }

        #endregion

        #region Convenience Functions
        public enum SavePath
        {
            AssetsFolder,
            StreamingAssets,
            PersistentDataPath
        }

        public enum FileType
        {
            json,
            txt
        }

        static string GetFilePath(SavePath savePathType, string name, FileType fileType)
        {
            string path = "";
            string format = fileType == FileType.json ? ".json" : ".txt";

            switch (savePathType)
            {
                case SavePath.AssetsFolder:
                    path = Path.Combine(Application.dataPath, name + format);
                    break;
                case SavePath.StreamingAssets:
                    MakeStreamingAssetsFolder();
                    path = Path.Combine(Application.streamingAssetsPath, name + format);
                    break;
                case SavePath.PersistentDataPath:
                    path = Path.Combine(Application.persistentDataPath, name + format);
                    break;
            }
            return path;
        }


        static void MakeStreamingAssetsFolder()
        {
            if (!File.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }
        }

        static byte[] ObjectToBytes(object obj)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, obj);
            return memoryStream.ToArray();
        }

#if UNITY_EDITOR
        static void RefreshEditor()
        {
            UnityEditor.AssetDatabase.Refresh();
        }

        static void DebugSavePath(string path)
        {
            Debug.Log(path);
        }

#endif

        #endregion
    }
}


