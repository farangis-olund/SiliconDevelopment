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
});


const checkScreenSize = () => {
    if (window.innerWidth >= 1200) {
        hideMenuAndOverlay(); 
    } else {
        //mobileMenu.classList.remove('show-menu');
        //if (!mobileAccountButtons.classList.contains('hide')) {
        //    mobileAccountButtons.classList.add('hide');
        //}
    }
};

window.addEventListener('resize', checkScreenSize);
checkScreenSize();