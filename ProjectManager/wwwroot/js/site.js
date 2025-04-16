let quill;

// Initiera Quill när sidan laddats
document.addEventListener("DOMContentLoaded", () => {
    quill = new Quill('#editor', {
        theme: 'snow'
    });

    document.getElementById("projectForm").addEventListener("submit", function (e) {
        // Validera fält
        const name = document.getElementById("projectName").value.trim();
        const client = document.getElementById("clientName").value.trim();
        const budget = document.getElementById("budget").value.trim();
        const startDate = document.getElementById("startDate").value;
        const endDate = document.getElementById("endDate").value;

        let errors = [];

        if (name === "") errors.push("Project Name is required.");
        if (client === "") errors.push("Client Name is required.");
        if (budget === "" || isNaN(budget) || Number(budget) < 0) errors.push("Valid Budget is required.");
        if (startDate && endDate && new Date(endDate) < new Date(startDate)) errors.push("End Date cannot be before Start Date.");

        if (errors.length > 0) {
            e.preventDefault();
            alert(errors.join("\n"));
            return;
        }

        // Om valideringen passerar – lägg in description från Quill
        const description = quill.root.innerHTML;
        document.getElementById("hiddenDescription").value = description;

        // Modal stängs ej direkt, utan vänta på redirect
    });


    // Lägg till event delegation för dropdown-knappar på korten
    document.addEventListener("click", function (event) {
        const isDotButton = event.target.closest('.dot-button');
        const isInsideDropdown = event.target.closest('.card-dropdown');

        // Om du klickar på en dot-button
        if (isDotButton) {
            event.stopPropagation(); // Förhindra att andra klick stänger den direkt
            toggleCardDropdown(isDotButton);
            return;
        }

        // Om du klickar utanför dropdown, stäng alla
        if (!isInsideDropdown) {
            document.querySelectorAll('.card-dropdown').forEach(drop => drop.classList.remove("show"));
        }
    });

    window.addEventListener("click", function (event) {
        const avatar = document.querySelector(".avatar");
        const dropdown = document.getElementById("dropdownMenu");
        if (!avatar.contains(event.target) && !dropdown.contains(event.target)) {
            dropdown.classList.remove("show");
        }

        if (!event.target.closest('.dropdown-container')) {
            document.querySelectorAll('.card-dropdown').forEach(el => el.classList.remove('show'));
        }
    });
});

// Visa modal för nytt projekt
function openModal() {
    resetForm();
    document.getElementById("modalTitle").textContent = "Add Project";
    document.querySelector(".create-btn").textContent = "Create";
    document.getElementById("modalOverlay").classList.add("active");
}

// Visa modal för redigering
function editProject(button) {
    const card = button.closest('.project-card');
    const project = JSON.parse(card.dataset.project);

    document.getElementById("modalTitle").textContent = "Edit Project";
    document.querySelector(".create-btn").textContent = "Save";

    document.getElementById("projectId").value = project.Id;
    document.getElementById("projectName").value = project.ProjectName;
    document.getElementById("clientName").value = project.ClientName;
    quill.root.innerHTML = project.Description || "";
    document.getElementById("startDate").value = project.StartDate?.split("T")[0] || "";
    document.getElementById("endDate").value = project.EndDate?.split("T")[0] || "";
    document.getElementById("budget").value = project.Budget;
    document.getElementById("status").value = project.Status;

    document.getElementById("modalOverlay").classList.add("active");
}

// Stäng modal
function closeModal() {
    document.getElementById("modalOverlay").classList.remove("active");
    resetForm();
}

// Återställ formulärfält
function resetForm() {
    document.getElementById("projectId").value = "0";
    document.getElementById("projectName").value = "";
    document.getElementById("clientName").value = "";
    quill.root.innerHTML = "";
    document.getElementById("startDate").value = "";
    document.getElementById("endDate").value = "";
    document.getElementById("budget").value = "";
    document.getElementById("status").value = "0";
    document.querySelector(".create-btn").textContent = "Create";
    document.getElementById("modalTitle").textContent = "Add Project";
}

// Visa/dölj dropdown för kort
function toggleCardDropdown(button) {
    const dropdown = button.nextElementSibling;
    const isVisible = dropdown.classList.contains("show");

    // Stäng alla först
    document.querySelectorAll('.card-dropdown').forEach(el => el.classList.remove('show'));

    if (!isVisible) {
        dropdown.classList.add("show");
    }
}


// Visa/dölj profilmeny
function toggleDropdown() {
    document.getElementById("dropdownMenu").classList.toggle("show");
}
