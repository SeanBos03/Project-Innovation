
public static class GameData
{
    public static int currentLevel;
    public static bool timeRanOut = false;
    public static bool shouldStick = true;
    public static int cam = 1;
    public static int life = 3;
    public static void Restart(int amountOfLife)
    {
        life = amountOfLife;
    }
}