$(document).ready(function () {
    // 🔎 Arama tahminleri
    var $searchBox = $("#searchBox");
    var $searchHidden = $("#searchHidden");
    var $suggestions = $("#suggestions");

    $searchBox.on("input", function () {
        var query = $(this).val();
        $searchHidden.val(query); // form submit olduğunda gönderilecek değer

        if (query.length < 2) {
            $suggestions.hide();
            return;
        }

        $.getJSON('/Product/SearchSuggestions', { term: query }, function (data) {
            $suggestions.empty();
            if (data.length > 0) {
                data.forEach(function (item) {
                    $suggestions.append(
                        `<a href="/Product/Index?search=${encodeURIComponent(item)}" 
                           class="list-group-item list-group-item-action">${item}</a>`
                    );
                });
                $suggestions.show();
            } else {
                $suggestions.hide();
            }
        });
    });

    // Boş alana tıklayınca önerileri kapat
    $(document).click(function (e) {
        if (!$(e.target).closest('#searchBox, #suggestions').length) {
            $suggestions.hide();
        }
    });

    // 🛒 Sepete ekle
    $('.btn-cart').click(function (e) {
        e.preventDefault(); // Formun submit olmasını engelle
        var form = $(this).closest('form');
        $.post(form.attr('action'), form.serialize(), function (data) {
            alert('Ürün sepete eklendi!');
        });
    });

    // ❤️ Favori ekle
    $('.btn-favorite').click(function (e) {
        e.preventDefault();
        var form = $(this).closest('form');
        $.post(form.attr('action'), form.serialize(), function (data) {
            alert('Ürün favorilere eklendi!');
        });
    });
});
