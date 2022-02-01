using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealtimeRendering.Examples
{

    public class SavingTest : MonoBehaviour
    {
        public Data byteData;

        void Start()
        {
            var data = new Data()
            {
                name = "Bob",
                value = 1
            };

            SaveLoad.SaveToJson(data, "Bob", SaveLoad.SavePath.StreamingAssets, true);
            SaveLoad.SaveToJson(data, "Bob", SaveLoad.SavePath.AssetsFolder, true);

            SaveLoad.SaveToBytes(data, "Byte Bob", SaveLoad.SavePath.StreamingAssets, true);
            var bytes = SaveLoad.SaveToBytes(data, "Byte Bob", SaveLoad.SavePath.AssetsFolder, true);

            byteData = SaveLoad.LoadByteArray(bytes) as Data;
        }
    }

    [System.Serializable]
    public class Data
    {
        public string name;
        public int value;
    }
}
