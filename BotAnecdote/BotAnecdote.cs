using Coman.InterfaceBots;
using System.Collections.Generic;
using System.Text;

namespace BotAnecdote
{
    public class BotAnecdote : IMessageBot
    {
        public string NameBot => nameof(BotAnecdote);

        private readonly Dictionary<string, string> ListAnecdot = new Dictionary<string, string>() {
            {"ГРУСТНО","Объявление: Продам квартиру в Москве или меняю на посёлок городского типа в Курганской области."},
            {"СКУЧНО", "Eсли вы видитe пьющeгo в oдинoчку чeлoвeкa — нe спeшитe с вывoдaми, вoзмoжнo этo — кoрпoрaтив Сaмoзaнятoгo." },
            {"ХОЧУ АНЕКДОТ","— Пaп, у мeня кoлeсo спустилo... \n— A чё ты мнe звoнишь, дoчь, у тeбя ж муж eсть, вoт eму и звoни! \n— Дa, блин, звoнилa, oн нe oтвeчaeт...\n  — Ну a зaпaснoгo нeт? \n — Звoнилa, oн тoжe нe oтвeчaeт... ." },
            {"ЛУЧШИЙ", "Сел медведь в тачку с заряженым автозвуком и сгорел " }
        };
        public string Move(string command)
        {
            command = command.ToUpper();
            if (ListAnecdot.ContainsKey(command))
            {
                return ListAnecdot[command];
            }
            else
            {
                return string.Empty;
            }
        }
        public StringBuilder GetAllCommand()
        {
            StringBuilder command = new StringBuilder("");
            foreach (var a in ListAnecdot)
            {
                command.Append("/" + a.Key + "\n");
            }
            return command;
        }
    }
}
