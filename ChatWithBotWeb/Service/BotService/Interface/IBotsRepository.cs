using Coman.InterfaceBots;
using System.Collections.Generic;

namespace ChatWithBotWeb_v2.Service.BotService.Interface
{
    public interface IBotsRepository
    {
        /// <summary>
        /// Получить список всех ботов 
        /// </summary>
        /// <returns></returns>
        IEnumerable<IBot> GetAllBots();
    }
}
