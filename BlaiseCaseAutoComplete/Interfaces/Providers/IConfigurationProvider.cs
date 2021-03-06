namespace BlaiseCaseAutoComplete.Interfaces.Providers
{
    public interface IConfigurationProvider
    {
        string ProjectId { get; }

        string PublishTopicId { get; }

        string SubscriptionTopicId { get; }

        string SubscriptionId { get; }

        string VmName { get; }
    }
}
