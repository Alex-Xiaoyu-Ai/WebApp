$(function () {
    $("#Observation").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "/Reports/GetObservationTemplate",
                data: { Prefix: request.term },
                dataType: "json",
                success: function (data) {
                    console.log("Observation Data received from server: " + data);
                    response($.map(data, function (data) {
                        return {
                            label: data,
                            value: data
                        }
                    }));
                },
                error: function (result) {
                    alert("Error");
                }
            });
            
        },
        minLength: 1
    });

    $("#Diagnosis").autocomplete({
        source:            
            function (request, response) {
            $.ajax({
                type: "POST",
                url: "/Reports/GetDiagnosisTemplate",
                data: {Prefix: request.term},
                dataType: "json",
                success: function (data) {
                    console.log("Observation Data received from server: " + data);
                    response($.map(data, function (data) {
                        return {
                            label: data,
                            value: data 
                        }
                    }));
                },
                error: function (result) {
                    alert("Error");
                }
            });

        }
        
       ,
        minLength: 1
    })
})