document.getElementById('selectAll').addEventListener('change', function() {
    var checkboxes = document.querySelectorAll('input[name="selectedItems"]');
    checkboxes.forEach(function(checkbox) {
        checkbox.checked = document.getElementById('selectAll').checked;
    });
});