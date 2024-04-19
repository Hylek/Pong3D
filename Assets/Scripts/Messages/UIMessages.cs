using DC.MessageService;

namespace Messages
{
    public class OpenMainMenuMessage : ITinyMessage { public object Sender { get; } }
    public class StartSinglePlayerMessage : ITinyMessage { public object Sender { get; } }
    public class StartMultiPlayerMessage : ITinyMessage { public object Sender { get; } }
    public class OpenControlsPageMessage : ITinyMessage { public object Sender { get; } }
    public class OpenSettingsPageMessage : ITinyMessage { public object Sender { get; } }
}