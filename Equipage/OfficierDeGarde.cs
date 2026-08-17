using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class OfficierDeGarde : MembreEquipage
{
    public OfficierDeGarde(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }
    public override void ReagirAlerte(Incident incident)
    {
        if (incident.Gravite == IncidentFactory.GraviteMineur)
        {
            Annoncer($"Officier de garde au PC Sécurité. Alerte reçue : {incident.Decrire()}. Équipe de garde, un incident vient d'être détecté. Prenez les mesures de sécurité adaptées à votre secteur et rendre compte.");
        }
        else if (incident.Gravite is IncidentFactory.GraviteMajeur or IncidentFactory.GraviteCritique)
        {
            Annoncer($"Officier de garde au PC Sécurité. Alerte reçue : {incident.Decrire()}. Equipe de garde, procédez immédiatement à la levée des doutes.");
            Annoncer("Je bascule les réseaux de secours et je passe le poste de garde en posture de vigilance.");
            if (incident.Gravite == IncidentFactory.GraviteCritique)
            {
                EscaladerVersCommandant(incident);
            }
        }
    }
    public void EscaladerVersCommandant(Incident incident)
    {
        Annoncer($"Je réveille le commandant et l'en informe : {incident.Decrire()}");
    }
}
