
using ChatBotWeb.Service.BotService.Interface;
using Coman.Extensions;
using Coman.InterfaceBots;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ChatBotWeb.Service.BotService
{
    public class BotsRepository : IBotsRepository
    {
        /// <summary>
        /// C:\\Users\\f.volodin\\source\\repos\\ChatWithBotWeb v2\\ChatWithBotWeb v2\\bin\\Debug\\net5.0\\AssemblyBots\\BotAnecdote.dll
        /// C:\Users\f.volodin\source\repos\ChatWithBotWeb v2\ChatWithBotWeb v2\bin\Debug\net5.0\AssemblyBots
        /// </summary>
        /// C:\\Users\\f.volodin\\source\\repos\\ChatWithBotWeb v2\\ChatWithBotWeb v2\\bin\\Debug\\net5.0\\AssemblyBots\\BotEvent.dll
        /// 
        /// 
        /// <returns></returns>
        public IEnumerable<IBot> GetAllBots()
        {
            var bots = new List<IBot>();

            var fileNames = GetAllAssemblyFilesNameFrom("AssemblyBots");

            foreach (var fileName in fileNames)
            {
                var types = Assembly.LoadFile(fileName).GetTypes();
                foreach (var type in types)
                {
                    if (type.IsInterfaceImplemented(nameof(IEventBot)))
                    {
                        bots.Add((IEventBot)Activator.CreateInstance(type));
                        continue;
                    }

                    if (type.IsInterfaceImplemented(nameof(IMessageBot)))
                    {
                        bots.Add((IMessageBot)Activator.CreateInstance(type));
                    }
                }
            }
            return bots;
        }

        private string GetExecutionAssemblyFolder()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Получить название всех файлов с расширением .dll
        /// </summary>
        /// <param name="directory">Целевая папка</param>
        /// <returns></returns>
        private IEnumerable<string> GetAllAssemblyFilesNameFrom(string directory)
        {
            return Directory.GetFiles(Path.Combine(GetExecutionAssemblyFolder(), directory), "*.dll");
        }

    }
}
