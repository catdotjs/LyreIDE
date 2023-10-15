using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Lyre.Controls;

/// <summary>
/// Its a stylised button that is used for opening websites and such
/// Sadly click colours are hard coded(we can hopefully fix this later)
/// </summary>
public partial class Hyperlink : TextBlock{
    public static readonly StyledProperty<string> Href = AvaloniaProperty.Register<Hyperlink,string>(nameof(Reference));
    public string Reference{
        get => GetValue(Href);
        set => SetValue(Href,value);
    }
    private IBrush currentColour;
    
    public Hyperlink(){
        InitializeComponent();
        currentColour = Foreground ?? Brushes.LightBlue;
    }
    // On release send them to the hyperlink
    protected override void OnPointerReleased(PointerReleasedEventArgs e){
        base.OnPointerReleased(e);
        OSInteraction.Open(Reference);
        currentColour = Brushes.MediumPurple;
        Foreground = currentColour;
    }

    /// Styling
    protected override void OnPointerPressed(PointerPressedEventArgs e){
        base.OnPointerPressed(e);
        Foreground = Brushes.DarkMagenta;
    }
    protected override void OnPointerEntered(PointerEventArgs e){
        base.OnPointerEntered(e);
        Foreground = Brushes.LightCyan;
    }
    protected override void OnPointerExited(PointerEventArgs e){
        base.OnPointerExited(e);
        Foreground = currentColour;
    }
}