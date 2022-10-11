using Adventure.Physics.Swap.ReloadedII.Enums;
using Adventure.Physics.Swap.Shared.Configs;
using Adventure.Physics.Swap.Shared.Enums;
using Adventure.Physics.Swap.Shared.Structs;
using Reloaded.Memory;
using System.Diagnostics;

namespace Adventure.Physics.Swap.ReloadedII;

public class PhysicsMod
{
    public Game Game { get; private set; }
    private nuint PhysicsBaseAddress { get; set; }
    private AdventurePhysics[] PhysicsDump { get; set; }

    /* Setup / Teardown */

    public PhysicsMod()
    {
        Game = GetGame();
        PhysicsBaseAddress = GetBaseAddress();
        DumpPhysics(Game, out var physics);
        PhysicsDump = physics;
    }

    private Game GetGame()
    {
        var process = Process.GetCurrentProcess();
        var game = Game.Null;

        string processName = process.ProcessName;
        if (processName.Equals("tsonic_win", StringComparison.OrdinalIgnoreCase))
            game = Game.Heroes;

        if (processName.Equals("sonic2app", StringComparison.OrdinalIgnoreCase))
            game = Game.Sa2b;

        if (processName.Equals("sonic", StringComparison.OrdinalIgnoreCase))
            game = Game.Sadx;

        return game;
    }

    private nuint GetBaseAddress()
    {
        return Game switch
        {
            Game.Sadx => 0x009154E8,
            Game.Sa2b => 0x017391E8,
            Game.Heroes => 0x008BE550,
            _ => 0
        };
    }

    private void DumpPhysics(Game game, out AdventurePhysics[] physics)
    {
        switch (game)
        {
            case Game.Sadx:
                DumpPhysics<SadxCharacter>(out physics);
                break;
            case Game.Sa2b:
                DumpPhysics<Sa2bCharacter>(out physics);
                break;
            case Game.Heroes:
                DumpPhysics<HeroesCharacter>(out physics);
                break;
            default:
                physics = Array.Empty<AdventurePhysics>();
                break;
        }
    }

    private void DumpPhysics<TGame>(out AdventurePhysics[] physics) where TGame : Enum
    {
        StructArray.FromPtr(PhysicsBaseAddress, out physics, Enum.GetValues(typeof(TGame)).Length, true);
    }

    /* Logic */

    /// <summary>
    /// Restores the original backup of game physics.
    /// </summary>
    public void RestorePhysics() => StructArray.ToPtr(PhysicsBaseAddress, PhysicsDump, true);

    /// <summary>
    /// Obtains the physics entries from a configuration file and applies them to game.
    /// </summary>
    public void ApplyFromConfig(Config config)
    {
        switch (Game)
        {
            case Game.Sadx:
                ApplyFromConfig<SadxCharacter>(config);
                break;
            case Game.Sa2b:
                ApplyFromConfig<Sa2bCharacter>(config);
                break;
            case Game.Heroes:
                ApplyFromConfig<HeroesCharacter>(config);
                break;
            default:
                return;
        }
    }

    private void ApplyFromConfig<TGame>(Config config) where TGame : Enum
    {
        var values = (TGame[])Enum.GetValues(typeof(TGame));
        var physics = values.Select(config.GetPhysics).ToArray();
        StructArray.ToPtr(PhysicsBaseAddress, physics, true);
    }
}