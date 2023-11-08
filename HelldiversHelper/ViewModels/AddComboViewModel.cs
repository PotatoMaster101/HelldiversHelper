using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using HelldiversHelper.Models;
using HelldiversHelper.Resources;
using HelldiversHelper.Services;
using WindowsInput.Events;

namespace HelldiversHelper.ViewModels;

/// <summary>
/// View model for add combo view.
/// </summary>
public class AddComboViewModel : DialogViewModel
{
    private readonly IKeyboardService _keyboardService;
    private readonly IViewCreator _viewCreator;
    private string _selectedStratagem = "Resupply";
    private string _addModifierButtonText = ResourceStrings.Add;
    private KeyCode _selectedModifierKey = KeyCode.None;
    private bool _isModifyingTriggerKey;
    private bool _isAddingModifierKey;

    /// <summary>
    /// Gets the list of stratagems and the resource string.
    /// </summary>
    public ObservableCollection<string> Stratagems { get; } = new(StratagemCollection.GetStratagemNames().OrderBy(x => x));

    /// <summary>
    /// Gets the list of modifier keys.
    /// </summary>
    public ObservableCollection<KeyCode> ModifierKeys { get; } = new();

    /// <summary>
    /// Gets the trigger key button text.
    /// </summary>
    public string TriggerKeyButtonText => _isModifyingTriggerKey ? ResourceStrings.Listening : TriggerKey.ToString();

    /// <summary>
    /// Gets or sets the trigger key.
    /// </summary>
    public KeyCode TriggerKey { get; set; } = KeyCode.Z;

    /// <summary>
    /// Gets whether the user can read a key.
    /// </summary>
    public bool CanReadKey => !_isAddingModifierKey && !_isModifyingTriggerKey;

    /// <summary>
    /// Gets whether the user can delete a modifier key.
    /// </summary>
    public bool CanRemoveModifier => SelectedModifierKey is not KeyCode.None;

    /// <summary>
    /// Gets or sets the selected stratagem.
    /// </summary>
    public string SelectedStratagem
    {
        get => _selectedStratagem;
        set
        {
            _selectedStratagem = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the selected modifier key.
    /// </summary>
    public KeyCode SelectedModifierKey
    {
        get => _selectedModifierKey;
        set
        {
            _selectedModifierKey = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanRemoveModifier));
        }
    }

    /// <summary>
    /// Gets or sets the add modifier button text.
    /// </summary>
    public string AddModifierButtonText
    {
        get => _addModifierButtonText;
        set
        {
            _addModifierButtonText = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets the command for modifying the trigger key.
    /// </summary>
    public IRelayCommand ModifyTriggerKeyCommand { get; }

    /// <summary>
    /// Gets the command for adding a modifier key.
    /// </summary>
    public IRelayCommand AddModifierKeyCommand { get; }

    /// <summary>
    /// Gets the command for removing a modifier key.
    /// </summary>
    public IRelayCommand RemoveModifierKeyCommand { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="AddComboViewModel"/>.
    /// </summary>
    public AddComboViewModel(IKeyboardService keyboardService, IViewCreator viewCreator)
    {
        _keyboardService = keyboardService;
        _viewCreator = viewCreator;
        Title = ResourceStrings.AddCombo;
        ModifyTriggerKeyCommand = new RelayCommand(OnModifyTriggerKey);
        AddModifierKeyCommand = new RelayCommand(OnAddModifierKey);
        RemoveModifierKeyCommand = new RelayCommand(OnRemoveModifierKey);
    }

    /// <summary>
    /// Returns the combo object.
    /// </summary>
    /// <returns>The combo object.</returns>
    public Combo GetCombo()
    {
        return new Combo
        {
            Stratagem = SelectedStratagem,
            TriggerKey = TriggerKey,
            ModifierKeys = ModifierKeys.ToList()
        };
    }

    /// <inheritdoc cref="BaseViewModel.OnClosing"/>
    public override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        if (_isAddingModifierKey || _isModifyingTriggerKey)
        {
            _viewCreator.ShowDialog(new MessageViewModel
            {
                Title = ResourceStrings.Warning,
                Message = ResourceStrings.FinishInputBeforeClosing,
                ShowCancelButton = false
            });
            e.Cancel = true;
        }
    }

    /// <summary>
    /// Invoked when user modifies trigger key.
    /// </summary>
    private void OnModifyTriggerKey()
    {
        _isModifyingTriggerKey = true;
        OnPropertyChanged(nameof(TriggerKeyButtonText));
        OnPropertyChanged(nameof(CanReadKey));
        _keyboardService.KeyDown += OnTriggerKeyDown;
    }

    /// <summary>
    /// Invoked when user inputs a trigger key.
    /// </summary>
    private void OnTriggerKeyDown(object? sender, KeyCode e)
    {
        _keyboardService.KeyDown -= OnTriggerKeyDown;
        TriggerKey = e;
        _isModifyingTriggerKey = false;
        OnPropertyChanged(nameof(TriggerKeyButtonText));
        OnPropertyChanged(nameof(CanReadKey));
    }

    /// <summary>
    /// Invoked when user adds a modifier key.
    /// </summary>
    private void OnAddModifierKey()
    {
        _isAddingModifierKey = true;
        AddModifierButtonText = ResourceStrings.Listening;
        OnPropertyChanged(nameof(CanReadKey));
        _keyboardService.KeyDown += OnModifierKeyDown;
    }

    /// <summary>
    /// Invoked when user inputs a modifier key.
    /// </summary>
    private void OnModifierKeyDown(object? sender, KeyCode e)
    {
        _keyboardService.KeyDown -= OnModifierKeyDown;
        if (_keyboardService.IsModifier(e) && !ModifierKeys.Contains(e))
            Application.Current.Dispatcher.Invoke(() => ModifierKeys.Add(e));

        _isAddingModifierKey = false;
        AddModifierButtonText = ResourceStrings.Add;
        OnPropertyChanged(nameof(CanReadKey));
    }

    /// <summary>
    /// Invoked when user removes a modifier key.
    /// </summary>
    private void OnRemoveModifierKey()
    {
        ModifierKeys.Remove(SelectedModifierKey);
        SelectedModifierKey = ModifierKeys.FirstOrDefault();
        OnPropertyChanged(nameof(CanRemoveModifier));
    }
}
