
DoWithoutRefresh(".delete-from-card", "Basket/Delete");
DoWithoutRefresh(".delete-item", "Admin/CourseAdmin/Delete");
DoWithoutRefresh(".archive-item", "Admin/CourseAdmin/Archive");
DoWithoutRefresh(".restore-item", "Restore");
$(document).on("click", ".add-card", function (e) {
    e.preventDefault()
    let id = $(this).attr("data-id");
    let data = { id: id };
    console.log(id)
    $.ajax({
        url: "Course/AddBasket",
        type: "Post",
        data: data,
        success: function () {
            console.log("ok")

        }
    })
})

$('.reload-page').click(function () {
    location.reload();
});
$(".delete-from-card").click(function () {
    $('.reload-page').removeClass("d-none")
});
function DoWithoutRefresh(btn, url) {
    $(document).on("click", btn, function (e) {
        e.preventDefault()
        let parent = $(this).parent().parent();
        let id = $(this).attr("data-id");
        let data = { id: id };
        console.log(id);

        $.ajax({
            url: url,
            type: "Post",
            data: data,
            success: function () {
                parent.addClass("d-none");
              

            }
        })
    })
}

$(document).on("click", ".load-more", function () {

    let parent = $(".parent-products-elem");

    let skip = $(parent).children().length;

    let datas = $(parent).attr("data-count");
    console.log(parent);
    console.log(datas);

    $.ajax({
        url: `course/loadmore?skip=${skip}`,
        type: "Get",
        success: function (res) {


            $(parent).append(res);
            console.log(res);

            skip = $(parent).children().length;

            if (skip >= datas) {
                $(".load-more").addClass("d-none");

            }
        }

    })
});

