using Swack.Logic;
using Swack.Logic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Swack.UI.ViewModels
{
    internal class ChannelViewModel : NotifyPropertyChanged // INotifyPropertyChanged
    {
        private readonly IMessagingLogic messagingLogic;

        private int unreadMessages;
        private string? currentMessage;

        public ChannelViewModel(Channel channel, IMessagingLogic messagingLogic)
        {
            this.Channel = channel ?? throw new ArgumentNullException(nameof(channel));
            this.messagingLogic = messagingLogic ?? throw new ArgumentNullException(nameof(messagingLogic));
            this.SendMessageCommand = new AsyncDelegateCommand(
                this.sendMessageAsync,
                _ => !string.IsNullOrEmpty(CurrentMessage)
            );
        }

        public Channel Channel { get; }
        public ObservableCollection<Message> Messages { get; } = [];
        //public event PropertyChangedEventHandler? PropertyChanged;
        public int UnreadMessages {
            get => unreadMessages;
            set
            {
                //if (unreadMessages != value)
                //{
                //    unreadMessages = value;
                //    PropertyChanged?.Invoke(
                //        this,
                //        new PropertyChangedEventArgs(nameof(unreadMessages))
                //    );
                //}
                this.Set(ref this.unreadMessages, value);
            }
        }
        public string? CurrentMessage
        {
            get => currentMessage;
            set
            {
                //if (this.CurrentMessage != value)
                //{
                //    currentMessage = value;
                //    PropertyChanged?.Invoke(
                //        this,
                //        new PropertyChangedEventArgs(nameof(currentMessage))
                //    );
                //}
                this.Set(ref currentMessage, value);
            }
        }

        public ICommand SendMessageCommand { get; private set; }

        private async Task sendMessageAsync(object? _)
        {
            if (!string.IsNullOrEmpty(CurrentMessage))
            {
                await this.messagingLogic.SendMessageAsync(this.Channel, this.CurrentMessage);
                this.CurrentMessage = null;
            }
        }

    }
}
