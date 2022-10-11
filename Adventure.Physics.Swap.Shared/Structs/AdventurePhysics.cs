using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Adventure.Physics.Swap.Shared.Structs;

/// <summary>
/// A structure representing the physics data layout for Sonic Adventure, Sonic Adventure DX, Sonic Adventure 2,
/// Sonic Adventure 2 Battle, Sonic Heroes. Phys.bin contains an array of this structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class AdventurePhysics
{
    private const string CategoryUnknown      = "Unknown";
    private const string CategoryMisc         = "Miscellaneous";
    private const string CategoryActions      = "Actions";
    private const string CategoryAcceleration = "Acceleration";
    private const string CategorySpeed        = "Speed";

    /// <summary>
    /// Amount of frames in which the character will fall at a decreased rate when rolling off a ledge.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("Amount of frames in which the character will fall at a decreased rate when rolling off a ledge.")]
    public int HangTime { get; set; }

    /// <summary>
    /// Higher values indicate easier acceleration on rough surfaces.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("Higher values indicate easier acceleration on rough surfaces, rarely used in Heroes.")]
    public float FloorGrip { get; set; }

    /// <summary>
    /// The horizontal speed cap, maximum speed in X/Z axis.
    /// </summary>
    [Category(CategorySpeed)]
    [Description("The horizontal speed cap, maximum speed in X/Z axis.")]
    public float HorizontalSpeedCap { get; set; }

    /// <summary>
    /// The vertical speed cap, maximum speed in Y axis.
    /// </summary>
    [Category(CategorySpeed)]
    [Description("The vertical speed cap, maximum speed in Y axis.")]
    public float VerticalSpeedCap { get; set; }

    /// <summary>
    /// Affects character acceleration.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("Affects character acceleration.")]
    public float UnknownAccelRelated { get; set; }

    /// <summary>
    /// Unknown. This value is same as in SADX.
    /// </summary>
    [Category(CategoryUnknown)]
    [Description("Unknown. This value is the same as in SADX.")]
    public float Unknown1 { get; set; }

    /// <summary>
    /// The initial vertical speed set by the game when the player presses the "Jump" button.
    /// </summary>
    [Category(CategoryActions)]
    [Description("The initial vertical speed set by the game when the player presses the \"Jump\" button.")]
    public float InitialJumpSpeed { get; set; }

    /// <summary>
    /// Seems related to springs (at least in SADX), unknown.
    /// </summary>
    [Category(CategoryUnknown)]
    [Description("Seems related to springs (at least in SADX), unknown.")]
    public float SpringControl { get; set; }

    /// <summary>
    /// Unknown value.
    /// </summary>
    [Category(CategoryUnknown)]
    [Description("Unknown value")]
    public float Unknown2 { get; set; }

    /// <summary>
    /// If the character is below this speed, the roll will be cancelled.
    /// </summary>
    [Category(CategoryActions)]
    [Description("If the character is below this speed, the roll will be cancelled.")]
    public float RollingMinimumSpeed { get; set; }

    /// <summary>
    /// Unknown value. SADX/SA2: Rolling End Speed
    /// </summary>
    [Category(CategoryUnknown)]
    [Description("Unknown value. SADX/SA2: Rolling End Speed")]
    public float RollingEndSpeed { get; set; }

    /// <summary>
    /// Speed after starting to roll as Sonic or punching as Knuckles.
    /// </summary>
    [Category(CategoryActions)]
    [Description("Speed after starting to roll as Sonic or punching as Knuckles.")]
    public float Action1Speed { get; set; }

    /// <summary>
    /// The minimum speed of knockback/push in another direction when making contact with a wall.
    /// </summary>
    [Category(CategoryMisc)]
    [Description("The minimum speed of knockback/push in another direction when making contact with a wall.")]
    public float MinWallHitKnockbackSpeed { get; set; }

    /// <summary>
    /// Speed after kicking as Sonic or punching as Knuckles.
    /// </summary>
    [Category(CategoryActions)]
    [Description("Speed after kicking as Sonic. This is also the speed below which gravity resets when falling.")]
    public float Action2Speed { get; set; }

    /// <summary>
    /// While jump is held, this is added to speed. Allows for higher jumps when holding jump.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("While jump is held, this is added to speed. Allows for higher jumps when holding jump.")]
    public float JumpHoldAddSpeed { get; set; }

    /// <summary>
    /// The character's ground horizontal acceleration. Speed is set to this value when starting from a neutral position and is applied constantly until the character reaches a set speed.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("The character's ground horizontal acceleration. Speed is set to this value when starting from a neutral position and is applied constantly until the character reaches a set speed.")]
    public float GroundStartingAcceleration { get; set; }

    /// <summary>
    /// How fast the character gains speed in the air.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("How fast the character gains speed in the air.")]
    public float AirAcceleration { get; set; }

    /// <summary>
    /// How fast the character decelerates naturally on the ground when no buttons are held.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("How fast the character decelerates naturally on the ground when no buttons are held.")]
    public float GroundDeceleration { get; set; }

    /// <summary>
    /// How fast the character can halt on the ground after holding the direction opposite to direction of travel when running.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("How fast the character can halt on the ground after holding the direction opposite to direction of travel when running.")]
    public float BrakeSpeed { get; set; }

    /// <summary>
    /// How fast the character can halt in the air when holding the direction opposite to direction of travel.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("How fast the character can halt in the air when holding the direction opposite to direction of travel.")]
    public float AirBrakeSpeed { get; set; }

    /// <summary>
    /// How fast the character decelerates naturally in the air when no buttons are held.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("How fast the character decelerates naturally in the air when no buttons are held.")]
    public float AirDeceleration { get; set; }

    /// <summary>
    /// How fast the character decelerates naturally when rolling as a ball when no buttons are held.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("How fast the character decelerates naturally when rolling as a ball when no buttons are held.")]
    public float RollingDeceleration { get; set; }

    /// <summary>
    /// This speed is added every frame in the direction that the character is travelling in the Y Axis. e.g. If you are going up, a positive value will push you up but will push you down when falling.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("This speed is added every frame in the direction that the character is travelling in the Y Axis. e.g. If you are going up, a positive value will push you up but will push you down when falling.")]
    public float GravityOffsetSpeed { get; set; }

    /// <summary>
    /// The speed applied to Sonic when turning by pushing left or right in mid air.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("This speed is added every frame in the direction that the character is travelling in the Y Axis. e.g. If you are going up, a positive value will push you up but will push you down when falling.")]
    public float MidAirSwerveAcceleration { get; set; }

    /// <summary>
    /// The minimum speed until the character will stop moving completely.
    /// </summary>
    [Category(CategoryActions)]
    [Description("The minimum speed until the character will stop moving completely.")]
    public float MinSpeedBeforeStopping { get; set; }

    /// <summary>
    /// Constant force applied to the left of the character, used to make characters appear to run sideways.
    /// </summary>
    [Category(CategoryUnknown)]
    [Description("Constant force applied to the left of the character, used to make characters appear to run sideways.")]
    public float ConstantSpeedOffset { get; set; }

    /// <summary>
    /// Unknown value, affects off road acceleration. The closer to -0 on the negative, the slower the offroad acceleration.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("Unknown value, affects off road acceleration. The closer to -0 on the negative, the slower the offroad acceleration.")]
    public float UnknownOffRoad { get; set; }

    [Category(CategoryUnknown)]
    public float Unknown3 { get; set; }

    [Category(CategoryUnknown)]
    public float Unknown4 { get; set; }

    /// <summary>
    /// Represents the radius of the collision cylinder which represents Sonic.
    /// </summary>
    [Category(CategoryMisc)]
    [Description("Represents the radius of the collision cylinder which represents Sonic.")]
    public float CollisionSize { get; set; }

    /// <summary>
    /// Gravity Constant for the character.
    /// </summary>
    [Category(CategoryAcceleration)]
    [Description("Rate of constant gravitational pull for the character.")]
    public float GravitationalPull { get; set; }

    /// <summary>
    /// (Only affects when playing as partner?) Y Offset for camera.
    /// </summary>
    [Category(CategoryMisc)]
    [Description("(Only affects when playing as partner?) Y Offset for camera.")]
    public float YOffsetCamera { get; set; }

    /// <summary>
    /// (Only affects when playing as partner?) Y Offset for characters' physical location.
    /// </summary>
    [Category(CategoryMisc)]
    [Description("(Only affects when playing as partner?) Y Offset for camera.")]
    public float YOffset { get; set; }

    /* Autogenerated by R# */
    public bool Equals(AdventurePhysics other) => HangTime == other.HangTime && FloorGrip.Equals(other.FloorGrip) && HorizontalSpeedCap.Equals(other.HorizontalSpeedCap) && VerticalSpeedCap.Equals(other.VerticalSpeedCap) && UnknownAccelRelated.Equals(other.UnknownAccelRelated) && Unknown1.Equals(other.Unknown1) && InitialJumpSpeed.Equals(other.InitialJumpSpeed) && SpringControl.Equals(other.SpringControl) && Unknown2.Equals(other.Unknown2) && RollingMinimumSpeed.Equals(other.RollingMinimumSpeed) && RollingEndSpeed.Equals(other.RollingEndSpeed) && Action1Speed.Equals(other.Action1Speed) && MinWallHitKnockbackSpeed.Equals(other.MinWallHitKnockbackSpeed) && Action2Speed.Equals(other.Action2Speed) && JumpHoldAddSpeed.Equals(other.JumpHoldAddSpeed) && GroundStartingAcceleration.Equals(other.GroundStartingAcceleration) && AirAcceleration.Equals(other.AirAcceleration) && GroundDeceleration.Equals(other.GroundDeceleration) && BrakeSpeed.Equals(other.BrakeSpeed) && AirBrakeSpeed.Equals(other.AirBrakeSpeed) && AirDeceleration.Equals(other.AirDeceleration) && RollingDeceleration.Equals(other.RollingDeceleration) && GravityOffsetSpeed.Equals(other.GravityOffsetSpeed) && MidAirSwerveAcceleration.Equals(other.MidAirSwerveAcceleration) && MinSpeedBeforeStopping.Equals(other.MinSpeedBeforeStopping) && ConstantSpeedOffset.Equals(other.ConstantSpeedOffset) && UnknownOffRoad.Equals(other.UnknownOffRoad) && Unknown3.Equals(other.Unknown3) && Unknown4.Equals(other.Unknown4) && CollisionSize.Equals(other.CollisionSize) && GravitationalPull.Equals(other.GravitationalPull) && YOffsetCamera.Equals(other.YOffsetCamera) && YOffset.Equals(other.YOffset);
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        return obj is AdventurePhysics other && Equals(other);
    }
    
    public override int GetHashCode() => base.GetHashCode();
}