$(document).ready(function () {
    let tagIndex = 0;
    $("#add-tag-button").click(function () {
        var tagValue = $("#tag-input").val().trim();
        if (tagValue) {
            addTag(tagValue);
            $("#tag-input").val(''); 
        }
    });


    function addTag(tag)
    {
        let tagExists = false;

        $("#selected-tags li").each(function () {
            const inputValue = $(this).find('input[type="hidden"]').val().trim();
            if (inputValue === tag.trim()) {
                tagExists = true; 
                return false; 
            }
        });


        if (!tagExists)
        {
            $("#selected-tags").append(
                '<li class="list-group-item">' +
                '<input type="hidden" name="Tags[' + tagIndex + '].Name" value="' + tag + '" />' +
                tag +
                ' <button type="button" class="btn btn-danger btn-sm remove-tag">x</button>' +
                '</li>'
            );
            tagIndex++;
        }
        else
        {
            alert("You can't input the same tag name");
        }

    }

    $(document).on('click', '.remove-tag', function () {
        $(this).parent().remove(); 
    });
});
	