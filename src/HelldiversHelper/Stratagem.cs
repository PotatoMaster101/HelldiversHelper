using WindowsInput;
using WindowsInput.Events;

namespace HelldiversHelper;

/// <summary>
/// Represents a stratagem in Helldivers.
/// </summary>
public class Stratagem
{
    /// <summary>
    /// Gets the stratagem name.
    /// </summary>
    /// <value>The stratagem name.</value>
    public string Name { get; }

    /// <summary>
    /// Gets the stratagem key combination.
    /// </summary>
    /// <value>The stratagem key combination.</value>
    public KeyCode[] Combo { get; }

    /// <summary>
    /// Gets the collection of stratagems available in Helldivers.
    /// </summary>
    /// <value>The collection of stratagems available in Helldivers.</value>
    public static IReadOnlyCollection<Stratagem> Collection { get; } = new[]
    {
        // support
        new Stratagem("Resupply", new[] { KeyCode.S, KeyCode.S, KeyCode.W, KeyCode.D }),
        new Stratagem("REP-80", new[] { KeyCode.S, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.S }),

        // backpacks
        new Stratagem("AD-289 Angel", new[] { KeyCode.S, KeyCode.W, KeyCode.A, KeyCode.A, KeyCode.D, KeyCode.S }),
        new Stratagem("AD-334 Guard Dog", new[] { KeyCode.S, KeyCode.W, KeyCode.A, KeyCode.W, KeyCode.D, KeyCode.S }),
        new Stratagem("LIFT-850 Jump Pack", new[] { KeyCode.S, KeyCode.W, KeyCode.W, KeyCode.S, KeyCode.W }),
        new Stratagem("Resupply Pack", new[] { KeyCode.S, KeyCode.W, KeyCode.S, KeyCode.S, KeyCode.D }),
        new Stratagem("SH-20 Shield Generator Pack", new[] { KeyCode.S, KeyCode.W, KeyCode.A, KeyCode.D, KeyCode.A, KeyCode.D }),
        new Stratagem("SH-32 Directional Kinetic Shield", new[] { KeyCode.S, KeyCode.W, KeyCode.A, KeyCode.D, KeyCode.A, KeyCode.S }),

        // secondary weapons
        new Stratagem("AC-22 Dum-Dum", new[] { KeyCode.S, KeyCode.A, KeyCode.S, KeyCode.W, KeyCode.W, KeyCode.D }),
        new Stratagem("EAT-17", new[] { KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S }),
        new Stratagem("FLAM-40 Incinerator", new[] { KeyCode.S, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.A }),
        new Stratagem("LAS-98 Laser Cannon", new[] { KeyCode.S, KeyCode.A, KeyCode.S, KeyCode.W, KeyCode.A }),
        new Stratagem("M-25 Rumbler", new[] { KeyCode.S, KeyCode.A, KeyCode.W, KeyCode.A, KeyCode.A }),
        new Stratagem("MG-94 Machine Gun", new[] { KeyCode.S, KeyCode.A, KeyCode.S, KeyCode.W, KeyCode.D }),
        new Stratagem("MGX-42 Machine Gun", new[] { KeyCode.S, KeyCode.A, KeyCode.S, KeyCode.W, KeyCode.W, KeyCode.A }),
        new Stratagem("MLS-4X Commando", new[] { KeyCode.S, KeyCode.A, KeyCode.W, KeyCode.S, KeyCode.D }),
        new Stratagem("Obliterator Grenade Launcher", new[] { KeyCode.S, KeyCode.A, KeyCode.W, KeyCode.A, KeyCode.S }),
        new Stratagem("REC-6 Demolisher", new[] { KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.W }),
        new Stratagem("RL-112 Recoilless Rifle", new[] { KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.D, KeyCode.A }),
        new Stratagem("TOX-13 Avenger", new[] { KeyCode.S, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.D }),

        // vehicles
        new Stratagem("EXO-44 Walker Exosuit", new[] { KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.S }),
        new Stratagem("EXO-48 Obsidian Exosuit", new[] { KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.A }),
        new Stratagem("EXO-51 Lumberer Exosuit", new[] { KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D }),
        new Stratagem("M5 APC", new[] { KeyCode.S, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.A, KeyCode.D }),
        new Stratagem("M5-32 HAV", new[] { KeyCode.S, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.W, KeyCode.S }),
        new Stratagem("TD-110 Bastion", new[] { KeyCode.S, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.W, KeyCode.D }),
        new Stratagem("MC-109 Hammer Motorcycle", new[] { KeyCode.S, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.A, KeyCode.W }),

        // defensive
        new Stratagem("A/AC-6 Tesla Tower", new[] { KeyCode.A, KeyCode.S, KeyCode.S, KeyCode.W, KeyCode.D, KeyCode.A }),
        new Stratagem("A/GL-8 Launcher Turret", new[] { KeyCode.A, KeyCode.S, KeyCode.W, KeyCode.A, KeyCode.A, KeyCode.S }),
        new Stratagem("A/MG-11 Minigun Turret", new[] { KeyCode.A, KeyCode.S, KeyCode.W, KeyCode.D, KeyCode.A }),
        new Stratagem("A/RX-34 Railcannon Turret", new[] { KeyCode.A, KeyCode.S, KeyCode.W, KeyCode.A, KeyCode.D }),
        new Stratagem("Airdropped Anti-Personnel Mines", new[] { KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.W }),
        new Stratagem("Airdropped Stun Mines", new[] { KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S }),
        new Stratagem("Anti-Personnel Barrier", new[] { KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.S, KeyCode.D }),
        new Stratagem("AT-47 Anti-Tank Emplacement", new[] { KeyCode.A, KeyCode.A, KeyCode.W, KeyCode.W, KeyCode.D, KeyCode.A }),
        new Stratagem("Distractor Beacon", new[] { KeyCode.A, KeyCode.D, KeyCode.D }),
        new Stratagem("Humblebee UAV Drone", new[] { KeyCode.A, KeyCode.W, KeyCode.D }),
        new Stratagem("Thunderer Smoke Round", new[] { KeyCode.D, KeyCode.S, KeyCode.W, KeyCode.W, KeyCode.S }),

        // offensive
        new Stratagem("Airstrike", new[] { KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.D, KeyCode.A }),
        new Stratagem("Close Air Support", new[] { KeyCode.D, KeyCode.D, KeyCode.S, KeyCode.A }),
        new Stratagem("Heavy Strafing Run", new[] { KeyCode.D, KeyCode.D, KeyCode.S, KeyCode.A }),
        new Stratagem("Incendiary Bombs", new[] { KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.D }),
        new Stratagem("Missile Barrage", new[] { KeyCode.D, KeyCode.S, KeyCode.S, KeyCode.S, KeyCode.A, KeyCode.S }),
        new Stratagem("Orbital Laser Strike", new[] { KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.W, KeyCode.D, KeyCode.A }),
        new Stratagem("Railcannon Strike", new[] { KeyCode.D, KeyCode.S, KeyCode.W, KeyCode.S, KeyCode.A }),
        new Stratagem("Shredder Missile Strike", new[] { KeyCode.D, KeyCode.A, KeyCode.D, KeyCode.A, KeyCode.S, KeyCode.S, KeyCode.D }),
        new Stratagem("Sledge Precision Artillery", new[] { KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.W, KeyCode.S, KeyCode.D }),
        new Stratagem("Static Field Conductors", new[] { KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.D }),
        new Stratagem("Strafing Run", new[] { KeyCode.D, KeyCode.D, KeyCode.W }),
        new Stratagem("Thunderer Barrage", new[] { KeyCode.D, KeyCode.S, KeyCode.W, KeyCode.W, KeyCode.A, KeyCode.S }),
        new Stratagem("Vindicator Dive Bomb", new[] { KeyCode.D, KeyCode.D, KeyCode.D }),

        // special
        new Stratagem("Emergency Beacon", new[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.W }),
        new Stratagem("ME-1 Sniffer Metal Detector", new[] { KeyCode.S, KeyCode.S, KeyCode.A, KeyCode.W }),
        new Stratagem("NUX-223 Hellbomb", new[] { KeyCode.W, KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.W, KeyCode.S }),
        new Stratagem("Reinforce", new[] { KeyCode.W, KeyCode.S, KeyCode.D, KeyCode.A, KeyCode.W }),
    };

    /// <summary>
    /// Constructs a new instance of <see cref="Stratagem"/>.
    /// </summary>
    /// <param name="name">The name of the stratagem.</param>
    /// <param name="combo">The key combination of the stratagem.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is whitespace or <paramref name="combo"/> is empty.</exception>
    private Stratagem(string name, KeyCode[] combo)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(name));
        if (combo.Length is 0)
            throw new ArgumentException($"{nameof(combo.Length)} is 0", nameof(combo));

        Name = name;
        Combo = combo;
    }

    /// <summary>
    /// Presses the keys in <see cref="Combo"/> in sequence.
    /// </summary>
    /// <param name="delay">The delay between key presses in milliseconds.</param>
    /// <returns>The result of the key presses.</returns>
    public Task<bool> Activate(int delay = 20)
    {
        var e = Simulate.Events();
        foreach (var key in Combo)
            e.Click(key).Wait(delay);
        return e.Invoke();
    }
}
