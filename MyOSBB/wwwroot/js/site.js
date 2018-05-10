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

// Announcements
function page0() {
    $.getJSON("/api/AnnouncementsApi").done(function (data) {
        var htmlDiv = '<h4>Announcements</h4>';
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

// Contributions
function page1() { 
    $.getJSON("/api/ContributionsApi").done(function (data) {
        var htmlDiv = '<h4>Contributions</h4>';
        var htmlThead = "";
        var htmlTbody = "";

        htmlThead += '<tr>';
        htmlThead += '<th>Flat number</span></th>';
        htmlThead += '<th>User</th>';
        htmlThead += '<th>Payment</span></th>';
        htmlThead += '<th>Payment date</span></th>';
        htmlThead += '<th>For period</span></th>';
        htmlThead += '</tr>';

        data.forEach(function (item) {
            console.log(item);
            htmlTbody += '<tr><td>' + item.flatnumber + '</td><td>' + item.user + '</td><td>' + item.payment + '</td><td>' + item.paymentday + '</td><td>' + item.forperiod + '</td></tr>';
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