document.addEventListener('DOMContentLoaded', function () {
    const searchParams = decodeURIComponent(window.location.search);
    const urlParams = new URLSearchParams(searchParams);
    console.log(window.location.search);
    if (urlParams.has('scrollToSubscription')) {
       
        console.log(urlParams.has('scrollToSubscription'));
        var subscriptionElement = document.getElementById('subscription');
        if (subscriptionElement) {
            subscriptionElement.scrollIntoView();
        }
    }
});
