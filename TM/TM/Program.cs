using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TM.Model;

namespace TM
{
    class Program
    {
        static void Main(string[] args)
        {
            TelegramClient TelegramClient = new TelegramClient();
            TelegramClient.Start();
            Console.ReadKey();

        }
    }
}