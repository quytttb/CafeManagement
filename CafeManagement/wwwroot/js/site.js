// wwwroot/js/site.js

// Function to set theme
function setTheme(themeName) {
    document.documentElement.setAttribute('data-theme', themeName);
    localStorage.setItem('theme', themeName);
}

// Function to toggle theme
function toggleTheme() {
    const themeToggleIcon = document.querySelector('.theme-toggle i'); // Chọn icon trong button

    if (localStorage.getItem('theme') === 'dark') {
        setTheme('light');
        themeToggleIcon.classList.remove('fa-moon');
        themeToggleIcon.classList.add('fa-sun'); // Đổi sang icon mặt trời
    } else {
        setTheme('dark');
        themeToggleIcon.classList.remove('fa-sun');
        themeToggleIcon.classList.add('fa-moon'); // Đổi về icon mặt trăng
    }
}


// Function to get system theme preference
function getSystemThemePreference() {
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
}

// Initialize theme
function initializeTheme() {
    // First check for saved theme preference
    const savedTheme = localStorage.getItem('theme');

    if (savedTheme) {
        // If we have a saved preference, use it
        setTheme(savedTheme);
    } else {
        // Otherwise, use system preference
        setTheme(getSystemThemePreference());
    }

    // Listen for system theme changes
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', e => {
        if (!localStorage.getItem('theme')) {
            // Only auto-switch if user hasn't manually set a preference
            setTheme(e.matches ? 'dark' : 'light');
        }
    });
}

// Run initialization on page load
document.addEventListener('DOMContentLoaded', initializeTheme);