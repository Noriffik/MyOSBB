// Write your JavaScript code.
jQuery.loadScript = function (url, callback) {
    jQuery.ajax({
        url: url,
        dataType: 'script',
        success: callback,
        async: true
    });
}

$.loadScript('https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js', function () {
    //Stuff to do after someScript has loaded
});
$.loadScript('https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap.min.js', function () {
    //Stuff to do after someScript has loaded
});

//$('#userDataTable').DataTable({
//    "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
//});
var table;
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
        case 2:
            pageShow = page2;
            pageShow();
            break;
        case 3:
            pageShow = page3;
            pageShow();
            break;
        case 4:
            pageShow = page4;
            pageShow();
            break;
        case 5:
            pageShow = page5;
            pageShow();
            break;
        case 6:
            pageShow = page6;
            pageShow();
            break;
    }
}

function page0() {
    $.getJSON("/api/AnnouncementsApi").done(function (data) {
        var htmlDiv = '<h4>Announcements</h4>';
        var htmlThead = "";
        var htmlTbody = "";

        htmlThead += '<tr>';
        //htmlThead += '<th onclick="sortTable(0)">Title <span id="userDataSpan0"></span></th>';
        //htmlThead += '<th onclick="sortTable(1)">Content <span id="userDataSpan1"></span></th>';
        //htmlThead += '<th onclick="sortTable(2)">Date <span id="userDataSpan2"></span></th>';
        //htmlThead += '<th onclick="sortTable(3)">UserId <span id="userDataSpan3"></span></th>';
        htmlThead += '<th>Title</span></th>';
        htmlThead += '<th>Content</th>';
        htmlThead += '<th>Date</span></th>';
        htmlThead += '<th>UserId</span></th>';
        htmlThead += '</tr>';
        
        data.forEach(function (item) {
            console.log(item);
            htmlTbody += '<tr><td>' + item.title + '</td><td>' + item.content + '</td><td>' + item.date + '</td><td>' + item.userId + '</td></tr>';
        });

        $(".bg-primary").html(htmlDiv);
        $("#userDataTable thead").html(htmlThead);
        $("#userDataTable tbody").html(htmlTbody);
        
        //$('#userDataTable').DataTable({
        //        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
        //});

        if ($.fn.dataTable.isDataTable('#userDataTable')) {
            table = $('#userDataTable').DataTable();
        }
        else {
            table = $('#userDataTable').DataTable({
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
            });
        }
        table
            .search('')
            .columns().search('')
            .draw();
    });
}

function page1() {
    $.getJSON("/api/AnnouncementsApi").done(function (data) {
        var htmlDiv = '<h4>Contributions</h4>';
        var htmlThead = "";
        var htmlTbody = "";

        htmlThead += '<tr>';
        htmlThead += '<th>Title</span></th>';
        htmlThead += '<th>Content</th>';
        htmlThead += '<th>Date</span></th>';
        htmlThead += '<th>UserId</span></th>';
        htmlThead += '</tr>';

        data.forEach(function (item) {
            console.log(item);
            htmlTbody += '<tr><td>' + item.title + '</td><td>' + item.content + '</td><td>' + item.date + '</td><td>' + item.userId + '</td></tr>';
        });

        $(".bg-primary").html(htmlDiv);
        $("#userDataTable thead").html(htmlThead);
        $("#userDataTable tbody").html(htmlTbody);

        if ($.fn.dataTable.isDataTable('#userDataTable')) {
            table = $('#userDataTable').DataTable();
        }
        else {
            table = $('#userDataTable').DataTable({
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
            });
        }
        table
            .search('')
            .columns().search('')
            .draw();
    });
}

//$.post('script.php', data, function (response) {
//    // Do something with the request
//}, 'json');

//function sortTable(index) {
//    var spanClass = document.getElementById("userDataSpan" + index).className;
//    //alert(spanClass);
//    for (i = 0; i < 7; i++) {
//        if (i !== index) {
//            $("#userDataSpan" + i).removeAttr("class");
//        }
//    }
//    var sort = "0";
//    if (spanClass === "") {
//        $("#userDataSpan" + index).addClass("glyphicon glyphicon-triangle-bottom");
//        sort = "1";
//    } else {
//        if (spanClass === "glyphicon glyphicon-triangle-bottom") {
//            $("#userDataSpan" + index).removeClass("glyphicon glyphicon-triangle-bottom");
//            $("#userDataSpan" + index).addClass("glyphicon glyphicon-triangle-top");
//            sort = "2";
//        } else {
//            if (spanClass === "glyphicon glyphicon-triangle-top") {
//                $("#userDataSpan" + index).removeClass("glyphicon glyphicon-triangle-top");
//                sort = "0";
//            }
//        }
//    }

//    var table, rows, switching, i, x, y, shouldSwitch;
//    table = document.getElementById("userDataTable");
//    switching = true;
//    /* Make a loop that will continue until
//    no switching has been done: */
//    while (switching) {
//        // Start by saying: no switching is done:
//        switching = false;
//        rows = table.getElementsByTagName("TR");
//        /* Loop through all table rows (except the
//        first, which contains table headers): */
//        for (i = 1; i < rows.length - 1; i++) {
//            // Start by saying there should be no switching:
//            shouldSwitch = false;
//            /* Get the two elements you want to compare,
//            one from current row and one from the next: */
//            x = rows[i].getElementsByTagName("TD")[index];
//            y = rows[i + 1].getElementsByTagName("TD")[index];
//            // Check if the two rows should switch place:
//            if (sort === "1") {
//                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
//                    // I so, mark as a switch and break the loop:
//                    shouldSwitch = true;
//                    break;
//                }
//            }
//            else {
//                if (sort === "2") {
//                    if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
//                        // I so, mark as a switch and break the loop:
//                        shouldSwitch = true;
//                        break;
//                    }
//                }
//                else {
//                    pageShow();
//                }
//            }
//        }
//        if (shouldSwitch) {
//            /* If a switch has been marked, make the switch
//            and mark that a switch has been done: */
//            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
//            switching = true;
//        }        
//    }
//}