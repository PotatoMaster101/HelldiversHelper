using System.ComponentModel;
using System.Linq;
using System.Windows;
using WindowsInput.Events;

namespace HelldiversHelper.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow
{
    /// <summary>
    /// Constructs a new instance of <see cref="MainWindow"/>.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Invoked when add button is clicked.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
    {
        var editKey = new EditKeyMapWindow();
        if (editKey.ShowDialog() is not true)
            return;

        if (ListBoxKeyMappings.Items.Contains(editKey.UserKey))
        {
            MessageBox.Show("Hotkey already in use.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        ListBoxKeyMappings.Items.Add(editKey.UserKey);
        GlobalKeyMapper.KeyMappings[editKey.UserKey] = Stratagem.Collection.First(x => x.Name == editKey.SelectedStratagem);
    }

    /// <summary>
    /// Invoked when remove button is clicked.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void ButtonRemove_OnClick(object sender, RoutedEventArgs e)
    {
        var key = ListBoxKeyMappings.SelectedItem;
        if (key is KeyCode k)
        {
            ListBoxKeyMappings.Items.Remove(ListBoxKeyMappings.SelectedItem);
            GlobalKeyMapper.KeyMappings.Remove(k);
        }
    }

    /// <summary>
    /// Invoked when clear button is clicked.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
    {
        ListBoxKeyMappings.Items.Clear();
        GlobalKeyMapper.KeyMappings.Clear();
    }

    /// <summary>
    /// Invoked when window is closing.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void Window_OnClosing(object? sender, CancelEventArgs e)
    {
        GlobalKeyMapper.Dispose();
    }
}
