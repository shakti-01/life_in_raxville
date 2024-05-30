using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    static string playerPath = Application.persistentDataPath + "/player.save";
    static string questTimesPath = Application.persistentDataPath + "/questTimes.save";

    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(playerPath, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveQuestTime(float questCounter, uint[] questTimes)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(questTimesPath, FileMode.Create);

        QuestTimeData timeData = new QuestTimeData(questCounter, questTimes);

        formatter.Serialize(stream, timeData);
        stream.Close();
    }
    /// <summary> returns null if no player data exits</summary>
    public static PlayerData TryLoadPlayer()
    {
        PlayerData data = null;
        if (File.Exists(playerPath)) { data = LoadPlayer(); }
        else { Logger.Log("No player data in " + playerPath); }
        return data;
    }

    public static PlayerData LoadPlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(playerPath, FileMode.Open);

        if (stream.Length == 0) { Logger.LogError("attempting to deserialise from an empty stream"); return null; }
        PlayerData data = formatter.Deserialize(stream) as PlayerData;
        stream.Close();

        return data;
    }
    ///<summary>returns null if no time data is found</summary>
    public static QuestTimeData TryLoadQuestTimes()
    {
        QuestTimeData data = null;
        if (File.Exists(questTimesPath)){ data = LoadQuestTimes();}
        else { Logger.Log("No quest times data in " + questTimesPath);}
        return data;
    }
    public static QuestTimeData LoadQuestTimes()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(questTimesPath, FileMode.Open);

        if (stream.Length == 0) { Logger.LogError("attempting to deserialise from an empty stream"); return null; }
        QuestTimeData data = formatter.Deserialize(stream) as QuestTimeData;
        stream.Close();

        return data;
    }
}
