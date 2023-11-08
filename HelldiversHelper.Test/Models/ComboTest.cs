using System.Collections;
using HelldiversHelper.Models;
using WindowsInput.Events;
using Xunit;

namespace HelldiversHelper.Test.Models;

/// <summary>
/// Unit tests for <see cref="Combo"/>
/// </summary>
public class ComboTest
{
    [Theory]
    [InlineData("Resupply")]
    [InlineData("")]
    [InlineData(null)]
    public void Stratagem_GetsAndSetsCorrectValue(string stratagem)
    {
        // arrange
        var combo = new Combo
        {
            Stratagem = string.Empty
        };

        // act
        combo.Stratagem = stratagem;

        // assert
        Assert.Equal(stratagem, combo.Stratagem);
    }

    [Theory]
    [InlineData(KeyCode.A)]
    [InlineData(KeyCode.LShift)]
    [InlineData(KeyCode.None)]
    public void TriggerKey_GetsAndSetsCorrectValue(KeyCode triggerKey)
    {
        // arrange
        var combo = new Combo
        {
            Stratagem = string.Empty
        };

        // act
        combo.TriggerKey = triggerKey;

        // assert
        Assert.Equal(triggerKey, combo.TriggerKey);
    }

    [Theory]
    [InlineData]
    [InlineData(KeyCode.LShift, KeyCode.LControl)]
    public void ModifierKeys_GetsAndSetsCorrectValue(params KeyCode[] modifiers)
    {
        // arrange
        var combo = new Combo
        {
            Stratagem = string.Empty
        };

        // act
        combo.ModifierKeys = modifiers.ToList();

        // assert
        Assert.True(modifiers.SequenceEqual(combo.ModifierKeys));
    }

    [Theory]
    [InlineData("")]
    [InlineData("LShift", KeyCode.LShift)]
    [InlineData("LShift + LControl", KeyCode.LShift, KeyCode.LControl)]
    public void ModifierKeysText_ReturnsCorrectValue(string expected, params KeyCode[] keys)
    {
        // arrange
        var combo = new Combo
        {
            Stratagem = string.Empty,
            ModifierKeys = keys.ToList()
        };

        // act
        var result = combo.ModifierKeysText;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(CanTriggerTestData))]
    public void CanTrigger_ReturnsCorrectValue(KeyCode trigger, List<KeyCode> mod, IReadOnlySet<KeyCode> input, bool expected)
    {
        // arrange
        var combo = new Combo
        {
            Stratagem = string.Empty,
            TriggerKey = trigger,
            ModifierKeys = mod
        };

        // act
        var result = combo.CanTrigger(input);

        // assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CanTrigger_ReturnsFalseWhenDisabled()
    {
        // arrange
        var combo = new Combo
        {
            Stratagem = string.Empty,
            TriggerKey = KeyCode.Z,
            Enabled = false
        };

        // act
        var result = combo.CanTrigger(new HashSet<KeyCode> { KeyCode.Z });

        // assert
        Assert.False(result);
    }

    [Theory]
    [ClassData(typeof(IsSameTriggerTestData))]
    public void IsSameTrigger_ReturnsCorrectValue(KeyCode trigger1, KeyCode trigger2, List<KeyCode> mod1, List<KeyCode> mod2, bool expected)
    {
        // arrange
        var combo1 = new Combo
        {
            Stratagem = string.Empty,
            TriggerKey = trigger1,
            ModifierKeys = mod1
        };
        var combo2 = new Combo
        {
            Stratagem = string.Empty,
            TriggerKey = trigger2,
            ModifierKeys = mod2
        };

        // act
        var result = combo1.IsSameTrigger(combo2);

        // assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Test data for <see cref="IsSameTrigger_ReturnsCorrectValue"/>.
    /// </summary>
    private class IsSameTriggerTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { KeyCode.A, KeyCode.A, new List<KeyCode>(), new List<KeyCode>(), true };
            yield return new object[] { KeyCode.A, KeyCode.B, new List<KeyCode>(), new List<KeyCode>(), false };
            yield return new object[] { KeyCode.A, KeyCode.A, new List<KeyCode> { KeyCode.LShift }, new List<KeyCode> { KeyCode.LShift }, true };
            yield return new object[] { KeyCode.A, KeyCode.A, new List<KeyCode> { KeyCode.LShift }, new List<KeyCode>(), false };
            yield return new object[] { KeyCode.A, KeyCode.A, new List<KeyCode> { KeyCode.LShift, KeyCode.LControl }, new List<KeyCode> { KeyCode.LShift }, false };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Test data for <see cref="CanTrigger_ReturnsCorrectValue"/>.
    /// </summary>
    private class CanTriggerTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { KeyCode.A, new List<KeyCode>(), new HashSet<KeyCode> { KeyCode.A }, true };
            yield return new object[] { KeyCode.Z, new List<KeyCode>(), new HashSet<KeyCode> { KeyCode.A }, false };
            yield return new object[] { KeyCode.A, new List<KeyCode> { KeyCode.LShift }, new HashSet<KeyCode> { KeyCode.A }, false };
            yield return new object[] { KeyCode.A, new List<KeyCode> { KeyCode.LShift }, new HashSet<KeyCode> { KeyCode.A, KeyCode.LShift }, true };
            yield return new object[] { KeyCode.A, new List<KeyCode> { KeyCode.LShift }, new HashSet<KeyCode> { KeyCode.A, KeyCode.LAlt }, false };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
