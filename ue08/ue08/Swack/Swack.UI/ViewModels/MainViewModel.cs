using Swack.Logic;
using Swack.Logic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swack.UI.ViewModels
{
    internal class MainViewModel
    {

        private IMessagingLogic messagingLogic;
        private ChannelViewModel? currentChannel;

        public MainViewModel(IMessagingLogic messagingLogic)
        {
            //Channels = new List<Channel>()
            //{
            //    new("#swk5"),
            //    new("#wea5"),
            //    new("#kurztests")
            //};

            this.messagingLogic = messagingLogic ?? throw new ArgumentNullException(nameof(messagingLogic));

        }

        // ObservableCollection implementiert für uns das INotifyPropertyChanged
        public ObservableCollection<ChannelViewModel> Channels { get; private set; } = []; // = Enumerable.Empty<Channel>();

        public ChannelViewModel? CurrentChannel {
            get => currentChannel;
            set
            {
                currentChannel = value;
                if (currentChannel is not null)
                {
                    currentChannel.UnreadMessages = 0;
                }
            }
        }

        public async Task InitializeAsync()
        {
            foreach( var channel in await this.messagingLogic.GetChannelsAsync())
            {
                this.Channels.Add(new ChannelViewModel(channel, messagingLogic));
            }

            this.messagingLogic.MessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(Message message)
        {
            var channel = this.Channels.FirstOrDefault(c => c.Channel.Name == message.Channel.Name);
            channel?.Messages.Add(message);

            if (   channel is not null
                && channel != CurrentChannel)
            {
                ++channel.UnreadMessages;
            }
        }

    }
}
