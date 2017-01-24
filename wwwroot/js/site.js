// Write your Javascript code.
$(document).ready(function() {

    var errorFunction = function(jqXHR, textStatus, errorThrown){
        jqXHR.responseText;
    }
    
    //var createRecordButton = $("#createRecord");
    var createRecordButton = document.getElementById("createRecord");
    createRecordButton.onclick = function(){
        //JSON data
        var dataType = 'application/x-www-form-urlencoded; charset=utf-8';
        var data = {
            QuestionID: 1,
            TableID: 10,
            Score: 5,
            Comments: "Great question!"
            // FirstName: 'Andrew',
            // LastName: 'Lock',
            // Age: 31
        };

        console.log('Submitting form...');
        $.ajax({
            type: 'POST',
            url: '/Home/Create',
            dataType: 'json',
            contentType: dataType,
            data: data,
            success: function(result) {
                console.log('Data received: ');
                console.log(result);
            },
            error: errorFunction
        });  
    };
});
