

function AfterRefreshPage(id) {

    $.ajax({

        url: "/VentaCabeceras/GetProductShopCart",
        data: { IdProduct: id },
        type: "post",
        cache: false,

        success: function (retorno) {
            if (retorno !== undefined) {
            var qtSpan = "QuantitySpan_" + id;
            var qt = $("#" + qtSpan).html(retorno.quantity);

            $("#TotalPrice_" + id).html("$ " + retorno.totalProducto)
            }
          
        },
        error: function (error) {


        }
    });
}


function addProduct(id) {

    $.ajax({

        url: "/VentaCabeceras/AddProductToCart",
        data: { IdProduct: id },
        type: "post",
        cache: false,

        success: function (retorno) {

            var qtSpan = "QuantitySpan_" + id;
            var qt = $("#" + qtSpan).html(retorno.quantity);

            $("#TotalPrice_" + id).html("$ " + retorno.totalProducto)

            GetTotal();
            console.log(qt);
        },
        error: function (error) {


            console.log(error);
        }
    });
}

function GetTotal() {
    $.ajax({

        url: "/VentaCabeceras/GetImporteTotal",
        type: "post",
        cache: false,
        success: function (retorno) {
            $("#Total").html("$ " + retorno);

        },
        error: function (error) {


        }


    })

}


$("#CategoryIdCmb").change(function () {
    redirection();
});
$("#FilterStr").change(function () {
    redirection();
});

//java script
var input = document.getElementById("FilterStr");

// Execute a function when the user releases a key on the keyboard
input.addEventListener("keyup", function (event) {
    // Number 13 is the "Enter" key on the keyboard
    if (event.keyCode === 13) {
        // Cancel the default action, if needed
        event.preventDefault();
        // Trigger the button element with a click
        redirection();
    }
});

function redirection() {
    var str = $("#FilterStr").val();
    var idCategory = $("#CategoryIdCmb").val();
    window.location = "/VentaCabeceras/Create?idCategory=" + idCategory + "&strFilter=" + str;
}

