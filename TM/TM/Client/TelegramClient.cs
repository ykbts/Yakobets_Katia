
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TM.Model;



namespace TM
{
    public class TelegramClient
    {
        MovieClient movieClient = new MovieClient();
        string text, trailerId, trailerURL, trailerTitle, trailerFullTitle, inline1, button;
        MovieURL Url;




        TelegramBotClient botClient = new TelegramBotClient("5322140821:AAFOs_8fcOd2d5Ua-JlsFLlcqGaljJ5m16M");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };

        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine($"Bot{botMe.Username} is working");
            Console.ReadKey();

        }


        private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Помилка в тг-бот АПІ\n {apiRequestException.ErrorCode}" +
                $"\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandlerMessageAsync(botClient, update.Message);
            }

            if (update?.Type == UpdateType.CallbackQuery)
            {
                await HandlerCallbackQuery(botClient, update.CallbackQuery);
            }

        }

        async Task HandlerCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery)
        {
            if (callbackQuery.Data.StartsWith("Callback1"))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text: $"{inline1}");
                return;
            }


            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text: $" \n{callbackQuery.Data}");
            return;
        }



        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {

            if (message.Text == "/start" || message.Text == "Back🔙")
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new
                (
                    new[]
                    {
                      new KeyboardButton[]{ "Information🗒", "Watch the trailer🎞" }
                    }
                )
                { ResizeKeyboard = true };
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Welcome to the virtual cinema🍿.\nI'll help you choose a movie🎥.\n Make your choice😋", replyMarkup: replyKeyboardMarkup);
                return;

            }


            else if (message.Text == "Information🗒" || message.Text == "Watch the trailer🎞")
            {
                button = message.Text;
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Type the title of the movie or series");
            }


            else if (message.Text != "Information🗒" && message.Text != "Watch the trailer🎞" && message.Text != "Recommend to others👍🏻" && message.Text != "Change to full title" && message.Text != "Unrecommend👎🏻" && message.Text != "Recommended movies❤️" && message.Text != "Back🔙")
            {
                char delim = ','; string text = message.Text;
                string rating = movieClient.GetRating(text).Result.IMDb;
               
                if ( rating == null )
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Oops :(\nTry again");
                }

                else
                {
                    inline1 = $"Rating🌟: {rating}";
                    
                   

                   

                    if (button == "Information🗒")
                    {
                        string fullTitle = movieClient.Get_Movie_Serial("en", text).Result.FullTitle;
                        string year = movieClient.Get_Movie_Serial("en", text).Result.Year;
                        string time = movieClient.Get_Movie_Serial("en", text).Result.RuntimeStr;

                        string plot = movieClient.Get_Movie_Serial("en", text).Result.Plot;

                        string director = String.Join(delim, movieClient.Get_Movie_Serial("en", text).Result.DirectorList);
                        string star = String.Join(delim, movieClient.Get_Movie_Serial("en", text).Result.StarList);
                        string keywords = String.Join(delim, movieClient.Get_Movie_Serial("en", text).Result.KeywordList);
                        string genres = movieClient.Get_Movie_Serial("en", text).Result.Genres;

                        string inf = $"\nTitle🎥: {fullTitle}\nYear🔎: {year}\nRuntime⏰: {time}\nPlot📖: {plot}\nDirectors🎬: {director}\nStars💫: {star}\nGenres🍿: {genres}\nKeyWords🔑: {keywords}"; ;



                        await botClient.SendTextMessageAsync(message.Chat.Id, $"{inf}");




                        InlineKeyboardMarkup keyboardMarkup = new(
                               new[]
                               {
                            new[]
                            {
                               InlineKeyboardButton.WithCallbackData("Rating",$"Callback1 {inline1}"),

                            }
                               }
                               );
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Click to know Rating", replyMarkup: keyboardMarkup);
                        return;


                    }


                    else if (button == "Watch the trailer🎞")
                    {
                        trailerId = movieClient.Trailer(text).Result.IMDbId;
                        trailerURL = movieClient.Trailer(text).Result.VideoUrl;
                        trailerTitle = movieClient.Trailer(text).Result.Title;
                        trailerFullTitle = movieClient.Trailer(text).Result.FullTitle;
                        MovieURL Url = new MovieURL(trailerId, trailerURL, trailerTitle);


                        await botClient.SendTextMessageAsync(message.Chat.Id, $"You can watch the trailer here⬇️: {trailerURL}");
                        ReplyKeyboardMarkup replyKeyboardMarkup = new
                        (
                            new[]
                            {
                                       new KeyboardButton[]{ "Recommend to others👍🏻", "Change to full title", "Unrecommend👎🏻" },
                                       new KeyboardButton[]{ "Recommended movies❤️", "Back🔙" }
                            }
                             )
                        { ResizeKeyboard = true };
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Choose🤔", replyMarkup: replyKeyboardMarkup);
                        return;




                    }
                }
            }


                        if (message.Text == "Recommend to others👍🏻")
                        {
                MovieURL Url = new MovieURL(trailerId, trailerURL, trailerTitle);
                movieClient.PostMovieURL(Url);
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"{trailerTitle} added to favorites❤️");


                        }
                        else if (message.Text == "Change to full title")
                        {
                            MovieURL Url = new MovieURL(trailerId, trailerURL, trailerFullTitle);
                            movieClient.PutMovieURL(trailerId, Url);
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"{trailerFullTitle} successfully changed");
                        }
                        else if (message.Text == "Unrecommend👎🏻")
                        {
                            movieClient.DeleteMovieURL(trailerId);
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"{trailerTitle} successfully deleted from favorites");
                        }
                        else if (message.Text == "Recommended movies❤️")
                        {
                            MovieURL[] list = movieClient.GetAllMovies().Result;
                            foreach (var m in list)
                            {
                                await botClient.SendTextMessageAsync(message.Chat.Id, $" Recommended movie: {m.ToString()}");
                            }

                        }
                    }

                }
            }
        
    


    

    

  

