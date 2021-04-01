$(document).ready(function () {

    $("#ShowModal").click(function () {
        var Mymodal = $("#ModalCategories");
        var url = Mymodal.data('url');
        $.get(url, function (data) {
            $("#ModalCategoriesBody").html(data);
            Mymodal.modal("show");
        });



    })

});



var url = "/Categories/CreatePartial";
function EnviarPost(CategoryViewModelJs) {
    var desc = $("#DescriptionCategory").val();


    $.ajax({
        url: this.url,
        data: { categoryStr: desc },
        type: "post",
        success: function (data) {
            if (data == "OK") {


                $("#ModalCategories").modal('hide');
                UpdateSelect();
            } else {

                $("#validationCategory").html("La descripcion es obligatoria");
            }
        },
        error: function (error) {
           
            console.log(error);
        }


    });
}

function UpdateSelect() {

    //Creamos una funcion ajax que envie los datos al metodo Buscar del Controler Persona
    $.ajax({
        //Direccion donde nos queremos comunicar Controller/Metodo
        url: "/Categories/GetCategorySelectList",
        //parametros que le pasamos a el Metodo del Controller ( si se fijan en el Controller el metodo Busqueda, recibe como parametro "filtro")
        
        //El tipo es post ya que enviamos datos
        type: "post",
        cache: false,
        success: function (data) {
            //Si el metodo busqueda del controller devuelve algo, lo guardamos en retorno - lo que devuelve es la pagina (View) buscar, osea es un pedazo de codigo HTML que podemos insertar en el DivDinamico
            //$("#DivDetalleVenta").html(retorno);
            var item = "";
            $("#CategoryId").empty();
            $.each(data, function (i, state) {
                item += '<option value="' + state.value + '">' + state.text + '</option>';
            });
            $("#CategoryId").html(item);
        },
        error: function (retorno) {
            //Si el metodo ajax falla entra por aca y nos advierte de un error

            //alert("Se ha producido un error");


        }
    });

}
