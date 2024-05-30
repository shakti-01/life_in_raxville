[System.Serializable]
public class PlayerData
{
    public int questCount;
    public bool questIsActive;
    public bool questTargetFound;
    public string questCountString;
    public float[] position;

    public PlayerData(Player player)
    {
        questCount = player.questGiver.questCount;
        questIsActive = player.quest.isActive;
        questTargetFound = player.quest.goal.targetFound;
        questCountString = player.questCountString.text;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
