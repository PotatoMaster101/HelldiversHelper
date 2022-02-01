using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WindowsInput.Events;

namespace HelldiversHelper.Wpf;

/// <summary>
/// Interaction logic for EditKeyMapWindow.xaml.
/// </summary>
public partial class EditKeyMapWindow
{
    /// <summary>
    /// Gets the user selected stratagem.
    /// </summary>
    /// <value>The user selected stratagem.</value>
    public string SelectedStratagem { get; private set; }

    /// <summary>
    /// Gets the user key.
    /// </summary>
    /// <value>The user key.</value>
    public KeyCode UserKey { get; private set; }

    /// <summary>
    /// List of supported keys.
    /// </summary>
    private readonly IReadOnlyCollection<char> _supportedKeys = new[] { 'Z', 'X', 'C', 'V', 'F' };

    /// <summary>
    /// Constructs a new instance of <see cref="EditKeyMapWindow"/>.
    /// </summary>
    public EditKeyMapWindow()
    {
        InitializeComponent();

        foreach (var stratagem in Stratagem.Collection.OrderBy(x => x.Name))
            ComboBoxStratagem.Items.Add(stratagem.Name);
        foreach (var key in _supportedKeys)
            ComboBoxKey.Items.Add(key.ToString());

        ComboBoxStratagem.SelectedIndex = 0;
        ComboBoxKey.SelectedIndex = 0;
        SelectedStratagem = ComboBoxStratagem.Text;
        UserKey = GetKeyCode();
    }

    /// <summary>
    /// Invoked when OK button is clicked.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
    {
        SelectedStratagem = ComboBoxStratagem.Text;
        UserKey = GetKeyCode();
        DialogResult = true;
    }

    /// <summary>
    /// Returns the <see cref="KeyCode"/> of user selected key.
    /// </summary>
    /// <returns>The <see cref="KeyCode"/> of user selected key.</returns>
    private KeyCode GetKeyCode()
    {
        return ComboBoxKey.Text[0] switch
        {
            'X' => KeyCode.X,
            'C' => KeyCode.C,
            'V' => KeyCode.V,
            'F' => KeyCode.F,
            _ => KeyCode.Z
        };
    }
}
