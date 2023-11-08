using WindowsInput.Events;

namespace HelldiversHelper.Models;

/// <summary>
/// Represents a collection of Helldivers stratagems and their key combination.
/// </summary>
public static class StratagemCollection
{
    private const KeyCode A = KeyCode.A;
    private const KeyCode D = KeyCode.D;
    private const KeyCode S = KeyCode.S;
    private const KeyCode W = KeyCode.W;

    /// <summary>
    /// Internal data.
    /// </summary>
    private static readonly IReadOnlyDictionary<string, KeyCode[]> Data = new Dictionary<string, KeyCode[]>
    {
        // supply
        ["AC-22 Dum-Dum"] = new[] { S, A, S, W, W, D },
        ["AD-289 Angel"] = new[] { S, W, A, A, D, S },
        ["AD-334 Guard Dog"] = new[] { S, W, A, W, D, S },
        ["EAT-17"] = new[] { S, A, D, W, S },
        ["EXO-44 Walker Exosuit"] = new[] { S, D, W, A, S, S },
        ["EXO-48 Obsidian Exosuit"] = new[] { S, D, W, A, S, A },
        ["EXO-51 Lumberer Exosuit"] = new[] { S, D, W, A, S, D },
        ["FLAM-40 Incinerator"] = new[] { S, A, S, D, A },
        ["LAS-98 Laser Cannon"] = new[] { S, A, S, W, A },
        ["LIFT-850 Jump Pack"] = new[] { S, W, W, S, W },
        ["M-25 Rumbler"] = new[] { S, A, W, A, S },
        ["M5 APC"] = new[] { S, D, S, A, A, D },
        ["M5-32 HAV"] = new[] { S, D, S, A, W, S, },
        ["MC-109 Hammer Motorcycle"] = new[] { S, D, S, A, A, W },
        ["MG-94 Machine Gun"] = new[] { S, A, S, W, D },
        ["MGX-42 Machine Gun"] = new[] { S, A, S, W, W, A },
        ["MLS-4X Commando"] = new[] { S, A, W, S, D },
        ["Obliterator Grenade Launcher"] = new[] { S, A, W, A, S },
        ["REC-6 Demolisher"] = new[] { S, A, D, W, W },
        ["REP-80"] = new[] { S, S, A, D, S },
        ["Resupply Pack"] = new[] { S, W, S, S, D },
        ["Resupply"] = new[] { S, S, W, D },
        ["RL-112 Recoilless Rifle"] = new[] { S, A, D, D, A },
        ["SH-20 Shield Generator Pack"] = new[] { S, W, A, D, A, D },
        ["SH-32 Directional Kinetic Shield"] = new[] { S, W, A, D, A, S },
        ["TD-110 Bastion"] = new[] { S, D, S, A, W, D },
        ["TOX-13 Avenger"] = new[] { S, A, S, D, D },

        // defensive
        ["A/AC-6 Tesla Tower"] = new[] { A, S, S, W, D, A },
        ["A/GL-8 Launcher Turret"] = new[] { A, S, W, D, D, S },
        ["A/MG-11 Minigun Turret"] = new[] { A, S, W, D, A },
        ["A/RX-34 Railcannon Turret"] = new[] { A, S, W, A, D },
        ["Airdropped Anti-Personnel Mines"] = new[] { A, D, S, W },
        ["Airdropped Stun Mines"] = new[] { A, D, W, S },
        ["Anti-Personnel Barrier"] = new[] { A, D, S, S, D },
        ["AT-47 Anti-Tank Emplacement"] = new[] { A, A, W, W, D, A },
        ["Distractor Beacon"] = new[] { A, S, D },
        ["Humblebee UAV drone"] = new[] { A, W, D },
        ["Thunderer Smoke Round"] = new[] { D, S, W, W, S },

        // offensive
        ["Close Air Support"] = new[] { D, D, S, A },
        ["Heavy Airstrike"] = new[] { D, W, S, D, A },
        ["Heavy Strafing Run"] = new[] { D, D, S, W },
        ["Incendiary Bombs"] = new[] { D, W, A, D },
        ["Missile Barrage"] = new[] { D, S, S, S, A, S },
        ["Orbital Laser Strike"] = new[] { D, W, A, W, D, A },
        ["Railcannon Strike"] = new[] { D, S, W, S, A },
        ["Shredder Missile Strike"] = new[] { D, A, D, A, S, S, D },
        ["Sledge Precision Artillery"] = new[] { D, W, A, W, S, D },
        ["Static Field Conductors"] = new[] { D, W, A, S },
        ["Strafing Run"] = new[] { D, D, W },
        ["Thunderer Barrage"] = new[] { D, S, W, W, A, S },
        ["Vindicator Dive Bomb"] = new[] { D, D, D },

        // special
        ["Emergency Beacon"] = new[] { W, S, D, W },
        ["ME-1 Sniffer Metal Detector"] = new[] { S, S, D, W },
        ["NUX-223 Hellbomb"] = new[] { W, A, D, S, W, S },
        ["Reinforce"] = new[] { W, S, D, A, W }
    };

    /// <summary>
    /// Gets the stratagem names.
    /// </summary>
    /// <returns>The stratagem names.</returns>
    public static IEnumerable<string> GetStratagemNames()
    {
        return Data.Keys;
    }

    /// <summary>
    /// Gets the stratagem key combinations.
    /// </summary>
    /// <param name="stratagem">The name of the stratagem.</param>
    /// <returns>The stratagem key combination.</returns>
    public static KeyCode[] GetStratagemKeyCombo(string stratagem)
    {
        return Data.TryGetValue(stratagem, out var keyCodes) ? keyCodes : Array.Empty<KeyCode>();
    }
}
