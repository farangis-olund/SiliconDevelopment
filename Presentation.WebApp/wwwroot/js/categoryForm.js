$(document).ready(function () {
    $('#searchInput').on('input', function () {
        
    });

    $('#searchInput').on('keypress', function (e) {
       if (e.which === 13) {
            $('#categoryForm').submit();
       }
    });

    $('#categorySelect').change(function () {
        $('#categoryForm').submit();
    });
});

