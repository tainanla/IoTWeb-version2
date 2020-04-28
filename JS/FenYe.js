
        function submitForm(pagenumber) {
            pagenumber = parseInt(pagenumber, 10);
        $('#PageIndex').val(pagenumber - 1);
        $('#searchForm').submit();
    }

        $(function () {

            $('#searchButton').click(function () {
                submitForm($('#pagebar .currentpagenumber').text());
            });

            $('#pagebar .pagenumber').click(function () {
            submitForm($(this).text());
    });

});
   