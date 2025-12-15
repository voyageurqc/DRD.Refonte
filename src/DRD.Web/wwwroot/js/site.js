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
//     - Gère l'affichage dynamique de la modale système de métadonnées (AJAX).
//
// 🛠️ Modifications :
//     2025-12-14 : Ajout gestion globale de la modale système de métadonnées (chargement AJAX).
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


// ============================================================================
// GESTION GLOBALE – MODALE MÉTADONNÉES
// ----------------------------------------------------------------------------
// Gère le chargement AJAX et l'affichage de la modale système de métadonnées
// pour toutes les DataTables via le bouton .metadata-trigger
// ============================================================================
function initializeMetadataModal() {

    document.addEventListener('click', function (event) {

        const trigger = event.target.closest('.metadata-trigger');
        if (!trigger) return;

        event.preventDefault();

        const entityId = trigger.getAttribute('data-entity-id');
        if (!entityId) return;

        fetch(`/CdSet/GetMetadata?entityId=${encodeURIComponent(entityId)}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Erreur chargement métadonnées');
                }
                return response.text();
            })
            .then(html => {

                // Supprimer toute modale existante
                const existingModal = document.getElementById('systemMetadataModal');
                if (existingModal) {
                    const existingInstance = bootstrap.Modal.getInstance(existingModal);
                    if (existingInstance) {
                        existingInstance.hide();
                        existingInstance.dispose();
                    }

                    existingModal.remove();

                    document.body.classList.remove('modal-open');
                    document.body.style.removeProperty('padding-right');
                    document.querySelectorAll('.modal-backdrop')
                        .forEach(el => el.remove());
                }

                // Injecter la modale reçue
                document.body.insertAdjacentHTML('beforeend', html);

                // Ouvrir la modale Bootstrap
                const modalEl = document.getElementById('systemMetadataModal');
                const modal = new bootstrap.Modal(modalEl, {
                    backdrop: true,
                    keyboard: true,
                    focus: true
                });
                modal.show();
                modalEl.addEventListener('hidden.bs.modal', function () {
                    const instance = bootstrap.Modal.getInstance(modalEl);
                    if (instance) {
                        instance.dispose();
                    }

                    modalEl.remove();

                    document.body.classList.remove('modal-open');
                    document.body.style.removeProperty('padding-right');
                    document.querySelectorAll('.modal-backdrop')
                        .forEach(el => el.remove());
                }, { once: true });

            })
            .catch(error => {
                console.error(error);
                if (typeof toastr !== 'undefined' && typeof drdConfig !== 'undefined') {
                    toastr.error(drdConfig.genericErrorMessage || 'Erreur');
                }
            });
    });
}


// ============================================================================
// INITIALISATION GLOBALE
// ============================================================================
document.addEventListener('DOMContentLoaded', function () {

    // ============================================================================
    // GESTION DE LA MODALE DE CONFIRMATION
    // ----------------------------------------------------------------------------
    const confirmationModalEl = document.getElementById('confirmationModal');
    if (confirmationModalEl) {
        confirmationModalEl.addEventListener('show.bs.modal', function (event) {
            const button = event.relatedTarget;

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
                console.log("CONFIRM CLICK");
                console.log("Callback:", successCallbackName);

                if (typeof window[successCallbackName] === 'function') {
                    const itemId = button.getAttribute('data-item-id');
                    console.log("ItemId:", itemId);


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
        const languageUrl =
            drdConfig.dataTablesLangUrlBase +
            (drdConfig.culture.startsWith('fr') ? 'fr-FR.json' : 'en-GB.json');

        jQuery('.datatable-bilingual').DataTable({
            language: { url: languageUrl },
            paging: true,
            pageLength: 10,
            autoWidth: true,
            responsive: true,
            columnDefs: [
                { targets: -1, orderable: false }
            ],
            initComplete: function () {
                const tableApi = this.api();
                tableApi.columns.adjust().draw();
                $(window).on('resize', function () {
                    tableApi.columns.adjust().draw(false);
                });
            }
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
                    event.preventDefault();
                    errorDiv.style.display = 'block';
                } else {
                    errorDiv.style.display = 'none';
                }
            });

            dropdown.addEventListener('change', function () {
                if (dropdown.value !== "") {
                    errorDiv.style.display = 'none';
                }
            });
        }
    });


    // ============================================================================
    // INITIALISATION MÉTADONNÉES
    // ----------------------------------------------------------------------------
    initializeMetadataModal();

});
