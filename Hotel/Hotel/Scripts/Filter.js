function expandFilter() {

    var isHidden = $("#filterForm").hasClass("hidden");

    if (isHidden) {
        $("#filterForm").removeClass("hidden");
    } else {
        $("#filterForm").addClass("hidden");
    }
}

$("#filter").click(expandFilter);