using HelldiversHelper.Models;

namespace HelldiversHelper.Services;

/// <summary>
/// Service for importing and exporting configs.
/// </summary>
public interface IImportExportService
{
    /// <summary>
    /// Opens a file select dialog.
    /// </summary>
    /// <param name="importing">Whether the dialog is for importing config.</param>
    /// <param name="filter">The filter for the file selection.</param>
    /// <returns>The file selected by user.</returns>
    string OpenDialog(bool importing, string filter = "JSON (*.json)|*.json");

    /// <summary>
    /// Imports a saved combo file.
    /// </summary>
    /// <param name="filePath">The path to the combo file.</param>
    /// <returns>The imported combos.</returns>
    Task<IEnumerable<Combo>> ImportCombos(string filePath);

    /// <summary>
    /// Exports a combo file.
    /// </summary>
    /// <param name="combos">The combos to save.</param>
    /// <param name="filePath">The path to the combo file.</param>
    Task ExportCombos(IEnumerable<Combo> combos, string filePath);
}
