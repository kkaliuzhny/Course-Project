$(document).ready(function () {
    let tagIndex = 0;
    $("#add-tag-button").click(function () {
        var tagValue = $("#tag-input").val().trim();
        if (tagValue) {
            addTag(tagValue);
            $("#tag-input").val(''); // Clear the input
        }
    });


    function addTag(tag)
    {
        let tagExists = false;

        // Check if the tag is already in the list by comparing input values
        $("#selected-tags li").each(function () {
            // Get the value of the hidden input in the current <li>
            const inputValue = $(this).find('input[type="hidden"]').val().trim();

            // Compare trimmed input value with the new tag
            if (inputValue === tag.trim()) {
                tagExists = true; // Tag already exists
                return false; // Exit loop early
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
        $(this).parent().remove(); // Remove the tag from the list
    });
});
	