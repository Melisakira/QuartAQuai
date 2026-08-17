using QuartAQuai.Equipage;

namespace QuartAQuai.AQuai;

public class Navire
{
    public string Nom { get; }
    public string Type { get; }
    public List<string> Compartiments { get; }
    public Navire(string nom, string type)
    {
        Nom = nom;
        Type = type;
        Compartiments =
        [
            "Coursives",
            "Espaces logistiques et de stockage",
            "Locaux de vie et d'habitation",
            "Locaux techniques et propulsifs",
            "Locaux de commandement et d'accès",
        ];
    }
    public static void FaireRonde(RondeurSecurite rondeurSecurite, string compartiment, string observation)
    {
        Console.WriteLine($"{rondeurSecurite.Identite} - Je fais ma ronde dans {compartiment}, attentif à toute anomalie - {observation}");
    }
}


