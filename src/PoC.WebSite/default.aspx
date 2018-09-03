<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Asynchronous Job Validation</title>
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.min.css" />
</head>
<body class="container">
    <div class="row">
        <form class="col-5">
            <label for="id">
                Command Id:
                <input class="form-control" type="text" id="id" name="id" readonly="readonly" value="<%= Guid.NewGuid() %>" />
            </label>
            <label for="shouldfail">
                Fake failing command:
                <input type="checkbox" id="shouldfail" name="shouldfail" />
            </label>
            <label for="longrunning">
                Fake long running command:
                <input type="checkbox" id="longrunning" name="longrunning" />
            </label>

            <button class="btn" type="submit">Create Job</button>
        </form>
        <div class="col-5">
            <h4>Notifications</h4>
            <ul class="list-unstyled" id="notifications"></ul>
        </div>
    </div>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/jquery.signalR-2.3.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="signalr/hubs"></script>
    <script>
        $(function () {
            "use strict";
            $("form").submit(function (e) {
                e.preventDefault();
                $.post("api/jobs", $(this).serialize());
            });

            var notify = function (message) {
                console.log(message);
                $("#notifications").append("<li>" + message + "</li>");
            };
            
            $.connection.notificationsHub.client.notify = notify;
            $.connection.logging = true;
            $.connection.fn.log  = function(message) { console.log(message); };
            $.connection.hub.start();
        });
    </script>
</body>
</html>
