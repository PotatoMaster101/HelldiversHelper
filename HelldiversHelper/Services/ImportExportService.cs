using System.IO;
using System.Text.Json;
using HelldiversHelper.Models;
using Microsoft.Win32;

namespace HelldiversHelper.Services;

/// <inheritdoc cref="IImportExportService"/>
public class ImportExportService : IImportExportService
{
    /// <inheritdoc cref="IImportExportService.OpenDialog"/>
    public string OpenDialog(bool importing, string filter = "JSON (*.json)|*.json")
    {
        FileDialog dlg = importing ? new OpenFileDialog() : new SaveFileDialog();
        dlg.Filter = filter;
        return dlg.ShowDialog() == true ? dlg.FileName : string.Empty;
    }

    /// <inheritdoc cref="IImportExportService.ImportCombos"/>
    public async Task<IEnumerable<Combo>> ImportCombos(string filePath)
    {
        var content = await File.ReadAllTextAsync(filePath).ConfigureAwait(false);
        return JsonSerializer.Deserialize<List<Combo>>(content) ?? Enumerable.Empty<Combo>();
    }

    /// <inheritdoc cref="IImportExportService.ExportCombos"/>
    public Task ExportCombos(IEnumerable<Combo> combos, string filePath)
    {
        var json = JsonSerializer.Serialize(combos);
        return File.WriteAllTextAsync(filePath, json);
    }
}
