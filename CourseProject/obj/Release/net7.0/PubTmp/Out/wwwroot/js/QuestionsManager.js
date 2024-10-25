$(document).ready(function () {
    let questionIndex = 0;
    function addQuestion() {
        const template = $('#question-template').html();
        const questionHtml = template.replace(/{{index}}/g, questionIndex);
        $('#questions-list').append(questionHtml);
        questionIndex++;
        refreshSortable(); 
    }


    function refreshSortable() {
        $('#questions-list').sortable({
            update: function (event, ui) {
             
                $('#questions-list .question-item').each(function (index, element) {
                    $(element).find('.order-input').val(index);
                });
            }
        });
    }
    $('#add-question').click(function () {
        addQuestion();
    });
    
    $(document).on('click', '.remove-question', function () {
        $(this).closest('.question-item').remove();
        $('#questions-list').sortable('refresh'); 
    });

    refreshSortable();
});