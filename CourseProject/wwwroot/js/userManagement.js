var users = []; 

$(document).on('click', '.remove-user', function () { 
    var index = $(this).parent().index(); 
    users.splice(index, 1); 
    updateUserList(); 
}); 

$(document).ready(function () { 
    $('#add-user').click(function () {
        var userName = $('#txtUsers').val();
        var userEmail = "";
        if (userName !== "")
        {
            $.ajax({
                url: '/Template/CheckUserExistence',
                type: 'POST',
                data: { username: userName },
                success: function (exists) {
                    if (exists) {
                        users.push({ name: userName, email: userEmail });
                        updateUserList();
                        $('#txtUsers').val('');
                    } else {
                        alert("User does not exist in the database.");
                    }
                },
                error: function () {
                    alert("An error occurred while checking for the user.");
                } 
            });
        }
        else
        {
            alert("Please enter a username.");
        }
    }); 
}); 

$('#create-template').click(function () { 
    var selectedUsers = [];
    $('#usersList li').each(function () {
        selectedUsers.push($(this).text());
    });

   
    $('<input>').attr({
        type: 'hidden',
        name: 'Users',
        value: JSON.stringify(selectedUsers)  
    }).appendTo('#templateForm');
}); 

$('#sortOptions').change(function () { 
    updateUserList();
}); 

function updateUserList() { 
    var sortBy = $('#sortOptions').val();

    users.sort(function (a, b) {
        if (sortBy === 'name') {
            return a.name.localeCompare(b.name);
        } else {
            return a.email.localeCompare(b.email);
        }
    });

    $('#usersList').empty();
    users.forEach(function (user) {
        $('#usersList').append('<li>' + user.name + ' (' + user.email + ') <button class="remove-user btn btn-danger btn-sm">Remove</button></li>');
    });
} 