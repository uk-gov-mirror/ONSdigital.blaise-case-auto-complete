using Blaise.Nuget.PubSub.Contracts.Interfaces;
using BlaiseCaseAutoComplete.Interfaces.Providers;
using BlaiseCaseAutoComplete.Services;
using Moq;
using NUnit.Framework;

namespace BlaiseCaseAutoComplete.Tests.Services
{
    public class QueueServiceTests
    {
        private Mock<IConfigurationProvider> _configurationProviderMock;
        private Mock<IMessageHandler> _messageHandlerMock;
        private Mock<IFluentQueueApi> _queueProviderMock;

        private readonly string _projectId;
        private readonly string _subscriptionTopicId;
        private readonly string _subscriptionId;
        private readonly string _vmName;

        private QueueService _sut;

        public QueueServiceTests()
        {
            _projectId = "ProjectId";
            _subscriptionTopicId = "subscriptionTopicId";
            _subscriptionId = "SubscriptionId";
            _vmName = "VmName";
        }

        [SetUp]
        public void SetUpTests()
        {
            _configurationProviderMock = new Mock<IConfigurationProvider>();
            _configurationProviderMock.Setup(c => c.ProjectId).Returns(_projectId);
            _configurationProviderMock.Setup(c => c.SubscriptionId).Returns(_subscriptionId);
            _configurationProviderMock.Setup(c => c.SubscriptionTopicId).Returns(_subscriptionTopicId);
            _configurationProviderMock.Setup(c => c.VmName).Returns(_vmName);

            _messageHandlerMock = new Mock<IMessageHandler>();

            _queueProviderMock = new Mock<IFluentQueueApi>();

            _sut = new QueueService(
                _configurationProviderMock.Object,
                _queueProviderMock.Object);
        }

        [Test]
        public void Given_I_Call_Subscribe_Then_The_Correct_Call_Is_Made_And_Subscribes_To_The_Appropriate_Queues()
        {
            //arrange
            _queueProviderMock.Setup(q => q.WithProject(It.IsAny<string>())).Returns(_queueProviderMock.Object);
            _queueProviderMock.Setup(q => q.WithTopic(It.IsAny<string>())).Returns(_queueProviderMock.Object);
            _queueProviderMock.Setup(q => q.CreateSubscription(It.IsAny<string>(), It.IsAny<int>())).Returns(_queueProviderMock.Object);
            _queueProviderMock.Setup(q => q.StartConsuming(It.IsAny<IMessageHandler>(), It.IsAny<bool>()));

            //act
            _sut.Subscribe(_messageHandlerMock.Object);

            //assert
            _queueProviderMock.Verify(v => v.WithProject(_projectId), Times.Once);
            _queueProviderMock.Verify(v => v.WithTopic(_subscriptionTopicId), Times.Once);
            _queueProviderMock.Verify(v => v.CreateSubscription($"{_subscriptionId}-{_vmName}", It.IsAny<int>()), Times.Once);
            _queueProviderMock.Verify(v => v.StartConsuming(_messageHandlerMock.Object, It.IsAny<bool>()), Times.Once);
        }


        [Test]
        public void Given_I_Call_CancelAllSubscriptions_Then_The_Correct_Call_Is_Made()
        {
            //arrange
            _queueProviderMock.Setup(q => q.StopConsuming());

            //act
            _sut.CancelAllSubscriptions();

            //assert
            _queueProviderMock.Verify(v => v.StopConsuming(), Times.Once);
        }
    }
}
