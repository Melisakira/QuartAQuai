# QuartEnMer

## Contexte du projet

Ce projet a été réalisé dans le cadre du cours de Programmation Orientée Objet (seconde session).
Le scénario s'appuie sur mon expérience en tant qu'électricienne de bord à la Marine Belge.

## Scénario

Un équipage est de quart de nuit en mer : officier de quart, électricien, mécanicien et matelot de pont,
chacun affecté à un poste précis. L'application simule les actions typiques d'un quart :
consulter les consignes laissées par le quart précédent, faire sa ronde, réagir à un incident
signalé, assurer la veille.

## Fonctionnalités (4, indépendantes)

| # | Fonctionnalité | Classe porteuse |
|---|---|---|
| 1 | Consulter le journal de bord (briefing de relève : cap, météo, événements du quart précédent) | `JournalDeBord.ConsulterEntrees()` |
| 2 | Faire la ronde des postes (machines, électricité, pont) — dépend du poste de la personne | `Navire.FaireRonde(MembreEquipage)` |
| 3 | Déclarer un incident (panne électrique, avarie mécanique ou alerte météo), avec escalade hiérarchique jusqu'au commandant si la gravité est critique | `CentreAlerte.DeclarerIncident(Incident)` |
| 4 | Assurer la veille (surveiller l'horizon, le trafic, le cap) | `Officier.AssurerVeille()` |

Chaque fonctionnalité porte sur un sous-système distinct du navire ; aucune n'est la suite
obligée d'une autre.

## Architecture - deux hiérarchies d'héritage

### `MembreEquipage` (hiérarchie principale, polymorphisme)

Classe abstraite `MembreEquipage` (nom, grade, poste affecté) avec une méthode abstraite
`ReagirAlerte()`. Quatre rôles la spécialisent réellement, chacun avec sa propre réaction :
`Officier`, `Electricien`, `Mecanicien`, `Matelot`.

### `Incident` (hiérarchie secondaire)

Classe abstraite `Incident` (gravité, description) avec une méthode abstraite `Decrire()`.
Trois types concrets : `PanneElectrique`, `AvarieMecanique`, `AlerteMeteo`.

## Design pattern : Observer

Problème : quand un incident est déclaré au CentreAlerte, plusieurs membres d'équipage doivent réagir automatiquement, sans que le centre d'alerte connaisse à l'avance qui ils sont individuellement (couplage faible).

Solution retenue : pattern Observer

ISujet : rôle du CentreAlerte (Abonner, Desabonner, Notifier)
IObservateur : implémenté par chaque sous-classe de MembreEquipage (MettreAJour)
CentreAlerte ne connaît que l'interface IObservateur, jamais les classes concrètes directement — ajouter un nouveau rôle observateur ne modifie jamais CentreAlerte.
