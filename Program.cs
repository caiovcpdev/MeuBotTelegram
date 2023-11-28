using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MeuBotTelegram
{
    class Program
    {
        private static TelegramBotClient ?_botClient;
        static async Task Main(string[] args)
        {
            string botToken = "6599680172:AAEe5-06DmI6oylA8Qsm76y9uir_FRCOCfg"; // Substitua pelo token do seu bot
            _botClient = new TelegramBotClient(botToken);

            var enviarMensagemTask = EnviarMensagemTelegram();
            var receberMensagemTask = ReceberMensagemTelegram();
            await Task.WhenAll(enviarMensagemTask);

            //Console.WriteLine("Mensagem enviada com sucesso!");
        }

        private static async Task EnviarMensagemTelegram()
        {
            long chatID = -4046702465; // Substitua pelo ID do chat que você deseja enviar a mensagem
            string mensagem = " ⠀⠚⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡄⠀⠀⠀ ⠀⠀⠀⠘⠛⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⠀⠀ ⠀⠀⠀⠀⠀⠀⠈⠛⢿⣿⣿⣿⠟⠛⠋⠉⠉⠉⠙⣿⣿⣿⣿⣶⣀⠀ ⠶⠿⢰⣿⣶⣦⠀⠀⢸⣿⣿⣦⡄⠀⢀⣴⣾⣿⣿⣿⡿⠿⣿⣿⣿ ⠀⠀⠀⠻⠿⠃⠀⠀⠀⣿⣿⣿⣧⠀⠀⠉⣉⣉⣩⣥⡶⠀⠀⠀⣿⡇ ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠻⣿⣿⣿⣿⠀⠀⣻⣿⣿⣿⠃⠀⠀⢠⣿⠃ ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠉⠁⣴⣿⣿⣿⠟⠃⡀⠀⢠⣿⠟⠀ ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣤⣄⠀⣤⣤⣤⢰⣿⡦⣿⠀⣿⣿⠀⠀ ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠛⠛⠛⠀⠛⠛⠋⠈⠉⠀⠀⢀⣿⣿⠀⠀ ⠀⠀⠀⠀⠀⠀⠀⢴⡆⣤⣤⣄⡄⢀⣀⣀⢀⣀⢀⡄⠀⢨⣿⣿⠀⠀ ⠀⠀⠀⠀⠀⠀⠀⠘⠃⣿⣿⣿⠇⣿⣿⡋⣿⠏⠛⣃⣤⣾⣿⣿⠀⠀ ⠀⠀⠀⠀⠀⠀⠀⠀⠀⣤⣤⣄⣠⣤⣠⣴⣶⣾⣿⣿⣿⣿⣿⣿ ";
            int temporizadorEmSegundos = 2;

            while (true)
            {
                try
                {
                    await _botClient.SendTextMessageAsync(chatID, mensagem);
                    await Task.Delay(TimeSpan.FromSeconds(temporizadorEmSegundos));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro ao enviar a mensagem: {ex.Message}");
                }
            }
        }

        private static async Task ReceberMensagemTelegram()
        {
            int? ultimoIdMensagemProcessada = null;

            while (true)
            {
                try
                {
                    var updates = await _botClient.GetUpdatesAsync(offset: ultimoIdMensagemProcessada);

                    foreach (var update in updates)
                    {
                        if (update.Message != null)
                        {
                            Console.WriteLine($"Mensagem recebida: '{update.Message.Text}'");
                            // Aqui você pode adicionar lógica para processar ou reagir às mensagens recebidas

                            // Atualize o ID da última mensagem processada
                            ultimoIdMensagemProcessada = update.Message.MessageId + 1;
                        }
                    }

                    // Aguarda um tempo antes de fazer a próxima verificação
                    await Task.Delay(TimeSpan.FromSeconds(5)); // Por exemplo, espera 5 segundos antes de verificar novamente
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro ao receber a mensagem: {ex.Message}");
                }
            }
        }


    }
}
