using UnityEngine;
public static class GameData
{
    public static float gyroRotationSpeed;
    public static float gameVolume;
    public static Quaternion mainCamDeaultRotation;
    public static int currentLevel = 1;
    public static bool timeRanOut = false;
    public static bool shouldStick = false;
    public static int cam = 1;
    public static int lifeMax = 3;
    public static int life = 3;
    public static bool rotationReady = false;
    public static bool shouldRoate = true;
    public static void Restart(int amountOfLife)
    {
        life = amountOfLife;
    }
}