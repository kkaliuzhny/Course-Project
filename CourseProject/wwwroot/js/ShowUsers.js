document.getElementById("isPublicCheckbox").addEventListener("change", function () {
    var userSelection = document.getElementById("usersSelection");
    userSelection.style.display = this.checked ? "none" : "block";
});