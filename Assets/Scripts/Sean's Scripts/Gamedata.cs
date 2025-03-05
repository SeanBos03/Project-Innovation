
public static class GameData
{
    public static bool shouldStick = true;
    public static int cam = 1;
    public static int life = 3;
    public static void Restart(int amountOfLife)
    {
        life = amountOfLife;
    }
}