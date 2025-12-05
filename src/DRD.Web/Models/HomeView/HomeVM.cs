/*
    Fichier             : HomeVM.cs
    Entité(s) touchée   : ApplicationUser, WebMessage
    Date de création    : 26-06-2025
    Description         : ViewModel pour la page d'accueil (Home/Index).
                        : Représente les données nécessaires pour la vue d'accueil,
                        : incluant le nom complet de l'utilisateur et la liste des messages actifs.
*/

using System.Collections.Generic;

namespace DRD.Web.Models.Home
{
    public class HomeVM
    {
        public string UserFullName { get; set; } = string.Empty;

    }
}
