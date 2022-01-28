using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coman.InterfaceBots
{
    public interface IEventBot : IBot
    {
        public string NameBot { get; }
        /// <summary>
        /// Действие бота
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string Move(EventChat command);
    }
}
