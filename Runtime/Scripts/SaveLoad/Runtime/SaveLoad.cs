using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RealtimeRendering
{
    public static class SaveLoad
    {
        #region Json

        public static string SaveToJson(this object obj, string path, bool prettyPrint = false, bool debugSavePath = false)
        {
            string json = JsonUtility.ToJson(obj, prettyPrint);
            File.WriteAllText(path, json);
            if (debugSavePath)
            {
                DebugSavePath(path);
            }
            RefreshEditor();
            return path;
        }

        public static string SaveToJson(this object obj, string name, SavePath savePathType, bool prettyPrint = false, bool debugSavePath = false)
        {
            string path = GetFilePath(savePathType, name, FileType.json);
            SaveToJson(obj, path, prettyPrint, debugSavePath);
            return path;
        }

        public static object LoadJson(string path, Type type)
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson(json, type);
        }
        #endregion

        #region Bytes

        public static string SaveToBytes(this object obj, string path, bool debugSavePath = false)
        {
            File.WriteAllBytes(path, ObjectToByteArray(obj));
            if (debugSavePath)
            {
                DebugSavePath(path);
            }
            RefreshEditor();
            return path;
        }

        public static string SaveToBytes(this object obj, string name, SavePath savePathType, bool debugSavePath = false)
        {
            string path = GetFilePath(savePathType, name, FileType.txt);
            SaveToBytes(obj, path, debugSavePath);
            return path;
        }

        public static object LoadByteArray(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            return ByteArrayToObject(bytes);
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

        static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        static object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = (object)binForm.Deserialize(memStream);
            return obj;
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


