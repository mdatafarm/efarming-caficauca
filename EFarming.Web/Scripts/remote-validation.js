$(function () {
    $(".remote form.to-validate input.form-control, .remote form.to-validate select.form-control, .remote textarea.form-control").focusout(function (e) {
        validateElement(this);
    });

    $(".remote form.to-validate").attr('novalidate', 'novalidate');

    $(".remote form.to-validate").submit(function (e) {
        //alert($("#Options").val());
        console.log(($("#Options").val()));

        if ($("#Options").val() == null || $("#Options").val() == "") {
            //alert("entro");
            $("#Options").val(" ");

        }
        $("#btnSubmit").prop('disabled', true);
        $("#information").addClass("label label-warning");
        $("#information").css("font-size", "11px");
        $("#information").html("Se esta validando los datos ingresados en el formulario por favor espere...");
        e.preventDefault();
        var elements = $(this).find("input.form-control, select.form-control, textarea.form-control");
        [].forEach.call(elements, function (element) {
            validateElement(element);
        });

        

        var errors = $(this).find('.' + errorClass);
        if (errors.length == 0) {
            var data_target = $(this).attr("data-target");
            var target = document.getElementById(data_target);
            $.post($(this).attr('action'), $(this).serialize(), function (data) {
                $("#information").removeClass("label label-warning");
                $("#information").addClass("label label-success");
                $("#information").css("font-size", "11px");
                $("#btnSubmit").hide();
                $("#btnClosePopup").fadeIn(1500).click();
                $("#information").html("Se guardaron los datos correctamente.");
                alert("Se modifico correctamente el registro.");
                target.innerHTML = data;
                paginate(data_target);
                
            });
        }
        else {
            $("#information").removeClass("label label-warning");
            $("#information").addClass("label label-danger");
            $("#information").css("font-size", "11px");
            $("#information").html("Hubo un error, por favor intentelo nuevamente.");
            $("#SaveFarm").prop('disabled', false);
            alert("Hubo un error modificando el registro, intente nuevamente o recargue la pagina.");
        }
    });

    $(".remote form.to-validateproductivity").submit(function (e) {
        console.log("Pasó por el de validar productivity");
        $("#SaveFarm").prop('disabled', true);
        $("#information").addClass("label label-warning");
        $("#information").css("font-size", "11px");
        $("#information").html("Se esta validando los datos ingresados en el formulario por favor espere...");

        e.preventDefault();
        var elements = $(this).find("input.form-control, select.form-control, textarea.form-control");
        [].forEach.call(elements, function (element) {
            console.log('element ' + JSON.stringify(element));
            validateElement(element);
        });

        //VALIDACION DE NUMERO DE PLANTAS
        if (parseInt($("#NumberOfPlants").val()) <= 0) {

            var parent = $("#NumberOfPlants").parent().parent();
            var span = $("#NumberOfPlants").next(".glyphicon.form-control-feedback");
            var help = span.next(".help-block");

            parent.removeClass(successClass).addClass(errorClass);
            help.text('El número de plantas debe ser mayor a 0');
        }

        var errors = $(this).find('.' + errorClass);
        if (errors.length == 0) {
            var data_target = $(this).attr("data-target");
            var target = document.getElementById(data_target);
            var farmId = $(this).farmId;
            console.log('Action ' + $('.to-validateproductivity').find('#FarmId').val() + 'el target es: ' + target)
            $.post($(this).attr('action'), $(this).serialize(), function (data) {
                $("#information").removeClass("label label-warning");
                $("#information").addClass("label label-success");
                $("#information").css("font-size", "11px");
                $("#information").html("Se guardaron los datos correctamente.");
                target.innerHTML = data;
                paginate(data_target);
                alert("Se guardaron los datos correctamente.");
                $("#btnClosePopup").fadeIn(1500).click();
                //CierraPopup()
                //$(function () {
                //    CierraPopup()
                //});
            });
        }
        else {
            $("#information").removeClass("label label-warning");
            $("#information").addClass("label label-danger");
            $("#information").css("font-size", "11px");
            $("#information").html("Hubo un error, por favor intentelo nuevamente.");
            $("#SaveFarm").prop('disabled', false);
            //setTimeout((".modal-close").click(), 10000);
        }
    });

    function CierraPopup() {
        $("#modal").modal('hide').delay(10000).fadeIn();//ocultamos el modal
        $('body').removeClass('modal-open').delay(10000).fadeIn();//eliminamos la clase del body para poder hacer scroll
        $('.modal-backdrop').remove().delay(10000).fadeIn();//eliminamos el backdrop del modal
    }

    $("body").on('click', '.config', function (e) {
        alert("entro 11");

        e.preventDefault();
        var data_target = $(this).attr("data-target");
        var target = document.getElementById(data_target);
        $.post($(this).attr("href"), function (data) {
            target.innerHTML = data;
        });
    });

    $('#modal').on('hidden.bs.modal', function () {
        //alert("entro 111");

        console.log('Cierra el modal' + '/Productivity/Index?FarmId=' + $('body').find('#FarmId').val())
        $.get('/Productivity/Index?FarmId=' + $('body').find('#FarmId').val(), '', function (data) {
            console.log(JSON.stringify(data))
            $('body').find('#productivity-table').html(data);
            //$(".modal-close").click();
            //paginate(data_target);
        });
        $(this).removeData('bs.modal');
    });

    $('#modalLarge').on('hidden.bs.modal', function () {
        //alert("entro 111");

        console.log('Cierra el modal' + '/Productivity/Index?FarmId=' + $('body').find('#FarmId').val())
        $.get('/Productivity/Index?FarmId=' + $('body').find('#FarmId').val(), '', function (data) {
            console.log(JSON.stringify(data))
            $('body').find('#productivity-table').html(data);
            //$(".modal-close").click();
            //paginate(data_target);
        });
        $(this).removeData('bs.modal');
    });

    /*$('#modal').on('hidden.bs.modal', function () {
        console.log('Cierra el modal' + '/Productivity/Index?FarmId=' + $('body').find('#FarmId').val())
        $.get('/Productivity/Index?FarmId=' + $('body').find('#FarmId').val(), '', function (data) {
            console.log(JSON.stringify(data))
            $('body').find('#productivity-table').html(data);
            //$(".modal-close").click();
            //paginate(data_target);
        });
        $(this).removeData('bs.modal');
    });*/
    $('#modalspa').on('hidden.bs.modal', function () {
        alert("entro 12");

        $.get( function (data) {

            $('body').find('#spa-table').html(data);
            //$(".modal-close").click();
            //paginate(data_target);

        });
        $(this).removeData('bs.modal');
    });
    $('#modalspa').off('hidden.bs.modal', function () {
        location.reload();
        $("#modalspa").reload();
    });
    /*  $('#modalspa').on('hidden.bs.modal', function () {
          
          $.get('/Productivity/Index?FarmId=' + $('body').find('#FarmId').val(), '', function (data) {
             
              $('body').find('#productivity-table').html(data);
              //$(".modal-close").click();
              //paginate(data_target);
          });
          $(this).removeData('bs.modal');
      });
      */

});