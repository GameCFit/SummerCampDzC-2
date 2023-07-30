using System;
using System.Collections;

namespace DZ2
{
    public enum MatchState
    {
        Win,
        Lose
    }

    public enum AvailableHeroes
    {
        Lusianna,
        Korkes,
        Nova,
        YoonJin,
        Reiko
    }
    class Program
    {
        public static void Main()
        {
            Hero[] heroes = new Hero[] {
                new Hero(AvailableHeroes.Lusianna, new MatchState[] {MatchState.Win, MatchState.Lose, MatchState.Win}),
                new Hero(AvailableHeroes.Korkes, new MatchState[] { MatchState.Win, MatchState.Win, MatchState.Win, MatchState.Lose, MatchState.Win }),
                new Hero(AvailableHeroes.Nova, new MatchState[] { MatchState.Win, MatchState.Win, MatchState.Lose, MatchState.Win, MatchState.Win, MatchState.Win, MatchState.Lose }),
                new Hero(AvailableHeroes.YoonJin, new MatchState[] { MatchState.Win, MatchState.Lose, MatchState.Lose }),
                new Hero(AvailableHeroes.Reiko, new MatchState[] { MatchState.Lose, MatchState.Lose, MatchState.Win, MatchState.Win, MatchState.Lose })};

            List<Hero> favoriteHeroes = new() { heroes[0] };
            List<Hero> notFavoriteHeroes = new() { heroes[0] };
            List<Hero> successfulHeroes = new() { heroes[0] };
            List<Hero> notSuccessfulHeroes = new() { heroes[0] };
            List<Hero> winsRowHeroes = new() { heroes[0] };

            //Ищем самый большой параметр
            foreach (Hero hero in heroes)
            {
                favoriteHeroes[0] = hero.MatchesNumber > favoriteHeroes[0].MatchesNumber ? hero : favoriteHeroes[0];
                notFavoriteHeroes[0] = hero.MatchesNumber < notFavoriteHeroes[0].MatchesNumber ? hero : notFavoriteHeroes[0];
                successfulHeroes[0] = hero.WinRating() > successfulHeroes[0].WinRating() ? hero : successfulHeroes[0];
                notSuccessfulHeroes[0] = hero.WinRating() < notSuccessfulHeroes[0].WinRating() ? hero : notSuccessfulHeroes[0];
                winsRowHeroes[0] = hero.WinsRow() > winsRowHeroes[0].WinsRow() ? hero : winsRowHeroes[0];
            }

            //Ищем повторяющиеся
            foreach (Hero hero in heroes)
            {
                if (hero.MatchesNumber == favoriteHeroes[0].MatchesNumber && hero != favoriteHeroes[0])
                    favoriteHeroes.Add(hero);
                if (hero.MatchesNumber == notFavoriteHeroes[0].MatchesNumber && hero != notFavoriteHeroes[0])
                    notFavoriteHeroes.Add(hero);

                if (hero.WinRating() == successfulHeroes[0].MatchesNumber && hero != successfulHeroes[0])
                    successfulHeroes.Add(hero);
                if (hero.WinRating() == notSuccessfulHeroes[0].MatchesNumber && hero != notSuccessfulHeroes[0])
                    notSuccessfulHeroes.Add(hero);

                if (hero.WinsRow() == winsRowHeroes[0].WinsRow() && hero != winsRowHeroes[0])
                    winsRowHeroes.Add(hero);
            }

            string favoriteText = $"Самый любимый герой: ";
            string notFavoriteText = $"Самый нелюбимый герой: ";

            string successfulText = $"Самый успешный герой: ";
            string notSuccessfulText = $"Самый неуспешный герой: ";

            string winsRowText = $"Герой с самым большим винстриком: ";

            for (int i = 0; i < successfulHeroes.Count; i++)
            {
                string winRaiting = $"(винрейт {string.Format("{0:0.00}", successfulHeroes[i].WinRating())})";
                successfulText += i != 0 ? ", " : " ";
                successfulText += $"{Enum.GetName(successfulHeroes[i].SelectedHero)}   {winRaiting}";
            }
            for (int i = 0; i < notSuccessfulHeroes.Count; i++)
            {
                string winRaiting = $"(винрейт {string.Format("{0:0.00}", notSuccessfulHeroes[i].WinRating())})";
                notSuccessfulText += i != 0 ? ", " : " ";
                notSuccessfulText += $"{Enum.GetName(notSuccessfulHeroes[i].SelectedHero)}   {winRaiting}";
            }

            for (int i = 0; i < favoriteHeroes.Count; i++)
            {
                string match = $"({favoriteHeroes[i].MatchesNumber} матчей)";
                favoriteText += i != 0 ? ", " : " ";
                favoriteText += $"{Enum.GetName(favoriteHeroes[i].SelectedHero)} {match}";
            }
            for (int i = 0; i < notFavoriteHeroes.Count; i++)
            {
                string match = $"({notFavoriteHeroes[i].MatchesNumber} матчей)";
                notFavoriteText += i != 0 ? ", " : " ";
                notFavoriteText += $"{Enum.GetName(notFavoriteHeroes[i].SelectedHero)} {match}";
            }

            for (int i = 0; i < winsRowHeroes.Count; i++)
            {
                string winRow = $"(винстрик {winsRowHeroes[i].WinsRow()})";
                winsRowText += i != 0 ? ", " : " ";
                winsRowText += $"{Enum.GetName(winsRowHeroes[i].SelectedHero)} {winRow}";
            }

            Console.WriteLine(successfulText + "\n" + notSuccessfulText + "\n" + favoriteText + "\n" + notFavoriteText + "\n" + winsRowText);
        }
    }

    class Hero
    {
        public AvailableHeroes SelectedHero { get; private set; }

        public int MatchesNumber { get; private set; }

        public MatchState[] MatchesResult { get; private set; }

        public Hero(AvailableHeroes selectedHero, MatchState[] matchesResult)
        {
            SelectedHero = selectedHero;
            MatchesResult = matchesResult;

            MatchesNumber = matchesResult.Length;
        }

        public float WinRating()
        {
            float victories = 0;

            foreach (MatchState matchResult in MatchesResult)
                victories += matchResult == MatchState.Win ? 1 : 0;
            return victories / MatchesNumber;
        }

        public int WinsRow()
        {
            int maxRowVictories = 0;

            int rowVictories = 0;

            foreach (MatchState matchResult in MatchesResult)
            {
                if (matchResult == MatchState.Lose)
                {
                    maxRowVictories = rowVictories > maxRowVictories ? rowVictories : maxRowVictories;
                    rowVictories = 0;
                }
                else
                    rowVictories++;
            }
            return rowVictories > maxRowVictories ? rowVictories : maxRowVictories;
        }
    }
}
