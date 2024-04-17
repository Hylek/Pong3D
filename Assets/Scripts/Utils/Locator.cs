using DC.MessageService;

namespace Utils
{
    public class Locator : BaseLocator
    {
        public static ITinyMessengerHub EBus
        {
            get
            {
                var hub = Find<ITinyMessengerHub>();

                if (hub != null) return hub;

                var instance = Add<ITinyMessengerHub>(new TinyMessengerHub());

                return instance;
            }
        }
    }
}