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
        htmlThead += '<th>User</span></th>';
        htmlThead += '</tr>';
        
        data.forEach(function (item) {
            console.log(item);
            htmlTbody += '<tr>';
            htmlTbody += '<td>' + item.title + '</td>';
            htmlTbody += '<td>' + item.content + '</td>';
            htmlTbody += '<td>' + item.date + '</td>';
            htmlTbody += '<td>' + item.user.userName + '</td>';
            htmlTbody += '</tr>';
        });

        $(".bg-primary").html(htmlDiv);
        $("#userDataTable thead").html(htmlThead);
        $("#userDataTable tbody").html(htmlTbody);
        
        if ($.fn.dataTable.isDataTable('#userDataTable')) {
            table.fnClearTable();
            table.fnDestroy();
            $('#userDataTable').empty();
            table = $('#userDataTable').DataTable({
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
            });
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
            htmlTbody += '<tr>';
            htmlTbody += '<td>' + item.user.flatNumber + '</td>';
            htmlTbody += '<td>' + item.user.userName + '</td>';
            htmlTbody += '<td>' + item.payment + '</td>';
            htmlTbody += '<td>' + item.paymentDate + '</td>';
            htmlTbody += '<td>' + item.month.name + '</td>';
            htmlTbody += '</tr>';
        });

        $(".bg-primary").html(htmlDiv);
        $("#userDataTable thead").html(htmlThead);
        $("#userDataTable tbody").html(htmlTbody);
        
        if ($.fn.dataTable.isDataTable('#userDataTable')) {
            table.fnClearTable();
            table.fnDestroy();
            $('#userDataTable').empty();
            table = $('#userDataTable').DataTable({
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
            });
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

function page2() {
    $.getJSON("/api/InvoiceElectroesApi").done(function (data) {
        var htmlDiv = '<h4>Invoice electricity</h4>';
        var htmlThead = "";
        var htmlTbody = "";

        htmlThead += '<tr>';
        htmlThead += '<th>Invoice date</span></th>';
        htmlThead += '<th>Flat number</span></th>';
        htmlThead += '<th>Provider name</th>';
        htmlThead += '<th>Payment</span></th>';
        htmlThead += '<th>Debt</span></th>';
        htmlThead += '<th>Overpaid</span></th>';
        htmlThead += '<th>For period</span></th>';
        htmlThead += '<th>Previous number</span></th>';
        htmlThead += '<th>Current number</span></th>';
        htmlThead += '</tr>';

        data.forEach(function (item) {
            console.log(item);
            htmlTbody += '<tr>';
            htmlTbody += '<td>' + item.invoiceDate + '</td>';
            htmlTbody += '<td>' + item.user.flatNumber + '</td>';
            htmlTbody += '<td>' + item.providerName + '</td>';
            htmlTbody += '<td>' + item.payment + '</td>';
            htmlTbody += '<td>' + item.debt + '</td>';
            htmlTbody += '<td>' + item.overpaid + '</td>';
            htmlTbody += '<td>' + item.month.name + '</td>';
            htmlTbody += '<td>' + item.prevNumber + '</td>';
            htmlTbody += '<td>' + item.currentNumber + '</td>';
            htmlTbody += '</tr>';
        });

        $(".bg-primary").html(htmlDiv);
        $("#userDataTable thead").html(htmlThead);
        $("#userDataTable tbody").html(htmlTbody);

        if ($.fn.dataTable.isDataTable('#userDataTable')) {
            table.fnClearTable();
            table.fnDestroy();
            $('#userDataTable').empty();
            table = $('#userDataTable').DataTable({
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
            });
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

function page3() {
    $.getJSON("/api/InvoiceGazsApi").done(function (data) {
        var htmlDiv = '<h4>Invoice gas</h4>';
        var htmlThead = "";
        var htmlTbody = "";

        htmlThead += '<tr>';
        htmlThead += '<th>Invoice date</span></th>';
        htmlThead += '<th>Flat number</span></th>';
        htmlThead += '<th>Provider name</th>';
        htmlThead += '<th>Payment</span></th>';
        htmlThead += '<th>Debt</span></th>';
        htmlThead += '<th>Overpaid</span></th>';
        htmlThead += '<th>For period</span></th>';
        htmlThead += '<th>Previous number</span></th>';
        htmlThead += '<th>Current number</span></th>';
        htmlThead += '</tr>';

        data.forEach(function (item) {
            console.log(item);
            htmlTbody += '<tr>';
            htmlTbody += '<td>' + item.invoiceDate + '</td>';
            htmlTbody += '<td>' + item.user.flatNumber + '</td>';
            htmlTbody += '<td>' + item.providerName + '</td>';
            htmlTbody += '<td>' + item.payment + '</td>';
            htmlTbody += '<td>' + item.debt + '</td>';
            htmlTbody += '<td>' + item.overpaid + '</td>';
            htmlTbody += '<td>' + item.month.name + '</td>';
            htmlTbody += '<td>' + item.prevNumber + '</td>';
            htmlTbody += '<td>' + item.currentNumber + '</td>';
            htmlTbody += '</tr>';
        });

        $(".bg-primary").html(htmlDiv);
        $("#userDataTable thead").html(htmlThead);
        $("#userDataTable tbody").html(htmlTbody);

        if ($.fn.dataTable.isDataTable('#userDataTable')) {
            table.fnClearTable();
            table.fnDestroy();
            $('#userDataTable').empty();
            table = $('#userDataTable').DataTable({
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
            });
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

function page4() {
    $.getJSON("/api/InvoiceServicesApi").done(function (data) {
        var htmlDiv = '<h4>Invoice services</h4>';
        var htmlThead = "";
        var htmlTbody = "";

        htmlThead += '<tr>';
        htmlThead += '<th>Invoice date</span></th>';
        htmlThead += '<th>Flat number</span></th>';
        htmlThead += '<th>Provider name</th>';
        htmlThead += '<th>Payment</span></th>';
        htmlThead += '<th>Debt</span></th>';
        htmlThead += '<th>Overpaid</span></th>';
        htmlThead += '<th>For period</span></th>';
        //htmlThead += '<th>Previous number</span></th>';
        //htmlThead += '<th>Current number</span></th>';
        htmlThead += '</tr>';

        data.forEach(function (item) {
            console.log(item);
            htmlTbody += '<tr>';
            htmlTbody += '<td>' + item.invoiceDate + '</td>';
            htmlTbody += '<td>' + item.user.flatNumber + '</td>';
            htmlTbody += '<td>' + item.providerName + '</td>';
            htmlTbody += '<td>' + item.payment + '</td>';
            htmlTbody += '<td>' + item.debt + '</td>';
            htmlTbody += '<td>' + item.overpaid + '</td>';
            htmlTbody += '<td>' + item.month.name + '</td>';
            //htmlTbody += '<td>' + item.prevNumber + '</td>';
            //htmlTbody += '<td>' + item.currentNumber + '</td>';
            htmlTbody += '</tr>';
        });

        $(".bg-primary").html(htmlDiv);
        $("#userDataTable thead").html(htmlThead);
        $("#userDataTable tbody").html(htmlTbody);

        if ($.fn.dataTable.isDataTable('#userDataTable')) {
            table.fnClearTable();
            table.fnDestroy();
            $('#userDataTable').empty();
            table = $('#userDataTable').DataTable({
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
            });
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

function page5() {
    $.getJSON("/api/InvoiceTelsApi").done(function (data) {
        var htmlDiv = '<h4>Invoice phones</h4>';
        var htmlThead = "";
        var htmlTbody = "";

        htmlThead += '<tr>';
        htmlThead += '<th>Invoice date</span></th>';
        htmlThead += '<th>Flat number</span></th>';
        htmlThead += '<th>Provider name</th>';
        htmlThead += '<th>Payment</span></th>';
        htmlThead += '<th>Debt</span></th>';
        htmlThead += '<th>Overpaid</span></th>';
        htmlThead += '<th>For period</span></th>';
        htmlThead += '<th>Phone number</span></th>';
        //htmlThead += '<th>Current number</span></th>';
        htmlThead += '</tr>';

        data.forEach(function (item) {
            console.log(item);
            htmlTbody += '<tr>';
            htmlTbody += '<td>' + item.invoiceDate + '</td>';
            htmlTbody += '<td>' + item.user.flatNumber + '</td>';
            htmlTbody += '<td>' + item.providerName + '</td>';
            htmlTbody += '<td>' + item.payment + '</td>';
            htmlTbody += '<td>' + item.debt + '</td>';
            htmlTbody += '<td>' + item.overpaid + '</td>';
            htmlTbody += '<td>' + item.month.name + '</td>';
            htmlTbody += '<td>' + item.telNumber + '</td>';
            //htmlTbody += '<td>' + item.currentNumber + '</td>';
            htmlTbody += '</tr>';
        });

        $(".bg-primary").html(htmlDiv);
        $("#userDataTable thead").html(htmlThead);
        $("#userDataTable tbody").html(htmlTbody);

        if ($.fn.dataTable.isDataTable('#userDataTable')) {
            table.fnClearTable();
            table.fnDestroy();
            $('#userDataTable').empty();
            table = $('#userDataTable').DataTable({
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
            });
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

function page6() {
    $.getJSON("/api/InvoiceWatersApi").done(function (data) {
        var htmlDiv = '<h4>Invoice water</h4>';
        var htmlThead = "";
        var htmlTbody = "";

        htmlThead += '<tr>';
        htmlThead += '<th>Invoice date</span></th>';
        htmlThead += '<th>Flat number</span></th>';
        htmlThead += '<th>Provider name</th>';
        htmlThead += '<th>Payment</span></th>';
        htmlThead += '<th>Debt</span></th>';
        htmlThead += '<th>Overpaid</span></th>';
        htmlThead += '<th>For period</span></th>';
        //htmlThead += '<th>Previous number</span></th>';
        //htmlThead += '<th>Current number</span></th>';
        htmlThead += '</tr>';

        data.forEach(function (item) {
            console.log(item);
            htmlTbody += '<tr>';
            htmlTbody += '<td>' + item.invoiceDate + '</td>';
            htmlTbody += '<td>' + item.user.flatNumber + '</td>';
            htmlTbody += '<td>' + item.providerName + '</td>';
            htmlTbody += '<td>' + item.payment + '</td>';
            htmlTbody += '<td>' + item.debt + '</td>';
            htmlTbody += '<td>' + item.overpaid + '</td>';
            htmlTbody += '<td>' + item.month.name + '</td>';
            //htmlTbody += '<td>' + item.prevNumber + '</td>';
            //htmlTbody += '<td>' + item.currentNumber + '</td>';
            htmlTbody += '</tr>';
        });

        $(".bg-primary").html(htmlDiv);
        $("#userDataTable thead").html(htmlThead);
        $("#userDataTable tbody").html(htmlTbody);

        if ($.fn.dataTable.isDataTable('#userDataTable')) {
            table.fnClearTable();
            table.fnDestroy();
            $('#userDataTable').empty();
            table = $('#userDataTable').DataTable({
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
            });
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