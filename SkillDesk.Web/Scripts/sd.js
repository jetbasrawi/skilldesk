/// <reference path="jquery-1.4.1-vsdoc.js" />

$(function () {   

     $("a.modal").click(function (evt) {
        var $this = $(this);
        $("#modal").html("");

        evt.preventDefault();

        var url = $this.attr("href");
        if (url && url != "") {

            $("#modal").dialog({
                title: 'Add skill',
                autoOpen: true,
                closeOnEscape: true,                
                height: 300,
                width: 350,
                modal: false,
                open: function () {

                    $("#modal").load(url, function (responseText, textStatus, xhr) {

                        if (textStatus == "error") {
                            $("#modal").html("<p>An error has occurred, please try again later.</p>");
                            return;
                        }

                        $("#modal #addSkill").bind('submit', function () {
                            var action = $(this).attr("action");
                            var serializedForm = $(this).serialize();
                            $.post(action, serializedForm, function (data) {
                                alert('We are back!');
                            });
                            return false;
                        })
                    });
                },
                close: function () {
                    $(this).dialog('destroy');
                }
            });      
        }
    });

   
});

