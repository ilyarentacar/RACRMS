$(() => {
    if (typeof Noty == 'undefined') {
        console.warn('Warning - noty.min.js is not loaded.');
        return;
    }

    if (!$().DataTable) {
        console.warn('Warning - datatables.min.js is not loaded.');
        return;
    }

    if (!$().select2) {
        console.warn('Warning - select2.min.js is not loaded.');
        return;
    }

    if (!$().daterangepicker) {
        console.warn('Warning - daterangepicker.js is not loaded.');
        return;
    }

    // Override Noty defaults
    Noty.overrideDefaults({
        theme: 'limitless',
        layout: 'topRight',
        type: 'alert',
        timeout: 2500
    });

    // Setting datatable defaults
    $.extend($.fn.dataTable.defaults, {
        autoWidth: false,
        responsive: true,
        columnDefs: [{
            orderable: false,
            width: 100,
            targets: [10]
        }],
        dom: '<"datatable-header"fl><"datatable-scroll-wrap"t><"datatable-footer"ip>',
        language: {
            search: '<span>Filter:</span> _INPUT_',
            searchPlaceholder: 'Aranacak metin...',
            lengthMenu: '<span>Show:</span> _MENU_',
            paginate: { 'first': 'First', 'last': 'Last', 'next': $('html').attr('dir') == 'rtl' ? '&larr;' : '&rarr;', 'previous': $('html').attr('dir') == 'rtl' ? '&rarr;' : '&larr;' }
        }
    });

    // Apply custom style to select
    $.extend($.fn.dataTableExt.oStdClasses, {
        "sLengthSelect": "custom-select"
    });

    $('.select').select2({
        minimumResultsForSearch: Infinity
    });

    // First picker
    $('.daterange-single').daterangepicker({
        parentEl: '.content-inner',
        singleDatePicker: true,
        minDate: moment().format('DD.MM.yyyy'),
        locale: {
            format: 'DD.MM.yyyy',
            cancelLabel: 'Temizle',
            applyLabel: 'Tamam'
        }
    });

    $('.daterange-single').on('cancel.daterangepicker', (ev, picker) => $(this).val(''));

    $('.daterange-single-second').daterangepicker({
        parentEl: '.content-inner',
        singleDatePicker: true,
        minDate: moment((moment().format('DD-MM-YYYY')), "DD-MM-YYYY").add(1, 'days').format('DD.MM.yyyy'),
        locale: {
            format: 'DD.MM.yyyy',
            cancelLabel: 'Temizle',
            applyLabel: 'Tamam'
        }
    });

    $('.daterange-single-second').on('cancel.daterangepicker', (ev, picker) => $(this).val(''));
});