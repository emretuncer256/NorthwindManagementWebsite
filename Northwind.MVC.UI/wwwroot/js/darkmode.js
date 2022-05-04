document.addEventListener('DOMContentLoaded', function () {
    var body = document.getElementsByTagName("BODY")[0];
    var icon = document.getElementById("dark-icon");
    if (localStorage.getItem("theme") == "dark") {
        changeDarkMode(true, body, icon)
    } else if (localStorage.getItem("theme") == "light") {
        changeDarkMode(false, body, icon)
    } else {
        changeDarkMode(false, body, icon)
    }
}, false);
function darkMode() {
    var body = document.getElementsByTagName("BODY")[0];
    var icon = document.getElementById("dark-icon");
    if (body.classList.contains("dark-mode")) {
        changeDarkMode(false, body, icon)
        localStorage.setItem("theme", "light");
    } else {
        changeDarkMode(true, body, icon)
        localStorage.setItem("theme", "dark");
    }
}
function changeDarkMode(activate, body, icon) {
    if (activate) {
        body.classList.add("dark-mode");
        icon.classList.add("fa-sun");
        icon.classList.remove("fa-moon");
    } else {
        body.classList.remove("dark-mode");
        icon.classList.add("fa-moon");
        icon.classList.remove("fa-sun");
    }
}