using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamTracker
{
    class Program
    {
        static void Main()
        {
            var httpClient = new HttpClient();

            string[] gameUrls = new[]
            {
                @"https://store.steampowered.com/app/266390/Farm_for_your_Life/",
                @"https://store.steampowered.com/app/427520/Factorio/",
                @"https://store.steampowered.com/app/546430/Pathway",
                @"https://store.steampowered.com/app/305660/Infect_and_Destroy/",
                @"https://store.steampowered.com/app/275850/No_Mans_Sky/"
            };

            foreach (string gameUrl in gameUrls)
            {
                string websiteCode = httpClient.GetStringAsync(gameUrl).Result;

                Match match = Regex.Match(websiteCode, @"apphub_AppName.*>(.*)<");
                string gameName = match.Groups[1].Value;
                Console.WriteLine(gameName.ToUpperInvariant());

                match = Regex.Match(websiteCode, @"Recent Reviews.*\n.*\n.*game_review_summary.*>(.*)<");
                if (match.Success)
                {
                    string recentReviews = match.Groups[1].Value;
                    Console.WriteLine($"Recent reviews: {recentReviews}");
                }

                match = Regex.Match(websiteCode, @"All Reviews.*\n.*\n.*game_review_summary.*>(.*)<");
                string allReviews = match.Groups[1].Value;
                Console.WriteLine($"All reviews: {allReviews}");

                Console.WriteLine();
            }
        }
    }
}
