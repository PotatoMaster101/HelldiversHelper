using HelldiversHelper.Models;
using WindowsInput.Events;
using Xunit;

namespace HelldiversHelper.Test.Models;

/// <summary>
/// Unit tests for <see cref="StratagemCollection"/>.
/// </summary>
public class StratagemCollectionTest
{
    [Fact]
    public void GetStratagemNames_ReturnsNames()
    {
        // act
        var result = StratagemCollection.GetStratagemNames();

        // assert
        Assert.True(result.Any());
    }

    [Theory]
    [InlineData("Resupply", KeyCode.S, KeyCode.S, KeyCode.W, KeyCode.D)]
    [InlineData("invalid stratagem")]
    public void GetStratagemKeyCombo_ReturnsKeyCodes(string stratagem, params KeyCode[] expected)
    {
        // act
        var result = StratagemCollection.GetStratagemKeyCombo(stratagem);

        // assert
        Assert.True(expected.SequenceEqual(result));
    }
}
