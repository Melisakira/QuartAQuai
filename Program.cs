namespace QuartAQuai;

class Program
{
    static void Main(string[] args)
    {
        bool continuerLeQuart = true;
        while (continuerLeQuart)
        {
            Console.WriteLine("1. Consulter le journal de bord");
            Console.WriteLine("2. Faire la ronde de sécurité");
            Console.WriteLine("3. Assurer la veille à la coupée");
            Console.WriteLine("4. Déclarer un incident");
            Console.WriteLine("0. Terminer le quart");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    Console.WriteLine("(à coder)"); break;
                case "2":
                    FaireRonde(); break;
                case "3":
                    AssurerVeille(); break;
                case "4":
                    DeclarerIncident(); break;
                case "0":
                    continuerLeQuart = false;
                    Console.WriteLine("Fin du quart."); break;
            }
        }
    }

    static void FaireRonde()
    {
        Console.WriteLine("(à coder)");
    }
    static void AssurerVeille()
    {
        Console.WriteLine("(à coder)");
    }
    static void DeclarerIncident()
    {
        Console.WriteLine("(à coder)");
    }
}


