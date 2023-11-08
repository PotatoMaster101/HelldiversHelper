using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using HelldiversHelper.Message;
using HelldiversHelper.ViewModels;
using HelldiversHelper.Views;

namespace HelldiversHelper.Services;

/// <inheritdoc cref="IViewCreator"/>
public class ViewCreator : IViewCreator
{
    /// <inheritdoc cref="IViewCreator.Show"/>
    public void Show(BaseViewModel vm)
    {
        var view = GetView(vm);
        SetViewProperties(view, vm);
        view.Show();
    }

    /// <inheritdoc cref="IViewCreator.ShowDialog"/>
    public bool? ShowDialog(BaseViewModel vm)
    {
        var view = GetView(vm);
        SetViewProperties(view, vm);
        return view.ShowDialog();
    }

    /// <summary>
    /// Gets the view object.
    /// </summary>
    /// <param name="vm">The view model of the view.</param>
    /// <returns>The view object.</returns>
    private static Window GetView(BaseViewModel vm)
    {
        return vm switch
        {
            MessageViewModel => new MessageView(),
            AddComboViewModel => new AddComboView(),
            KeyPressDelayViewModel => new KeyPressDelayView(),
            _ => new MainView()
        };
    }

    /// <summary>
    /// Sets the properties for a view.
    /// </summary>
    /// <param name="view">The view to set properties.</param>
    /// <param name="vm">The view model of the view.</param>
    private static void SetViewProperties(Window view, BaseViewModel vm)
    {
        view.DataContext = vm;
        WeakReferenceMessenger.Default.Register<Window, CloseViewMessage>(view, (r, m) =>
        {
            if (r.DataContext != m.DataContext)
                return;

            WeakReferenceMessenger.Default.UnregisterAll(r);
            r.DialogResult = m.DialogResult;
            r.Close();
        });
    }
}
