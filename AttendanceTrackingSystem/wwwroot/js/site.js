$(document).ready(function () {
    // Toggle sidebar when the button is clicked
    $('.navbar--toggler').click(function () {
        console.log("click")
        $('#sidebarCollapse').toggleClass('show-nav');
        // Animate the sidebar's display
        $('#sidebarCollapse').slideToggle();
    });
});
