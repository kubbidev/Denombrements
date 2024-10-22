namespace Denombrements;

public static class Program
{
    private static readonly List<Menu> Menus = [];

    private abstract class Menu(int id, string title)
    {
        protected internal string Title { get; } = title;
        protected internal int Id { get; } = id;

        public abstract void Run();
    }

    private class ExitMenu() : Menu(0, "Exit")
    {
        public override void Run()
        {
        }
    }

    private class PermutationMenu() : Menu(1, "Permutation")
    {
        public override void Run()
        {
            var n = Prompt(["Nombre total d'éléments à gérer:"]);
            var r = 1L;
            for (var i = 1; i <= n; i++)
            {
                r *= i;
            }

            Console.WriteLine(n + "! = " + r);
        }
    }

    private class ArrangementMenu() : Menu(2, "Arrangement")
    {
        public override void Run()
        {
            var t = Prompt(["Nombre total d'éléments à gérer:"]);
            var n = Prompt(["Nombre d'éléments dans le sous ensemble:"]);
            var r = 1L;
            for (var i = t - n + 1; i <= t; i++)
            {
                r *= i;
            }

            Console.WriteLine("A(" + t + "/" + n + ") = " + r);
        }
    }

    private class CombinaisonMenu() : Menu(3, "Combinaison")
    {
        public override void Run()
        {
            var t = Prompt(["Nombre total d'éléments à gérer:"]);
            var n = Prompt(["Nombre d'éléments dans le sous ensemble:"]);
            var r1 = 1L;
            for (var i = t - n + 1; i <= t; i++)
            {
                r1 *= i;
            }

            var r2 = 1L;
            for (var i = 1; i <= n; i++)
            {
                r2 *= i;
            }

            Console.WriteLine("C(" + t + "/" + n + ") = " + r1 / r2);
        }
    }

    private static string TryReadConsole()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Please enter a valid input");
            }
            else return input;
        }
    }

    private static int Prompt(List<string> messages)
    {
        messages.ForEach(Console.WriteLine);
        var input = TryReadConsole();
        int parse;
        try
        {
            parse = int.Parse(input);
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid number");
            return Prompt(messages);
        }

        return parse;
    }

    private static List<string> ShowMenus()
    {
        var length = Menus.Select(menu => menu.Title.Length).Max();
        if (length == 0) return [];
        length += 10;

        return Menus
            .Select(menu => menu.Title.PadRight(length, '.') + menu.Id)
            .ToList();
    }

    private static Menu? GetMenu(int id)
    {
        return Menus.Find(match => match.Id == id);
    }

    private static void SelectMenu()
    {
        while (true)
        {
            var menus = ShowMenus();
            menus.Add("Choice:");

            var menuId = Prompt(menus);
            if (menuId == 0) return;

            var menu = GetMenu(menuId);
            if (menu == null)
            {
                Console.WriteLine("Unknown operation");
                continue;
            }

            menu.Run();
            // break;
        }
    }

    public static void Main()
    {
        Menus.Add(new ExitMenu());
        Menus.Add(new PermutationMenu());
        Menus.Add(new ArrangementMenu());
        Menus.Add(new CombinaisonMenu());
        SelectMenu();
    }
}