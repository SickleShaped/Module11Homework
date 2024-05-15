using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Services.Interfaces;

namespace VoiceTexterBot.Controllers
{
    public class MessageController
    {
        private readonly IStorage _memoryStorage; // Добавим это
        private readonly ITelegramBotClient _telegramClient;
        private readonly ISum _sum;
        private readonly ICount _count;

        public MessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, ISum sum, ICount count)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _sum = sum;
            _count = count;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Text;
            if (fileId == null)
                return;

            if(message.Text == "/start")
            {
                var buttons = new List<InlineKeyboardButton[]>();
                 buttons.Add(new[]
                {
                    InlineKeyboardButton.WithCallbackData($"Сложение" , $"sum"),
                    InlineKeyboardButton.WithCallbackData($"Подсчет символов" , $"count")
                });

                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>Бот складывает числа или считает количество символов в тексте.</b> {Environment.NewLine}" +
                $"{Environment.NewLine}Можно записать сообщение и переслать другу, если лень печатать.{Environment.NewLine}", cancellationToken: ct, replyMarkup: new InlineKeyboardMarkup(buttons));

            }
            else
            {
                string actionCode = _memoryStorage.GetSession(message.Chat.Id).ActionCode; // Здесь получим язык из сессии пользователя
                switch (actionCode)
                {
                    case "count":
                        var count = _count.GetCount(message.Text);
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"В этом тексте {count} символов");
                        break;

                    case "sum":
                        var sum = _sum.GetSum(message.Text);
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел - {sum}");
                        break;
                }
            }
            

            
        }
    }
}