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
                new Hero(AvailableHeroes.Korkes, new MatchState[] { MatchState.Lose, MatchState.Lose, MatchState.Win }),
                new Hero(AvailableHeroes.Nova, new MatchState[] { MatchState.Lose, MatchState.Lose, MatchState.Lose }),
                new Hero(AvailableHeroes.YoonJin, new MatchState[] { MatchState.Win, MatchState.Win, MatchState.Win }),
                new Hero(AvailableHeroes.Reiko, new MatchState[] { MatchState.Win, MatchState.Lose, MatchState.Win })};

            List<Hero> favoriteHeroes = new() { heroes[0] };
            List<Hero> notFavoriteHeroes = new() { heroes[0] };
            List<Hero> successfulHeroes = new() { heroes[0] };
            List<Hero> notSuccessfulHeroes = new() { heroes[0] };
            List<Hero> winsRowHeroes = new() { heroes[0] };

            //Ищим самый большой параметр
            foreach (Hero hero in heroes)
            {
                favoriteHeroes[0] = hero.MatchesNumber > favoriteHeroes[0].MatchesNumber ? hero : favoriteHeroes[0];
                notFavoriteHeroes[0] = hero.MatchesNumber < notFavoriteHeroes[0].MatchesNumber ? hero : notFavoriteHeroes[0];
                successfulHeroes[0] = hero.WinRating() > successfulHeroes[0].WinRating() ? hero : successfulHeroes[0];
                notSuccessfulHeroes[0] = hero.WinRating() < notSuccessfulHeroes[0].WinRating() ? hero : notSuccessfulHeroes[0];
                winsRowHeroes[0] = hero.WinsRow() > winsRowHeroes[0].WinsRow() ? hero : winsRowHeroes[0];
            }

            //Ищим повторяющиеся
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

            //Вывод игоков
            foreach (Hero hero in heroes)
            {
                string heroName = Enum.GetName(typeof(AvailableHeroes), hero.SelectedHero);
                Console.WriteLine($"{hero.SelectedHero}, рейтинг: {hero.WinRating()}%, всего матчей: {hero.MatchesNumber}");
            }

            string favoriteText = $"Любимый: ";
            string notFavoriteText = $"Не любимый: ";

            string successfulText = $"Успешный: ";
            string notSuccessfulText = $"Не успешный: ";

            string winsRowText = $"С самым большим винстриком: ";

            for (int i = 0; i < favoriteHeroes.Count; i++)
            {
                favoriteText += $", {Enum.GetName(favoriteHeroes[i].SelectedHero)}";
            }
            for (int i = 0; i < notFavoriteHeroes.Count; i++)
            {
                notFavoriteText += $", {Enum.GetName(notFavoriteHeroes[i].SelectedHero)}";
            }

            for (int i = 0; i < successfulHeroes.Count; i++)
            {
                successfulText += $", {Enum.GetName(successfulHeroes[i].SelectedHero)}";
            }
            for (int i = 0; i < notSuccessfulHeroes.Count; i++)
            {
                notSuccessfulText += $", {Enum.GetName(notSuccessfulHeroes[i].SelectedHero)}";
            }

            for (int i = 0; i < winsRowHeroes.Count; i++)
            {
                winsRowText += $", {Enum.GetName(winsRowHeroes[i].SelectedHero)}";
            }

            Console.WriteLine(favoriteText + "\n" + notFavoriteText + "\n" + successfulText + "\n" + notSuccessfulText + "\n" + winsRowText);
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
            return victories / MatchesNumber * 100;
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
                rowVictories++;
            }
            return rowVictories > maxRowVictories ? rowVictories : maxRowVictories;
        }
    }
}
