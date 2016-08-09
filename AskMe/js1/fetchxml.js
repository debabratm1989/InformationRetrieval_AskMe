$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "books.xml",
        dataType: "xml",
        success: function (data) {
            alert("file is loaded");
            $(data).find('book').each(function () {
                var title = $(this).find('title').text();
                var year = $(this).find('year').text();
                var price = $(this).find('price').text();
                var auth = $(this).find('author');
                var len = auth.length;
                var Authors = "";
                var j = 0;
                $(this).find('author').each(function () {
                    j++;
                    if (j == len) {
                        Authors = Authors + $(this).text();
                    }
                    else {
                        Authors = Authors + $(this).text() + ", ";
                    }
                });
                var category = $(this).attr("category");
                var html = '<tr><td width = "auto">' + title + '</td><td width = "auto">' + Authors + ' </td><td width = "auto">' + year + '</td><td width = "auto">' + price + '</td><td width="auto">' + category + '</td></tr>';
                $("#Books1").append(html);
            });

        },
        error: function () { alert("error loading file"); }
    });
});