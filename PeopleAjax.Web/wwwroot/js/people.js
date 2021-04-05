$(() => {

    $("#add").on('click', function () {
        const firstName = $("#first-name").val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();

        $("#first-name").val('');
        $("#last-name").val('');
        $("#age").val('');

        $.post("/Home/AddPerson", { firstName, lastName, age }, function (p) {
            fillTable();
        })
    })

    function fillTable() {
        $("tbody").empty();
        $.get("/Home/GetPeople", function (ppl) {

            ppl.forEach(p => {
                $("tbody").append(`
<tr>
    <td>${p.firstName}</td>
    <td>${p.lastName}</td>
    <td>${p.age}</td>
    <td><button class="btn btn-danger" data-id=${p.id}>Delete!</button></td>
    <td><button class="btn btn-success" data-id=${p.id} data-first-name=${p.firstName} data-last-name=${p.lastName} data-age=${p.age} > Edit</button ></td >

</tr>` );

            })
        })
    }

    fillTable();

    $("tbody").on('click', ".btn-danger", function () {
        const id = $(this).data('id');
        $.post("/home/deleteperson", { id }, function () {
            fillTable();
        })
    })

    $("tbody").on('click', ".btn-success", function () {
        const id = $(this).data("id");
        $("#edit-id").val(id);
        $("#edit-first-name").val($(this).data('first-name'));
        $("#edit-last-name").val($(this).data('last-name'));
        $("#edit-age").val($(this).data('age'));
        $(".modal").modal('show');

    })

    $("#save").on('click', function () {

        const firstName = $("#edit-first-name").val();
        const lastName = $("#edit-last-name").val();
        const age = $("#edit-age").val();
        const id = $("#edit-id").val();

        $.post("/home/editperson", { firstName, lastName, age, id }, function (p) {
            fillTable();
            $(".modal").modal('hide');
        })
    })
})
