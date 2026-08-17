namespace QuartAQuai.Alertes;

public static class IncidentFactory
{
    public const string TypePanneElectrique = "Panne électrique";
    public const string TypeAvarieMecanique = "Avarie mécanique";
    public const string TypeAlerteMeteo = "Alerte météo";
    public const string TypeIncidentSurete = "Incident sûreté";

    public const string GraviteMineur = "mineur";
    public const string GraviteMajeur = "majeur";
    public const string GraviteCritique = "critique";

    public static readonly string[] Types =
    [
        TypePanneElectrique,
        TypeAvarieMecanique,
        TypeAlerteMeteo,
        TypeIncidentSurete,
    ];

    public static readonly string[] Gravites =
    [
        GraviteMineur,
        GraviteMajeur,
        GraviteCritique,
    ];

    public static string LibelleDetail(string type)
    {
        return type switch
        {
            TypePanneElectrique or TypeAvarieMecanique => "Équipement concerné :",
            TypeAlerteMeteo => "Phénomène observé :",
            TypeIncidentSurete => "Nature de la menace :",
            _ => throw new InvalidOperationException($"Type d'incident invalide : {type}")
        };
    }

    public static Incident Creer(string type, string gravite, string description, string detail)
    {
        return type switch
        {
            TypePanneElectrique => new PanneElectrique(gravite, description, detail),
            TypeAvarieMecanique => new AvarieMecanique(gravite, description, detail),
            TypeAlerteMeteo => new AlerteMeteo(gravite, description, detail),
            TypeIncidentSurete => new IncidentSurete(gravite, description, detail),
            _ => throw new InvalidOperationException($"Type d'incident invalide : {type}")
        };
    }
}
