using UnityEditor;

public struct Stats
{
    public int level;
    public int enemy_killed;
    public int boss_killed;
    public int pickup_taken;

    public int CalculateScore()
    {
        return level * 15 + enemy_killed * 3 + boss_killed * 20 + pickup_taken * 5;
    }
}
