using System.Text;
using ShikimoriNET.Enums;
using ShikimoriNET.Helpers;
using ShikimoriNET.Models.Anime;

namespace ShikimoriTelegramBot.Helpers
{
    public static class MarkdownHelpers
    {
        public static string GetMarkdown(Anime anime)
        {
            var kind = AttributeHelpers.GetDescriptionAttributeData(anime.Kind) ?? "?";

            var markdownStringBuilder = new StringBuilder()
                .AppendLine($"<a href=\"{Settings.ShikimoriUrl + anime.Url}\">{anime.Russian ?? anime.Name}</a>")
                .AppendLine($"Тип: {kind}")
                .Append($"Статус: {AttributeHelpers.GetDescriptionAttributeData(anime.Status)}");

            switch (anime.Status)
            {
                case Status.Released:
                    markdownStringBuilder.Append($", {anime.Episodes.ToString()} эп.");

                    break;

                case Status.Ongoing:
                    markdownStringBuilder.Append($", {anime.EpisodesAired.ToString()}/{anime.Episodes.ToString()}");

                    break;
            }

            return markdownStringBuilder.ToString();
        }
    }
}