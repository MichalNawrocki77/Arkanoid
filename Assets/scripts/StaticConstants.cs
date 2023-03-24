public enum PowerUpType
{
    LifeUp,
    LifeDown,
    BallSpeedUp,
    BallSpeedDown,
    PlatfromSpeedUp,
    PlatfromSpeedDown,
}
public class StaticConstants
{
    //Multiplier as well as the divider are public, since I do intent on making them changable during game runtime :)
    public const float BallSpeedUpMultiplier = 1.5f;
    public const float BallSlowDownDivider = 1.5f;
    public const float BallStartSpeed = 10f;
    public const float BallSpedUpSpeed = BallStartSpeed * BallSpeedUpMultiplier;
    public const float BallSlowedDownSpeed = BallStartSpeed / BallSlowDownDivider;

    //Multiplier as well as the divider are public, since I do intent on making them changable during game runtime :)
    public const float PlatformSpeedUpMultiplier = 1.5f;
    public const float PlatformSlowedDownDivider = 1.5f;
    public const float PlatformStartSpeed = 7.5f;
    public const float PlatformSpedUpSpeed = PlatformStartSpeed * PlatformSpeedUpMultiplier;
    public const float PlatformSlowedDownSpeed = PlatformStartSpeed / PlatformSlowedDownDivider;

    public const int PowerUpInitialUpwardPushForce = 5;
}
