using QuartAQuai.Alertes;
using QuartAQuai.Equipage;

namespace QuartAQuai;

static class Program
{
    static void Main(string[] args)
    {
        OfficierDeGarde officier = new("Pierre", "Lieutenant", "PC Sécurité");
        AvarieMecanique avarieCritique = new("critique", "panne de régulation", "Génératrice 1");
        PanneElectrique panneElectrique = new("majeur", "coupure de courant", "Système d'éclairage");
        Console.WriteLine("--- DÉBUT DU TEST DE L'OFFICIER DE GARDE ---");

        officier.ReagirAlerte(avarieCritique);
        officier.ReagirAlerte(panneElectrique);


        Console.WriteLine("--- FIN DU TEST ---");
    }
}

// S1118 signifie qu'une classe qui ne contient que des membres statiques devrait être marquée static ou bien fournir un constructeur protected pour empêcher l'instanciation.
// or class Program
// protected Program() { }

// à réfélchir : est-ce que je veux que cette classe soit instanciable ou pas ? 