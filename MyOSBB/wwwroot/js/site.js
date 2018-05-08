// Write your JavaScript code.

var pageShow;
function userData(index) {
    switch (index) {
        case 0:
            pageShow = page0;
            pageShow();
            break;
        case 1:
            pageShow = page1;
            pageShow();
            break;
    }
}

function page0() {
    $.getJSON("/api/AnnouncementsApi").done( function (data) {
        var userhtml = '<div class="bg-primary text-white"><h4>Announcements</h4></div><br /><table id="userDataTable" class="table table-sm table-bordered"><thead><tr><th onclick="sortTable(0)">Title <span id="userDataSpan0"></span></th><th onclick="sortTable(1)">Content <span id="userDataSpan1"></span></th><th onclick="sortTable(2)">Date <span id="userDataSpan2"></span></th><th onclick="sortTable(3)">UserId <span id="userDataSpan3"></span></th></tr></thead><tbody>';
        data.forEach(function (item) {
            console.log(item);
            userhtml += '<tr><td>' + item.title + '</td><td>' + item.content + '</td><td>' + item.date + '</td><td>' + item.userId + '</td></tr>';
        });
            userhtml += '</tbody></table>';

        $("#userdatadisplay").html(userhtml);
    });
}

function page1() {
    $.getJSON("/api/AnnouncementsApi").done(function (data) {
        var userhtml = '<div class="bg-primary text-white"><h4>Contributions</h4></div><br /><table id="userDataTable" class="table table-sm table-bordered"><thead><tr><th onclick="sortTable(0)">Title <span id="userDataSpan0"></span></th><th onclick="sortTable(1)">Content <span id="userDataSpan1"></span></th><th onclick="sortTable(2)">Date <span id="userDataSpan2"></span></th><th onclick="sortTable(3)">UserId <span id="userDataSpan3"></span></th></tr></thead><tbody>';
        data.forEach(function (item) {
            console.log(item);
            userhtml += '<tr><td>' + item.title + '</td><td>' + item.content + '</td><td>' + item.date + '</td><td>' + item.userId + '</td></tr>';
        });
        userhtml += '</tbody></table>';

        $("#userdatadisplay").html(userhtml);
    });
}

//$.post('script.php', data, function (response) {
//    // Do something with the request
//}, 'json');

function sortTable(index) {
    var spanClass = document.getElementById("userDataSpan" + index).className;
    //alert(spanClass);
    for (i = 0; i < 7; i++) {
        if (i !== index) {
            $("#userDataSpan" + i).removeAttr("class");
        }
    }
    var sort = "0";
    if (spanClass === "") {
        $("#userDataSpan" + index).addClass("glyphicon glyphicon-triangle-bottom");
        sort = "1";
    } else {
        if (spanClass === "glyphicon glyphicon-triangle-bottom") {
            $("#userDataSpan" + index).removeClass("glyphicon glyphicon-triangle-bottom");
            $("#userDataSpan" + index).addClass("glyphicon glyphicon-triangle-top");
            sort = "2";
        } else {
            if (spanClass === "glyphicon glyphicon-triangle-top") {
                $("#userDataSpan" + index).removeClass("glyphicon glyphicon-triangle-top");
                sort = "0";
            }
        }
    }

    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("userDataTable");
    switching = true;
    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {
        // Start by saying: no switching is done:
        switching = false;
        rows = table.getElementsByTagName("TR");
        /* Loop through all table rows (except the
        first, which contains table headers): */
        for (i = 1; i < rows.length - 1; i++) {
            // Start by saying there should be no switching:
            shouldSwitch = false;
            /* Get the two elements you want to compare,
            one from current row and one from the next: */
            x = rows[i].getElementsByTagName("TD")[index];
            y = rows[i + 1].getElementsByTagName("TD")[index];
            // Check if the two rows should switch place:
            if (sort === "1") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    // I so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            }
            else {
                if (sort === "2") {
                    if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                        // I so, mark as a switch and break the loop:
                        shouldSwitch = true;
                        break;
                    }
                }
                else {
                    pageShow();
                }
            }
        }
        if (shouldSwitch) {
            /* If a switch has been marked, make the switch
            and mark that a switch has been done: */
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }        
    }
}