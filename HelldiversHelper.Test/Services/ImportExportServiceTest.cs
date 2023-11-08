using HelldiversHelper.Models;
using HelldiversHelper.Services;
using WindowsInput.Events;
using Xunit;

namespace HelldiversHelper.Test.Services;

/// <summary>
/// Unit tests for <see cref="ImportExportService"/>.
/// </summary>
public class ImportExportServiceTest
{
    [Fact]
    public async Task ImportCombos_ImportsCombos()
    {
        // arrange
        var srv = new ImportExportService();

        const string filename = "test.json";
        await File.WriteAllTextAsync(filename, """[{"Stratagem":"Resupply","TriggerKey":90,"ModifierKeys":[162]}]""");

        // act
        var result = await srv.ImportCombos(filename);

        // assert
        var list = result.ToList();
        Assert.Single(list);
        Assert.Equal("Resupply", list[0].Stratagem);
    }

    [Fact]
    public async Task ExportCombos_ExportsCombos()
    {
        // arrange
        const string filename = "test.json";
        var srv = new ImportExportService();
        var combos = new List<Combo>
        {
            new()
            {
                Stratagem = "Resupply",
                TriggerKey = KeyCode.Z,
                ModifierKeys = new List<KeyCode> { KeyCode.LControl }
            }
        };

        // act
        await srv.ExportCombos(combos, filename);

        // assert
        var result = await File.ReadAllTextAsync(filename);
        Assert.Equal("""[{"Stratagem":"Resupply","TriggerKey":90,"ModifierKeys":[162]}]""", result);
    }
}
