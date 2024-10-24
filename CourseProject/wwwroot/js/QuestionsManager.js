$(document).ready(function () {
    let questionIndex = 0;
    function addQuestion() {
        const template = $('#question-template').html();
        const questionHtml = template.replace(/{{index}}/g, questionIndex);
        $('#questions-list').append(questionHtml);
        questionIndex++;
        refreshSortable(); // Инициализация перетаскивания после добавления нового элемента
    }


    function refreshSortable() {
        $('#questions-list').sortable({

            update: function (event, ui) {
                // Обновляем порядок (индексы) вопросов после сортировки
                $('#questions-list .question-item').each(function (index, element) {
                    $(element).find('.order-input').val(index);
                });
            }
        });
    }

    // Добавление вопроса по нажатию на кнопку
    $('#add-question').click(function () {
        addQuestion();
    });

    // Удаление вопроса
    $(document).on('click', '.remove-question', function () {
        $(this).closest('.question-item').remove();
        $('#questions-list').sortable('refresh'); // Обновляем sortable после удаления
    });

    refreshSortable();
});