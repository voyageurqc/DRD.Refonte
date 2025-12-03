// ============================================================================
// 💻 Projet              : DRD.Web
// 📄 Nom du fichier        : site.js
// 📄 Classe du fichier     : Script
// 📍 Emplacement           : wwwroot/js/
// 🏛️ Entité(s) touchée(s)  : N/A (Global)
// 📅 Créé le              : 2025-07-24
//
// 📌 Description :
//     Fichier de scripts JavaScript globaux pour l'ensemble de l'application.
//
// 🌟 Fonctionnalité :
//     - Gère l'affichage dynamique de la modale de confirmation universelle.
//     - Fournit des fonctions de callback génériques (ex: soumission de formulaire).
//     - Gère l'initialisation automatique et globale des tables DataTables.
//     - Fournit un système de validation universel pour les modales de sélection.
//
// 🛠️ Modifications :
//     2025-07-24 : Utilisation de la variable de configuration globale 'drdConfig'
//                  pour le message d'erreur Toastr afin de supporter la localisation.
//     2025-07-24 : Ajout de la validation universelle pour les modales de sélection.
//     2025-07-24 : Mise à jour de l'en-tête pour conformité avec les standards.
// ============================================================================

// ============================================================================
// FONCTION DE CALLBACK UNIVERSELLE
// ----------------------------------------------------------------------------
// Cette fonction est appelée par le script de la modale pour soumettre un
// formulaire par son ID.
// ============================================================================
function submitFormById(formId) {
    const form = document.getElementById(formId);
    if (form) {
        form.submit();
    } else {
        console.error(`Le formulaire avec l'ID '${formId}' est introuvable.`);
        if (typeof toastr !== 'undefined' && typeof drdConfig !== 'undefined') {
            toastr.error(drdConfig.genericErrorMessage || "An unexpected error occurred.");
        }
    }
}


document.addEventListener('DOMContentLoaded', function () {

    // ============================================================================
    // GESTION DE LA MODALE DE CONFIRMATION
    // ----------------------------------------------------------------------------
    const confirmationModalEl = document.getElementById('confirmationModal');
    if (confirmationModalEl) {
        confirmationModalEl.addEventListener('show.bs.modal', function (event) {
            const button = event.relatedTarget; // Le bouton qui a cliqué

            const title = button.getAttribute('data-modal-title');
            const message = button.getAttribute('data-modal-message');
            const confirmText = button.getAttribute('data-modal-confirm-text');
            const successCallbackName = button.getAttribute('data-modal-success-callback');

            const modalTitle = confirmationModalEl.querySelector('.modal-title');
            const modalBody = confirmationModalEl.querySelector('.modal-body p');
            const confirmButton = confirmationModalEl.querySelector('[data-modal-confirm]');

            if (modalTitle) modalTitle.textContent = title || 'Confirmation';
            if (modalBody) modalBody.textContent = message || 'Êtes-vous sûr?';

            if (confirmButton) {
                const icon = confirmButton.querySelector('i');
                const newText = confirmText || 'Accepter';
                confirmButton.textContent = '';
                if (icon) confirmButton.appendChild(icon);
                confirmButton.append(` ${newText}`);
            }

            const newConfirmButton = confirmButton.cloneNode(true);
            confirmButton.parentNode.replaceChild(newConfirmButton, confirmButton);

            newConfirmButton.addEventListener('click', function () {
                if (typeof window[successCallbackName] === 'function') {
                    const itemId = button.getAttribute('data-item-id');
                    const additionalItemId = button.getAttribute('data-additional-item-id');

                    if (additionalItemId) {
                        window[successCallbackName](itemId, additionalItemId);
                    } else {
                        window[successCallbackName](itemId);
                    }
                }
                const modalInstance = bootstrap.Modal.getInstance(confirmationModalEl);
                modalInstance.hide();
            });
        });
    }

    // ============================================================================
    // INITIALISATION GLOBALE DE DATATABLES
    // ----------------------------------------------------------------------------
    if (typeof jQuery !== 'undefined' && jQuery.fn.DataTable && typeof drdConfig !== 'undefined') {
        const languageUrl = drdConfig.dataTablesLangUrlBase + (drdConfig.culture.startsWith('fr') ? 'fr-FR.json' : 'en-GB.json');

        jQuery('.datatable-bilingual').DataTable({
            language: {
                url: languageUrl
            },
            paging: true,
            pageLength: 10,
            autoWidth: true,
            responsive: true,


            columnDefs: [
                // Désactiver l'ordonnancement UNIQUEMENT sur la dernière colonne (Actions)
                { targets: -1, orderable: false }
                // Les autres colonnes (y compris Activité) sont triables par défaut.
            ], 
            "initComplete": function (settings, json) {
                // Recalculer immédiatement après le rendu initial
                this.api().columns.adjust().draw();
                const tableApi = this.api();

                // Écouteur pour le redimensionnement de la fenêtre (correction de réactivité)
                $(window).on('resize', function () {
                    tableApi.columns.adjust().draw(false); 
                });            } 
        }); 
    }
    // ============================================================================
    // VALIDATION UNIVERSELLE POUR LES MODALES DE SÉLECTION
    // ----------------------------------------------------------------------------
    const selectionForms = document.querySelectorAll('.needs-selection-validation');

    selectionForms.forEach(form => {
        const dropdown = form.querySelector('.selection-dropdown');
        const errorDiv = form.querySelector('.selection-error-message');

        if (dropdown && errorDiv) {
            form.addEventListener('submit', function (event) {
                if (dropdown.value === "") {
                    event.preventDefault(); // Bloque la soumission
                    errorDiv.style.display = 'block'; // Affiche l'erreur
                } else {
                    errorDiv.style.display = 'none'; // Cache l'erreur
                }
            });

            dropdown.addEventListener('change', function () {
                if (dropdown.value !== "") {
                    errorDiv.style.display = 'none';
                }
            });
        }
    });

});
