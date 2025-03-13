using UnityEngine;
public static class GameData
{
    public static bool gameStarts;
    public static float gyroRotationSpeed = 1.5f;
    public static float gameVolume = 0.5f; //10 --> double the value
    public static Quaternion mainCamDeaultRotation;
    public static int currentLevel = 1;
    public static bool timeRanOut = false;
    public static bool shouldStick = false;
    public static int cam = 1;
    public static int lifeMax = 3;
    public static int life = 3;
    public static bool rotationReady = false;
    public static bool shouldRoate = true;
    public static bool swipeLock = false;
    public static void Restart(int amountOfLife)
    {
        life = amountOfLife;
    }
}