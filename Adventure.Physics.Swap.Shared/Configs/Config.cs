using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Adventure.Physics.Swap.Shared.Configs.Json;
using Adventure.Physics.Swap.Shared.Enums;
using Adventure.Physics.Swap.Shared.Structs;
using Ninject;
using Reloaded.Memory;

namespace Adventure.Physics.Swap.Shared.Configs
{
    public class Config : ObservableJsonSerializable<Config>, IInitializable
    {
        private const string DefaultPhysicsFile = "DefaultPhysics.bin";
        private const string ConfigFileName = "Config.json";

        public Dictionary<HeroesCharacter, AllCharacters> HeroesMapping { get; set; } = new Dictionary<HeroesCharacter, AllCharacters>
        {
            { HeroesCharacter.Amy, AllCharacters.HeroesAmy         },
            { HeroesCharacter.Big, AllCharacters.HeroesBig         },
            { HeroesCharacter.Charmy, AllCharacters.HeroesCharmy   },
            { HeroesCharacter.Cream, AllCharacters.HeroesCream     },
            { HeroesCharacter.Espio, AllCharacters.HeroesEspio     },
            { HeroesCharacter.Knuckles, AllCharacters.HeroesKnuckles },
            { HeroesCharacter.Omega, AllCharacters.HeroesOmega     },
            { HeroesCharacter.Rouge, AllCharacters.HeroesRouge     },
            { HeroesCharacter.Shadow, AllCharacters.HeroesShadow   },
            { HeroesCharacter.Sonic, AllCharacters.HeroesSonic     },
            { HeroesCharacter.Tails, AllCharacters.HeroesTails     },
            { HeroesCharacter.Vector, AllCharacters.HeroesVector   }
        };

        public Dictionary<Sa2bCharacter, AllCharacters> Sa2bMapping { get; set; } = new Dictionary<Sa2bCharacter, AllCharacters>()
        {
            { Sa2bCharacter.Amy, AllCharacters.Sa2bAmy },
            { Sa2bCharacter.ChaoWalker, AllCharacters.Sa2bChaoWalker },
            { Sa2bCharacter.Chaos, AllCharacters.Sa2bChaos },
            { Sa2bCharacter.DarkChaoWalker, AllCharacters.Sa2bDarkChaoWalker },
            { Sa2bCharacter.Eggman, AllCharacters.Sa2bEggman },
            { Sa2bCharacter.Knuckles, AllCharacters.Sa2bKnuckles },
            { Sa2bCharacter.MechEggman, AllCharacters.Sa2bMechEggman },
            { Sa2bCharacter.MechTails, AllCharacters.Sa2bMechTails },
            { Sa2bCharacter.MetalSonic, AllCharacters.Sa2bMetalSonic },
            { Sa2bCharacter.Rouge, AllCharacters.Sa2bRouge },
            { Sa2bCharacter.Shadow, AllCharacters.Sa2bShadow },
            { Sa2bCharacter.Sonic, AllCharacters.Sa2bSonic },
            { Sa2bCharacter.Tails, AllCharacters.Sa2bTails },
            { Sa2bCharacter.Tikal, AllCharacters.Sa2bTikal },
            { Sa2bCharacter.Unused, AllCharacters.Sa2bUnused },
            { Sa2bCharacter.Unused2, AllCharacters.Sa2bUnused2 },
            { Sa2bCharacter.Unused3, AllCharacters.Sa2bUnused3 },
            { Sa2bCharacter.SuperSonic, AllCharacters.Sa2bSuperSonic},
            { Sa2bCharacter.SuperShadow, AllCharacters.Sa2bSuperShadow },
        };

        public Dictionary<SadxCharacter, AllCharacters> SadxMapping { get; set; } = new Dictionary<SadxCharacter, AllCharacters>
        {
            { SadxCharacter.Amy, AllCharacters.SadxAmy },
            { SadxCharacter.Big, AllCharacters.SadxBig },
            { SadxCharacter.Eggman, AllCharacters.SadxEggman },
            { SadxCharacter.Gamma, AllCharacters.SadxGamma },
            { SadxCharacter.Knuckles, AllCharacters.SadxKnuckles },
            { SadxCharacter.Sonic, AllCharacters.SadxSonic },
            { SadxCharacter.Tails, AllCharacters.SadxTails },
            { SadxCharacter.Tikal, AllCharacters.SadxTikal }
        };

        public Dictionary<AllCharacters, AdventurePhysics> Physics { get; set; } = new Dictionary<AllCharacters, AdventurePhysics>();

        public Config() { }

        /// <summary>
        /// Obtains physics data from binary file if not read from file.
        /// </summary>
        public void Initialize()
        {
            if (Physics.Keys.Count > 0)
                return;

            string filePath = DefaultPhysicsFile;
            if (! File.Exists(filePath))
            {
                filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DefaultPhysicsFile);
                if (!File.Exists(filePath))
                    throw new Exception("Failed to find default physics binary file.");
            }

            var file = File.ReadAllBytes(filePath);
            StructArray.FromArray<AdventurePhysics>(file, out var physics, true);

            // WARNING: Order of AllCharacters must match binary file!
            foreach (AllCharacters character in (AllCharacters[])Enum.GetValues(typeof(AllCharacters)))
            {
                Physics[character] = physics[(int) character];
            }
        }

        /// <summary>
        /// Attempts to obtain a character for a given supported generic enum type.
        /// </summary>
        /// <returns>Null if failed.</returns>
        public AdventurePhysics GetPhysics<TCharacter>(TCharacter character) where TCharacter : Enum
        {
            switch (character)
            {
                case HeroesCharacter heroesCharacter:
                    return GetPhysics(heroesCharacter);
                case SadxCharacter sadxCharacter:
                    return GetPhysics(sadxCharacter);
                case Sa2bCharacter sa2bCharacter:
                    return GetPhysics(sa2bCharacter);
            }

            return null;
        }

        public AdventurePhysics GetPhysics(HeroesCharacter character) => Physics[HeroesMapping[character]];
        public AdventurePhysics GetPhysics(SadxCharacter character)   => Physics[SadxMapping[character]];
        public AdventurePhysics GetPhysics(Sa2bCharacter character)   => Physics[Sa2bMapping[character]];
        public static string GetFilePath(string fullDirectoryPath) => Path.Combine(fullDirectoryPath, ConfigFileName);
        public static Config FromJson(string fullDirectoryPath) => FromPath(GetFilePath(fullDirectoryPath));
        public void ToJson(string fullDirectoryPath) => ToPath(this, GetFilePath(fullDirectoryPath));
    }
}
