using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class saveManager
{
    //Add  a reference that chooses which save state it will use
    public static void saveData(saveController _savedata, int saveSlot)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/6F6_save_" + saveSlot.ToString() + "_data.hcm";
        FileStream stream = new FileStream(path, FileMode.Create);

        saveData data = new saveData(_savedata);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static saveData loadData (int saveSlot)
    {
        string path = Application.persistentDataPath + "/6F6_save_" + saveSlot.ToString() + "_data.hcm";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            saveData data = formatter.Deserialize(stream) as saveData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("File Not found");
            return null;
        }
    }
}
