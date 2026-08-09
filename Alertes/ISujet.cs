using System;

public interface ISujet
{
    void AjouterObservateur(IObservateur observateur);
    void RetirerObservateur(IObservateur observateur);
    void NotifierObservateurs(Incident incident);
}
