using QuartAQuai.AQuai;
using QuartAQuai.Equipage;

namespace QuartAQuai;

static class Program
{
    static void Main(string[] args)
    {
        Navire Navire = new("Wesdiep", "Frégate");
        RondeurSecurite rondeurSecurite = new("Jean-Pierre", "Quartier-Maître", "Ronde");

        Console.WriteLine($"--- DÉBUT DE LA RONDE SÉCURITÉ ---");

        // Boucle 
        foreach (string compartiment in Navire.Compartiments)
        {
            Console.WriteLine($"\nInspection : {compartiment}");
            Console.Write("Taper l'observation du rondeur > ");

            string observationSaisie = Console.ReadLine();

            Navire.FaireRonde(rondeurSecurite, compartiment, observationSaisie);
        }

        Console.WriteLine("\n--- FIN DE LA RONDE — TOUS LES COMPARTIMENTS ONT ÉTÉ INSPECTÉS ---");
    }
}


