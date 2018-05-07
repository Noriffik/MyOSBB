// Write your JavaScript code.
function userData(index) {
    switch (index) {
        case 0:
            page0();
            break;
        case 1:
            page1();
            break;
    }
}

function page0() {
    $.getJSON("/api/AnnouncementsApi").done( function (data) {
        var userhtml = '<table class="table"><thead><tr><th>Name</th><th>Content</th><th>Date</th><th>UserId</th><th></th></tr></thead><tbody>';
        data.forEach(function (item) {
            console.log(item);
            userhtml += '<tr><td>' + item.name + '</td><td>' + item.content + '</td><td>' + item.date + '</td><td>' + item.userId + '</td></tr>';
        });
            userhtml += '</tbody></table>';

        $("#userdatadisplay").html(userhtml)
    });
}

function page1() {
    var userhtml = "<div>Test display 1</div>";
    $("#userdatadisplay").html(userhtml)
}

//$.post('script.php', data, function (response) {
//    // Do something with the request
//}, 'json');