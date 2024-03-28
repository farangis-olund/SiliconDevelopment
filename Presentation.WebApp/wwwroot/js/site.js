document.addEventListener('DOMContentLoaded', function () {
    const mobileButton = document.querySelector('.btn-mobile');
    const mobileMenu = document.getElementById('menu');
    const mobileAccountButtons = document.querySelector('.account-buttons');
    const menuOverlay = document.getElementById('menu-overlay');

    const hideMenuAndOverlay = () => {
        mobileMenu.classList.remove('show-menu');
        mobileAccountButtons.classList.remove('show-buttons');
        menuOverlay.classList.remove('show-overlay');
    };

    mobileButton.addEventListener('click', function () {
        mobileMenu.classList.toggle('show-menu');
        mobileAccountButtons.classList.toggle('show-buttons');
        menuOverlay.classList.toggle('show-overlay');
    });

    const menuLinks = document.querySelectorAll('.menu-link');
    menuLinks.forEach(link => {
        link.addEventListener('click', hideMenuAndOverlay);
    });

    const accountButtons = document.querySelectorAll('.account-buttons a');
    accountButtons.forEach(button => {
        button.addEventListener('click', hideMenuAndOverlay);
    });

    const checkScreenSize = () => {
        if (window.innerWidth >= 1200) {
            hideMenuAndOverlay();
        } else {
           
        }
    };

    window.addEventListener('resize', checkScreenSize);
    checkScreenSize();
});

document.addEventListener('DOMContentLoaded', function () {
    let sm = document.querySelector('#switch-mode');
    
    sm.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light";
        fetch(`/settings/changetheme?theme=${theme}`)
            .then(res => {
                if (res.ok) {
                    window.location.reload();
                } else {
                    console.error('Failed to change theme:', res.statusText);
                }
            })
            .catch(error => {
                console.error('Error changing theme:', error);
            });
    });
});