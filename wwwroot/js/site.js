// Write your Javascript code.
$(document).ready(function() {

    var errorFunction = function(jqXHR, textStatus, errorThrown){
        jqXHR.responseText;
    }

    $('.error').hide();
    
    //var createRecordButton = $("#createRecord");
    var createRecordButton = document.getElementById("createRecord");
    createRecordButton.onclick = function(){
        //JSON data
        var dataType = 'application/x-www-form-urlencoded; charset=utf-8';
        
        var tableID = $("input#tableID").val();
  		if (tableID == "") {
        $("label#tableID_error").show();
        $("input#tableID").focus();
        return false;
        }

  		var score = $("input#score").val();
  		if (score == "") {
        $("label#score_error").show();
        $("input#score").focus();
        return false;
        }

  		var comments = $("input#comments").val();
  		if (comments == "") {
        $("label#comments_error").show();
        $("input#comments").focus();
        return false;
        }

        var data = {
            QuestionID: 1,
            TableID: tableID,
            Score: score,
            Comments: comments
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
