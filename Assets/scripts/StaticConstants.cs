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
    #region Ball
    //Multiplier as well as the divider are public, since I do intent on making them changable during game runtime :)
    public const float BallSpeedUpMultiplier = 1.5f;
    public const float BallSlowDownDivider = 1.5f;
    public const float BallStartSpeed = 10f;
    public const float BallSpedUpSpeed = BallStartSpeed * BallSpeedUpMultiplier;
    public const float BallSlowedDownSpeed = BallStartSpeed / BallSlowDownDivider;
    #endregion

    #region Platform
    //Multiplier as well as the divider are public, since I do intent on making them changable during game runtime :)
    public const float PlatformSpeedUpMultiplier = 1.5f;
    public const float PlatformSlowedDownDivider = 1.5f;
    public const float PlatformStartSpeed = 7.5f;
    public const float PlatformSpedUpSpeed = PlatformStartSpeed * PlatformSpeedUpMultiplier;
    public const float PlatformSlowedDownSpeed = PlatformStartSpeed / PlatformSlowedDownDivider;
    #endregion

    #region PowerUps
    public const int PowerUpInitialUpwardPushForce = 5;
    #endregion

    #region Tags
    public const string platfromTag = "Platform";
    public const string gameManagerTag = "GameManager";
    public const string powerUpBlockTag = "PowerUpBlock";
    public const string bottomBorderTag = "BottomBorder";
    public const string ballTag = "Ball";
    public const string BlockTag = "Block";
    #endregion
}
