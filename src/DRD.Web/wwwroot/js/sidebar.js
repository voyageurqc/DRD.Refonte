// ===============================================================
// DRD Sidebar Intelligent v10
// Gère :
//  - Ouverture automatique de la bonne section
//  - Surlignage de l'item actif
//  - Mémoire du menu via localStorage
// ===============================================================

document.addEventListener("DOMContentLoaded", function () {

    const currentUrl = window.location.pathname.toLowerCase();

    // -----------------------------------------------------------
    // 1. Détection de l'item actif
    // -----------------------------------------------------------
    document.querySelectorAll(".sidebar-item").forEach(link => {

        const href = link.getAttribute("href")?.toLowerCase();

        if (href && currentUrl === href) {
            link.classList.add("active");

            // Retrouver la section parent
            const section = link.closest(".submenu");
            if (section) {
                const parentId = section.getAttribute("id");
                localStorage.setItem("sidebar_last_open", parentId);
            }
        }
    });

    // -----------------------------------------------------------
    // 2. Réouverture automatique du dernier menu
    // -----------------------------------------------------------
    const lastOpen = localStorage.getItem("sidebar_last_open");
    if (lastOpen) {
        const elem = document.getElementById(lastOpen);
        if (elem) {
            new bootstrap.Collapse(elem, { toggle: true });
        }
    }

    // -----------------------------------------------------------
    // 3. Lors du clic sur une section : mémoriser l'ouverture
    // -----------------------------------------------------------
    document.querySelectorAll(".sidebar-section").forEach(section => {
        section.addEventListener("click", function () {
            const target = section.getAttribute("href")?.replace("#", "");
            if (target) {
                localStorage.setItem("sidebar_last_open", target);
            }
        });
    });

});
