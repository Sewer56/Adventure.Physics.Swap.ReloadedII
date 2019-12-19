using Adventure.Physics.Swap.ReloadedII.Enums;
using Adventure.Physics.Swap.Shared.Configs;
using Adventure.Physics.Swap.Shared.Enums;
using Adventure.Physics.Swap.Shared.Structs;
using Reloaded.Memory;
using System;
using System.Diagnostics;
using System.Linq;

namespace Adventure.Physics.Swap.ReloadedII
{
    public class PhysicsMod
    {
        public IntPtr PhysicsBaseAddress            { get; private set; }
        public AdventurePhysics[] PhysicsDump       { get; private set; }
        public Game Game                            { get; private set; }

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

        private IntPtr GetBaseAddress()
        {
            // TODO: Sadx and SA2B
            switch (Game)
            {
                case Game.Sadx:
                    return new IntPtr(0x009154E8);
                case Game.Sa2b:
                    return new IntPtr(0x017391E8);
                case Game.Heroes:
                    return new IntPtr(0x008BE550);
                default:
                    return IntPtr.Zero;
            }
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
                    physics = new AdventurePhysics[0];
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
            TGame[] values = (TGame[])Enum.GetValues(typeof(TGame));
            AdventurePhysics[] physics = values.Select(config.GetPhysics).ToArray();
            StructArray.ToPtr(PhysicsBaseAddress, physics, true);
        }
    }
}
