// modalform.js

$(function () {
    $.ajaxSetup({ cache: false });

    $("a[data-modal]").on("click", function (e) {
        //var refrence = $(this);
        // hide dropdown if any (this is used wehen invoking modal from link in bootstrap dropdown )
        //$(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');

        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                /*backdrop: 'static',*/
                keyboard: true
            }, 'show');
            bindForm(this);
        });
        return false;
    });
});

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    if(confirm("Are you sure you want to perform this action?"),function(success)
                    {
                         if(success)
                    {
                          $('#myModal').modal('hide');
                         window.location.href = '@Url.Action("BioDepPosition","BioDepPositions")';
                         location.reload();
                    }
                    
                    //$('#replacetarget').load(result.url); //  Load data from the server and place the returned HTML into the matched element
                   
                   
                  
                   
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });
        return false;
    });
}