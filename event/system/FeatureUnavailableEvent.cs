namespace DrinkShop.Events
{
    public class FeatureUnavailableEvent : IEvent
    {
        public string Feature { get; }
        public string Reason { get; }

        public FeatureUnavailableEvent(string feature, string reason)
        {
            Feature = feature;
            Reason = reason;
        }
    }

}

