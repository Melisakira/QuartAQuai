# QuartAQuai

Simulation en console c# d'un quart de nuit à quai à bord de la frégate F911 Wesdiep (Force Navale Belge). Projet réalisé dans le cadre de l'examen de seonde session de Programmation Orientée Objet.

## Présentation

Le programme met en scène un équipage de garde qui assure le quart de nuit : 


Le scénario s'appuie sur une expérience vécue d'électricienne de bord.





## Fonctionnalités (4, indépendantes)

| # | Fonctionnalité | Classe porteuse |
|---|---|---|
| 1 |  Sécurité incendie et technique: l'électricien et le mécanicien sureveillent les systèmes électriques et mécaniques du navire. Une anomalie peut déclencher une panne électrique ou une avarie mécanique. |
|2| Sécurité nautique : le veilleur observe la tension des amarres face au vent et à la marée. Une tension excessive déclenche une alerte météo. |
| 3 | Sûreté et contrôle d'accès : le veilleur filtre les identités à la coupée et contrôle l'accès au navire. Une identité suspecte déclenche une alerte de sûreté. |
| 4 | Permanence du commandement et intervention d'urgence: l'officier de quart assure la permanence du commandement et la communication. Il centralise les alertes et coordonne les interventions. |

Chaque fonctionnalité peut déclarer un type d'incident qui lui est propre (`PanneElectrique`, `AvarieMecanique`, `AlerteMeteo`, `BrècheDeSûreté`). Le CentreAlerte notifie tous les membres d'équipage abonnés, qui réagissent chacun selon leur rôle.

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


## Remarque

Toutes les classes métier (`Equipage/`, `Alertes/`, `Navigation/`) utilisent uniquement `Console.WriteLine`: auncune ne dépend de Spectre.Console. Seul `Program.cs` utilise Spectre.Console pour l'affichage. Cela permet de tester les classes métier indépendamment de l'interface utilisateur.