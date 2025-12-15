$(function () {

    $(".dateField").each(function () {
        $(this).datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "yy-mm-dd",
            /*Configuracion para mostrar la fecha abajo del textbox*/
            beforeShow: function (input, inst) {
                var rect = input.getBoundingClientRect();
                setTimeout(function () {
                    inst.dpDiv.css({ top: rect.top + 40, left: rect.left + 0 });
                }, 0);
            }
        });
    })

    $(function () {
        $('.modal1').click(function () {
            $('<div/>').appendTo('body').dialog({
                close: function (event, ui) {
                    dialog.remove();
                },
                modal: true,
                width: 500
            }).load(this.href + "#deletePage");
            console.log(this.href + "#deletePage")
            return false;
        });
    });

    $(function () {
        $('.modalsend').click(function () {
            $('<div/>').appendTo('body').dialog({
                close: function (event, ui) {
                    dialog.remove();
                },
                modal: true,
                width: 500
            }).load($('.modalsend')[0].baseURI + '#modalsend');
            console.log($('.modalsend')[0].baseURI + '#modalsend');
            return false;
        });
    });

    $('.filterable .btn-filter').click(function () {
        var $panel = $(this).parents('.filterable'),
  $filters = $panel.find('.filters input'),
  $tbody = $panel.find('.table tbody');
        if ($filters.prop('disabled') == true) {
            $filters.prop('disabled', false);
            $filters.first().focus();
        } else {
            $filters.val('').prop('disabled', true);
            $tbody.find('.no-result').remove();
            $tbody.find('tr').show();
        }
    });

    $('.filterable .filters input').keyup(function (e) {
        /* Ignore tab key */
        var code = e.keyCode || e.which;
        if (code == '9') return;
        /* Useful DOM data and selectors */
        var $input = $(this),
      inputContent = $input.val().toLowerCase(),
      $panel = $input.parents('.filterable'),
      column = $panel.find('.filters th').index($input.parents('th')),
      $table = $panel.find('.table'),
      $rows = $table.find('tbody tr');
        /* Dirtiest filter function ever ;) */
        var $filteredRows = $rows.filter(function () {
            var value = $(this).find('td').eq(column).text().toLowerCase();
            return value.indexOf(inputContent) === -1;
        });
        /* Clean previous no-result if exist */
        $table.find('tbody .no-result').remove();
        /* Show all rows, hide filtered ones (never do that outside of a demo ! xD) */
        $rows.show();
        $filteredRows.hide();
        /* Prepend no-result row if all rows are filtered */
        if ($filteredRows.length === $rows.length) {
            $table.find('tbody').prepend($('<tr class="no-result text-center"><td colspan="' + $table.find('.filters th').length + '">No result found</td></tr>'));
        }
    });


});