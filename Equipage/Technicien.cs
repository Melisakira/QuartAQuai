using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public abstract class Technicien : MembreEquipage
{
    private readonly string _origineCompteRendu;

    protected Technicien(string nom, string grade, string posteAffecte, string origineCompteRendu)
        : base(nom, grade, posteAffecte)
    {
        _origineCompteRendu = origineCompteRendu;
    }

    protected abstract bool EstDeMonDomaine(Incident incident);

    public override void ReagirAlerte(Incident incident)
    {
        if (!EstDeMonDomaine(incident))
        {
            AnnoncerHorsDomaine(incident);
            return;
        }

        Annoncer($"Je me rends en {PosteAffecte} pour traiter l'incident : {incident.Decrire()}");
        Annoncer("J'isole le circuit défaillant et je procède à la réparation");
        Annoncer($"Officier de quart, ici {_origineCompteRendu}. Panne résolue concernant : {incident.Decrire()}");
    }
}
