namespace HelldiversHelper.Message;

/// <summary>
/// Represents a message to inform a view to close.
/// </summary>
public class CloseViewMessage
{
    /// <summary>
    /// Gets the data context of the view.
    /// </summary>
    public required object DataContext { get; init; }

    /// <summary>
    /// Gets the dialog result of the view after closing.
    /// </summary>
    public required bool? DialogResult { get; init; }
}
