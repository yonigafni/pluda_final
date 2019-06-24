
$(function () {

    /*-------------------Popup-Detail-------------------*/
    var TeamDetailPostBackURLDetail = '/Appointment/Details';
    $(".detailButton").click(function () {
        debugger;
        var $buttonClicked = $(this);
        var customerId = $buttonClicked.attr('data-customerId');
        var appointmentId = $buttonClicked.attr('data-appointmentId');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURLDetail,
            contentType: "application/json; charset=utf-8",
            data: { "CustomerID": customerId, "appointmentId": appointmentId },
            datatype: "json",
            success: function (data) {
                debugger;
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

    $(".trButton").click(function () {
        debugger;
        var $buttonClicked = $(this);
        var customerId = $buttonClicked.attr('data-customerId');
        var appointmentId = $buttonClicked.attr('data-appointmentId');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURLDetail,
            contentType: "application/json; charset=utf-8",
            data: { "CustomerID": customerId, "appointmentId": appointmentId },
            datatype: "json",
            success: function (data) {
                debugger;
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });

    $(window).click(function () {
        $('#myModal').modal('hide');
    });
    /*-------------------Popup-Detail-End--------------------*/

    /*-------------------Popup-Create-------------------*/
    var TeamDetailPostBackURLCreate = '/Appointment/Create';
    $("#actionButton2").click(function () {
        debugger;
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURLCreate,
            contentType: "application/json; charset=utf-8",
            data: {},
            datatype: "json",
            success: function (data) {
                debugger;
                $('#myModalContentCreate').html(data);
                $('#myModalCreate').modal(options);
                $('#myModalCreate').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

    /*-------------------Popup-Create-End--------------------*/

    /*-------------------Popup-Edit-------------------*/
    var TeamDetailPostBackURLEdit = '/Appointment/Edit';
    $(".editButton").click(function () {
        debugger;
        var $buttonClicked = $(this);
        var customerId = $buttonClicked.attr('data-customerId');
        var appointmentId = $buttonClicked.attr('data-appointmentId');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURLEdit,
            contentType: "application/json; charset=utf-8",
            data: { "CustomerID": customerId, "appointmentId": appointmentId },
            datatype: "json",
            success: function (data) {
                debugger;
                $('#myModalContentEdit').html(data);
                $('#myModalEdit').modal(options);
                $('#myModalEdit').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

    /*-------------------Popup-Edit-End--------------------*/

    /*--------------------Filters--------------------*/
    $("#filterName").hide();
    $("#filterDate").hide();

    var rows = $("table#table1 tr:not(:first-child)");

    function selected(selected1) {

        if (selected1 == "selectFieldsByName") {
            debugger;
            $("#filterName").show();
            $("#filterDate").hide();
            $(".selectFieldsByName").on("change", function () {
                var selected2 = document.getElementById("selectFieldsByName").value;
                if (selected2 != "All") {
                    debugger;
                    rows.filter("[Name1=" + selected2 + "]").show();
                    rows.not("[Name1=" + selected2 + "]").hide();
                }
                else {
                    debugger;
                    rows.show();
                }
            });
            $(".selectFieldsByField").on("change", function () {
                rows.show();
                var selected1 = this.value;
                selected(selected1);
            });
            
        }
        else if (selected1 == "selectFieldsByDate") {
            debugger;
            $("#filterName").hide();
            $("#filterDate").show();
            $(".selectFieldsByDate").on("change", function () {
                var selected3 = document.getElementById("selectFieldsByDate").value;
                if (selected3 != "All") {
                    debugger;
                    rows.filter("[Date1=" + selected3 + "]").show();
                    rows.not("[Date1=" + selected3 + "]").hide();
                }
                else {
                    debugger;
                    rows.show();
                }
            });
            $(".selectFieldsByField").on("change", function () {
                rows.show();
                document.getElementById("selectFieldsByName").value = "All";
                document.getElementById("selectFieldsByDate").value = "All";
                var selected1 = this.value;
                selected(selected1);
            });
        }
        else {
            $("#filterName").hide();
            $("#filterDate").hide();
            rows.show();
        }

    }

    /*Filter by Type*/
    $(".selectFieldsByField").on("change", function () {
        var selected1 = this.value;
        selected(selected1);
    });

    /*-----------------Filters-End-----------------*/

});